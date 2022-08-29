using System;

namespace Template.Shared.Core.Extensions
{
    public static class DecimalExtensions
    {
        public static string FormatAsHour(this decimal value)
        {
            var isNegative = false;

            if (value < 0)
            {
                isNegative = true;
                value = value * -1;
            }

            var time = TimeSpan.FromHours((double)value);

            if (time.Seconds > 29)
                time += TimeSpan.FromMinutes(1);

            var ret = (time.TotalHours >= 10 ? ((int)time.TotalHours).ToString() : "0" + ((int)time.TotalHours).ToString()) + ":" + (time.Minutes >= 10 ? time.Minutes.ToString() : "0" + time.Minutes);

            if (isNegative)
                ret = "-" + ret;

            return ret;
        }

        public static decimal FormatHourToDecimal(this string value)
        {
            if (!string.IsNullOrEmpty(value) && value.Length >= 5)
            {
                var split = value.Split(':');
                TimeSpan ts = new TimeSpan(Convert.ToInt32(split[0]), Convert.ToInt32(split[1]), 0);
                return Math.Round(Convert.ToDecimal(ts.TotalHours), 2);
            }

            return 0;
        }

        public static decimal RoundHour(this decimal value)
        {
            var hours = TimeSpan.FromHours((double)value);
            var ts = new TimeSpan(hours.Days, hours.Hours, hours.Seconds >= 30 ? hours.Minutes + 1 : hours.Minutes, 0);

            return (decimal)Math.Round(ts.TotalHours, 2);
        }
    }
}