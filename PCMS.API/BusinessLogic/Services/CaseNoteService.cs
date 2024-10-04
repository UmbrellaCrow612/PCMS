using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PCMS.API.BusinessLogic.Interfaces;
using PCMS.API.BusinessLogic.Models;
using PCMS.API.Dtos.Create;
using PCMS.API.Dtos.Read;
using PCMS.API.Dtos.Update;

namespace PCMS.API.BusinessLogic.Services
{
    public class CaseNoteService(ApplicationDbContext context, IMapper mapper) : ICaseNoteService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<CaseNoteDto?> CreateCaseNoteAsync(string caseId, string userId, CreateCaseNoteDto request)
        {
            var caseExists = await _context.Cases.AnyAsync(c => c.Id == caseId);
            if (!caseExists)
            {
                return null;
            }

            var caseNoteToAdd = _mapper.Map<CaseNote>(request);
            caseNoteToAdd.CaseId = caseId;
            caseNoteToAdd.CreatedById = userId;

            await _context.CaseNotes.AddAsync(caseNoteToAdd);
            await _context.SaveChangesAsync();

            return _mapper.Map<CaseNoteDto>(caseNoteToAdd);

        }

        public async Task<bool> DeleteCaseNoteByIdAsync(string caseNoteId, string caseId, string userId)
        {
            var caseExists = await _context.Cases.AnyAsync(c => c.Id == caseId);
            if (!caseExists)
            {
                return false;
            }

            var caseNoteToDelete = await _context.CaseNotes.FirstOrDefaultAsync(c => c.Id == caseNoteId && c.CaseId == caseId);
            if (caseNoteToDelete is null)
            {
                return false;
            }

            caseNoteToDelete.IsDeleted = true;
            caseNoteToDelete.DeletedById = userId;
            caseNoteToDelete.DeletedAtUtc = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<CaseNoteDto?> GetCaseNoteByIdAsync(string caseNoteId, string caseId)
        {
            var caseExists = await _context.Cases.AnyAsync(c => c.Id == caseId);
            if (!caseExists)
            {
                return null;
            }

            var caseNoteToGet = await _context.CaseNotes
                .Include(x => x.Creator)
                .Include(x => x.LastModifiedBy)
                .FirstOrDefaultAsync(x => x.Id == caseNoteId && x.CaseId == caseId);

            if (caseNoteToGet is null)
            {
                return null;
            }

            return _mapper.Map<CaseNoteDto>(caseNoteToGet);
        }

        public async Task<List<CaseNoteDto>?> GetCaseNotesForCaseIdAsync(string caseId)
        {
            var caseExists = await _context.Cases.AnyAsync(c => c.Id == caseId);
            if (!caseExists)
            {
                return null;
            }

            var caseNotes = await _context.CaseNotes
                .Where(x => x.CaseId == caseId)
                .Include(x => x.Creator)
                .Include(x => x.LastModifiedBy)
                .ToListAsync();

            return _mapper.Map<List<CaseNoteDto>>(caseNotes);
        }

        public async Task<CaseNoteDto?> UpdateCaseNoteByIdAsync(string caseNoteId, string caseId, string userId, UpdateCaseNoteDto request)
        {
            var caseExists = await _context.Cases.AnyAsync(c => c.Id == caseId);
            if (!caseExists)
            {
                return null;
            }

            var caseNoteToUpdate = await _context.CaseNotes.FirstOrDefaultAsync(x => x.Id == caseNoteId && x.CaseId == caseId);
            if (caseNoteToUpdate is null)
            {
                return null;
            }

            _mapper.Map(request, caseNoteToUpdate);
            caseNoteToUpdate.LastModifiedById = userId;
            caseNoteToUpdate.LastModifiedAtUtc = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return _mapper.Map<CaseNoteDto>(caseNoteToUpdate);

        }
    }
}
