using System.ComponentModel;
using System.Reflection;

namespace EzPPPwn.Extensions
{
    public static class DP_EnumExtensions
    {
        public static string Description<TEnum>(this TEnum EnumValue) where TEnum : struct
        {
            string? result = EnumValue.ToString();
            if (result != null)
            {
                DescriptionAttribute? attribute = typeof(TEnum).GetRuntimeField(result)?.GetCustomAttributes<DescriptionAttribute>(false)
                .SingleOrDefault();
                if (attribute != null)
                {
                    return attribute.Description;
                }
            }
            if (result != null)
            {
                return result;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
