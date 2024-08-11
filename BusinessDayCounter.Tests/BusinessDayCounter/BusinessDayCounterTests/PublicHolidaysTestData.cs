using BusinessDayCounter.Contracts.PublicHolidayRuleModels;
using BusinessDayCounter.Contracts.PublicHolidayRuleModels.Base;

namespace BusinessDayCounter.Tests.BusinessDayCounter.BusinessDayCounterTests
{
    public static class PublicHolidaysTestData
    {
        public static IList<DateTime> DateOnlyPublicHolidays = new List<DateTime>
        {
            new DateTime(2013,12,25),
            new DateTime(2013,12,26),
            new DateTime(2014,01,01)
        };

        public static IList<IPublicHolidayRuleModel> GetAllRules()
            => new List<IPublicHolidayRuleModel>()
            {
                // ANZAC Day (April 25th)
                new ExactDateHolidayRule()
                {
                    HolidayDay = 25,
                    HolidayMonth = 4,
                    DoMoveToMondayIfFallsOnWeekend = false
                },

                // NYD Holiday (1st Jan, or 1st Monday if falls on weekend)
                new ExactDateHolidayRule()
                {
                    HolidayDay = 1,
                    HolidayMonth = 1,
                    DoMoveToMondayIfFallsOnWeekend = true
                },

                // Queen's (King's?) Bday - 2nd Monday of June
                new NthDayOfWeekOfMonthHolidayRule()
                {
                    HolidayMonth = 6,
                    DayOfWeek = DayOfWeek.Monday,
                    NthOccurenceModifier = 2,
                },

                // Christmas - (25th Dec, or Monday if weekend)
                new ExactDateHolidayRule()
                {
                    HolidayDay = 25,
                    HolidayMonth = 12,
                    DoMoveToMondayIfFallsOnWeekend = true
                },

                // Boxing Day - (25th Dec, or Monday if weekend)
                // ** TODO: WOULD NEED ADDITIONAL LOGIC IF CHRISTMAS FELL ON SAT/SUN  **
                new ExactDateHolidayRule()
                {
                    HolidayDay = 26,
                    HolidayMonth = 12,
                    DoMoveToMondayIfFallsOnWeekend = true
                },
            };
    }
}
