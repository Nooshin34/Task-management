using System.Globalization;

namespace TaskManagement.Web.Helpers;

public static class DateTimeExtensions
{
    private static readonly char[] PersianDigits = ['۰', '۱', '۲', '۳', '۴', '۵', '۶', '۷', '۸', '۹'];

    public static string ToPersianDateTime(this DateTime date)
    {
        var calendar = new PersianCalendar();
        var text = $"{calendar.GetYear(date):0000}/{calendar.GetMonth(date):00}/{calendar.GetDayOfMonth(date):00} {date:HH:mm}";
        return ToPersianDigits(text);
    }

    private static string ToPersianDigits(string value)
    {
        var chars = value.ToCharArray();
        for (var i = 0; i < chars.Length; i++)
        {
            if (chars[i] is >= '0' and <= '9')
            {
                chars[i] = PersianDigits[chars[i] - '0'];
            }
        }

        return new string(chars);
    }
}
