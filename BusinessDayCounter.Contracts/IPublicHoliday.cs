namespace BusinessDayCounter.Contracts
{
    public interface IPublicHoliday
    {
        int HolidayMonth { get; }
        DateTime? GetPublicHolidayDate(int year);
    }
}
