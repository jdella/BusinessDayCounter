using BusinessDayCounter.Contracts.PublicHolidayRuleModels.Base;

namespace BusinessDayCounter.Contracts.PublicHolidayRuleModels
{
    public class ExactDateHolidayRule : PublicHolidayRuleModelBase
    {
        public int HolidayDay { get; set; }
        public override PublicHolidayRuleType RuleType => PublicHolidayRuleType.ExactDate;
    }
}
