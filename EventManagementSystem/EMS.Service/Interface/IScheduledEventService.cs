using EMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Service.Interface
{
    public interface IScheduledEventService
    {
        List<ScheduledEvent> GetAllScheduledEvents();
        ScheduledEvent GetDetailsForScheduledEvent(Guid? id);
        void CreateNewScheduledEvent(ScheduledEvent s);
        void UpdateExistingScheduledEvent(ScheduledEvent s);
        void DeleteScheduledEvent(Guid id);
    }
}
