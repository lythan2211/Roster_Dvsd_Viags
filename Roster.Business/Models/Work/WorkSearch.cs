using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roster.Business.Models
{
    public class WorkSearch
    {
        public string ShortName { get; set; }
        public DateTime DateFrom { get; set; }

        public DateTime  DateTo{ get; set; }

        public string WorkingCode { get; set; }

         
        public WorkSearch()
        {
             
        }
        public WorkSearch(string location, string workingCode, DateTime dateWorking) 
            : this()
        {
            this.ShortName = location;
            this.WorkingCode = workingCode;
            this.DateFrom = dateWorking.AddDays(-1);
        }

        public WorkSearch(string location, string workingCode, int day, DateTime dateWorking, bool isCheckBetweeenDate)
            : this()
        {
            this.DateTo = dateWorking;
            this.WorkingCode = workingCode;
            this.DateFrom = dateWorking.AddDays(day  * - 1);
            this.ShortName = location;
        }
    }
}

