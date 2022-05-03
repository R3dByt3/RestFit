namespace RestFit.Client.Abstract.Model
{
    public record UnitDto
    {
        public string Id = string.Empty;
        public string UserId = string.Empty;
        public string Type = string.Empty;
        public ulong Repitions = 0;
        public TimeSpan Duration = TimeSpan.Zero;
    }
}
