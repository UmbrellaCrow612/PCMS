using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PCMS.API.DTOS.GET;
using PCMS.API.DTOS.PATCH;
using PCMS.API.DTOS.POST;
using PCMS.API.Models;

namespace PCMS.API.BusinessLogic
{
    public class ReportService(ApplicationDbContext context, IMapper mapper) : IReportService
    {
        private readonly IMapper _mapper = mapper;
        private readonly ApplicationDbContext _context = context;
        public async Task<GETReport?> CreateReportAsync(string caseId, string userId, POSTReport request)
        {
            var caseExists = await _context.Cases.AnyAsync(c => c.Id == caseId);
            if (!caseExists)
            {
                return null;
            }

            var reportToCreate = _mapper.Map<Report>(request);
            reportToCreate.CaseId = caseId;
            reportToCreate.CreatedById = userId;

            await _context.Reports.AddAsync(reportToCreate);
            await _context.SaveChangesAsync();

            var createdReport = await _context.Reports
                .Where(x => x.Id == reportToCreate.Id)
                .Include(x => x.Creator)
                .FirstOrDefaultAsync() ?? throw new InvalidOperationException("Failed to get the created report.");

            return _mapper.Map<GETReport>(createdReport);
        }

        public async Task<bool> DeleteReportByIdAsync(string reportId, string caseId, string userId)
        {
            var caseExists = await _context.Cases.AnyAsync(x => x.Id == caseId);
            if (!caseExists)
            {
                return false;
            }

            var report = await _context.Reports
                .Where(x => x.Id == reportId && x.CaseId == caseId)
                .FirstOrDefaultAsync();

            if (report is null)
            {
                return false;
            }

            report.IsDeleted = true;
            report.DeletedAtUtc = DateTime.UtcNow;
            report.DeletedById = userId;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<GETReport?> GetReportByIdAsync(string reportId, string caseId)
        {
            var reportToGet = await _context.Reports
                .Where(x => x.Id == reportId && x.CaseId == caseId)
                .FirstOrDefaultAsync();

            if (reportToGet is null)
            {
                return null;
            }

            return _mapper.Map<GETReport>(reportToGet);
        }

        public async Task<List<GETReport>?> GetReportsForCaseIdAsync(string caseId)
        {
            var caseExists = await _context.Cases.AnyAsync(x => x.Id == caseId);
            if (!caseExists) 
            { 
                return null; 
            }

            var reports = await _context.Reports.Where(x => x.CaseId == caseId).ToListAsync();

            return _mapper.Map<List<GETReport>>(reports);
        }

        public async Task<GETReport?> UpdateReportByIdAsync(string reportId, string caseId, string userId, PATCHReport request)
        {
            var reportToUpdate = await _context.Reports
                .Where(x => x.Id == reportId && x.CaseId == caseId)
                .FirstOrDefaultAsync();

            if (reportToUpdate is null)
            {
                return null;
            }

            _mapper.Map(request, reportToUpdate);
            reportToUpdate.LastModifiedById = userId;
            reportToUpdate.LastModifiedAtUtc = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            var updatedReport = await _context.Reports
                .Where(x => x.Id == reportId && x.CaseId == caseId)
                .FirstOrDefaultAsync() ?? throw new InvalidOperationException("Failed to get updated report.");

            return _mapper.Map<GETReport>(updatedReport);
        }
    }
}
