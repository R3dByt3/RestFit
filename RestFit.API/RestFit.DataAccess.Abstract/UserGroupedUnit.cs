namespace RestFit.DataAccess.Abstract
{
    public record UserGroupedUnit
    {
        public string Id { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public long SetsSum { get; set; } = 0;
        public long RepetitionsSum { get; set; } = 0;
        public double WeightsSum { get; set; } = 0;
        public long DocumentCount { get; set; } = 0;
        public IEnumerable<string> DocumentIds { get; set; } = Array.Empty<string>();
        public string Type { get; set; } = string.Empty;
    }
}
