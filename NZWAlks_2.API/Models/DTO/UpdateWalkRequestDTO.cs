namespace NZWAlks_2.API.Models.DTO
{
    public class UpdateWalkRequestDTO
    {
        public string Name { get; set; }
        public double Length { get; set; }
        public Guid RegionId { get; set; }
        public Guid WalkDifficultyId { get; set; }
    }
}
