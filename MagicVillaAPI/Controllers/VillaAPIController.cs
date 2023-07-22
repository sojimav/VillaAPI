using MagicVillaAPI.Data;
using MagicVillaAPI.DTOs;
using MagicVillaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace MagicVillaAPI.Controllers
{
	// it is always better to use in specific name instead of the [controller] syntax
	[Route("api/villa")]
	[ApiController]
	public class VillaAPIController : ControllerBase
	{

		private readonly ILogger<VillaAPIController> _logger;

		public VillaAPIController(ILogger<VillaAPIController> logger)
		{
			_logger = logger;
		}


        [HttpGet("getVilla")]
		public ActionResult<IEnumerable<VillaDto>> GetVillas()
		{
			_logger.LogInformation("Getting all villas");
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
			if (id == 0)
			{
				_logger.LogError("Get Villa Error with Id" + id);
				return BadRequest();
			}
			var villa = VillaStore.villaDtos.FirstOrDefault(x => x.Id == id);
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
		public ActionResult<VillaDto> CreateVilla([FromBody] VillaDto villaDto)
		{
			if (VillaStore.villaDtos.FirstOrDefault(row => row.Name.ToLower() == villaDto.Name.ToLower()) != null)
			{
				ModelState.AddModelError("CustomError", "Villa already Exist");
				return BadRequest(ModelState);
			}
			if (villaDto == null)
			{
				return BadRequest(villaDto);
			}
			else if (villaDto.Id > 0)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
			villaDto.Id = VillaStore.villaDtos.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;
			VillaStore.villaDtos.Add(villaDto);
			//return Ok(villaDto);
			return CreatedAtRoute("GetVilla", new { id = villaDto.Id }, villaDto);

		}


		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[HttpDelete("{id:int}", Name = "DeleteVilla")]
		public IActionResult Delete(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}
			var villa = VillaStore.villaDtos.FirstOrDefault(row => row.Id == id);
			if(villa == null)
			{
				return NotFound();
			}
			VillaStore.villaDtos.Remove(villa);
			return NoContent();
			
		}



		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[HttpPut("{id:int}", Name ="UpdateVilla")]
		public IActionResult UpdateVilla(int id, [FromBody]VillaDto dto)
		{		
			if(dto == null || id != dto.Id)
			{
				return BadRequest();
			}	
			var vto = VillaStore.villaDtos.FirstOrDefault(row => row.Id == id);
			vto.Name = dto.Name;
			vto.Sqft = dto.Sqft;
			vto.Occupancy = dto.Occupancy;
			return NoContent();
		}


		[HttpPatch("{id:int}", Name = "PatchVilla")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult PatchVilla(int id, JsonPatchDocument<VillaDto> patchDTO)
		{
			if ( patchDTO == null || id == 0)
			{
				return BadRequest();
			}
			var vto = VillaStore.villaDtos.FirstOrDefault(row => row.Id == id);

			if(vto ==  null)
			{
				return BadRequest();
			}

			patchDTO.ApplyTo(vto, ModelState);
			if(!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			return NoContent();
		}
	}
}
