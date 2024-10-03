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
        public async Task<CaseActionDto?> CreateCaseActionAsync(string caseId, string userId, CreateCaseActionDto request)
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

            return _mapper.Map<CaseActionDto>(createdCaseAction);
        }

        public async Task<bool> DeleteCaseActionByIdAsync(string caseActionId, string caseId)
        {
            var caseActionToDelete = await _context.CaseActions
                .Where(x => x.Id == caseActionId && x.CaseId == caseId)
                .FirstOrDefaultAsync();

            if (caseActionToDelete is null)
            {
                return false;
            }

            _context.CaseActions .Remove(caseActionToDelete);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<CaseActionDto?> GetCaseActionByIdAsync(string caseActionId, string caseId)
        {
            var caseActionToGet = await _context.CaseActions
                .Where(x => x.Id == caseActionId && x.CaseId == caseId)
                .FirstOrDefaultAsync();

            if (caseActionToGet is null)
            {
                return null;
            }

            return _mapper.Map<CaseActionDto>(caseActionToGet);
        }

        public async Task<List<CaseActionDto>> GetCaseActionsForCaseIdAsync(string caseId)
        {
            var caseActionsToGet = await _context.CaseActions
                .Where(x => x.CaseId == caseId)
                .ToListAsync();

            return _mapper.Map<List<CaseActionDto>>(caseActionsToGet);
        }

        public async Task<CaseActionDto?> UpdateCaseActionByIdAsync(string caseActionId, string caseId, string userId, PATCHCaseAction request)
        {
            var caseActionToUpdate = await _context.CaseActions
                .Where(x => x.Id == caseActionId && x.CaseId == caseId)
                .FirstOrDefaultAsync();

            if (caseActionToUpdate is null)
            {
                return null;
            }

            _mapper.Map(request, caseActionToUpdate);
            caseActionToUpdate.LastModifiedById = userId;
            caseActionToUpdate.LastModifiedAtUtc = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            var updatedCaseAction = await _context.CaseActions
                .Where(x => x.Id == caseActionId && x.CaseId == caseId)
                .Include(x => x.Creator)
                 .Include(x => x.LastModifiedBy)
                .FirstOrDefaultAsync() ?? throw new InvalidOperationException("Failed to get updated case action.");

            return _mapper.Map<CaseActionDto>(updatedCaseAction);

        }
    }
}
