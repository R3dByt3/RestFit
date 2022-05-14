namespace RestFit.DataAccess.Abstract
{
    public record UnitGroup
    {
        public string UserId { get; set; } = string.Empty;
        public long SetsCount { get; set; } = 0;
        public long SetsSum { get; set; } = 0;
        public long RepitionsCount { get; set; } = 0;
        public long RepitionsSum { get; set; } = 0;
        public long WeightCount { get; set; } = 0;
        public long WeightSum { get; set; } = 0;
    }
}
