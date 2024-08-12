using EMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Repository.Interface
{
    public interface ITicketRepository
    {
        List<Ticket> GetAllTickets();
        Ticket GetDetailsForTicket(BaseEntity id);
    }
}
