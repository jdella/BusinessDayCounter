using BusinessDayCounter.Contracts.PublicHolidayRuleModel.Base;

namespace BusinessDayCounter.Contracts.PublicHolidayRuleModel
{
    public class ExactDateHolidayRule : PublicHolidayRuleModelBase
    {
        public int HolidayDay { get; set; }
        public override PublicHolidayRuleType RuleType => PublicHolidayRuleType.ExactDate;
    }
}
