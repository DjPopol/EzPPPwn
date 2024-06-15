namespace DpLib.Extensions
{
    public static class DpStringExtensions
    {
        public static string ToFirstUpper(this string str)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(str.Trim()))
            {
                return str;
            }
            string ret = str.Trim().ToLower();
            return ret[..1].ToUpper() + ret[1..];
        }
        public static string ToFirstUppers(this string str, char separator)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(str.Trim()))
            {
                return str;
            }

            string ret = string.Empty;
            string[] strings = str.Trim().Split(separator);
            foreach (string s in strings)
            {
                ret += s.ToFirstUpper() + separator;
            }
            return ret.Trim();
        }
    }
}
