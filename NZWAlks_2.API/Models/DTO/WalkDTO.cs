﻿namespace NZWAlks_2.API.Models.DTO
{
    public class WalkDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        public Guid RegionId { get; set; }
        public Guid WalkDifficultyId { get; set; }

        //Navigation Property
        public RegionDTO Region { get; set; }
        public WalkDifficultyRequestDTO WalkDifficulty { get; set; }
    }
}
