using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PCMS.API.BusinessLogic.Interfaces;
using PCMS.API.BusinessLogic.Models;
using PCMS.API.Dtos.Create;
using PCMS.API.DTOS.Read;
using PCMS.API.DTOS.Update;

namespace PCMS.API.BusinessLogic.Services
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

            return _mapper.Map<CaseActionDto>(caseActionToCreate);
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

            _context.CaseActions.Remove(caseActionToDelete);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<CaseActionDto?> GetCaseActionByIdAsync(string caseActionId, string caseId)
        {
            var caseActionToGet = await _context.CaseActions
                .Where(x => x.Id == caseActionId && x.CaseId == caseId)
                .Include(x => x.Creator)
                .Include(x => x.LastModifiedBy)
                .FirstOrDefaultAsync();

            if (caseActionToGet is null)
            {
                return null;
            }

            return _mapper.Map<CaseActionDto>(caseActionToGet);
        }

        public async Task<List<CaseActionDto>?> GetCaseActionsForCaseIdAsync(string caseId)
        {
            var caseExists = await _context.Cases.AnyAsync(x => x.Id == caseId);
            if (!caseExists)
            {
                return null;
            }

            var caseActionsToGet = await _context.CaseActions
                .Where(x => x.CaseId == caseId)
                .Include(x => x.Creator)
                .Include(x => x.LastModifiedBy)
                .ToListAsync();

            return _mapper.Map<List<CaseActionDto>>(caseActionsToGet);
        }

        public async Task<CaseActionDto?> UpdateCaseActionByIdAsync(string caseActionId, string caseId, string userId, UpdateCaseActionDto request)
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

            return _mapper.Map<CaseActionDto>(caseActionToUpdate);

        }
    }
}
