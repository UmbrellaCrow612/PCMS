﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PCMS.API.BusinessLogic.Interfaces;
using PCMS.API.BusinessLogic.Models;
using PCMS.API.DTOS.Create;
using PCMS.API.DTOS.Read;
using PCMS.API.DTOS.Update;

namespace PCMS.API.BusinessLogic.Services
{
    public class EvidenceService(ApplicationDbContext context, IMapper mapper) : IEvidenceService
    {
        private readonly IMapper _mapper = mapper;
        private readonly ApplicationDbContext _context = context;

        public async Task<EvidenceDto?> CreateEvidenceAsync(string caseId, string userId, CreateEvidenceDto request)
        {
            var caseExists = await _context.Cases.AnyAsync(x => x.Id == caseId);
            if (!caseExists)
            {
                return null;
            }

            var _evidence = _mapper.Map<Evidence>(request);
            _evidence.CreatedById = userId;
            _evidence.CaseId = caseId;

            await _context.Evidences.AddAsync(_evidence);
            await _context.SaveChangesAsync();

            return _mapper.Map<EvidenceDto>(_evidence);
        }

        public async Task<bool> DeleteEvidenceByIdAsync(string evidenceId, string caseId, string userId)
        {
            var caseExists = await _context.Evidences.AnyAsync(x => x.Id == caseId);
            if (!caseExists)
            {
                return false;
            }

            var _evidence = await _context.Evidences
                .Where(x => x.Id == evidenceId && x.CaseId == caseId)
                .FirstOrDefaultAsync();

            if (_evidence is null)
            {
                return false;
            }

            _evidence.IsDeleted = true;
            _evidence.DeletedAtUtc = DateTime.UtcNow;
            _evidence.DeletedById = userId;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<EvidenceDto?> GetEvidenceByIdAsync(string evidenceId, string caseId)
        {
            var caseExists = await _context.Cases.AnyAsync(x => x.Id == caseId);
            if (!caseExists)
            {
                return null;
            }

            var _evidence = await _context.Evidences
                .Where(x => x.Id == evidenceId && x.CaseId == caseId)
                .Include(x => x.Creator)
                .Include(x => x.LastModifiedBy)
                .FirstOrDefaultAsync();

            if (_evidence is null)
            {
                return null;
            }

            return _mapper.Map<EvidenceDto>(_evidence);
        }

        public async Task<List<EvidenceDto>?> GetEvidenceForCaseIdAsync(string caseId)
        {
            var caseExists = await _context.Cases.AnyAsync(x => x.Id == caseId);
            if (!caseExists)
            {
                return null;
            }

            var evidences = await _context.Evidences
                .Where(x => x.CaseId == caseId)
                .Include(x => x.Creator)
                .Include(x => x.LastModifiedBy)
                .ToListAsync();

            return _mapper.Map<List<EvidenceDto>>(evidences);
        }

        public async Task<EvidenceDto?> UpdatetEvidenceByIdAsync(string evidenceId, string caseId, string userId, UpdateEvidenceDto request)
        {
            var caseExists = await _context.Cases.AnyAsync(x => x.Id == caseId);
            if (!caseExists)
            {
                return null;
            }

            var _evidence = await _context.Evidences
                .Where(x => x.Id == evidenceId && x.CaseId == caseId)
                .FirstOrDefaultAsync();

            if (_evidence is null)
            {
                return null;
            }

            _mapper.Map(request, _evidence);
            _evidence.LastModifiedById = userId;
            _evidence.LastModifiedAtUtc = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return _mapper.Map<EvidenceDto>(_evidence);
        }
    }
}
