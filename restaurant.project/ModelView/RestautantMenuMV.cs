using System;
using System.ComponentModel.DataAnnotations;

namespace restaurant.project.ModelView
{
    public class RestautantMenuMV
    {
        public int Id { get; set; }
        public string MaelName { get; set; }
        public int Quatity { get; set; }
        public float PriceNis { get; set; }
      //  public float PriceUsd { get; set; }
        public int ResurantId { get; set; }
        [Timestamp]
        public DateTime CreatedDate { get; set; }

        [Timestamp]
        public DateTime UpdatedDate { get; set; }
        public bool Archived { get; set; }
    }
}
