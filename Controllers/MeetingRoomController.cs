using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using aspreact.Models;

namespace aspreact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingRoomsController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public MeetingRoomsController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/MeetingRooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MeetingRoomModel>>> GetMeetingRooms()
        {
            return await _context.MeetingRooms.OrderBy(x => x.Id).ToListAsync();
        }

        // GET: api/MeetingRooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MeetingRoomModel>> GetMeetingRoomModel(int id)
        {
            var meetingRoomModel = await _context.MeetingRooms.FindAsync(id);

            if (meetingRoomModel == null)
            {
                return NotFound();
            }

            return meetingRoomModel;
        }

        // PUT: api/MeetingRooms/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeetingRoomModel(int id, MeetingRoomModel meetingRoomModel)
        {
            if (id != meetingRoomModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(meetingRoomModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeetingRoomModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MeetingRooms
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MeetingRoomModel>> PostMeetingRoomModel(MeetingRoomModel meetingRoomModel)
        {
            _context.MeetingRooms.Add(meetingRoomModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMeetingRoomModel", new { id = meetingRoomModel.Id }, meetingRoomModel);
        }

        // DELETE: api/MeetingRooms/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MeetingRoomModel>> DeleteMeetingRoomModel(int id)
        {
            var meetingRoomModel = await _context.MeetingRooms.FindAsync(id);
            if (meetingRoomModel == null)
            {
                return NotFound();
            }

            _context.MeetingRooms.Remove(meetingRoomModel);
            await _context.SaveChangesAsync();

            return meetingRoomModel;
        }

        private bool MeetingRoomModelExists(int id)
        {
            return _context.MeetingRooms.Any(e => e.Id == id);
        }
    }
}
