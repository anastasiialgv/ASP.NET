using APBD_Task_7.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Task_7.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class RoomsController : ControllerBase
    {
   
        [HttpGet]
        public IActionResult Get([FromQuery]int? minCapacity = 0)
        {
            return Ok(Data.Rooms.Where(r => r.Capacity >= minCapacity));
        }
        
        [Route("{id}")]
        [HttpGet]
        public IActionResult GetById([FromRoute] int id)
        {
            var room = Data.Rooms.FirstOrDefault(r => r.Id == id);
            if (room == null)
            {
                return NotFound();
            }
            
            return Ok(room);
        }
        
        public IActionResult GetAll()
        {
            return Ok(Data.Rooms);
        }

[Route("building/{buildingCode}")]
        [HttpGet]
        public IActionResult GetByBuilding([FromRoute] string buildingCode)
        {
            var rooms = Data.Rooms
                .Where(r => r.BuildingCode.Equals(buildingCode, StringComparison.OrdinalIgnoreCase))
                .ToList();
            return Ok(rooms);
        }

        [Route("filter")]
        [HttpGet]
        public IActionResult GetFiltered(
            [FromQuery] int? minCapacity, 
            [FromQuery] bool? hasProjector, 
            [FromQuery] bool activeOnly = true)
        {
            var filteredRooms = Data.Rooms.AsQueryable();

            if (minCapacity.HasValue)
                filteredRooms = filteredRooms.Where(r => r.Capacity >= minCapacity.Value);

            if (hasProjector.HasValue)
                filteredRooms = filteredRooms.Where(r => r.HasProjector == hasProjector.Value);

            if (activeOnly)
                filteredRooms = filteredRooms.Where(r => r.IsActive);

            return Ok(filteredRooms.ToList());
        }

        [HttpPost]
        public IActionResult Create([FromBody] Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            room.Id = Data.Rooms.Any() ? Data.Rooms.Max(r => r.Id) + 1 : 1;
            Data.Rooms.Add(room);

            return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
        }

        [Route("{id}")]
        [HttpPut]
        public IActionResult Update([FromRoute] int id, [FromBody] Room updatedRoom)
        {
            var existingRoom = Data.Rooms.FirstOrDefault(r => r.Id == id);
            if (existingRoom == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            existingRoom.Name = updatedRoom.Name;
            existingRoom.BuildingCode = updatedRoom.BuildingCode;
            existingRoom.Floor = updatedRoom.Floor;
            existingRoom.Capacity = updatedRoom.Capacity;
            existingRoom.HasProjector = updatedRoom.HasProjector;
            existingRoom.IsActive = updatedRoom.IsActive;

            return Ok(existingRoom);
        }

        [Route("{id}")]
        [HttpDelete]
        public IActionResult Delete([FromRoute] int id)
        {
            var room = Data.Rooms.FirstOrDefault(r => r.Id == id);
            if (room == null)
            {
                return NotFound();
            }

            var hasReservations = Data.Reservations.Any(res => res.RoomId == id);
            if (hasReservations)
            {
                return Conflict("Nie można usunąć sali, która ma przypisane rezerwacje.");
            }

            Data.Rooms.Remove(room);
            return NoContent(); 
        }
    }
    
}
