using System;
using System.ComponentModel.DataAnnotations;

namespace restaurant.project.ModelView
{
    public class CustomerMV
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Timestamp]
        public DateTime CreatedDate { get; set; }

        [Timestamp]
        public DateTime UpdatedDate { get; set; }
        public bool Archived { get; set; }

    }
}
