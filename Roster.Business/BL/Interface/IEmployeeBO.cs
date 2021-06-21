using Roster.Business.Models;
using System;
using System.Collections.Generic;

namespace Roster.Business.BL
{
    public interface IEmployeeBO
    {
        List<EmployeerInfo> Filter(ConditionSearchEmployee condition);

        EmployeerInfo Create(EmployeerInfo WorkScheInfo);

        EmployeerInfo Update(int id, EmployeerInfo WorkScheInfo);

        bool Delete(int id);
    }
}
