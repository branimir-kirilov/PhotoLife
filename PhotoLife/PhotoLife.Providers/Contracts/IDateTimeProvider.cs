using System;

namespace PhotoLife.Providers.Contracts
{
    public interface IDateTimeProvider
    {
        DateTime GetCurrentDate();
    }
}
