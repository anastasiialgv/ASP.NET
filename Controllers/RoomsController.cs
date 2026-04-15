using APBD_Task_7.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Task_7.Controllers
{
    [Route("api/[controller]")] // [controller] [Rooms]Controller -> api/rooms
    [ApiController]
    public class RoomsController : ControllerBase
    {
        public static List<Room> Rooms = new List<Room>()
        {
        };
        [HttpGet]
        public IActionResult Get([FromQuery]int? minCapacity = 0)
        {
            return Ok(Rooms.Where(r => r.Capacity >= minCapacity));
        }
        
        [Route("{id}")]
        [HttpGet]
        public IActionResult GetById([FromRoute] int id)
        {
            var room = Rooms.FirstOrDefault(r => r.Id == id);
            if (room == null)
            {
                return NotFound();
            }
            
            return Ok(room);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Room room)
        {
            
        }
    }
    
}
