using System;
using System.ComponentModel.DataAnnotations;

namespace restaurant.project.ModelView
{
    public class RestaurantModelView
    {
        public int Id { get; set; }
        public string RestaurantName { get; set; }
        public string Phone { get; set; }
        [Timestamp]
        public DateTime CreatedDate { get; set; }

        [Timestamp]
        public DateTime UpdatedDate { get; set; }
        public bool Archived { get; set; }
    }
}
