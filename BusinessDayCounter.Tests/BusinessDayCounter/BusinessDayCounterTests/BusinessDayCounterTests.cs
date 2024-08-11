using BusinessDayCounter.Logic;
using BusinessDayCounter.Tests.BusinessDayCounter.WeekdayCounterTests;
using BusinessDayCounter.Tests.BusinessDayCounter.WeekdayCounterTests.TestModels;
using FluentAssertions;

namespace BusinessDayCounter.Tests.BusinessDayCounter.BusinessDayCounterTests
{
    public class BusinessDayCounterTests : BusinessDayCounterTestBase
    {
        private static IList<DateTime> TestPublicHolidays = new List<DateTime>
        {
            new DateTime(2013,12,25),
            new DateTime(2013,12,26),
            new DateTime(2014,01,01)
        };

        [Theory]
        [MemberData(nameof(WeekdayCounterTestData.PublicHolidayTestCases), MemberType = typeof(WeekdayCounterTestData))]
        public void DaysBetween_PublicHoliday_Tests(WeekdayCounterTestModel test)
        {
            var result = _sut.BusinessDaysBetweenTwoDates(test.FirstDate, test.SecondDate, TestPublicHolidays);
            result.Should().Be(test.Expected, because: $"{test.Expected} business days between {test.FirstDate} and {test.SecondDate}");
        }

        [Theory]
        [MemberData(nameof(WeekdayCounterTestData.PublicHolidayTestCases), MemberType = typeof(WeekdayCounterTestData))]
        public void DaysBetween_PublicHolidayRules_Tests(WeekdayCounterTestModel test)
        {
            var result = _sut.BusinessDaysBetweenTwoDates(test.FirstDate, test.SecondDate, PublicHolidays.GetAll());
            result.Should().Be(test.Expected, because: $"{test.Expected} business days between {test.FirstDate} and {test.SecondDate}");
        }


    }
}