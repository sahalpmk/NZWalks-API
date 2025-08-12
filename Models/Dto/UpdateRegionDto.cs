using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.Dto
{
    public class UpdateRegionDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Min 3 character required")]
        [MaxLength(3, ErrorMessage = "Max 3 character required")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Max 100 character allowed")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
