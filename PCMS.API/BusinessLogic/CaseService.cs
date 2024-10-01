using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PCMS.API.Dtos.GET;
using PCMS.API.DTOS.GET;
using PCMS.API.DTOS.PATCH;
using PCMS.API.DTOS.POST;
using PCMS.API.Models;

namespace PCMS.API.BusinessLogic
{
    public class CaseService(ApplicationDbContext context, IMapper mapper) : ICaseService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<GETCase> CreateCaseAsync(POSTCase request, string userId)
        {
            var newCase = _mapper.Map<Case>(request);
            newCase.CreatedById = userId;

            await _context.Cases.AddAsync(newCase);
            await _context.SaveChangesAsync();

            var createdCase = await _context.Cases
                .Include(x => x.Creator)
                .FirstOrDefaultAsync(c => c.Id == newCase.Id)
                ?? throw new InvalidOperationException("Failed to retrieve the created case");

            return _mapper.Map<GETCase>(createdCase);
        }

        public async Task<GETCase?> GetCaseByIdAsync(string id)
        {
            var caseToGet = await _context.Cases
                .Include(x => x.Creator)
                .Include(x => x.LastEditor)
                .Include(x => x.Department)
                .FirstOrDefaultAsync(x => x.Id == id);

            return caseToGet != null ? _mapper.Map<GETCase>(caseToGet) : null;
        }

        public async Task<GETCase?> UpdateCaseByIdAsync(string id,string userId, PATCHCase request)
        {
            var caseToUpdate = await _context.Cases.FindAsync(id);
            if (caseToUpdate == null)
            {
                return null;
            }

            var caseEdit = _mapper.Map<CaseEdit>(caseToUpdate);
            caseEdit.UserId = userId;
            caseEdit.CaseId = id;

            _mapper.Map(request, caseToUpdate);
            caseToUpdate.LastEditedById = userId;
            caseToUpdate.LastModifiedDate = DateTime.UtcNow;

            await _context.CaseEdits.AddAsync(caseEdit);
            await _context.SaveChangesAsync();

            return _mapper.Map<GETCase>(caseToUpdate);
        }

        public async Task<bool> DeleteCaseByIdAsync(string id, string userId)
        {
            var caseToDelete = await _context.Cases.FirstOrDefaultAsync(x => x.Id == id);
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

        public async Task<List<GETCasePerson>> GetCasePersonsAsync(string id)
        {
            var persons = await _context.CasePersons
                .Where(x => x.CaseId == id)
                .Include(x => x.Person)
                .ToListAsync();

            return _mapper.Map<List<GETCasePerson>>(persons);
        }

        public async Task<List<GETApplicationUser>> GetCaseUsersAsync(string id)
        {
            var users = await _context.ApplicationUserCases
                .Where(x => x.CaseId == id)
                .Include(x => x.User)
                .Select(x => x.User)
                .ToListAsync();

            return _mapper.Map<List<GETApplicationUser>>(users);
        }

        public async Task<List<GETCaseEdit>> GetCaseEditsByIdAsync(string id)
        {
            var edits = await _context.CaseEdits
                .Where(x => x.CaseId == id)
                .Include(x => x.User)
                .ToListAsync();

            return _mapper.Map<List<GETCaseEdit>>(edits);
        }

        public async Task<List<GETTag>> GetCaseTagsAsync(string id)
        {
            var tags = await _context.CaseTags
                .Where(x => x.CaseId == id)
                .Include(x => x.Tag)
                .Select(x => x.Tag)
                .ToListAsync();

            return _mapper.Map<List<GETTag>>(tags);
        }
    }

}
