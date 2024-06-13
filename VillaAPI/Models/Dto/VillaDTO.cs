using System.ComponentModel.DataAnnotations;

namespace VillaAPI.Models.Dto
{
    public class VillaDTO
    {
        public int id { get; set; }
        [Required]
        [MaxLength(30)]
        public string name { get; set; }

        public string Details { get; set; }
        [Required]

        public double Rate { get; set; }

        public int occupancy { get; set; }

        public string ImageUrl { get; set; }

        public string Amenity { get; set; }
        public int sqft { get; set; }
    }
}
