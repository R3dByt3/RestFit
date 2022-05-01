namespace RestFit.API.Models
{
    public record ErrorData
    {
        public string Message { get; set; } = string.Empty;
        public string AdditionalInfo { get; set; } = string.Empty;

        public ErrorData(string message)
        {
            Message = message;
        }
    }
}
