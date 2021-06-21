using Roster.Data.DBAccessor;
using Roster.Business.Models;
using Roster.Business.Extensions;
using System;

namespace Roster.Business.BL
{
    public static class TransferData
    {
        public static void CopyData(this TBL_Emp_Skill toObject, EmployeerSkillInfo fromObject)
        {
            if (fromObject == null)
            {
                return;
            }

            toObject.bBXE= fromObject.BXE;
        }
    }
}
