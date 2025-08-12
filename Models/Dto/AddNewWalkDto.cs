﻿using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.Dto
{
    public class AddNewWalkDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Max 100 character allowed")]
        public string Name { get; set; }
        [Required]
        [MaxLength(1000, ErrorMessage = "Max 1000 character allowed")]
        public string Description { get; set; }
        [Required]
        [Range(0, 50)]
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }
    }
}
