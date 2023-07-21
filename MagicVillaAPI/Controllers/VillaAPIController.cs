using MagicVillaAPI.Data;
using MagicVillaAPI.DTOs;
using MagicVillaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MagicVillaAPI.Controllers
{
	// it is always better to use in specific name instead of the [controller] syntax
	[Route("api/[controller]")]  
	[ApiController]
	public class VillaAPIController : ControllerBase
	{
		[HttpGet("getVilla")]
		public ActionResult<IEnumerable<VillaDto>> GetVillas()
		{
			return Ok(VillaStore.villaDtos);
		}


		//[ProducesResponseType(200)]
		//[ProducesResponseType(400)]
		//[ProducesResponseType(404)]
		//[ProducesResponseType(200, Type = typeof(VillaDto))] 
		// if you use the IActionResult instead of the ActionResult<> then you may need to specify a typeof
		[HttpGet("{id:int}", Name = "GetVilla")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
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

		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[HttpPost]
		public ActionResult<VillaDto> CreateVilla([FromBody]VillaDto villaDto)
		{
			if(VillaStore.villaDtos.FirstOrDefault(row => row.Name.ToLower() == villaDto.Name.ToLower()) != null)
			{
				ModelState.AddModelError("CustomError", "Villa already Exist");
				return BadRequest(ModelState);
			}
			if(villaDto == null)
			{
				return BadRequest(villaDto);
			}
			else if(villaDto.Id > 0)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
			villaDto.Id = VillaStore.villaDtos.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;
			VillaStore.villaDtos.Add(villaDto);
			//return Ok(villaDto);
			return CreatedAtRoute("GetVilla", new { id = villaDto.Id }, villaDto);
		}

	}
}
