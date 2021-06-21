using Roster.Business.BL;
using Roster.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Roster.Business
{
    public class EmployeeBusiess : BaseClass
    {
        //private readonly IScheduleBO scheduleBO;
        private readonly IEmployeeBO employeeBO;
        private readonly IEmpSkillBO employeeSkillBO;
        public EmployeeBusiess()
        {
            employeeBO = this.GetBOFactory().GetBO<IEmployeeBO>();
            employeeSkillBO = this.GetBOFactory().GetBO<IEmpSkillBO>();
        }

        public List<EmployeerInfo> GetEmployeeWork(DateTime fromDate, DateTime toDate)
        {
            ConditionSearchEmployee condition = new ConditionSearchEmployee(fromDate, toDate);
            List<EmployeerInfo> employees = this.employeeBO.Filter(condition).ToList();
            return StandardizeEmployee(employees);
        }


        public List<EmployeerInfo> StandardizeEmployee(List<EmployeerInfo> employees)
        {
            List<EmployeerInfo> result = new List<EmployeerInfo>();
            foreach (var item in employees)
            {
                List<string> skill = GetSkillOfEmployee(item.EmployeerId);
                EmployeerInfo employee = new EmployeerInfo(item.EmployeerId, skill);
                result.Add(employee);
            }

            return result;
        }


        private List<string> GetSkillOfEmployee(string eployeeId)
        {
            //TODO Change source get skill of Employee when copy database;
            var employeeSkill = employeeSkillBO.Filter(eployeeId);
            return employeeSkill.Select(p => p.EmployeerId).ToList();
        }
        
         
    }
}
