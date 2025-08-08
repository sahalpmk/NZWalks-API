using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SqlRegionRepository : IRegionRepository
    {
        public readonly NZWalksDbContext dbContext;
        public SqlRegionRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.dbContext = nZWalksDbContext;
        }

        public async Task<Region> AddNewRegionAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteRegionByIdAsync(Guid id)
        {
            var region = await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
            if (region == null)
            {
                return null;
            }
            dbContext.Regions.Remove(region);
            dbContext.SaveChanges();
            return region;
        }

        public async Task<List<Region>> GetAllRegionsAsync()
        {
            return await dbContext.Regions.ToListAsync();
            
        }

        public async Task<Region>? GetRegionByIdAsync(Guid id)
        {
            var region = await dbContext.Regions.SingleOrDefaultAsync(r => r.Id == id);
            if (region == null)
            {
                return null;
            }
            return region;
        }

        public async Task<Region> UpdateRegionAsync(Guid id, Region region)
        {
            var existingRegion = await dbContext.Regions.SingleOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null) {
                return null;
            }
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;

            await dbContext.SaveChangesAsync();
            return existingRegion;

        }
    }
}
