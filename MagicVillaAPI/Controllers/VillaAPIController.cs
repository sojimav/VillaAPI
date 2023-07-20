using MagicVillaAPI.Data;
using MagicVillaAPI.DTOs;
using MagicVillaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MagicVillaAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class VillaAPIController : ControllerBase
	{
		[HttpGet("getVilla")]
		public IEnumerable<VillaDto> GetVillas()
		{
			return VillaStore.villaDtos;
		}

		[HttpGet("{id:int}")]
		public VillaDto GetVilla(int id)
		{
			return VillaStore.villaDtos.FirstOrDefault(row => row.Id == id);
		}
	}
}
