using BusinessDayCounter.Contracts;
using BusinessDayCounter.Contracts.PublicHolidayRuleModel.Base;
using BusinessDayCounter.Logic.Extensions;
using BusinessDayCounter.Logic.PublicHolidayRules;

namespace BusinessDayCounter.Logic
{
    /* GENERAL NOTES: 
     *  Times/Timezones/conversions generally not considered and assumed not relevant/applicable
     *  Some minor code duplication - ran out of time
     */

    public class BusinessDayCounter : IBusinessDayCounter
    {
        /// <summary>
        /// Calculates the number of **whole** weekdays in **between** two dates.
        /// The <paramref name="firstDate"/> and <paramref name="secondDate"/> themselves are not counted.
        /// </summary>
        /// <param name="firstDate"></param>
        /// <param name="secondDate"></param>
        /// <returns>
        /// Number of **whole** weekdays **between** <paramref name="firstDate"/> and <paramref name="secondDate"/>.
        /// If <paramref name="secondDate"/> is same or before <paramref name="firstDate"/>, 0 will be returned.
        /// </returns>
        public int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
        {
            firstDate = firstDate.Date.AddDays(1); // first day not counted
            secondDate = secondDate.Date;

            var totalDaysBetween = DaysBetween(firstDate, secondDate);
            var daysToFirstSunday = firstDate.DayOfWeek.DaysTil(DayOfWeek.Sunday);
            var numWeekdaysTilFirstSunday = firstDate.DayOfWeek.WeekdaysUntilSunday();

            // no full weeks to count so will be the min of weekdays til sunday, or total num days between the dates
            if (daysToFirstSunday >= totalDaysBetween)
            {
                return Math.Min(numWeekdaysTilFirstSunday, totalDaysBetween);
            }

            int numDaysInLastPartialWeek;
            // Note: the -1 in the equation is because we want to count days from the first Monday, not Sunday
            var fullWeeksToCount = Math.DivRem(totalDaysBetween - daysToFirstSunday - 1, 7, out numDaysInLastPartialWeek);

            // We've the weeks starting from Monday, so we know max last number of weekdays in the final "partial" week will be 5
            var numWeekdaysInLastPartialWeek = Math.Min(numDaysInLastPartialWeek, 5);

            return numWeekdaysTilFirstSunday + fullWeeksToCount * 5 + numWeekdaysInLastPartialWeek;
        }

        /// <summary>
        /// Calculates number of (whole) business (week) days with a range, minus any holidays provided that fall in the range.
        /// The <paramref name="firstDate"/> and <paramref name="secondDate"/> themselves are not counted.
        /// </summary>
        /// <param name="firstDate"></param>
        /// <param name="secondDate"></param>
        /// <param name="publicHolidays">List of public holiday dates</param>
        /// <returns>
        /// Number of (whole) business (week) days **between** <paramref name="firstDate"/> and <paramref name="secondDate"/>
        /// that aren't public holidays (based on provided <paramref name="publicHolidays"/>)
        /// If <paramref name="secondDate"/> is same or before <paramref name="firstDate"/>, 0 will be returned.
        /// </returns>
        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays)
        {
            // start by getting the total number of weekdays between the two dates
            var weekDaysBetweenDates = WeekdaysBetweenTwoDates(firstDate, secondDate);

            if (weekDaysBetweenDates == 0)
                return 0;

            // now find if any public holidays fall within those dates
            // make sure these public holidays are actually weekdays
            var firstDateToCount = firstDate.Date.AddDays(1);
            var numberOfPublicHolidays = publicHolidays
                .Where(x => IsPublicHolidayInRangeAndWeekday(x.Date, firstDateToCount, secondDate.Date))
                .Count();

            return weekDaysBetweenDates - numberOfPublicHolidays;
        }

        /// <summary>
        /// Calculates number of (whole) business (week) days with a range, minus any holidays provided that fall in the range.
        /// The <paramref name="firstDate"/> and <paramref name="secondDate"/> themselves are not counted.
        /// Public holidays are determined by provided list of <see cref="IPublicHolidayRuleModel"/> rules.
        /// </summary>
        /// <param name="firstDate"></param>
        /// <param name="secondDate"></param>
        /// <param name="publicHolidays">List of public holiday rules</param>
        /// <returns>
        /// Number of (whole) business (week) days **between** <paramref name="firstDate"/> and <paramref name="secondDate"/>
        /// that aren't public holidays (based on provided <paramref name="publicHolidays"/>)
        /// If <paramref name="secondDate"/> is same or before <paramref name="firstDate"/>, 0 will be returned.
        /// </returns>
        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, 
            IList<IPublicHolidayRuleModel> publicHolidays)
        {
            // start by getting the total number of weekdays between the two dates
            var weekDaysBetweenDates = WeekdaysBetweenTwoDates(firstDate, secondDate);

            // Note: this guards against second date being before first date which would cause infinite while loop below
            if (weekDaysBetweenDates == 0)
                return 0;

            var firstDateToCount = firstDate.Date.AddDays(1); // first day not counted

            var publicHolidaysInRange = new List<DateTime?>();
            var year = firstDate.Year;
            while (year <= secondDate.Year) // i.e. for each year in range
            {
                var startMonth = year == firstDate.Year ? firstDate.Month : 1;
                var endMonth = year == secondDate.Year ? secondDate.Month : 12;
                foreach (var ph in 
                    publicHolidays.Where(x => x.HolidayMonth >= startMonth && x.HolidayMonth <= endMonth))
                {
                    publicHolidaysInRange.Add(PublicHolidayRuleHandler.GetPublicHolidayDate(year, ph));
                }
                year++;
            }

            return weekDaysBetweenDates -
                publicHolidaysInRange.Count(x => IsPublicHolidayInRangeAndWeekday(x, firstDateToCount, secondDate.Date));
        }

        /// <summary>
        /// Essentially used as a predicate for checking if public holiday falls on a business (week) day within a range.
        /// Note, <paramref name="firstDate"/> is inclusive, <paramref name="secondDate"/> is exclusive.
        /// </summary>
        /// <param name="publicHolidayDate"></param>
        /// <param name="firstDate"></param>
        /// <param name="secondDate"></param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="publicHolidayDate"/> falls on a weekday between <paramref name="firstDate"/> and <paramref name="secondDate"/>
        /// </returns>
        private bool IsPublicHolidayInRangeAndWeekday(DateTime? publicHolidayDate, DateTime firstDate, DateTime secondDate)
            => publicHolidayDate is not null
                && publicHolidayDate >= firstDate
                && publicHolidayDate < secondDate
                && publicHolidayDate.Value.IsWeekday(); // safegaurd as weekends won't affect counts (just in case)


        /// <summary>
        /// Calculates num days **between** two dates, non-inclusive of dates provided.
        /// Does not return negative results
        /// </summary>
        /// <param name="firstDate"></param>
        /// <param name="secondDate"></param>
        /// <returns>
        /// Number of days **between** <paramref name="firstDate"/> and <paramref name="secondDate"/>.
        /// If <paramref name="secondDate"/> is same or before <paramref name="firstDate"/>, 0 will be returned.
        /// </returns>
        protected int DaysBetween(DateTime firstDate, DateTime secondDate)
        {
            // we only want the date, time of day/timezones not considered.
            firstDate = firstDate.Date;
            secondDate = secondDate.Date;

            // Gaurd against second date being before first date (and negative values being returned)
            if (firstDate >= secondDate)
            {
                return 0;
            }
            // cast to int is fine, we're not dealing with/care about fractionals
            // there shouldnt be as we're only dealing with the date parts
            return (int)(secondDate - firstDate).TotalDays;
        }


    }
}
