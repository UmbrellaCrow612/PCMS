using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PCMS.API.BusinessLogic.Interfaces;
using PCMS.API.BusinessLogic.Models;
using PCMS.API.BusinessLogic.Models.Enums;
using PCMS.API.Dtos.Create;
using PCMS.API.DTOS.Read;
using PCMS.API.DTOS.Update;

namespace PCMS.API.BusinessLogic.Services
{
    public class PersonService(ApplicationDbContext context, IMapper mapper) : IPersonService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<bool> AddPersonToCaseAsync(string personId, string caseId, CaseRole role)
        {
            var personExists = await _context.Persons.AnyAsync(x => x.Id == personId);
            if (!personExists) return false;

            var caseExists = await _context.Cases.AnyAsync(c => c.Id == caseId);
            if (!caseExists) return false;

            var linkExists = await _context.CasePersons
                .AnyAsync(x => x.PersonId == personId && x.CaseId == caseId && x.Role == role);

            if (linkExists) return false;

            var link = new CasePerson
            {
                CaseId = caseId,
                PersonId = personId,
                Role = role
            };

            await _context.CasePersons.AddAsync(link);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AddPersonToCrimeSceneAsync(string personId, string crimeSceneId, CrimeSceneRole role)
        {
            var personExists = await _context.Persons.AnyAsync(x => x.Id == personId);
            if (!personExists) return false;

            var crimeSceneExists = await _context.CrimeScenes.AnyAsync(x => x.Id == crimeSceneId);
            if (!crimeSceneExists) return false;

            var linkExists = await _context.CrimeScenePersons.AnyAsync(x => x.PersonId == personId && x.CrimeSceneId == crimeSceneId && x.Role == role);
            if (linkExists) return false;

            var link = new CrimeScenePerson
            {
                CrimeSceneId = crimeSceneId,
                PersonId = personId,
                Role = role
            };

            await _context.CrimeScenePersons.AddAsync(link);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<PersonDto> CreatePersonAsync(CreatePersonDto request)
        {
            var person = _mapper.Map<Person>(request);

            await _context.Persons.AddAsync(person);
            await _context.SaveChangesAsync();

            return _mapper.Map<PersonDto>(person);
        }

        public async Task<bool> DeleteAllPersonCaseLinksAsync(string personId, string caseId)
        {
            var personExists = await _context.Persons.AnyAsync(x => x.Id == personId);
            if (!personExists) return false;

            var caseExists = await _context.Cases.AnyAsync(c => c.Id == caseId);
            if (!caseExists) return false;

            var links = await _context.CasePersons
                .Where(x => x.PersonId == personId && x.CaseId == caseId)
                .ToListAsync();
            if (links.Count is 0) return true;

            _context.Remove(links);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAllPersonCrimeSceneLinks(string personId, string crimeSceneId)
        {
            var personExists = await _context.Persons.AnyAsync(x => x.Id == personId);
            if (!personExists) return false;

            var crimeSceneExists = await _context.CrimeScenes.AnyAsync(x => x.Id == crimeSceneId);
            if (!crimeSceneExists) return false;

            var links = await _context.CrimeScenePersons
                .Where(x => x.PersonId == personId && x.CrimeSceneId == crimeSceneId)
                .ToListAsync();
            if (links.Count is 0) return true;

            _context.Remove(links);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeletePersonByIdAsync(string personId, string userId)
        {
            var person = await _context.Persons.FirstOrDefaultAsync(x => x.Id == personId);
            if (person is null) return false;

            person.IsDeleted = true;
            person.DeletedAtUtc = DateTime.UtcNow;
            person.DeletedById = userId;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeletePersonCaseLinkAsync(string personId, string caseId, string linkId)
        {
            var personExists = await _context.Persons.AnyAsync(x => x.Id == personId);
            if (!personExists) return false;

            var caseExists = await _context.Cases.AnyAsync(x => x.Id == caseId);
            if (!caseExists) return false;

            var link = await _context.CasePersons.FirstOrDefaultAsync(x => x.PersonId == personId && x.CaseId == caseId);
            if (link is null) return false;

            _context.Remove(link);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeletePersonCrimeSceneLink(string personId, string crimeSceneId, string linkId)
        {
            var personExists = await _context.Persons.AnyAsync(x => x.Id == personId);
            if (!personExists) return false;

            var crimeSceneExists = await _context.CrimeScenes.AnyAsync(x => x.Id == crimeSceneId);
            if (!crimeSceneExists) return false;

            var link = await _context.CrimeScenePersons.FirstOrDefaultAsync(x => x.PersonId == personId && x.CrimeSceneId == crimeSceneId);
            if (link is null) return false;

            _context.Remove(link);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<PersonDto?> GetPersonByIdAsync(string personId)
        {
            var person = await _context.Persons.FirstOrDefaultAsync(x => x.Id == personId);
            if (person is null) return null;

            return _mapper.Map<PersonDto>(person);
        }

        public Task<List<PersonDto>> SearchPersonsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdatePersonByIdAsync(string personId, UpdatePersonDto request)
        {
            var person = await _context.Persons.FirstOrDefaultAsync(x => x.Id == personId);
            if (person is null) return false;

            _mapper.Map(request, person);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
