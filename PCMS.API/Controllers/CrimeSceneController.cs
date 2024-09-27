﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCMS.API.Dtos.GET;
using PCMS.API.Dtos.POST;
using PCMS.API.Models;
using Microsoft.AspNetCore.Http;
using PCMS.API.DTOS.GET;

namespace PCMS.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("locations/{id}/crime-scenes")]
    public class CrimeSceneController(ApplicationDbContext context, IMapper mapper) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> CreateCrimeScene(string id, [FromBody] POSTCrimeScene request)
        {
            var locationExists = await _context.Locations.AnyAsync(x => x.Id == id);
            if (!locationExists)
            {
                return NotFound("Location not found.");
            }

            var crimeScene = _mapper.Map<CrimeScene>(request);
            crimeScene.LocationId = id;

            await _context.CrimeScenes.AddAsync(crimeScene);
            await _context.SaveChangesAsync();

            var _crimeScene = await _context.CrimeScenes
                .Where(x => x.Id == crimeScene.Id)
                .Include(x => x.Location)
                .FirstOrDefaultAsync() ?? throw new ApplicationException("Failed to get created crime scene");

            var returnCrimeScene = _mapper.Map<GETCrimeScene>(_crimeScene);

            return Created(nameof(CreateCrimeScene), returnCrimeScene);
        }


        [HttpGet("/crime-scenes/{id}/persons")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetCrimePersons(string id)
        {
            var crimeSceneExists = await _context.CrimeScenes.AnyAsync(x => x.Id == id);
            if (!crimeSceneExists)
            {
                return NotFound("Crime scene not found.");
            }

            var persons = await _context.CrimeScenePersons
                .Where(x => x.CrimeSceneId == id)
                .Include(x => x.Person)
                 .Select(x => new GETCrimeScenePerson
                 {
                     Id = x.Id,
                     Person = _mapper.Map<GETPerson>(x.Person),
                     Role = x.Role
                 })
                .ToListAsync();

            return Ok(persons);
        }

    }
}
