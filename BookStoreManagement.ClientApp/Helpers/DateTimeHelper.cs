using System;

namespace BookStoreManagement.ClientApp.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTimeOffset ConvertToSE(this DateTime utcTime)
        {
            TimeZoneInfo se = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            DateTimeOffset dateAndOffset = new(utcTime, se.GetUtcOffset(utcTime));

            return dateAndOffset;
        }
    }
}
