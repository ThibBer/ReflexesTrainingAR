using System;

public class DateUtils
{
    public static DateTime TimestampToDateTime(long timestamp) => new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(timestamp);

    public static string TimestampToFormattedTime(long timestamp, string format) => TimestampToDateTime(timestamp).ToString(format);

    public static long DateTimeToTimestamp(DateTime dateTime) => ((DateTimeOffset)dateTime).ToUnixTimeSeconds();
}
