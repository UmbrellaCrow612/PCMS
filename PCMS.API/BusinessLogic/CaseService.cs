using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PCMS.API.DTOS.GET;
using PCMS.API.DTOS.POST;
using PCMS.API.Models;

namespace PCMS.API.BusinessLogic
{
    public class CaseService(ApplicationDbContext context, IMapper mapper) : ICaseService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<GETCase> CreateCaseAsync(POSTCase request, string userId)
        {
            var newCase = _mapper.Map<Case>(request);
            newCase.CreatedById = userId;

            await _context.Cases.AddAsync(newCase);
            await _context.SaveChangesAsync();

            var createdCase = await _context.Cases
                .FirstOrDefaultAsync(c => c.Id == newCase.Id)
                ?? throw new ApplicationException("Failed to retrieve the created case");

            return _mapper.Map<GETCase>(createdCase);
        }
    }

}
