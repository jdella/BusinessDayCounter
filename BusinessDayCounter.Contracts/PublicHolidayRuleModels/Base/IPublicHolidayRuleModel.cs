namespace BusinessDayCounter.Contracts.PublicHolidayRuleModels.Base
{
    public interface IPublicHolidayRuleModel
    {
        public int HolidayMonth { get; }
        public bool DoMoveToMondayIfFallsOnWeekend { get; }
        public PublicHolidayRuleType RuleType { get; }
    }
}
