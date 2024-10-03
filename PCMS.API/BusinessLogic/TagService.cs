﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PCMS.API.Dtos.GET;
using PCMS.API.Dtos.PATCH;
using PCMS.API.Dtos.POST;
using PCMS.API.Models;

namespace PCMS.API.BusinessLogic
{
    public class TagService(ApplicationDbContext context, IMapper mapper) : ITagService
    {
        private readonly IMapper _mapper = mapper;
        private readonly ApplicationDbContext _context = context;

        public async Task<GETTag> CreateTag(string userId, POSTTag request)
        {
            var tagToCreate = _mapper.Map<Tag>(request);
            tagToCreate.CreatedById = userId;

            await _context.Tags.AddAsync(tagToCreate);
            await _context.SaveChangesAsync();

            return _mapper.Map<GETTag>(tagToCreate);
        }

        public async Task<bool> DeleteTagByIdAsync(string tagId)
        {
            var tagToDelete = await _context.Tags.FirstOrDefaultAsync(x => x.Id == tagId);
            if(tagToDelete is null) return false;

            var linksExist = await _context.CaseTags.AnyAsync(x => x.TagId == tagId);
            if (linksExist) return false;

            _context.Remove(tagToDelete);
            await _context.SaveChangesAsync();

            return true;
        }

        public Task<GETTag?> GetTagByIdAsync(string tagId)
        {
            throw new NotImplementedException();
        }

        public Task<List<GETTag>?> GetTagsForCaseIdAsync(string caseId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> LinkTagToCase(string tagId, string caseId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UnLinkTagFromCase(string tagId, string caseId)
        {
            throw new NotImplementedException();
        }

        public Task<GETTag?> UpdateTagByIdAsync(string tagId, string userId, PATCHTag request)
        {
            throw new NotImplementedException();
        }
    }
}
