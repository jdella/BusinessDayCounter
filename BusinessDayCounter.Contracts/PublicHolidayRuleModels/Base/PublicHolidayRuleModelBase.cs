using System.ComponentModel.DataAnnotations;

namespace BusinessDayCounter.Contracts.PublicHolidayRuleModel.Base
{
    public abstract class PublicHolidayRuleModelBase : IPublicHolidayRuleModel
    {
        public int HolidayMonth { get; set; }
        public bool DoMoveToMondayIfFallsOnWeekend { get; set; }
        public abstract PublicHolidayRuleType RuleType { get; }
    }

}
