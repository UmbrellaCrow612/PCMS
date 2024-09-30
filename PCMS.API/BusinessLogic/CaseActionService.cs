using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PCMS.API.DTOS.GET;
using PCMS.API.DTOS.PATCH;
using PCMS.API.DTOS.POST;
using PCMS.API.Models;

namespace PCMS.API.BusinessLogic
{
    public class CaseActionService(ApplicationDbContext context, IMapper mapper) : ICaseActionService
    {
        private readonly IMapper _mapper = mapper;
        private readonly ApplicationDbContext _context = context;
        public async Task<GETCaseAction?> CreateCaseActionAsync(string caseId, string userId, POSTCaseAction request)
        {
            var caseExists = await _context.Cases.AnyAsync(c => c.Id == caseId);
            if (!caseExists)
            {
                return null;
            }

            var caseActionToCreate = _mapper.Map<CaseAction>(request);
            caseActionToCreate.CaseId = caseId;
            caseActionToCreate.CreatedById = userId;

            await _context.CaseActions.AddAsync(caseActionToCreate);
            await _context.SaveChangesAsync();

            var createdCaseAction = await _context.CaseActions
                .Where(x => x.Id == caseActionToCreate.Id)
                .Include(x => x.Creator)
                .FirstOrDefaultAsync() ?? throw new InvalidOperationException("Failed to retrieve the created case action.");

            return _mapper.Map<GETCaseAction>(createdCaseAction);
        }

        public Task<bool> DeleteCaseActionByIdAsync(string caseActionId)
        {
            throw new NotImplementedException();
        }

        public Task<GETCaseAction?> GetCaseActionByIdAsync(string caseActionId)
        {
            throw new NotImplementedException();
        }

        public Task<List<GETCaseAction>> GetCaseActionsForCaseIdAsync(string caseId)
        {
            throw new NotImplementedException();
        }

        public Task<GETCaseAction?> UpdateCaseActionByIdAsync(string caseActionId, string userId, PATCHCaseAction request)
        {
            throw new NotImplementedException();
        }
    }
}
