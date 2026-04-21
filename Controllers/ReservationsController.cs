using APBD_Task_7.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Task_7.Controllers;

[ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(Data.Reservations);
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult GetById([FromRoute] int id)
        {
            var reservation = Data.Reservations.FirstOrDefault(r => r.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }
            return Ok(reservation);
        }

        [HttpGet("filter")]
        public IActionResult GetFiltered(
            [FromQuery] DateTime? date, 
            [FromQuery] string? status, 
            [FromQuery] int? roomId)
        {
            var query = Data.Reservations.AsQueryable();

            if (date.HasValue)
                query = query.Where(r => r.Date.Date == date.Value.Date);

            if (!string.IsNullOrEmpty(status))
                query = query.Where(r => r.Status.Equals(status, StringComparison.OrdinalIgnoreCase));

            if (roomId.HasValue)
                query = query.Where(r => r.RoomId == roomId.Value);

            return Ok(query.ToList());
        }

        [HttpPost]
        public IActionResult Create([FromBody] Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var room = Data.Rooms.FirstOrDefault(r => r.Id == reservation.RoomId);
            if (room == null)
            {
                return NotFound("Sala nie istnieje.");
            }

            if (!room.IsActive)
            {
                return BadRequest("Sala jest nieaktywna.");
            }

            bool hasCollision = Data.Reservations.Any(r => 
                r.RoomId == reservation.RoomId && 
                r.Date.Date == reservation.Date.Date &&
                r.Status != "cancelled" &&
                ((reservation.StartTime >= r.StartTime && reservation.StartTime < r.EndTime) ||
                 (reservation.EndTime > r.StartTime && reservation.EndTime <= r.EndTime) ||
                 (reservation.StartTime <= r.StartTime && reservation.EndTime >= r.EndTime)));

            if (hasCollision)
            {
                return Conflict("Termin rezerwacji koliduje z istniejącą rezerwacją.");
            }

            reservation.Id = Data.Reservations.Any() ? Data.Reservations.Max(r => r.Id) + 1 : 1;
            Data.Reservations.Add(reservation);

            return CreatedAtAction(nameof(GetById), new { id = reservation.Id }, reservation);
        }

        [Route("{id}")]
        [HttpPut]
        public IActionResult Update([FromRoute] int id, [FromBody] Reservation updatedRes)
        {
            var existingRes = Data.Reservations.FirstOrDefault(r => r.Id == id);
            if (existingRes == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            existingRes.OrganizerName = updatedRes.OrganizerName;
            existingRes.Topic = updatedRes.Topic;
            existingRes.Date = updatedRes.Date;
            existingRes.StartTime = updatedRes.StartTime;
            existingRes.EndTime = updatedRes.EndTime;
            existingRes.Status = updatedRes.Status;

            return Ok(existingRes);
        }

        [Route("{id}")]
        [HttpDelete]
        public IActionResult Delete([FromRoute] int id)
        {
            var reservation = Data.Reservations.FirstOrDefault(r => r.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            Data.Reservations.Remove(reservation);
            return NoContent();
        }
    }
