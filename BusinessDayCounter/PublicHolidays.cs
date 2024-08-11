using BusinessDayCounter.Contracts;
using BusinessDayCounter.Logic.PublicHolidayRules;

namespace BusinessDayCounter.Logic
{
    // Some consideration may need to be put into this approach if public holiday rules were to be provided dynamically (eg from a dataset)
    // The public holiday models may need to carry a "PublicHolidayRuleType" to help determine which rule they should be tranlsated as
    // (and possibly what logic to apply to get the exact date)
    public static class PublicHolidays
    {
        public static IList<IPublicHoliday> GetAll()
            => new List<IPublicHoliday>()
            {
                // ANZAC Day (April 25th)
                new ExactDateNoExceptionsHoliday(month: 4, day: 25),

                // NYD Holiday (1st Jan, or 1st Monday if falls on weekend)
                new ExactDateOrMondayIfWeekendHoliday(month: 1, day: 1),

                // Queen's (King's?) Bday - 2nd Monday of June
                new NthDayOfWeekOfMonthHoliday(month: 6, DayOfWeek.Monday, nthOccurence: 2),

                // Christmas - (25th Dec, or Monday if weekend)
                new ExactDateOrMondayIfWeekendHoliday(month: 12, day: 25),

                // Boxing Day - (25th Dec, or Monday if weekend)
                // ** TODO: WOULD NEED ADDITIONAL LOGIC IF CHRISTMAS FELL ON SAT/SUN/MON  **
                new ExactDateOrMondayIfWeekendHoliday(month: 12, day: 26),
            };
    }
}
