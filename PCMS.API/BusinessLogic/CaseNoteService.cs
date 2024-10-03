using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PCMS.API.Dtos.GET;
using PCMS.API.Dtos.PATCH;
using PCMS.API.Dtos.POST;
using PCMS.API.Models;

namespace PCMS.API.BusinessLogic
{
    public class CaseNoteService(ApplicationDbContext context, IMapper mapper) : ICaseNoteService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<CaseNoteDto?> CreateCaseNoteAsync(string caseId, string userId, POSTCaseNote request)
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

            var createdCaseNote = await _context.CaseNotes
                .Include(x => x.Creator)
                .FirstOrDefaultAsync(x => x.Id == caseNoteToAdd.Id) ?? throw new InvalidOperationException("Failed to get created case note.");

            return _mapper.Map<CaseNoteDto>(createdCaseNote);

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

        public async Task<CaseNoteDto?> UpdateCaseNoteByIdAsync(string caseNoteId, string caseId, string userId, PATCHCaseNote request)
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

            var updatedcaseNote = await _context.CaseNotes
                .Include(x => x.Creator)
                .Include(x => x.LastModifiedBy)
                .FirstOrDefaultAsync(x => x.Id == caseNoteId) 
                ?? throw new InvalidOperationException("Failed to get updated case note.");

            return _mapper.Map<CaseNoteDto>(updatedcaseNote);

        }
    }
}
