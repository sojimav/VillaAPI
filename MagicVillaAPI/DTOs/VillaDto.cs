using System.ComponentModel.DataAnnotations;

namespace MagicVillaAPI.DTOs
{
	public class VillaDto
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(30)]
		public string Name { get; set; } = string.Empty;
		public int Occupancy { get; set; }
		public int Sqft { get; set; }
	}
}
