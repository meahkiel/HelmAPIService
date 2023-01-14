using System.Text;

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

     public static string WithSingleQuote(this string value, string symbol = "'") {
            
            if(string.IsNullOrEmpty(value)) return value;

            var items = value.Split(",");
            StringBuilder builder = new StringBuilder();
            
            foreach(var item in items) {
               builder.Append("'" + item + "'");
               builder.Append(",");
            }
            var returnItem = builder.ToString();
            return returnItem.Substring(0,returnItem.Length - 1);
    }
}

public static class DateHelper {

    public static DateTime MergeDateTime(this DateTime date,string time) {

        return DateTime.Parse($"{date.ToShortDateString()} {time}");
    }
}