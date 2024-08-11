using BusinessDayCounter.Tests.BusinessDayCounter.WeekdayCounterTests.TestModels;

namespace BusinessDayCounter.Tests.BusinessDayCounter.WeekdayCounterTests
{

    public static class WeekdayCounterTestData
    {
        public static TheoryData WeekdaysBetweenTestCases => new TheoryData<WeekdayCounterTestModel>()
        {
            new WeekdayCounterTestModel {
                FirstDate = new DateTime(2013,10,7),    // Mon
                SecondDate = new DateTime(2013,10,9),   // Wed
                Expected = 1,
            },
            new WeekdayCounterTestModel {
                FirstDate = new DateTime(2013,10,5),    // Sat
                SecondDate = new DateTime(2013,10,14),  // Week Mon
                Expected = 5
            },
            new WeekdayCounterTestModel {
                FirstDate = new DateTime(2013,10,7),
                SecondDate = new DateTime(2014,1,1),
                Expected = 61
            },
            new WeekdayCounterTestModel {
                FirstDate = new DateTime(2013,10,7),
                SecondDate = new DateTime(2013,10,5), // second date before first
                Expected = 0
            },
            // Additional Test cases
            new WeekdayCounterTestModel {
                FirstDate = new DateTime(2013,10,4),    // Fri
                SecondDate = new DateTime(2013,10,7),   // Mon
                Expected = 0
            },

        };

        public static TheoryData PublicHolidayTestCases => new TheoryData<WeekdayCounterTestModel>()
        {
            new WeekdayCounterTestModel {
                FirstDate = new DateTime(2013,10,7),    // Mon
                SecondDate = new DateTime(2013,10,9),   // Wed
                Expected = 1,
            },
            new WeekdayCounterTestModel {
                FirstDate = new DateTime(2013,12, 24),    // Sat
                SecondDate = new DateTime(2013,12,27),  // Week Mon
                Expected = 0
            },
            new WeekdayCounterTestModel {
                FirstDate = new DateTime(2013,10,7),
                SecondDate = new DateTime(2014,1,1),
                Expected = 59
            }
        };

        public static TheoryData DaysBetweenEdgeCases => new TheoryData<WeekdayCounterTestModel>()
        {
            new WeekdayCounterTestModel {
                FirstDate = new DateTime(2013,10,7, 12,35,00),
                SecondDate = new DateTime(2013,10,9),
                Expected = 2,
            },
           new WeekdayCounterTestModel {
                FirstDate = new DateTime(2013,10,7, 0,0,0, DateTimeKind.Local),
                SecondDate = new DateTime(2013,10,8, 0,0,0, DateTimeKind.Utc), // no days between
                Expected = 1,
            },
            new WeekdayCounterTestModel {
                FirstDate = new DateTime(2013,10,7),
                SecondDate = new DateTime(2013,10,5), // second date before first
                Expected = 0
            },
            new WeekdayCounterTestModel {
                FirstDate = new DateTime(2013,10,7),
                SecondDate = new DateTime(2013,10,7), // second date same as first
                Expected = 0
            },
            new WeekdayCounterTestModel {
                FirstDate = new DateTime(2013,10,7),
                SecondDate = new DateTime(2013,10,8),
                Expected = 1
            },
        };
    }
}
