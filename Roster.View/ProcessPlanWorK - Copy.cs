using Roster.Business;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Roster.Business.Models;
using System.IO;
using System.Diagnostics;
using System.Linq;
using Roster.Data.DBAccessor;
using Roster.Business.Extensions;
using Roster.View.Business;


namespace Roster
{
    public partial class ProcessPlanWorK : Form
    {
        private FlightSchedulesBusiess flightSchedulesBusiess;
        private List<FlightSchedulesInfo> flights = new List<FlightSchedulesInfo>();
        private Dictionary<int, List<int>> ZoneMapping = new Dictionary<int, List<int>>();
        WaitWnd.WaitWndFun waitForm = new WaitWnd.WaitWndFun();

        public ProcessPlanWorK()
        {
            InitializeObject();
            InitializeComponent();
        }

        private void InitializeObject()
        {
            flightSchedulesBusiess = new FlightSchedulesBusiess();
            scheduleBus = new ScheduleBusiess();
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            EmployeesSkill = new List<EmployeerInfoOld>();//employeerSkillBus.GetEmployeerSkill();
            ZoneMapping = BuildMapZone();
            ProcessCommon.ZoneMap = ZoneMapping;
            workSchedules = workScheduleBus.GetList();
            ProcessPlanWorkByFlights();
        }

        private void ProcessPlanWorkByFlights()
        {
            double numberDay = this.dtDateTo.Value.Subtract(this.dtPFromDate.Value).TotalDays;
            if (numberDay < 0)
            {
                numberDay = 0;
            }
            List<string> shortNameWillBeAssign = new List<string>() { "XNG", "XNM", "XNL", "BTA", "BXM", "BXL", "BXE", "DKE" };
            for (double i = 0; i <= numberDay; i++)
            {
                List<int> workIdProcessed = new List<int>();

                List<ScheduleInfo> resultPlanWorkOfDay = new List<ScheduleInfo>();
                DateTime datePlan = this.dtPFromDate.Value.Date.AddDays(i);
                List<WorkSchedulesInfo> workSchedule = workScheduleBus.GetListByDatePlan(datePlan, 0);
                List<RosterDetailInfo> workOfDay = scheduleBus.GetList(datePlan);
                List<RosterDetailInfo> workNotAssignEmp = workOfDay.Where(p => p.FK_sEmpID.IsNullOrEmpty() && shortNameWillBeAssign.Contains(p.FK_sProduct_ShortName)
                    && (!p.sFlightNo.Contains("VN") || !p.sFlightNo.Contains("0V"))).ToList();
                List<RosterDetailInfo> workHasEmployeers = ProcessCommon.GetRosterDetailOfUser(workOfDay, workSchedule, datePlan);
                List<RosterDetailInfo> workbySchedule = AssignAgain(workNotAssignEmp, workHasEmployeers);
                scheduleBus.Create(workbySchedule);
                //SetDataToGrid(workbySchedule);
                //break;
            }
        }

        private List<RosterDetailInfo> AssignAgain(List<RosterDetailInfo> sorceNotYetAssign, List<RosterDetailInfo> workHasEmployeers)
        {
            //List<EmployerAccord> employeerIdAccord = FilterEmployeerAccord(sorceNotYetAssign, workHasEmployeers);
            List<RosterDetailInfo> rosterDetailNotAssign = BuildEmployeerAccountByNumberEmployeerNotEnough(sorceNotYetAssign);
            //Tính toán tổng số chuyến bay đang phục vụ trong thời gian chuyến bay cần phục vụ
            List<RosterDetailInfo> rosterAfterChange = workHasEmployeers.ToList();
            List<RosterDetailInfo> addWorkRemove = new List<RosterDetailInfo>();
            foreach (var item in rosterDetailNotAssign)
            {
                EmployerAccord employeerAccords = FilterEmployeerAccord(item, rosterAfterChange);
                if (employeerAccords.Employeers == null || employeerAccords.Employeers.Count == 0)
                {
                    continue;
                }

                EmployerAccord eployeerWillBeChange = employeerAccords.Employeers.OrderBy(p => p.NumberWorkSameTime).FirstOrDefault();
                if (eployeerWillBeChange == null)
                {
                    continue;
                }
                RosterDetailInfo firstItem = eployeerWillBeChange.WorkOfEmployeerSameTime.FirstOrDefault();
                foreach (var workOld in eployeerWillBeChange.WorkOfEmployeerSameTime)
                {
                    rosterAfterChange.Remove(workOld);
                    addWorkRemove.Add(workOld);
                    workOld.Change = 2;
                    List<string> shortNameWillBeRemove = new List<string>() { "WGD", "CCH", "CDU" };
                    var listWorkOfUserRemoved = rosterAfterChange.Where(p => p.FK_sEmpID.IsEquals(workOld.FK_sEmpID) && p.Shift.IsEquals(workOld.Shift) && p.sFlightNo.IsEquals(workOld.sFlightNo));
                    if (listWorkOfUserRemoved.Count(p => !shortNameWillBeRemove.Contains(p.FK_sProduct_ShortName)) == 0)
                    {
                        listWorkOfUserRemoved = listWorkOfUserRemoved.Where(p => !shortNameWillBeRemove.Contains(p.FK_sProduct_ShortName)).ToList();
                        foreach (var itemOther in listWorkOfUserRemoved)
                        {
                             addWorkRemove.Add(itemOther);
                             rosterAfterChange.Remove(itemOther);
                        }
                    }

                }
                if (firstItem == null)
                {
                    item.FK_sEmpID = eployeerWillBeChange.EmployeerId;
                    item.Shift = eployeerWillBeChange.ShiftName;
                    item.DateFrom = eployeerWillBeChange.StartTime;
                    item.DateTo = eployeerWillBeChange.EndTime;
                    item.Change = 1;
                }
                else
                {
                    item.FK_sEmpID = firstItem.FK_sEmpID;
                    item.Shift = firstItem.Shift;
                    item.DateFrom = firstItem.DateFrom;
                    item.DateTo = firstItem.DateTo;
                    item.Change = 1;
                }              
                rosterAfterChange.Add(item);
            }

            return AssignVNAirline(addWorkRemove,rosterAfterChange);
        }

        private List<RosterDetailInfo> AssignVNAirline(List<RosterDetailInfo> sourceNotAssigns, List<RosterDetailInfo> sourceHasEmployeers)
        {
            List<RosterDetailInfo> rosterAfterChange = sourceHasEmployeers.ToList();
            List<string> shortNameWillBeAssign = new List<string>() { "XNG", "XNM", "XNL", "BTA", "BXM", "BXL", "BXE", "DKE" };
            var sourceNotAssignOrder = sourceNotAssigns.Where(p => shortNameWillBeAssign.Contains(p.FK_sProduct_ShortName)).OrderBy(p => p.Orders).ToList();
            foreach (var item in sourceNotAssignOrder)
            {
                var employeerCanWorks = rosterAfterChange.Where(p => p.DateFrom <= item.StartTime
                   && p.DateTo >= item.EndTime
                   && p.FK_sProduct_ShortName.Contains(item.FK_sProduct_ShortName)
                   );
                List<string> employeers = employeerCanWorks.Select(p => string.Format("{0}_{1}", p.FK_sEmpID, p.Shift)).Distinct().ToList();
                foreach (var emp in employeers)
                {
                    string shift = emp.GetItemInArray(1);
                    string employeerId = emp.GetItemInArray(0);
                    var workByEmployeers = rosterAfterChange.Where(p => p.Shift.Equals(shift) && p.FK_sEmpID.IsEquals(employeerId)).ToList();
                    bool isExitsWork = IsBetweenDate(workByEmployeers, item.StartTime, item.EndTime);
                    if (!isExitsWork)
                    {
                        var firstWorkOfEmployeer = workByEmployeers.FirstOrDefault();
                        item.FK_sEmpID = firstWorkOfEmployeer.FK_sEmpID;
                        item.Shift = firstWorkOfEmployeer.Shift;
                        item.DateFrom = firstWorkOfEmployeer.DateFrom;
                        item.DateTo = firstWorkOfEmployeer.DateTo;
                        rosterAfterChange.Add(item);
                        break;
                    }
                }
            }

            return rosterAfterChange;
        }


        public static bool IsBetweenDate(List<RosterDetailInfo> worksOfUser, DateTime StartTime, DateTime EndTime, bool isAcceptionMinTask = false)
        {
            bool isExitsWork = false;
            if (isAcceptionMinTask)
            {
                isExitsWork = worksOfUser.Count(item =>
                (
                        (item.StartTime > StartTime && item.StartTime < EndTime)
                        ||
                        (item.EndTime > StartTime && item.EndTime < EndTime)
                        ||
                        (item.StartTime < StartTime && item.EndTime > StartTime)
                        ||
                        (item.StartTime < EndTime && item.EndTime > EndTime)
                )) > 0;
            }
            else
            {
                isExitsWork = worksOfUser.Count(item =>
               (
                       (item.StartTime >= StartTime && item.StartTime <= EndTime)
                       ||
                       (item.EndTime >= StartTime && item.EndTime <= EndTime)
                       ||
                       (item.StartTime <= StartTime && item.EndTime >= StartTime)
                       ||
                       (item.StartTime <= EndTime && item.EndTime >= EndTime)
               )) > 0;
            }

            return isExitsWork;
        }


        private EmployerAccord FilterEmployeerAccord(RosterDetailInfo rosterDetail, List<RosterDetailInfo> workHasEmployeers)
        {
            EmployerAccord result = new EmployerAccord();
            
            var employeerCanWorks = workHasEmployeers.Where(p => p.DateFrom <= rosterDetail.StartTime
                    && p.DateTo >= rosterDetail.EndTime
                    && p.FK_sProduct_ShortName.Contains(rosterDetail.FK_sProduct_ShortName)                    
                    );
            List<string> shortCodeNotWork = GetShortCodeNotAccord(rosterDetail.FK_sProduct_ShortName);
            if (shortCodeNotWork.Count > 0)
            {
                List<string> employeerIdCanNotWork = workHasEmployeers.Where(p => p.DateFrom <= rosterDetail.StartTime
                  && p.DateTo >= rosterDetail.EndTime
                  && shortCodeNotWork.Contains(p.FK_sProduct_ShortName)).Select(p => p.FK_sEmpID).ToList();
                employeerCanWorks = employeerCanWorks.Where(p => (!employeerIdCanNotWork.Contains(p.FK_sEmpID) || employeerIdCanNotWork.Count == 0));
            }

            List<string> employeers = employeerCanWorks.Select(p => string.Format("{0}_{1}", p.FK_sEmpID, p.Shift)).Distinct().ToList();
            List<EmployerAccord> employerAccordSameTime = new List<EmployerAccord>();
            foreach (var p in employeers)
            {
                var workOfEmpSameTime = GetWorkOfEmpSameTime(p, rosterDetail, workHasEmployeers);
                int numberOtherVN = workOfEmpSameTime.Where(i => !i.sFlightNo.Contains("VN") && !i.sFlightNo.Contains("0V")).Count();
                if (numberOtherVN > 0)
                {
                    continue;
                }
                if (workOfEmpSameTime.Count == 0)
                {
                    string employeerid = p.GetItemInArray(0);
                    string shift = p.GetItemInArray(1);
                    RosterDetailInfo firstRecord = workHasEmployeers.Where(e => e.FK_sEmpID.IsEquals(employeerid) && e.Shift.IsEquals(shift)).FirstOrDefault();
                    employerAccordSameTime.Add(new EmployerAccord(firstRecord, rosterDetail.Id, workOfEmpSameTime));
                }
                else
                {
                    employerAccordSameTime.Add(new EmployerAccord(p, rosterDetail.Id, workOfEmpSameTime));
                }
            }

            result = new EmployerAccord(rosterDetail, employerAccordSameTime);
            return result;
        }

        private List<RosterDetailInfo> BuildEmployeerAccountByNumberEmployeerNotEnough(List<RosterDetailInfo> sourceNotAssignEmployeers)
        {
            List<RosterDetailInfo> result = new List<RosterDetailInfo>();
            foreach (var item in sourceNotAssignEmployeers)
            {
                result.Add(item);
                for (int i = 1; i < item.NumberEmployeer; i++)
                {
                    result.Add(new RosterDetailInfo(item));
                }
            }

            return result.OrderBy(p => p.Orders).ToList();
        }

        private List<RosterDetailInfo> GetWorkOfEmpSameTime(string employeerInfo, RosterDetailInfo workWillBeAssign, List<RosterDetailInfo> sourceHasEmployeers)
        {
            string shift = employeerInfo.GetItemInArray(1);
            string employeerId = employeerInfo.GetItemInArray(0);
            var workByEmployeers = sourceHasEmployeers.Where(p => p.Shift.Equals(shift) && p.FK_sEmpID.IsEquals(employeerId)).ToList();
            return workByEmployeers.Where(item =>
                (
                        (item.StartTime >= workWillBeAssign.StartTime && item.StartTime <= workWillBeAssign.EndTime)
                           ||
                           (item.EndTime >= workWillBeAssign.StartTime && item.EndTime <= workWillBeAssign.EndTime)
                           ||
                           (item.StartTime <= workWillBeAssign.StartTime && item.EndTime >= workWillBeAssign.StartTime)
                           ||
                           (item.StartTime <= workWillBeAssign.EndTime && item.EndTime >= workWillBeAssign.EndTime)
                 )).ToList();
        }


        #region Export DataTo DataGrird
        private void SetDataToGrid(List<RosterDetailInfo> source)
        {
            gridControl1.DataSource = source;
            gridControl1.RefreshDataSource();
            gridView1.RefreshData();
        }

        private void SetDataToGridDetail(List<RosterDetailInfo> source)
        {
            gridControl1.DataSource = source;
            gridControl1.RefreshDataSource();
            gridView1.RefreshData();
        }


        private void SetDataToGridNo(List<ScheduleInfo> source)
        {
            gridControl2.DataSource = source;
            gridControl2.RefreshDataSource();
            gridView2.RefreshData();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            string folderPath = "C:\\Excel\\";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string fileName = string.Format("{0}_{1}.xlsx", "Plan", DateTime.Now.ToString("yyyyMMddHHmmss"));
            string rootPath = Path.Combine(folderPath, fileName);
            gridControl1.ExportToXlsx(rootPath);
            Process.Start("explorer.exe", string.Format("/select,{0}", folderPath));
        }
        #endregion

        #region Master Data
        private int SynchronizedData(DateTime dateFrom, DateTime dateTo)
        {
            //return flightBus.SynchronizedData(dateFrom, dateTo);
            return 0;
        }

        private List<ScheduleInfo> GetWorksByFlight(DateTime dateFrom, DateTime dateTo)
        {
            //return flightBus.GetFlightSchedule(dateFrom, dateTo);
            return new List<ScheduleInfo>();
        }

        //Thực hiện lấy những chuyến bay theo ca cần phân lịch
        private List<ScheduleInfo> GetWorksByShift(List<ScheduleInfo> source, string shift)
        {
            List<ScheduleInfo> worksByShift = source.Where(p => p.Shift.Equals(shift)).OrderByDescending(p => p.Zone).ThenBy(p => p.STD).ToList();
            return worksByShift;
        }


        private Dictionary<string, List<string>> GetProductCode()
        {
            Dictionary<string, List<string>> productCodeDitinct = new Dictionary<string, List<string>>()
            {
                {"XNL", new List<string>() {"XNG","XNM","XNL"}},
                {"BTA", new List<string>() {"BTA"}},
                {"DKE", new List<string>() {"DKE"}},
            };

            return productCodeDitinct;
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            string folderPath = "C:\\Excel\\";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string fileName = string.Format("{0}_{1}.xlsx", "Plan_NOT", DateTime.Now.ToString("yyyyMMddHHmmss"));
            string rootPath = Path.Combine(folderPath, fileName);
            gridControl2.ExportToXlsx(rootPath);
            Process.Start("explorer.exe", string.Format("/select,{0}", folderPath));
        }


        private Dictionary<int, List<int>> BuildMapZone()
        {
            List<MappingZoneInfo> mappingZone = new List<MappingZoneInfo>();//this.mappingZoneBus.GetMappingZone();
            Dictionary<int, List<int>> dcZoneMapping = new Dictionary<int, List<int>>();
            foreach (var item in mappingZone)
            {
                if (dcZoneMapping.ContainsKey(item.Zone))
                {
                    continue;
                }
                dcZoneMapping.Add(item.Zone, item.ZoneMapping);
            }

            return dcZoneMapping;
        }

        private List<int> GetMappingZone(int zone)
        {
            if (!ZoneMapping.ContainsKey(zone))
            {
                return new List<int>();
            }

            return ZoneMapping[zone];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //var  workDetails =  workBusinuss.CreateWorkOfMonth(dtPFromDate.Value, dtDateTo.Value);
            //SetDataToGrid(workDetails);
        }

        private List<string> GetShortCodeNotAccord(string shortCode)
        {
            List<string> shortNotAccord = new List<string>();
            switch (shortCode)
            {
                case "BXM":
                case "BXL": 
                case "BXE":
                    shortNotAccord = new List<string>() { "XNG", "XNM", "XNL", "BTA", "DKE" };
                    break;

                default:
                    break;
            }

            return shortNotAccord;
        }


    }
}

