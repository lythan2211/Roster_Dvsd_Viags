using System;
using System.Text.RegularExpressions;
using Roster.Business.Models;

namespace Roster.Business.Extensions
{
    public static class GetMinStartMinEndFLightExtensions
    {
        public static int[] GetMinStartMinEnd(FlightSchedulesDetail chuyenBay, ProductDetail sanPham)
        {
            int[] min = {0, 0};
            min = GetTimeServe(chuyenBay, sanPham);
            if (chuyenBay.ETA != "" && chuyenBay.ETD != "" && chuyenBay.ArrNo != "" && chuyenBay.DepNo != "" )
            {
                min[0] = TimeStringToMin(chuyenBay.ETA);
                min[1] = TimeStringToMin(chuyenBay.ETD);
                return min;

            }
            if (chuyenBay.ETA == "" && chuyenBay.ETD != ""  && (chuyenBay.STA != "" && chuyenBay.STD != "") && chuyenBay.ArrNo != "" && chuyenBay.DepNo != "" )
            {
                min[0] = TimeStringToMin(chuyenBay.STA);
                min[1] = TimeStringToMin(chuyenBay.STD);
                return min;

            }
            if (chuyenBay.ETA != "" && chuyenBay.ETD == ""  && (chuyenBay.STA != "" && chuyenBay.STD != "") && chuyenBay.ArrNo != "" && chuyenBay.DepNo != "")
            {
                min[0] = TimeStringToMin(chuyenBay.ETA);
                min[1] = TimeStringToMin(chuyenBay.STD);
                return min;

            }
            if (chuyenBay.ETA == "" && chuyenBay.ETD == ""  && (chuyenBay.STA != "" && chuyenBay.STD != "") && chuyenBay.ArrNo != "" && chuyenBay.DepNo != "")
            {
                min[0] = TimeStringToMin(chuyenBay.STA);
                min[1] = TimeStringToMin(chuyenBay.STD);
                return min;
            }

            return min;
        }

        public static int[] GetTimeServe(FlightSchedulesDetail chuyenbay, ProductDetail sanPham)
        {
            int[] min = {0, 0};
            int timeBaseMinOfDay = timeBaseToMinOfDay(chuyenbay, sanPham.TimeBase);
            int minStart = timeBaseMinOfDay + sanPham.StartTime;
            int minEnd = minStart + sanPham.DuringTime;
            min[0] = minStart;
            min[1] = minEnd;
            return min;
        }
        public static int timeBaseToMinOfDay(FlightSchedulesDetail chuyenBay, string timeBase)
        {
            switch (timeBase)
            {
                case "ETA":
                    // it must be same has '+' or not, but real data just invalid
                    if (chuyenBay.ETA == "") {
                    // just work around for invalid data of FlightSchedule
                    return TimeStringToMin(chuyenBay.STA);
                    } else
                    {
                        return TimeStringToMin(chuyenBay.ETA);
                    }
                case "ETD":
                    // it must be same has '+' or not, but real data just invalid
                    if (chuyenBay.ETD == "" ){ 
                        // just work around for invalid data of FlightSchedule
                        return TimeStringToMin(chuyenBay.STD);
                    } else {
                        return TimeStringToMin(chuyenBay.ETD);
                    }
                case "STA":
                    return TimeStringToMin(chuyenBay.STA);
                case "STD":
                    return TimeStringToMin(chuyenBay.STD);
                default:
                    return 0;
            }
        }
        public static int TimeStringToMin(string timeBase) {
            if (timeBase.IsNullOrEmpty())
            {
                return 0;
            }
            
            string sdtTimeBase = Regex.Replace(timeBase, @"\s", "");
            int daySkip = 0;
            if (sdtTimeBase.Contains("+"))
            {
                daySkip = 1;
            }
            sdtTimeBase = Regex.Replace(timeBase, @"\s", "+");
            var dateTime = DateTime.ParseExact(sdtTimeBase, "H:mm", null, System.Globalization.DateTimeStyles.None);
            return dateTime.Hour * 60 + daySkip * 1440;
        }
    }
}