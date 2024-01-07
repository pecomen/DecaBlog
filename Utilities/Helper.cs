using System.Text.RegularExpressions;

namespace DecaBlogMVC.Utilities
{
    public static class Helper
    {
        public static string FormatDate(this DateTime date)
        {
            return date.ToString("ddd dd MMM yy");
        }

        public static string ShortenText(this string text, int length = 50)
        {
            var strippedText = Regex.Replace(text, "<.*?>", string.Empty);
            strippedText = Regex.Replace(strippedText, "&nbsp;", " ");
            return strippedText.Length < length ? $"{strippedText}..." : $"{strippedText[..length]}...";
        }
    }
}