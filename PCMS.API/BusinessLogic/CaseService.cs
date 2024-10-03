using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PCMS.API.Dtos.GET;
using PCMS.API.Dtos.POST;
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

            return _mapper.Map<GETCase>(newCase);
        }

        public async Task<GETCase?> GetCaseByIdAsync(string id)
        {
            var caseToGet = await _context.Cases
                .Include(x => x.Creator)
                .Include(x => x.LastModifiedBy)
                .Include(x => x.Department)
                .FirstOrDefaultAsync(x => x.Id == id);

            return caseToGet != null ? _mapper.Map<GETCase>(caseToGet) : null;
        }

        public async Task<bool> UpdateCaseByIdAsync(string id,string userId, PATCHCase request)
        {
            var caseToUpdate = await _context.Cases.FirstOrDefaultAsync(x => x.Id == id);
            if (caseToUpdate == null)
            {
                return false;
            }

            var caseEdit = _mapper.Map<CaseEdit>(caseToUpdate);
            caseEdit.CreatedById = userId;
            caseEdit.CaseId = id;

            _mapper.Map(request, caseToUpdate);
            caseToUpdate.LastModifiedById = userId;
            caseToUpdate.LastModifiedAtUtc = DateTime.UtcNow;

            await _context.CaseEdits.AddAsync(caseEdit);
            await _context.SaveChangesAsync();

            return true;
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

        public async Task<List<GETCasePerson>> GetLinkedPersonsForCaseIdAsync(string id)
        {
            var persons = await _context.CasePersons
                .Where(x => x.CaseId == id)
                .Include(x => x.Person)
                .ToListAsync();

            return _mapper.Map<List<GETCasePerson>>(persons);
        }

        public async Task<List<GETApplicationUser>> GetLinkedUsersForCaseIdAsync(string id)
        {
            var users = await _context.ApplicationUserCases
                .Where(x => x.CaseId == id)
                .Include(x => x.User)
                .Select(x => x.User)
                .ToListAsync();

            return _mapper.Map<List<GETApplicationUser>>(users);
        }

        public async Task<List<GETCaseEdit>> GetCaseEditsForCaseIdAsync(string id)
        {
            var edits = await _context.CaseEdits
                .Where(x => x.CaseId == id)
                .Include(x => x.Creator)
                .ToListAsync();

            return _mapper.Map<List<GETCaseEdit>>(edits);
        }

        public async Task<List<GETTag>> GetLinkedTagsForCaseIdAsync(string id)
        {
            var tags = await _context.CaseTags
                .Where(x => x.CaseId == id)
                .Include(x => x.Tag)
                .Select(x => x.Tag)
                .ToListAsync();

            return _mapper.Map<List<GETTag>>(tags);
        }

        public Task<List<GETCase>> SearchForCases(POSTCaseSearch request)
        {
            throw new NotImplementedException();
        }
    }

}
