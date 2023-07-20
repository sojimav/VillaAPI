using MagicVillaAPI.DTOs;

namespace MagicVillaAPI.Data
{
	public static class VillaStore
	{
		public static List<VillaDto> villaDtos = new List<VillaDto>()
		{
			new VillaDto() {Id = 1, Name = "Mongo Perk"},
			new VillaDto() {Id = 2, Name = "Oliver Twist"}
		};

	}		
}
