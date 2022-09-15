using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace restaurant.project.Models
{
    public partial class Order
    {
        public int Id { get; set; }
        public int MealId { get; set; }
        public int CustomerId { get; set; }
        public int RestaurantId { get; set; }
        public int Quatity { get; set; }
        [Timestamp]
        public DateTime CreatedDate { get; set; }

        [Timestamp]
        public DateTime UpdatedDate { get; set; }
        public bool Archived { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Restaurantmenu Meal { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}
