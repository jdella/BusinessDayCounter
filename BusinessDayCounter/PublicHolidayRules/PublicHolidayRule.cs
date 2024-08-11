using BusinessDayCounter.Contracts;
using BusinessDayCounter.Logic.Extensions;

namespace BusinessDayCounter.Logic.PublicHolidayRules
{
    public abstract class PublicHolidayRuleBase : IPublicHoliday
    {
        protected readonly int _holidayMonth;
        public int HolidayMonth => _holidayMonth;
        protected PublicHolidayRuleBase(int holidayMonth)
        {
            _holidayMonth = holidayMonth;
        }
        public DateTime? GetPublicHolidayDate(int year)
        {
            try
            {
                return DetermineExactHolidayDate(year);
            }
            catch
            {
                // this might be a bit overly defensive but idea is to capture and handle invalid dates somewhat gracefully
                // (or at least not let it blow up the app)
                return null; // no/unable to determine holiday date
            }
        }
        protected abstract DateTime DetermineExactHolidayDate(int year);
    }
    public class ExactDateNoExceptionsHoliday : PublicHolidayRuleBase
    {
        private readonly int _holidayDay;

        public ExactDateNoExceptionsHoliday(int month, int day)
            : base(month)
        {
            _holidayDay = day;
        }

        protected override DateTime DetermineExactHolidayDate(int year)
           => new DateTime(year, _holidayMonth, _holidayDay);
    }

    public class ExactDateOrMondayIfWeekendHoliday : PublicHolidayRuleBase
    {
        private readonly int _holidayDay;

        public ExactDateOrMondayIfWeekendHoliday(int month, int day)
            : base(month)
        {
            _holidayDay = day;
        }
        protected override DateTime DetermineExactHolidayDate(int year)
        {
            var exactDate = new DateTime(year, _holidayMonth, _holidayDay);
            if (exactDate.IsWeekday())
                return exactDate;

            // date is weekend so we need to get Monday's date
            return exactDate.AddDays(exactDate.DayOfWeek.DaysTil(DayOfWeek.Monday));
        }
    }

    public class NthDayOfWeekOfMonthHoliday : PublicHolidayRuleBase
    {
        private readonly uint _nthModifier;
        private readonly DayOfWeek _holidayDayOfWeek;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="month"></param>
        /// <param name="dayOfWeek"></param>
        /// <param name="nthOccurence"></param>
        public NthDayOfWeekOfMonthHoliday(int month, DayOfWeek dayOfWeek, uint nthOccurence)
            : base(month)
        {
            // Decided against guarding here.  Similar validations could be placed on Month/Day.  Outside scope of this version.
            // For now we will catch exceptions in the base class
            //if (nthModifier > 4)
            //    throw new ArgumentOutOfRangeException("Unless its a blue moon holiday, this is effectively not realistic =)");

            _holidayDayOfWeek = dayOfWeek;
            _nthModifier = nthOccurence;
        }

        protected override DateTime DetermineExactHolidayDate(int year)
        {
            var firstDayOfMonth = new DateTime(year, _holidayMonth, 1);
            var firstDayOfWeekOfMonth = firstDayOfMonth.DayOfWeek;

            var daysTilHoliday = firstDayOfWeekOfMonth.DaysTil(_holidayDayOfWeek)
                + (_nthModifier - 1) * 7;

            return new DateTime(year, _holidayMonth, 1 + (int)daysTilHoliday);
        }
    }


    public enum PublicHolidayRuleType
    {
        None,
        ExactSameDateNoExceptions,
        ExactDateOrNextMondayIfWeekend,

    }
}
