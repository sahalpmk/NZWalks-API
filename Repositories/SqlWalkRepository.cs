using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SqlWalkRepository : IWalkRepository
    {
        public readonly NZWalksDbContext dbContext;
        public SqlWalkRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.dbContext = nZWalksDbContext;
        }
        public async Task<Walk> AddNewWalkAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteWalkByIdAsync(Guid id)
        {
            var walk = await dbContext.Walks.FirstOrDefaultAsync(r => r.Id == id);
            if (walk == null)
            {
                return null;
            }
            dbContext.Walks.Remove(walk);
            dbContext.SaveChanges();
            return walk;
        }
        
        public async Task<List<Walk>> GetAllWalksAsync(string? filterOn = null, string? filterQuery = null, 
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 100)
        {
            var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery)) 
            {
                if (filterOn.Equals("name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }

            }
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x =>  x.Name);
                }
            }
            var skipResult = (pageNumber - 1)*pageSize;

            return await walks.Skip(pageNumber).Take(pageSize).ToListAsync();
        }

        public async Task<Walk?> GetWalkByIdAsync(Guid id)
        {
            var walk = await dbContext.Walks.Include("Difficulty").Include("Region").SingleOrDefaultAsync(r => r.Id == id);
            if (walk == null)
            {
                return null;
            }
            return walk;
        }

        public async Task<Walk?> UpdateWalkAsync(Guid id, Walk walk)
        {
            var existingWalk = await dbContext.Walks.SingleOrDefaultAsync(x => x.Id == id);
            if (existingWalk == null)
            {
                return null;
            }
            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.RegionId = walk.RegionId;

            await dbContext.SaveChangesAsync();
            return existingWalk;
        }
    }
}
