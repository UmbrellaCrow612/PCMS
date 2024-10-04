using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PCMS.API.BusinessLogic.Interfaces;
using PCMS.API.BusinessLogic.Models;
using PCMS.API.Dtos.Create;
using PCMS.API.Dtos.Read;
using PCMS.API.DTOS.Read;
using PCMS.API.DTOS.Update;

namespace PCMS.API.BusinessLogic.Services
{
    public class CaseService(ApplicationDbContext context, IMapper mapper) : ICaseService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<CaseDto> CreateCaseAsync(CreateCaseDto request, string userId)
        {
            var newCase = _mapper.Map<Case>(request);
            newCase.CreatedById = userId;

            await _context.Cases.AddAsync(newCase);
            await _context.SaveChangesAsync();

            return _mapper.Map<CaseDto>(newCase);
        }

        public async Task<CaseDto?> GetCaseByIdAsync(string caseId)
        {
            var caseToGet = await _context.Cases
                .Include(x => x.Creator)
                .Include(x => x.LastModifiedBy)
                .Include(x => x.Department)
                .FirstOrDefaultAsync(x => x.Id == caseId);

            return caseToGet != null ? _mapper.Map<CaseDto>(caseToGet) : null;
        }

        public async Task<bool> UpdateCaseByIdAsync(string caseId, string userId, UpdateCaseDto request)
        {
            var caseToUpdate = await _context.Cases.FirstOrDefaultAsync(x => x.Id == caseId);
            if (caseToUpdate == null)
            {
                return false;
            }

            var caseEdit = _mapper.Map<CaseEdit>(caseToUpdate);
            caseEdit.CreatedById = userId;
            caseEdit.CaseId = caseId;

            _mapper.Map(request, caseToUpdate);
            caseToUpdate.LastModifiedById = userId;
            caseToUpdate.LastModifiedAtUtc = DateTime.UtcNow;

            await _context.CaseEdits.AddAsync(caseEdit);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteCaseByIdAsync(string caseId, string userId)
        {
            var caseToDelete = await _context.Cases.FirstOrDefaultAsync(x => x.Id == caseId);
            if (caseToDelete is null)
            {
                return false;
            }

            caseToDelete.IsDeleted = true;
            caseToDelete.DeletedAtUtc = DateTime.UtcNow;
            caseToDelete.DeletedById = userId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<CasePersonDto>> GetLinkedPersonsForCaseIdAsync(string caseId)
        {
            var persons = await _context.CasePersons
                .Where(x => x.CaseId == caseId)
                .Include(x => x.Person)
                .ToListAsync();

            return _mapper.Map<List<CasePersonDto>>(persons);
        }

        public async Task<List<ApplicationUserDto>> GetLinkedUsersForCaseIdAsync(string caseId)
        {
            var users = await _context.ApplicationUserCases
                .Where(x => x.CaseId == caseId)
                .Include(x => x.User)
                .Select(x => x.User)
                .ToListAsync();

            return _mapper.Map<List<ApplicationUserDto>>(users);
        }

        public async Task<List<CaseEditDto>> GetCaseEditsForCaseIdAsync(string caseId)
        {
            var edits = await _context.CaseEdits
                .Where(x => x.CaseId == caseId)
                .Include(x => x.Creator)
                .ToListAsync();

            return _mapper.Map<List<CaseEditDto>>(edits);
        }

        public async Task<List<TagDto>> GetLinkedTagsForCaseIdAsync(string caseId)
        {
            var tags = await _context.CaseTags
                .Where(x => x.CaseId == caseId)
                .Include(x => x.Tag)
                .Select(x => x.Tag)
                .ToListAsync();

            return _mapper.Map<List<TagDto>>(tags);
        }

        public Task<List<CaseDto>> SearchForCases(CreateCaseSearchDto request)
        {
            throw new NotImplementedException();
        }
    }

}
