using FlightSchedules.Business.BL;
using InvoiceServer.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using InvoiceServer.Business.Extensions;
using FlightSchedules.View.Business;

namespace FlightSchedules.Business
{
    public class WorkBusinuss : BaseClass
    {
        private readonly IWorkingBO workBO;
        private readonly IWorkOfMonthBO workOfMonthBO;
        public WorkBusinuss()
        {
            //workBO = this.GetBOFactory().GetBO<IWorkingBO>();
            //workOfMonthBO = this.GetBOFactory().GetBO<IWorkOfMonthBO>();
        }

        public IEnumerable<WokingInfo> FillterWork(string location, string workCode, DateTime datetimeWork)
        {
            WorkSearch workingSearch = new WorkSearch(location, workCode, datetimeWork);
            var listWoking = workBO.Filter(workingSearch).ToList();
            return listWoking;
        }

        

        public IEnumerable<WokingInfo> FillterALWork(DateTime datetimeWork, DateTime datetimeWorkTo, string shortName, List<int> employeerIds)
        {
            WorkSearch workingSearch = new WorkSearch(datetimeWork, datetimeWorkTo, shortName, employeerIds);
            var listWoking = workBO.FilterWork(workingSearch).Where(p => p.DateWorkView.IsNotNullOrEmpty());
            return listWoking.ToList();
        }

        public List<EmployeerAccords> GetEmployeerCanWork(string shortName,
            string shift,
            List<string> cerfiticateOfShift, 
            DateTime datetimeWork, 
            List<EmployeerInfo> employeerSkill,
            List<WorkSchedulesInfo> workSchedules) 
        {
            //Danh sách nhân sự có thể làm việc
            List<EmployeerAccords> employeerCanWorks = new List<EmployeerAccords>();
            List<string> shiftInWork = workSchedules.Select(p => p.Code).Distinct().ToList();

            //Lấy danh sách nhân viên có thể làm chuyến bay trong ca làm việc phù hợp với năng đing
            List<int> employeerIdCanWork = employeerSkill.Where(p => ProcessCommon.GetNumberItemNotMapping(p.Cerfiticate, cerfiticateOfShift) == 0).Select(p => (p.EmployeerId.ToInt(0))).ToList();
            //Tạo điều kiện
            WorkSearch workingSearchEmployeerAccourd = new WorkSearch(shortName, shift, datetimeWork, new List<int>());
            workingSearchEmployeerAccourd.EmployeerIds = employeerIdCanWork;

            //Những ID nhân viên có thể làm những chuyến bay trong ca làm việc theo tiêu chí cấu hình trong bảng UNIT_PlanConfig
            List<int> employeerIdAccordByShift = workBO.EmployeerCanWork(workingSearchEmployeerAccourd).ToList();


            //Nếu  không tìm thấy nhân sự có thể làm theo cấu hình trong bảng UNIT_PlanConfig thì sẽ lấy những nhân sự được nghỉ hoặc nghỉ ngày hôm trước.
            if (employeerIdAccordByShift.Count == 0)
            {
                workingSearchEmployeerAccourd.Shifts = workSchedules.Select(p => p.Code).ToList();
                employeerIdAccordByShift = workBO.EmployeerDayOff(workingSearchEmployeerAccourd).ToList();
            }

            //Nếu tiếp tục không tìm thấy nhân sự nào nghỉ ngày trước đó thì sẽ lấy mặc định tât cả nhân sự có thể đi làm 
            if (employeerIdAccordByShift.Count == 0)
            {
                employeerIdAccordByShift = employeerIdCanWork;
            }

            if (employeerIdAccordByShift.Count == 0)
            {
                return employeerCanWorks;
            }

            // Lập tiêu tí tính tổng tiền cho từng nhân sự đến tại thời điểm phân lịch
            DateTime FirstDate =  new DateTime(datetimeWork.Year, datetimeWork.Month, 01);
            WorkSearch workingSearchSum = new WorkSearch(FirstDate, datetimeWork, shortName, employeerIdAccordByShift);
            var listUserWorked = workBO.FilterWork(workingSearchSum).ToList();
            
            foreach (var item in employeerIdAccordByShift)
	        {
                decimal totalAmount = listUserWorked.Where(p => p.EmployeeId.ToInt(0) == item).Sum(p => p.Amount);
                int numberWorkInShift = listUserWorked.Count(p => p.EmployeeId.ToInt(0) == item && p.WorkScheduleCode.IsEquals(shift));
                int numberDayOff = listUserWorked.Count(p => p.EmployeeId.ToInt(0) == item && !shiftInWork.Contains(p.WorkScheduleCode));
                EmployeerInfo skillOfEmployeer = employeerSkill.FirstOrDefault(p => p.EmployeerId.ToInt(0) == item);
                if (skillOfEmployeer == null)
                {
                    employeerCanWorks.Add(new EmployeerAccords(item, totalAmount, shift));
                }
                else
                {
                    employeerCanWorks.Add(new EmployeerAccords(item, totalAmount, shift, skillOfEmployeer.Cerfiticate, numberWorkInShift, numberDayOff));
                }
                
	        }
            return employeerCanWorks;
        }

        public IEnumerable<int> GetEmployeerCanNotWork(string shortName, string workCode, DateTime datetimeWork)
        {
            WorkSearch workingSearch = new WorkSearch(shortName, workCode, datetimeWork, new List<int>());
            return workBO.EmployeerCanNotWork(workingSearch).ToList();
        }


        public IEnumerable<int> GetUserCanNotWorkBetweenDate(string shortName, string workingCode, int dayWork, DateTime datetimeWork, bool isCheckBetweenDate)
        {
            WorkSearch workingSearch = new WorkSearch(shortName, workingCode,dayWork, datetimeWork, isCheckBetweenDate);
            workingSearch.ShiftNotAllow.Add("T");
            return workBO.EmployeerCanNotWorkBetweenDate(workingSearch).ToList();
        }

        public bool InsertWoking(List<ScheduleInfo> working,List<EmployeerInfo> employeerSkill,  DateTime dateOfPlan, List<HolidayInfo> usersDayOff)
        {
            bool result = this.workBO.Create(working, dateOfPlan);
            if (!result)
            {
                return result;
            }
            List<int> employeerHasWork = working.Where(p => p.EmpId.IsNotNullOrEmpty()).Select(p => p.EmpId.ToInt(0)).ToList();
            List<int> employeerIdNotTask = employeerSkill.Where(p => !employeerHasWork.Contains(p.EmployeerId.ToInt(0))).Select(p => p.EmployeerId.ToInt(0)).ToList();
            result = this.workBO.InserUserNotTask(employeerIdNotTask, dateOfPlan, usersDayOff);
            return result;
        }



        public List<NumberWorkOfEmpl> GetNumberDayOffOfEmployeer(DateTime toDate)
        {
            return workBO.GetNumberDayOffOfEmployeer(toDate);
        }

        public List<NumberWorkOfEmpl> GetNumberWorkByShift(DateTime toDate, string workCode, List<int> employeerId, string location)
        {
            return workBO.GetNumberWorkByShift(toDate, workCode, employeerId, location);
        }

        public WokingInfo GetDetailWork(string dateWork, string employeerName, string workCode)
        {
            WorkChangeSearch condition = new WorkChangeSearch(employeerName, workCode, dateWork);
            return this.workBO.GetDetailWork(condition);
        }
        public List<WokingInfo> FilterByWork(string dateWork, string employeerName, string workCode)
        {
            WorkChangeSearch condition = new WorkChangeSearch(employeerName, workCode, dateWork);
            return this.workBO.FilterByWork(condition).ToList();
        }

        public bool DeleteWorkPlan(DateTime fromDate, DateTime toDate)
        {
            return this.workBO.Delete(fromDate, toDate);
        }

        public List<WorkDetail> CreateWorkOfMonth(DateTime fromDate, DateTime toDate)
        {
            List<WorkDetail> workDetail = this.workBO.FilterWork(fromDate, toDate);
            this.workOfMonthBO.Create(workDetail);
            return workDetail;
        }
    }
}
