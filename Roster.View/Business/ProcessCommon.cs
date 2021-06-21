using Roster.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Roster.Business.Extensions;
using Roster.Data.DBAccessor;
using Roster.Business;

namespace Roster.View.Business
{
    public static class ProcessCommon
    {
        public static Dictionary<int, List<int>> ZoneMap = new Dictionary<int, List<int>>();
        public static Dictionary<string, List<string>> GetProductCode()
        {
            Dictionary<string, List<string>> productCodeDitinct = new Dictionary<string, List<string>>()
            {
                {"XNL", new List<string>() {"XNG","XNM","XNL"}},
                {"BTA", new List<string>() {"BTA"}},
                {"DKE", new List<string>() {"DKE"}},
                {"BX", new List<string>() {"BXM","BXL","BXE"}},
            };

            return productCodeDitinct;
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
    }


}
