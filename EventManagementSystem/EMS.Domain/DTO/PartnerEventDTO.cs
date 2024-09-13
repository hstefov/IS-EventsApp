using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Domain.DTO
{
    public class PartnerEventDTO
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public string? DefaultImageURL { get; set; } = "https://t4.ftcdn.net/jpg/01/75/46/69/360_F_175466970_aRDdYku348o2ytRIJgeTuqychevhUe7u.jpg";
    }
}
