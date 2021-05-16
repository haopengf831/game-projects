using System;

public static partial class DateTimeExtension
{
    /// <summary>
    /// Ticks转DateTimeOffset
    /// </summary>
    /// <param name="ticks"></param>
    /// <returns></returns>
    public static DateTimeOffset ToDateTimeOffset(this long ticks)
    {
        return new DateTimeOffset(ticks,new TimeSpan());
    }
}