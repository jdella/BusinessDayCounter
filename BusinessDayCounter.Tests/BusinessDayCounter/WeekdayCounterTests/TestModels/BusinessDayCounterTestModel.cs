namespace BusinessDayCounter.Tests.BusinessDayCounter.WeekdayCounterTests.TestModels
{
    public class BusinessDayCounterTestModel : WeekdayCounterTestModel
    {
        public IList<DateTime> PublicHolidays { get; set; } = [];
    }
}
