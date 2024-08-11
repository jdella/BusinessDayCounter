using BusinessDayCounter.Contracts.PublicHolidayRuleModels.Base;

namespace BusinessDayCounter.Contracts.PublicHolidayRuleModels
{
    public class NthDayOfWeekOfMonthHolidayRule : PublicHolidayRuleModelBase
    {
        public DayOfWeek DayOfWeek { get; set; }
        public uint NthOccurenceModifier { get; set; }
        public override PublicHolidayRuleType RuleType => PublicHolidayRuleType.NthDayOfWeekOccurenceInMonth;
    }
}
