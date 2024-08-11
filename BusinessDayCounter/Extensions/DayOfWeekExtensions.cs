namespace BusinessDayCounter.Logic.Extensions
{
    public static class DayOfWeekExtensions
    {
        public static int WeekdaysUntilSunday(this DayOfWeek dayOfWeek)
        {
            //var daysTilSunday = DaysTilSunday(dayOfWeek);
            var daysTilSunday = dayOfWeek.DaysTil(DayOfWeek.Sunday);
            return daysTilSunday < 2 ? 0 : daysTilSunday - 1;
        }

        public static int DaysTil(this DayOfWeek fromDayOfWeek, DayOfWeek toDayOfWeek)
        {
            if (fromDayOfWeek == toDayOfWeek) return 0;

            var dayOfWeekEnumValueDiff = toDayOfWeek - fromDayOfWeek;
            return dayOfWeekEnumValueDiff < 0
                ? dayOfWeekEnumValueDiff + 7 // if from is before to day of week in enum, we add 7
                : dayOfWeekEnumValueDiff;
        }
    }
}
