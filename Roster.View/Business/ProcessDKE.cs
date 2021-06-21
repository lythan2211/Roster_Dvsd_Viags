using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceServer.Business.Models;
using FlightSchedules.Data.DBAccessor;
using FlightSchedules.Business;
using InvoiceServer.Business.Extensions;

namespace FlightSchedules.View.Business
{
    public static class ProcessDKE
    {
        public static List<int> idAssigned = new List<int>();
        public static List<ScheduleInfo> souceNotAssign = new List<ScheduleInfo>();
        public static Dictionary<int, List<int>> ZoneMap = new Dictionary<int, List<int>>();


        public static List<ScheduleInfo> ProcessPlan(List<ScheduleInfo> source,
            List<WorkSchedulesInfo> workSchedules1,
            string shortCode,
            string dateWork,
            int numbeLoop,
            int identity,
            bool isMappingZone = true)
        {
            List<string> works = ProcessCommon.GetProductCode()[shortCode];
            List<WorkSchedulesInfo> workScheduleRemovedShiftOverLight = workSchedules1.Where(p => p.BonusDay == 0).ToList();
            var sourceAfterPlanWork = DoWorkServicePlan(source, workScheduleRemovedShiftOverLight, shortCode, works, dateWork, numbeLoop, jobConfigs, identity,isMappingZone);
            idAssigned = sourceAfterPlanWork.Select(p => p.Id).Distinct().ToList();
            souceNotAssign = source.Where(p => works.Contains(p.ShortName) && !idAssigned.Contains(p.Id)).ToList();
            List<ScheduleInfo> extenShiftHasConfigShiftOverlight = ProcessCommon.BonusShiftWithUserFakeHasShiftOverlight(sourceAfterPlanWork, souceNotAssign, workSchedules1, dateWork, "XNL", isMappingZone);
            sourceAfterPlanWork.AddRange(extenShiftHasConfigShiftOverlight);
            
            //Thực hiện thay đổi ca làm việc nếu không đáp ứng được một số điều kiện
            List<ScheduleInfo> resultChangeShift = ProcessCommon.ChangeShiftWhenNotFillShiftOverlight(sourceAfterPlanWork, workSchedules1, dateWork);
            idAssigned = resultChangeShift.Select(p => p.Id).Distinct().ToList();
            souceNotAssign = source.Where(p => works.Contains(p.ShortName) && !idAssigned.Contains(p.Id)).ToList();
            List<ScheduleInfo> bonusWorkAfterResultChangeShift = ProcessCommon.BonusWorkForUser(souceNotAssign, resultChangeShift, isMappingZone);
            

            //Chấp nhận ca làm việc 1 chuyến bay
            idAssigned = bonusWorkAfterResultChangeShift.Select(p => p.Id).Distinct().ToList();
            souceNotAssign = source.Where(p => works.Contains(p.ShortName) && !idAssigned.Contains(p.Id)).ToList();
            List<ScheduleInfo> planWorkAcceptMinWork = DoWorkServicePlan(souceNotAssign, workSchedules1, shortCode, works, dateWork, numbeLoop, jobConfigs, (identity + 100), false, 10);             
            bonusWorkAfterResultChangeShift.AddRange(planWorkAcceptMinWork);
            List<ScheduleInfo> resultChangeShiftTow = ProcessCommon.ChangeShiftWhenNotFillShiftOverlight(bonusWorkAfterResultChangeShift, workSchedules1, dateWork);


            return resultChangeShiftTow;
        }       

        private static List<ScheduleInfo> DoWorkServicePlan(
            List<ScheduleInfo> source,
            List<WorkSchedulesInfo> workSchedules,
            string shortCode,
            List<string> workByShortName,
            string dateWork,
            int numbeLoop,
            int identity,
            bool isMappingZone,
            int bonusTime = 0,
            bool isAcceptionMinWork = false)
        {
            var sourceAfterPlanWork = new List<ScheduleInfo>();

            for (int i = 0; i < numbeLoop; i++)
            {

                int identityUserFake = identity * (i + 1);
                Setting.WriteFile(string.Format("identity {0}", identityUserFake));
                List<int> workIdHasPlanForUserFake = sourceAfterPlanWork.Select(p => p.Id).ToList();
                var workPlanNotYetProcess = source.Where(p => !workIdHasPlanForUserFake.Contains(p.Id) && workByShortName.Contains(p.ShortName)).ToList();
                if (workPlanNotYetProcess.Count == 0)
                {
                    break;
                }
                //Thực hiện build lại công việc vào ca làm việc để thực hiện phân công lại
                var sourcePlanWorkByShortName = ProcessCommon.BuildWorksWithWorkSchedule(workPlanNotYetProcess, workSchedules, dateWork, bonusTime);
                var worksOffUserFake = ProcessCommon.ProcessWorkForUserFake(jobConfigs, sourcePlanWorkByShortName, shortCode, identityUserFake, i, isAcceptionMinWork, i, isMappingZone);
                sourceAfterPlanWork.AddRange(worksOffUserFake);
            }

            //return sourceAfterPlanWork;
            List<int> idAssigned = sourceAfterPlanWork.Select(p => p.Id).Distinct().ToList();
            var souceNotAssign = source.Where(p => workByShortName.Contains(p.ShortName) && !idAssigned.Contains(p.Id)).ToList();
            bool isSameTime = bonusTime > 0;
            return ProcessCommon.BonusWorkForUser(souceNotAssign, sourceAfterPlanWork,isMappingZone, isSameTime);
        }

    }
}
