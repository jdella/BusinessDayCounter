using BusinessDayCounter.Logic.Extensions;
using FluentAssertions;

namespace BusinessDayCounter.Tests.DateTimeExtenstionTests
{
    public class DayOFWeekExtensionTests
    {
        [Theory]
        [InlineData(DayOfWeek.Sunday, 0)]
        [InlineData(DayOfWeek.Monday, 6)]
        [InlineData(DayOfWeek.Tuesday, 5)]
        [InlineData(DayOfWeek.Wednesday, 4)]
        [InlineData(DayOfWeek.Thursday, 3)]
        [InlineData(DayOfWeek.Friday, 2)]
        [InlineData(DayOfWeek.Saturday, 1)]
        public void DaysTilSunday_SanityTests(DayOfWeek dayOfWeek, int expected)
        {
            var result = dayOfWeek.DaysTil(DayOfWeek.Sunday);
            result.Should().Be(expected, because: $"{expected} days between {dayOfWeek} and Sunday");
        }

        [Theory]
        [InlineData(DayOfWeek.Sunday, 0)]
        [InlineData(DayOfWeek.Monday, 5)]
        [InlineData(DayOfWeek.Tuesday, 4)]
        [InlineData(DayOfWeek.Wednesday, 3)]
        [InlineData(DayOfWeek.Thursday, 2)]
        [InlineData(DayOfWeek.Friday, 1)]
        [InlineData(DayOfWeek.Saturday, 0)]
        public void WeekDaysTilSunday_SanityTests(DayOfWeek dayOfWeek, int expected)
        {
            var result = dayOfWeek.WeekdaysUntilSunday();
            result.Should().Be(expected, because: $"{expected} weekdays between {dayOfWeek} and Sunday");
        }

        [Theory]
        [InlineData(DayOfWeek.Sunday, DayOfWeek.Monday, 1)]
        [InlineData(DayOfWeek.Saturday, DayOfWeek.Sunday, 1)]
        [InlineData(DayOfWeek.Monday, DayOfWeek.Sunday, 6)]
        [InlineData(DayOfWeek.Wednesday, DayOfWeek.Wednesday, 0)]
        [InlineData(DayOfWeek.Tuesday, DayOfWeek.Saturday, 4)]
        [InlineData(DayOfWeek.Tuesday, DayOfWeek.Sunday, 5)]
        public void DaysTil_SanityTests(DayOfWeek fromDayOfWeek, DayOfWeek toDayOfWeek, int expected)
        {
            var result = fromDayOfWeek.DaysTil(toDayOfWeek);
            result.Should().Be(expected, because: $"{expected} days between {fromDayOfWeek} and {toDayOfWeek}");
        }
    }
}
