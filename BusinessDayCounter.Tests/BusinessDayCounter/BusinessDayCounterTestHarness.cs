using BDC = BusinessDayCounter.Logic.BusinessDayCounter;

namespace BusinessDayCounter.Tests.BusinessDayCounter
{
    /* NOTE:  
     * There would be a couple of different approaches to allowing testability of non-public methods - this is just one.
     * Some may even argue this should not be considered a "unit to be tested" and be covered by unit tests invoking the public calling methods of the encapsulating class.
     * The countert-argument would be a higher-level "unit" could still return expected results even if there's errors in logic at lower levels.
     * Taking a TDD approach, I unit tested these to confirm expected outputs prior to building the complete logic.
     * 
     * Finally, I realise this may not be a "TestHarness" in a certain sense of the word, however we are putting a "saddle" on this class in order to ride (test) it, so feels appropriate =)s
     */
    internal class BusinessDayCounterTestHarness : BDC
    {
        internal double InvokeDaysBetween(DateTime firstDate, DateTime secondDate) => DaysBetween(firstDate, secondDate);
    }
}
