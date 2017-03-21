using System;
using PhotoLife.Providers.Contracts;

namespace PhotoLife.Providers
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetCurrentDate()
        {
            return DateTime.Now;
        }
    }
}
    