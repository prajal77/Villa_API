using VillaAPI.Models.Dto;

namespace VillaAPI.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> villaList = new List<VillaDTO>
            {
                new VillaDTO { id =1,name ="Pool View",sqft=100,occupancy=4},
                new VillaDTO { id =2, name = "Beach View",sqft=100,occupancy=4 }

            };
    }
}
