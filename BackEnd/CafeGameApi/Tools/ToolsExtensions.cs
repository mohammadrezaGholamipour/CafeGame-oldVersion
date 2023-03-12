using CafeGameApi.Exceptions;
using System.ComponentModel;

namespace CafeGameApi.Tools;

public static class ToolsExtensions
{
    public static string GetDisplayName(this Type t)
    {
        // Using reflection.  
        var attrs = Attribute.GetCustomAttributes(t); // Reflection.  

        // Returning output.  
        return attrs.OfType<DisplayNameAttribute>()
                .Select(a => a.DisplayName)
                .FirstOrDefault() ?? nameof(t);
    }

    public static string GetDisplayName(this object obj)
    {
        var t = obj.GetType();

        // Using reflection.  
        var attrs = Attribute.GetCustomAttributes(t); // Reflection.  

        // Returning output.  
        return attrs.OfType<DisplayNameAttribute>()
                .Select(a => a.DisplayName)
                .FirstOrDefault() ?? nameof(t);
    }

    public static bool HasProperty(this object obj,
            string propertyName) =>
            obj.GetType().GetProperty(propertyName) != null;

    public static bool HasProperty(this Type t,
            string propertyName) =>
            t.GetProperty(propertyName) != null;

    public static void CheckNotNull<TDto>(this TDto dto)
            where TDto : class
    {
        if (dto == default) throw new EntityNotFoundException(typeof(TDto));
    }

    public static string ToTrackCode(this int id,
            DateTime createDate) =>
            (new string(createDate.Year.ToString().Skip(2).ToArray()) +
             id.ToString() +
             NormalizeDayOfYear(createDate.DayOfYear) +
             NormalizeSeconds(createDate.Second));

    public static int ToId(this string trackCode) =>
            int.TryParse(trackCode.Skip(2).SkipLast(5).ToArray(), out int id)
                    ? id
                    : throw new ValidationException(nameof(trackCode), "کد رهگیری اشتباه است");

    private static string NormalizeDayOfYear(int day)
    {
        return day switch
        {
            < 10 => "00" + day.ToString(),
            < 100 => "0" + day.ToString(),
            _ => day.ToString()
        };
    }

    private static string NormalizeSeconds(int second)
    {
        return second switch
        {
            < 10 => "0" + second.ToString(),
            _ => second.ToString()
        };
    }
}