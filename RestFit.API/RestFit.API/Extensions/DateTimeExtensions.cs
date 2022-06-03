namespace RestFit.API.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime TruncateDateTimeUtc(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0);
        }
    }
}
