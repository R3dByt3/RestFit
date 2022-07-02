namespace RestFit.DataAccess.Abstract
{
    public record Friend
    {
        public string Id { get; set; } = string.Empty;
        public double AverageSets { get; set; } = 0;
        public double AverageRepetitions { get; set; } = 0;
        public double AverageWeight { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string FriendId { get; set; } = string.Empty;
    }
}
