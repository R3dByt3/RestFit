﻿namespace RestFit.Client.Abstract.Model
{
    public record FriendDto
    {
        public double AverageSets { get; set; } = 0;
        public double AverageRepitions { get; set; } = 0;
        public double AverageWeight { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string FriendId { get; set; } = string.Empty;
    }
}