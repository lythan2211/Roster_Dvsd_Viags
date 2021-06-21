using System.Collections.Generic;
using System.Linq;

namespace Roster.Business.Models
{
    public class EmployeerInfo
    {
        public string EmployeerId { get; set; }

        public string EmployeeName { get; set; }

        public string GroupWork { get; set; }

        public List<string> Skill { get; set; }

        public List<WorkInfo> Works { get; set; }

        public EmployeerInfo()
        {
            this.Works = new List<WorkInfo>();
            this.Skill = new List<string>();
        }

        public EmployeerInfo(string employeerId, List<string> skill)
            : this()
        {
            this.EmployeerId = employeerId;
            this.Skill = skill;
        }

        public List<string> ZoneWoking()
        {
            return this.Works.Select(p => p.Zone).Distinct().ToList();
        }

        public bool CanWork(WorkInfo work)
        {
            bool workIsExist = this.Works.Count(p => p.StartTime <= work.StartTime
                  && p.EndTime >= work.EndTime) > 0;
            if (workIsExist)
            {
                return false;
            }

            this.Works.Add(work);
            return true;
        }
    }
}
