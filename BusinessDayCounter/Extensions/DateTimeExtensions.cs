namespace BusinessDayCounter.Logic.Extensions
{
    /// <summary>
    /// Intended for general DateTime extended functionality that is unambiguous* and realistically potentially generally useful.
    /// Also helps with code readibility in calling code blocks.
    /// *not considering/factoring in time zone conversions etc
    /// </summary>
    public static class DateTimeExtensions
    {
        public static bool IsWeekday(this DateTime dateTime)
            => dateTime.IsWeekend() is false;

        public static bool IsWeekend(this DateTime dateTime)
            => dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday;
    }
}
