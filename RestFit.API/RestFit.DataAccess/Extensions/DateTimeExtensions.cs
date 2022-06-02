namespace RestFit.DataAccess.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime? TruncateDateTimeUtc(this DateTime? dateTime)
        {
            if (dateTime == null)
                return null;

            return new DateTime(dateTime.Value.Year, dateTime.Value.Month, dateTime.Value.Day, 0, 0, 0, DateTimeKind.Utc);
        }
    }
}
