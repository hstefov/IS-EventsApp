﻿using EMS.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Service.Interface
{
    public interface IPartnerEventService
    {
        Task SyncPartnerEvents(List<PartnerEventDTO> partnerEvents);
    }
}
