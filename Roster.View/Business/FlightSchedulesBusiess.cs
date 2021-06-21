using Roster.Business.BL;
using Roster.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Roster.Business
{
    public class FlightSchedulesBusiess : BaseClass
    {
        //private readonly IScheduleBO scheduleBO;
        private readonly IFlightScheduleBO rosterDetailBO;
        private readonly IProductBO productBO;
        public FlightSchedulesBusiess()
        {
            rosterDetailBO = this.GetBOFactory().GetBO<IFlightScheduleBO>();
            productBO = this.GetBOFactory().GetBO<IProductBO>();
        }

        public List<FlightSchedulesInfo> GetFlight(DateTime fromDate, DateTime toDate)
        {
            ConditionSearchFlight condition = new ConditionSearchFlight(fromDate, toDate);
            List<FlightSchedulesInfo> flights = this.rosterDetailBO.Filter(condition).ToList();
            return StandardizeFlight(flights);
        }

       
        public List<FlightSchedulesInfo> StandardizeFlight(List<FlightSchedulesInfo> flights)
        {
            List<FlightSchedulesInfo> result = new List<FlightSchedulesInfo>();
            foreach (var flight in flights)
            {
                FlightSchedulesInfo flightWork = new FlightSchedulesInfo(flight);
                flightWork.Works = this.CreateWorkByProductCode(flight);
                result.Add(flightWork);
            }
            return result;
        }

        public List<WorkInfo> CreateWorkByProductCode(FlightSchedulesInfo flight)
        {
            
            List<WorkInfo> result = new List<WorkInfo>();
            var products = GetProduct(flight.ProductCode);
            foreach (var product in products)
            {
                var worksOfProducts = CreateWorkByQuotaOfProduct(product, flight);
                result.AddRange(worksOfProducts);
            }

            return result;
        }

        /// <summary>
        /// Get all product by product code of flight
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        public List<ProductInfo> GetProduct(List<string> productCode)
        {
            List<ProductInfo> result = new List<ProductInfo>();
            return result;
        }

        private List<WorkInfo> CreateWorkByQuotaOfProduct(ProductInfo product, FlightSchedulesInfo flight)
        {
            // Add condition remove product or sub time work of product.
            List<WorkInfo> result = new List<WorkInfo>();
            for (int i = 0; i < product.ManQuota; i++)
            {
                WorkInfo work = new WorkInfo();
                result.Add(work);
            }
            return result;
        }

    }
}
