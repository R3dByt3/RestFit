namespace RestFit.Client.Abstract.Model
{
    public record FriendDto
    {
        public string Name { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string FriendId { get; set; } = string.Empty;
        public ICollection<UnitAggregationDto> UnitAggregationDtos { get; set; } = Array.Empty<UnitAggregationDto>();
    }
}
