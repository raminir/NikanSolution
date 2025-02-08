using System;
using System.Globalization;

public static class PersianDateHelper
{
    public static string ToPersianDate(this DateTime date)
    {
        PersianCalendar pc = new PersianCalendar();
        int year = pc.GetYear(date);
        int month = pc.GetMonth(date);
        int day = pc.GetDayOfMonth(date);
        return $"{year}/{month.ToString("00")}/{day.ToString("00")}";
    }
}
