using NZWalks.API.Models.Domain;
using NZWalks.API.Models.Dto;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllRegionsAsync();
        Task<Region?> GetRegionByIdAsync(Guid id);
        Task<Region> AddNewRegionAsync(Region region);
        Task<Region?> UpdateRegionAsync(Guid id, Region region);
        Task<Region?> DeleteRegionByIdAsync(Guid id);
    }
}
