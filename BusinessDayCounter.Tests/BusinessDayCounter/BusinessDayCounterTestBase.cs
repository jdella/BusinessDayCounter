namespace BusinessDayCounter.Tests.BusinessDayCounter
{
    public abstract class BusinessDayCounterTestBase
    {
        internal readonly BusinessDayCounterTestHarness _sut;
        protected BusinessDayCounterTestBase()
        {
            _sut = new BusinessDayCounterTestHarness();
        }
    }
}
