using BusinessDayCounter.Tests.BusinessDayCounter.WeekdayCounterTests.TestModels;
using FluentAssertions;

namespace BusinessDayCounter.Tests.BusinessDayCounter.WeekdayCounterTests
{
    public class WeekdayCounterTests : BusinessDayCounterTestBase
    {
        // we use theory here simply for convenience - we can easily add additional test cases if required
        // alternatively we could separate into independent tests (Facts)
        // this would have the benefit of improved clarity in terms of the test runner (if tests named appropriately)
        [Theory]
        [MemberData(nameof(WeekdayCounterTestData.WeekdaysBetweenTestCases), MemberType = typeof(WeekdayCounterTestData))]
        public void WeekdaysBetween_SanityTests(WeekdayCounterTestModel test)
        {
            var result = _sut.WeekdaysBetweenTwoDates(test.FirstDate, test.SecondDate);
            result.Should().Be(test.Expected, because: $"{test.Expected} weekdays between {test.FirstDate} and {test.SecondDate}");
        }

        [Theory]
        [MemberData(nameof(WeekdayCounterTestData.DaysBetweenEdgeCases), MemberType = typeof(WeekdayCounterTestData))]
        public void DaysBetween_SanityTests(WeekdayCounterTestModel test)
        {
            var result = _sut.InvokeDaysBetween(test.FirstDate, test.SecondDate);
            result.Should().Be(test.Expected, because: $"{test.Expected} days between {test.FirstDate} and {test.SecondDate}");
        }



    }
}