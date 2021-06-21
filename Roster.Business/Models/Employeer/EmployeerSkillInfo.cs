using Roster.Data.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Roster.Business.Models
{
    public class EmployeerSkillInfo
    {
        public string EmployeerId { get; set; }

        public bool BXE { get; set; }

        public bool XN { get; set; }

        public EmployeerSkillInfo()
        {

        }

        public EmployeerSkillInfo(object srcObject)
            : this()
        {
            if (srcObject != null)
            {
                DataObjectConverter.Convert<object, EmployeerSkillInfo>(srcObject, this);
            }
        }


    }
}
