using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace restaurant.project.Models
{
    public partial class Restaurantmenu
    {
        public Restaurantmenu()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string MaelName { get; set; }
        public int Quatity { get; set; }
        public float PriceNis { get; set; }
        public float PriceUsd { get; set; }

        public int ResurantId { get; set; }
        [Timestamp]
        public DateTime CreatedDate { get; set; }

        [Timestamp]
        public DateTime UpdatedDate { get; set; }
        public bool Archived { get; set; }

        public virtual Restaurant Resurant { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
