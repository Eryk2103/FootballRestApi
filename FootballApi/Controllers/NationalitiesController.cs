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
    public class NationalitiesController : Controller
    {
        private readonly FootballDBContext _context;
        private IMapper _mapper;

        public NationalitiesController(FootballDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NationalityDto>>> Index()
        {
            var nationalities = await _context.Nationalities
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<NationalityDto>>(nationalities));

        }

        [HttpGet("{id}", Name ="GetNationalityById")]
        public async Task<ActionResult<NationalityDto>> GetNationalityById(int id)
        {
            var nationality = await _context.Nationalities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nationality != null)
            {
                return Ok(_mapper.Map<NationalityDto>(nationality));
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Create(NationalityCreateDto nationality)
        {
            var nationalityModel = _mapper.Map<Nationality>(nationality);

            _context.Nationalities.Add(nationalityModel);
            await _context.SaveChangesAsync();
            var nationalityDto = _mapper.Map<NationalityDto>(nationalityModel);
            return CreatedAtAction("GetNationalityById", new { id = nationalityDto.Id }, nationalityDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id, NationalityUpdateDto nationality)
        {
            var nationalityModel = await _context.Nationalities.FirstOrDefaultAsync(n => n.Id == id);
            if (nationalityModel == null)
            {
                return NotFound();
            }
            if (id == nationalityModel.Id)
            {
                _mapper.Map(nationality, nationalityModel);

                await _context.SaveChangesAsync();

                return NoContent();
            }
            return BadRequest();
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialUpdate(int id, JsonPatchDocument<NationalityUpdateDto> patchDocument)
        {
            var nationalityModel = await _context.Nationalities.FirstOrDefaultAsync(n => n.Id == id);
            if (nationalityModel == null)
            {
                return NotFound();
            }
            var nationalityToPatch = _mapper.Map<NationalityUpdateDto>(nationalityModel);
            patchDocument.ApplyTo(nationalityToPatch, ModelState);

            if (!TryValidateModel(nationalityToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(nationalityToPatch, nationalityModel);

            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var nationality = await _context.Nationalities.FindAsync(id);
            if (nationality == null)
            {
                return NotFound();
            }
            _context.Nationalities.Remove(nationality);
            await _context.SaveChangesAsync();
            return NoContent();
        }


    }
}
