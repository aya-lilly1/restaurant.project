using System;
using System.ComponentModel.DataAnnotations;

namespace restaurant.project.ModelView
{
    public class OrderMV
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
    }
}
