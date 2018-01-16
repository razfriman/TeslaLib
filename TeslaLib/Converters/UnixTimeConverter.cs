using System;

namespace TeslaLib.Converters
{
    public static class UnixTimeConverter
    {
        public static TimeSpan FromUnixTimeSpan(long unixTimeSpan) => TimeSpan.FromSeconds(unixTimeSpan);
    }
}
