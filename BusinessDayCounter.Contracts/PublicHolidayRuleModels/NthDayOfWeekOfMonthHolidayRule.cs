﻿using BusinessDayCounter.Contracts.PublicHolidayRuleModel.Base;

namespace BusinessDayCounter.Contracts.PublicHolidayRuleModel
{
    public class NthDayOfWeekOfMonthHolidayRule : PublicHolidayRuleModelBase
    {
        public DayOfWeek DayOfWeek { get; set; }
        public uint NthOccurenceModifier { get; set; }
        public override PublicHolidayRuleType RuleType => PublicHolidayRuleType.NthDayOfWeekOccurenceInMonth;
    }
}
