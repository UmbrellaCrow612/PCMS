using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PCMS.API.BusinessLogic.Interfaces;
using PCMS.API.BusinessLogic.Models;
using PCMS.API.Dtos.Create;
using PCMS.API.Dtos.Read;
using PCMS.API.Dtos.Update;

namespace PCMS.API.BusinessLogic.Services
{
    public class TagService(ApplicationDbContext context, IMapper mapper) : ITagService
    {
        private readonly IMapper _mapper = mapper;
        private readonly ApplicationDbContext _context = context;

        public async Task<TagDto> CreateTagAsync(string userId, CreateTagDto request)
        {
            var tagToCreate = _mapper.Map<Tag>(request);
            tagToCreate.CreatedById = userId;

            await _context.Tags.AddAsync(tagToCreate);
            await _context.SaveChangesAsync();

            return _mapper.Map<TagDto>(tagToCreate);
        }

        public async Task<bool> DeleteTagByIdAsync(string tagId)
        {
            var tagToDelete = await _context.Tags.FirstOrDefaultAsync(x => x.Id == tagId);
            if (tagToDelete is null) return false;

            var linksExist = await _context.CaseTags.AnyAsync(x => x.TagId == tagId);
            if (linksExist) return false;

            _context.Remove(tagToDelete);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<TagDto?> GetTagByIdAsync(string tagId)
        {
            var tag = await _context.Tags.Include(x => x.Creator).Include(x => x.LastModifiedBy).FirstOrDefaultAsync(x => x.Id == tagId);
            if(tag is null) return null;

            return _mapper.Map<TagDto>(tag);
        }

        public async Task<List<TagDto>?> GetTagsForCaseIdAsync(string caseId)
        {
            var caseExists = await _context.Cases.FirstOrDefaultAsync(x => x.Id == caseId);
            if (caseExists is null) return null;

            var tags = await _context.CaseTags.Where(x => x.CaseId == caseId).Include(x => x.Tag).Select(x => x.Tag).ToListAsync();

            return _mapper.Map <List<TagDto>>(tags);
        }

        public async Task<bool> LinkTagToCase(string tagId, string caseId)
        {
            var caseExists = await _context.Cases.AnyAsync(x => x.Id == caseId);
            if (!caseExists) return false;

            var tagExists = await _context.Tags.AnyAsync(y => y.Id == tagId);
            if (!tagExists) return false;

            var linkExists = await _context.CaseTags.AnyAsync(x => x.TagId == tagId && x.CaseId == caseId);
            if (linkExists) return false;

            var link = new CaseTag
            {
                CaseId = caseId,
                TagId = tagId,
            };

            await _context.CaseTags.AddAsync(link);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UnLinkTagFromCase(string tagId, string caseId)
        {
            var caseExists = await _context.Cases.AnyAsync(x => x.Id == caseId);
            if (!caseExists) return false;

            var tagExists = await _context.Tags.AnyAsync(y => y.Id == tagId);
            if (!tagExists) return false;

            var link = await _context.CaseTags.FirstOrDefaultAsync(x => x.TagId == tagId && x.CaseId == caseId);
            if (link is null) return false;

            _context.CaseTags.Remove(link);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateTagByIdAsync(string tagId, string userId, UpdateTagDto request)
        {
            var tag = await _context.Tags.FirstOrDefaultAsync(x => x.Id == tagId);
            if (tag is null) return false;

            _mapper.Map(request, tag);
            tag.LastModifiedById = userId;
            tag.LastModifiedAtUtc = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
