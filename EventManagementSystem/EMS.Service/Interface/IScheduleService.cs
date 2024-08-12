using EMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Service.Interface
{
    public interface IScheduleService
    {
        List<Schedule> GetAllSchedules();
        Schedule GetDetailsForSchedule(Guid? id);
        void CreateNewSchedule(Schedule s);
        void UpdateExistingSchedule(Schedule s);
        void DeleteSchedule(Guid id);
    }
}
