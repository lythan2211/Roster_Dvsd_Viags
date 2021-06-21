using Roster.Business;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Roster.Business.Models;
using System.IO;
using System.Diagnostics;
using System.Linq;


namespace Roster
{
    public partial class ProcessPlanWorK : Form
    {
        private FlightSchedulesBusiess flightSchedulesBusiess;
        private EmployeeBusiess employeeBussiess;
        private List<FlightSchedulesInfo> flights = new List<FlightSchedulesInfo>();
        private List<EmployeerInfo> employees = new List<EmployeerInfo>();
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
            employeeBussiess = new EmployeeBusiess();
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            ZoneMapping = BuildMapZone();
            this.flights = GetFlightSchedule();
            this.employees = GetEmployees();
        }

        public static bool IsBetweenDate(List<WorkInfo> worksOfUser, DateTime StartTime, DateTime EndTime, bool isAcceptionMinTask = false)
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

        #region Export DataTo DataGrird
        private void SetDataToGrid(List<WorkInfo> source)
        {
            gridControl1.DataSource = source;
            gridControl1.RefreshDataSource();
            gridView1.RefreshData();
        }

        private void SetDataToGridDetail(List<WorkInfo> source)
        {
            gridControl1.DataSource = source;
            gridControl1.RefreshDataSource();
            gridView1.RefreshData();
        }

        private void SetDataToGridNo(List<WorkInfo> source)
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

        private List<FlightSchedulesInfo> GetFlightSchedule()
        {
            return this.flightSchedulesBusiess.GetFlight(dtFromDate.Value.Date, dtDateTo.Value.Date);
        }

        private List<EmployeerInfo> GetEmployees()
        {
            return this.employeeBussiess.GetEmployeeWork(dtFromDate.Value.Date, dtDateTo.Value.Date);
        }

    }
}

