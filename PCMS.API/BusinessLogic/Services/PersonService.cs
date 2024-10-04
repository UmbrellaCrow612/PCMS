using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PCMS.API.BusinessLogic.Interfaces;
using PCMS.API.BusinessLogic.Models;
using PCMS.API.BusinessLogic.Models.Enums;
using PCMS.API.Dtos.Create;
using PCMS.API.DTOS.Read;
using PCMS.API.DTOS.Update;
using SQLitePCL;

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

            await _context.CrimeScenePersons.AddAsync (link);
            await _context.SaveChangesAsync();

            return true;
        }

        public Task<PersonDto> CreatePersonAsync(CreatePersonDto request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllPersonCaseLinksAsync(string personId, string caseId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAllPersonCrimeSceneLinks(string personId, string crimeSceneId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePersonByIdAsync(string personId, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePersonCaseLinkAsync(string personId, string caseId, string linkId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePersonCrimeSceneLink(string personId, string crimeSceneId, string linkId)
        {
            throw new NotImplementedException();
        }

        public Task<PersonDto?> GetPersonByIdAsync(string personId)
        {
            throw new NotImplementedException();
        }

        public Task<List<PersonDto>> SearchPersonsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdatePersonByIdAsync(UpdatePersonDto request)
        {
            throw new NotImplementedException();
        }
    }
}
