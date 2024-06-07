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

        [HttpGet("id:int")]
        //[ProducesResponseType(404)]// to document responseTime 
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]


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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [HttpPost]
        /*
        when we are working with http post we receive object [FromBody]
        The [FromBody] attribute tells ASP.NET Core to get the value of villaDTO from the body of the HTTP request.
        
         */
        public ActionResult<VillaDTO> CreateVilla([FromBody]VillaDTO villaDTO)
        {
            if(villaDTO == null)
            {
                return BadRequest(villaDTO);
            }
            if(villaDTO.id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            villaDTO.id = VillaStore.villaList.OrderByDescending(u => u.id).FirstOrDefault().id+1;
            VillaStore.villaList.Add(villaDTO);
            return Ok(villaDTO);
        }
    }

}
