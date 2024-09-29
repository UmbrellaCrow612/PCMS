using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PCMS.API.Dtos.GET;
using PCMS.API.DTOS.GET;
using PCMS.API.DTOS.PATCH;
using PCMS.API.DTOS.POST;
using PCMS.API.Models;
using PCMS.API.Models.Enums;

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
                .FirstOrDefaultAsync(c => c.Id == newCase.Id)
                ?? throw new ApplicationException("Failed to retrieve the created case");

            return _mapper.Map<GETCase>(createdCase);
        }

        public async Task<bool> DeleteCaseByIdAsync(string id)
        {
            var caseToDelete = await _context.Cases.FindAsync(id);

            if (caseToDelete is null)
            {
                return false;
            }

            _context.Cases.Remove(caseToDelete);
            await _context.SaveChangesAsync();

            return true;
        }

        public Task<GETCase> GetCaseByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<GETCaseEdit>> GetCaseEditsByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<GETPerson>> GetCasePersonsAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<GETCase>> GetCasesBySearchAsync(CaseStatus? status, CaseComplexity? complexity, CasePriority? priority, DateTime? startDate, DateTime? endDate, string? type, string? createdById, string? departmentId, int page = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public Task<List<GETTag>> GetCaseTagsAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<GETApplicationUser>> GetCaseUsersAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCaseByIdAsync(string id, PATCHCase request)
        {
            throw new NotImplementedException();
        }
    }

}
