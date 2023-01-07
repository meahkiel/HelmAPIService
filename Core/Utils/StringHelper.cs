namespace Core.Utils;

public static class StringHelper
{
    public static DateTime? ConvertToDateTime(this string value,bool IsNullable =false) {
        

        if(string.IsNullOrEmpty(value))
            return null;

        DateTime result;

        if(!DateTime.TryParse(value,out result)) {
            return null;
        }

        return result;

    }

     public static DateTime MergeAndConvert(this string date,string time) {

        return DateTime.Parse($"{ConvertToDateTime(date)!.Value.ToShortDateString()} {time}");
    }
}

public static class DateHelper {

    public static DateTime MergeDateTime(this DateTime date,string time) {

        return DateTime.Parse($"{date.ToShortDateString()} {time}");
    }
}