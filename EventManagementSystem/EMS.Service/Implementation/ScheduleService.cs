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
    public class ScheduleService : IScheduleService
    {
        private readonly IRepository<Schedule> _scheduleRepository;

        public ScheduleService(IRepository<Schedule> scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        public void CreateNewSchedule(Schedule s)
        {
            _scheduleRepository.Insert(s);
        }

        public void DeleteSchedule(Guid id)
        {
            var schedule = _scheduleRepository.Get(id);
            _scheduleRepository.Delete(schedule);
        }

        public List<Schedule> GetAllSchedules()
        {
            return _scheduleRepository.GetAll().ToList();
        }

        public Schedule GetDetailsForSchedule(Guid? id)
        {
            var schedule = _scheduleRepository.Get(id);
           return schedule;
        }

        public void UpdateExistingSchedule(Schedule s)
        {
            _scheduleRepository.Update(s);
        }
    }
}
