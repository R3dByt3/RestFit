namespace RestFit.Client.Abstract.Model
{
    public record ErrorDataDto
    {
        public string Message { get; set; } = string.Empty;
        public string AdditionalInfo { get; set; } = string.Empty;

        public ErrorDataDto(string message)
        {
            Message = message;
        }
    }
}
