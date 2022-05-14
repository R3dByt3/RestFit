﻿namespace RestFit.DataAccess.Abstract
{
    public record Unit
    {
        public string Id { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public ulong Sets { get; set; } = 0;
        public ulong Repitions { get; set; } = 0;
        public double Weight { get; set; } = 0;
        public string Comment { get; set; } = string.Empty;
        public string[] ProcessedFor { get; set; } = Array.Empty<string>();
    }
}
