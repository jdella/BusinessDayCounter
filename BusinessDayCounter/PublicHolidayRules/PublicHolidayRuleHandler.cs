using BusinessDayCounter.Contracts.PublicHolidayRuleModels;
using BusinessDayCounter.Contracts.PublicHolidayRuleModels.Base;
using BusinessDayCounter.Logic.Extensions;

namespace BusinessDayCounter.Logic.PublicHolidayRules
{
    public static class PublicHolidayRuleHandler
    {
        public static DateTime? GetPublicHolidayDate(int year, IPublicHolidayRuleModel ruleModel)
        {
            try
            {
                var exactHolidayDate = ruleModel.RuleType switch
                {
                    PublicHolidayRuleType.ExactDate
                        => DetermineExactHolidayDate(year, (ExactDateHolidayRule)ruleModel),

                    PublicHolidayRuleType.NthDayOfWeekOccurenceInMonth
                        => DetermineExactHolidayDate(year, (NthDayOfWeekOfMonthHolidayRule)ruleModel),

                    _ => throw new NotImplementedException($"{ruleModel.RuleType} not implemented"),
                };

                if (exactHolidayDate.IsWeekend() && ruleModel.DoMoveToMondayIfFallsOnWeekend)
                    return exactHolidayDate.AddDays(exactHolidayDate.DayOfWeek.DaysTil(DayOfWeek.Monday));

                return exactHolidayDate;
            }
            catch
            {
                // this might be a bit overly defensive but idea is to capture and handle invalid dates somewhat gracefully
                // (or at least not let it blow up the app)
                return null; // no/unable to determine holiday date
            }
        }

        public static DateTime DetermineExactHolidayDate(int year, ExactDateHolidayRule model)
        {
            return new DateTime(year, model.HolidayMonth, model.HolidayDay);
        }

        public static DateTime DetermineExactHolidayDate(int year, NthDayOfWeekOfMonthHolidayRule model)
        {
            var firstDayOfMonth = new DateTime(year, model.HolidayMonth, 1);
            var firstDayOfWeekOfMonth = firstDayOfMonth.DayOfWeek;

            var daysTilHoliday = firstDayOfWeekOfMonth.DaysTil(model.DayOfWeek)
                + (model.NthOccurenceModifier - 1) * 7;

            return new DateTime(year, model.HolidayMonth, 1 + (int)daysTilHoliday);
        }

    }
}
