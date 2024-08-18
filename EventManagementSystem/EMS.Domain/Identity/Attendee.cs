﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace EMS.Domain.Identity
{
    public class Attendee : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? Number { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }
        public Attendee()
        {
            ShoppingCart = new ShoppingCart();
        }
    }
}
