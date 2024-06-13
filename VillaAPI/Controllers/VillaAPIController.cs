using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using VillaAPI.Data;
using VillaAPI.Models;
using VillaAPI.Models.Dto;

namespace VillaAPI.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]//notify the application that its api controller
    public class VillaAPIController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        // when we work with api different status code need to define we use ActionResult 
        //it help to return both VillaDTO object as well as action result like HTTP 400 Bad Request
        //iEnumarable helps to return collection fo VillaDTO objects
        public ActionResult <IEnumerable<VillaDTO>> GetVillas()
        {
            return Ok(VillaStore.villaList) ;
        }
 
        //[ProducesResponseType(404)]// to document responseTime 
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        //to give explicit name of the controller
        [HttpGet("{id:int}", Name = "GetVilla")]
        public ActionResult< VillaDTO> GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(u => u.id == id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(villa);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        /*
        when we are working with http post we receive object [FromBody]
        The [FromBody] attribute tells ASP.NET Core to get the value of villaDTO from the body of the HTTP request.
        
         */
        public ActionResult<VillaDTO> CreateVilla([FromBody]VillaDTO villaDTO)
        {
            //For custom validation 
            //ModelState mean VillaDTO
            /*if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }*/

            if(VillaStore.villaList.FirstOrDefault(u=> u.name.ToLower() == villaDTO.name.ToLower() ) != null)
            {
                ModelState.AddModelError("CustomError", "Villa already Exists!");
                return BadRequest(ModelState);
            }

                if (villaDTO == null)
            {
                return BadRequest(villaDTO);
            }
            if(villaDTO.id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            villaDTO.id = VillaStore.villaList.OrderByDescending(u => u.id).FirstOrDefault().id+1;
            VillaStore.villaList.Add(villaDTO);

            //to create link to call GetVilla by Id 
            // route =Name, object?routeValue, object?value

            return CreatedAtRoute("GetVilla", new { Id = villaDTO.id }, villaDTO);

            //return Ok(villaDTO);
        }


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]


        [HttpDelete("{id:int}", Name ="DeleteVilla") ]

        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(u => u.id == id);
            if(villa == null)
            {
                return NotFound();
            }
            VillaStore.villaList.Remove(villa);
            return NoContent();
        }


        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDTO villaDTO)
        {
            if (villaDTO == null || id != villaDTO.id)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(u => u.id == id);
            villa.name = villaDTO.name;
            villa.sqft = villaDTO.sqft;
            villa.occupancy = villaDTO.occupancy;

            return NoContent();
        }

        [HttpPatch("{id:int}",Name ="UpdatePatchVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO )
        {
            if(patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(u=>u.id == id);
            patchDTO.ApplyTo(villa, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent() ;
        }
    }

}
