using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VillaAPI.Models
{
    public class Villa
    {
        //adding properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //identity to automatically manage id

        public int id { get; set; }
        public string name { get; set; }
        public string Details { get; set; }

        public double Rate { get; set; }

        public int occupancy { get; set; }

        public string ImageUrl { get; set; }

        public string Amenity { get; set; }
        public int sqft { get; set; }
        public DateTime CreatedDate  { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
