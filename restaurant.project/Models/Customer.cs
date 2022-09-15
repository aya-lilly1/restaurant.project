using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace restaurant.project.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Timestamp]
        public DateTime CreatedDate { get; set; }

        [Timestamp] 
        public DateTime UpdatedDate { get; set; }
        public bool Archived { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
