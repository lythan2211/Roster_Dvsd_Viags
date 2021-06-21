using Roster.Business.Models;
using System;
using System.Collections.Generic;

namespace Roster.Business.BL
{
    public interface IEmpSkillBO
    {
        List<EmployeerSkillInfo> Filter(string employeeId);
    }
}
