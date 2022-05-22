namespace RestFit.DataAccess.Abstract
{
    public record HealthUnit
    {
        public string Id { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public double Weight { get; set; } = 0d;
        public double ArmSize { get; set; } = 0d;
        public double WaistSize { get; set; } = 0d;
        public double HipSize { get; set; } = 0d;
        public double ThightSize { get; set; } = 0d;
        public DateTime DateUtc { get; set; } = DateTime.MinValue;
    }
}
