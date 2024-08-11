using BusinessDayCounter.Tests.BusinessDayCounter.WeekdayCounterTests;
using BusinessDayCounter.Tests.BusinessDayCounter.WeekdayCounterTests.TestModels;
using FluentAssertions;

namespace BusinessDayCounter.Tests.BusinessDayCounter.BusinessDayCounterTests
{
    public class BusinessDayCounterTests : BusinessDayCounterTestBase
    {
        [Theory]
        [MemberData(nameof(WeekdayCounterTestData.PublicHolidayTestCases), MemberType = typeof(WeekdayCounterTestData))]
        public void DaysBetween_StaticDateOnly_PublicHoliday_Tests(WeekdayCounterTestModel test)
        {
            var result = _sut.BusinessDaysBetweenTwoDates(test.FirstDate, test.SecondDate, PublicHolidaysTestData.DateOnlyPublicHolidays);
            result.Should().Be(test.Expected, because: $"{test.Expected} business days between {test.FirstDate} and {test.SecondDate}");
        }

        [Theory]
        [MemberData(nameof(WeekdayCounterTestData.PublicHolidayTestCases), MemberType = typeof(WeekdayCounterTestData))]
        public void DaysBetween_PublicHolidayRules_Tests(WeekdayCounterTestModel test)
        {
            var result = _sut.BusinessDaysBetweenTwoDates(test.FirstDate, test.SecondDate, PublicHolidaysTestData.GetAllRules());
            result.Should().Be(test.Expected, because: $"{test.Expected} business days between {test.FirstDate} and {test.SecondDate}");
        }
    }
}