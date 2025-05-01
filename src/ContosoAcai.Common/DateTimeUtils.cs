namespace ContosoAcai.Common;

public static class DateTimeUtils
{
    public static DateTime StartOfMonth(this DateTime date)
    {
        return new DateTime(date.Year, date.Month, 1, 0, 0, 0);
    }
    public static DateTime EndOfMonth(this DateTime date)
    {
        return date.StartOfMonth().AddMonths(1).AddSeconds(-1);
    }

    public static DateTime ToUserLocalTime(this DateTime utcDateTime, int offsetHours)
    {
        return utcDateTime.Add(TimeSpan.FromHours(offsetHours));
    }
        
    public static TimeOnly ToUserLocalTime(this TimeOnly utcTime, int offsetHours)
    {
        return utcTime.Add(TimeSpan.FromHours(offsetHours));
    }
        
    public static string Format(this DateTime dateTime, string format = DateTimeConstants.Date.DefaultFormatting)
    {
        return dateTime.ToString(format);
    }
        
    public static string Format(this DateTime? dateTime, string format = DateTimeConstants.Date.DefaultFormatting)
    {
        if (!dateTime.HasValue)
        {
            return string.Empty;
        }

        return dateTime.Value.ToString(format);
    }

    public static string Format(this TimeOnly time, string format = DateTimeConstants.Time.DefaultFormatting)
    {
        return time.ToString(format);
    }
}