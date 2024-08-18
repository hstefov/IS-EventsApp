using EMS.Domain.Models;
using EMS.Repository.Interface;
using EMS.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Service.Implementation
{
    public class ScheduledEventService : IScheduledEventService
    {
        private readonly IRepository<ScheduledEvent> _scheduleRepository;

        public ScheduledEventService(IRepository<ScheduledEvent> scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        public void CreateNewScheduledEvent(ScheduledEvent s)
        {
            _scheduleRepository.Insert(s);
        }

        public void DeleteScheduledEvent(Guid id)
        {
            var schedule = _scheduleRepository.Get(id);
            _scheduleRepository.Delete(schedule);
        }

        public List<ScheduledEvent> GetAllScheduledEvents()
        {
            return _scheduleRepository.GetAll().ToList();
        }

        public ScheduledEvent GetDetailsForScheduledEvent(Guid? id)
        {
            var schedule = _scheduleRepository.Get(id);
           return schedule;
        }

        public void UpdateExistingScheduledEvent(ScheduledEvent s)
        {
            _scheduleRepository.Update(s);
        }
    }
}
