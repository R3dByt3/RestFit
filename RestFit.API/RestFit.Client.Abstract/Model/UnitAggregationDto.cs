namespace RestFit.Client.Abstract.Model
{
    public record UnitAggregationDto
    {
        public double AverageSets { get; set; } = 0;
        public double AverageRepitions { get; set; } = 0;
        public double AverageWeight { get; set; } = 0;
        public string Type { get; set; } = string.Empty;

    }
}
