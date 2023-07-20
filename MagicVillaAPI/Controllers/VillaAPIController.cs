using MagicVillaAPI.Data;
using MagicVillaAPI.DTOs;
using MagicVillaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MagicVillaAPI.Controllers
{
	[Route("api/[controller]")]  // it is always better to use in specific name instead of the [controller] syntax
	[ApiController]
	public class VillaAPIController : ControllerBase
	{
		[HttpGet("getVilla")]
		public ActionResult<IEnumerable<VillaDto>> GetVillas()
		{
			return Ok(VillaStore.villaDtos);
		}

		[HttpGet("{id:int}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		//[ProducesResponseType(200, Type = typeof(VillaDto))] 
		// if you use the IActionResult instead of the ActionResult<>
		public ActionResult<VillaDto> GetVilla(int id)
		{
			if(id == 0)
			{
				return BadRequest();
			}
			var villa  = VillaStore.villaDtos.FirstOrDefault(x => x.Id == id);
			if (villa == null)
			{
				return NotFound();
			}
			return Ok(villa);
		}
	}
}
