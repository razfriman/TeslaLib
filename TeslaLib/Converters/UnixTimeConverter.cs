using System;

namespace TeslaLib.Converters
{
    public static class UnixTimeConverter
    {
        public static DateTime ToDateTime(long unixTimestamp) => new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixTimestamp);
    }
}
