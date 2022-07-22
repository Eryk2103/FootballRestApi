using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FootballApi.Models;
using FootballApi.Data;
using AutoMapper;
using FootballApi.Data.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace FootballApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubsController : Controller
    {
        private readonly FootballDBContext _context;
        private readonly IMapper _mapper;

        public ClubsController(FootballDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClubDto>>> GetAllClubs()
        {
            var clubs = await _context.Clubs
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<ClubDto>>(clubs));

        }

        [HttpGet("{id}", Name = "GetClubById")]
        public async Task<ActionResult<ClubDto>> GetClubById(int id)
        {
            var club = await _context.Clubs
               .FirstOrDefaultAsync(m => m.Id == id);

            if (club != null)
            {
                return Ok(_mapper.Map<ClubDto>(club));
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Create(ClubCreateDto club)
        {
            var clubModel = _mapper.Map<Club>(club);

            _context.Clubs.Add(clubModel);
            await _context.SaveChangesAsync();
            var clubDto = _mapper.Map<ClubDto>(clubModel);
            return CreatedAtAction("GetClubById", new { id = clubDto.Id }, clubDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id, ClubUpdateDto club)
        {
            var clubModel = await _context.Clubs.FirstOrDefaultAsync(c => c.Id == id);
            if (clubModel == null)
            {
                return NotFound();
            }
            if (id == clubModel.Id)
            {
                _mapper.Map(club, clubModel);

                await _context.SaveChangesAsync();

                return NoContent();
            }
            return BadRequest();
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialUpdate(int id, JsonPatchDocument<ClubUpdateDto> patchDocument)
        {
            var clubModel = await _context.Clubs.FirstOrDefaultAsync(c => c.Id == id);
            if (clubModel == null)
            {
                return NotFound();
            }
            var clubToPatch = _mapper.Map<ClubUpdateDto>(clubModel);
            patchDocument.ApplyTo(clubToPatch, ModelState);

            if (!TryValidateModel(clubToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(clubToPatch, clubModel);

            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var club = await _context.Clubs.FindAsync(id);
            if (club == null)
            {
                return NotFound();
            }
            _context.Clubs.Remove(club);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
