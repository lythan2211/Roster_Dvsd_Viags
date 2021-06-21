using FlightSchedules.Business.DAO;
using FlightSchedules.Data.DBAccessor;
using InvoiceServer.Business.Models;
using System.Collections.Generic;
using System.Linq;

namespace FlightSchedules.Business.BL
{
    public class WorkSchedulesBO : IWorkSchedulesBO
    {
        #region Fields, Properties

        private readonly IWorkSchedulesRepository workSchedulesRepository;
        #endregion

        #region Contructor

        public WorkSchedulesBO(IRepositoryFactory repoFactory)
        {
            this.workSchedulesRepository = repoFactory.GetRepository<IWorkSchedulesRepository>();
        }

        #endregion

        #region Methods
        public IEnumerable<WorkSchedule> Filter(CeviticateSearch condition, int skip = 0, int take = int.MaxValue)
        {
            var workSchedules = this.workSchedulesRepository.Filter(condition, skip, take).ToList();
            return workSchedules;
            
        }

        public int CountFilter(InvoiceServer.Business.Models.CeviticateSearch condition, int skip = 0, int take = int.MaxValue)
        {
            var cevitications = this.workSchedulesRepository.Filter(condition, skip, take).ToList();
            return cevitications.Count();
        }

        public WorkSchedule GetById(int id)
        {
            var ceviticate = this.workSchedulesRepository.GetById(id);
            if (ceviticate == null)
            {
                return new WorkSchedule();
            }

            return ceviticate;

        }
        #endregion



    }
}