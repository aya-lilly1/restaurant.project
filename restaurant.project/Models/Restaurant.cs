using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace restaurant.project.Models
{
    public partial class Restaurant
    {
        public Restaurant()
        {
            Orders = new HashSet<Order>();
            Restaurantmenus = new HashSet<Restaurantmenu>();
        }

        public int Id { get; set; }
        public string RestaurantName { get; set; }
        public string Phone { get; set; }
        [Timestamp]
        public DateTime CreatedDate { get; set; }

        [Timestamp]
        public DateTime UpdatedDate { get; set; }
        public bool Archived { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Restaurantmenu> Restaurantmenus { get; set; }
    }
}
