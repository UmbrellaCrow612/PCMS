﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PCMS.API.BusinessLogic.Interfaces;
using PCMS.API.BusinessLogic.Models;
using PCMS.API.Dtos.Create;
using PCMS.API.DTOS.Read;
using PCMS.API.DTOS.Update;

namespace PCMS.API.BusinessLogic.Services
{
    public class ReportService(ApplicationDbContext context, IMapper mapper) : IReportService
    {
        private readonly IMapper _mapper = mapper;
        private readonly ApplicationDbContext _context = context;
        public async Task<ReportDto?> CreateReportAsync(string caseId, string userId, CreateReportDto request)
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

            return _mapper.Map<ReportDto>(reportToCreate);
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

        public async Task<ReportDto?> GetReportByIdAsync(string reportId, string caseId)
        {
            var reportToGet = await _context.Reports
                .Where(x => x.Id == reportId && x.CaseId == caseId)
                .Include(x => x.Creator)
                .Include(x => x.LastModifiedBy)
                .FirstOrDefaultAsync();

            if (reportToGet is null)
            {
                return null;
            }

            return _mapper.Map<ReportDto>(reportToGet);
        }

        public async Task<List<ReportDto>?> GetReportsForCaseIdAsync(string caseId)
        {
            var caseExists = await _context.Cases.AnyAsync(x => x.Id == caseId);
            if (!caseExists)
            {
                return null;
            }

            var reports = await _context.Reports
                .Where(x => x.CaseId == caseId)
                .Include(x => x.Creator)
                .Include(x => x.LastModifiedBy)
                .ToListAsync();

            return _mapper.Map<List<ReportDto>>(reports);
        }

        public async Task<ReportDto?> UpdateReportByIdAsync(string reportId, string caseId, string userId, UpdateReportDto request)
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

            return _mapper.Map<ReportDto>(reportToUpdate);
        }
    }
}
