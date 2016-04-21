using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeslaLib.Converters
{
    public static class UnixTimeConverter
    {
        public static DateTimeOffset FromUnixTime(long unixTimeStamp)
        {
            DateTimeOffset time = new DateTimeOffset(1970, 1, 1, 0, 0, 0, 0, new TimeSpan(0));
            time = time.AddSeconds(unixTimeStamp).ToLocalTime();
            return time;
        }

        public static TimeSpan FromUnixTimeSpan(long unixTimeSpan)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(unixTimeSpan);
            return timeSpan;
        }
    }
}
