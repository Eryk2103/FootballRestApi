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
    public class PlayersController : Controller
    {
        private readonly FootballDBContext _context;
        private readonly IMapper _mapper;

        public PlayersController(FootballDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerDto>>> GetAllPlayers()
        {
            
            var players = await _context.Players
                .Include(p => p.Club)
                .Include(p => p.Nationality)
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<PlayerDto>>(players));
            
        }

        [HttpGet("{id}", Name="GetPlayersById")]
        public async Task<ActionResult<PlayerDto>> GetPlayerById(int id)
        {
            var player = await _context.Players
                .Include(p => p.Club)
                .Include(p => p.Nationality)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (player != null)
            {
                return Ok(_mapper.Map<PlayerDto>(player));
            }
            return NotFound();

        }

        [HttpPost]
        public async Task<ActionResult> Create(PlayerCreateDto player)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var playerModel = _mapper.Map<Player>(player);

            var club = _context.Clubs.FirstOrDefault(c => c.Id == player.ClubId);
            var nationality = _context.Nationalities.FirstOrDefault(c => c.Id == player.NationalityId);

            playerModel.Club = club;
            playerModel.Nationality = nationality;

            _context.Players.Add(playerModel);
            await _context.SaveChangesAsync();
            var playerDto = _mapper.Map<PlayerDto>(playerModel);

            return CreatedAtAction("GetPlayerById", new { id = playerDto.Id }, playerDto);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id, PlayerUpdateDto player)
        {
            var playerModel = await _context.Players.FirstOrDefaultAsync(p=>p.Id == id);
            if(playerModel == null)
            {
                return NotFound();
            }
            if(id == playerModel.Id)
            {
                _mapper.Map(player, playerModel);

                await _context.SaveChangesAsync();

                return NoContent();
            }
            return BadRequest();
            
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialUpdate(int id, JsonPatchDocument<PlayerUpdateDto> patchDocument)
        {
            var playerModel = await _context.Players.FirstOrDefaultAsync(p => p.Id == id);
            if (playerModel == null)
            {
                return NotFound();
            }
            var playerToPatch = _mapper.Map<PlayerUpdateDto>(playerModel);
            patchDocument.ApplyTo(playerToPatch, ModelState);

            if(!TryValidateModel(playerToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(playerToPatch, playerModel);

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if(player == null)
            {
                return NotFound();
            }
            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
