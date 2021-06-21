using InvoiceServer.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceServer.Business.Extensions;

namespace FlightSchedules.View.Business
{
   public static class ProcessWork
    {
       //Lấy danh sách nhân sự có thể làm ca làm việc
       public static List<EmployeerAccords> CreateWork(List<WokingInfo> workInfo, string shortName, string shift)
       {
           return null;
       }

       public static List<CerOfWork> CreateCerOfWork(List<ScheduleInfo> worksOfUserFake)
       {
           List<string> distinctUserFake = worksOfUserFake.Select(p => p.UserFake).Distinct().ToList();
           List<CerOfWork> cerOfUserFake = new List<CerOfWork>();
           foreach (var userFake in distinctUserFake)
           {
                List<string> cerfiticate = worksOfUserFake.Where(p => p.UserFake.IsEquals(userFake)).Select(p => p.ShortName).ToList();
                var firstOfUser = worksOfUserFake.FirstOrDefault(p => p.UserFake.IsEquals(userFake));
                decimal amountOfShift= worksOfUserFake.Sum(p => p.UnitPrice);
                cerOfUserFake.Add(new CerOfWork(userFake, firstOfUser.Shift, firstOfUser.ShortName, cerfiticate, amountOfShift));
           }

           return cerOfUserFake;
       }
    }
}
