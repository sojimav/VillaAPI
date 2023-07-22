using MagicVillaAPI.DTOs;

namespace MagicVillaAPI.Data
{
	public static class VillaStore
	{
		public static List<VillaDto> villaDtos = new List<VillaDto>()
		{
			new VillaDto() {Id = 1, Name = "Mongo Perk", Occupancy = 6, Sqft = 200},
			new VillaDto() {Id = 2, Name = "Oliver Twist", Occupancy = 5, Sqft = 100}
		};

	}		
}
