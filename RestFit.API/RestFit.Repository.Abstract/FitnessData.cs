namespace RestFit.Repository.Abstract
{
    public record FitnessData
    {
        public string Id { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string BodyPart { get; set; } = string.Empty;
        public double Value { get; set; } = 0;
        public string UnitType { get; set; } = string.Empty;
    }
}
