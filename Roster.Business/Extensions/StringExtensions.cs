using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roster.Business.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
        public static bool IsNotNullOrEmpty(this string value)
        {
            return !value.IsNullOrEmpty();
        }

        public static bool IsNotNullOrEmpty(this string value, string valueReplace)
        {
            if (value.IsNullOrEmpty())
            {
                return false;
            }
            return !value.Replace(valueReplace, "").IsNullOrEmpty();
        }

        public static bool IsNotNullOrEmptyTrim(this string value)
        {
            if (value.IsNullOrEmpty())
            {
                return false;
            }
            return !value.Trim().IsNullOrEmpty();
        }

        public static string IsReplace(this string value, string valueReplace)
        {
            if (value.IsNullOrEmpty())
            {
                return string.Empty;
            }
            return value.Replace(valueReplace, "").Trim();
        }

        public static string IsReplace(this string value, string source, string taget)
        {
            if (value.IsNullOrEmpty())
            {
                return string.Empty;
            }
            return value.Replace(source, taget).Trim();
        }
        public static int ToInt(this string value, int valueDefault)
        {
            if (value.IsNullOrEmpty())
            {
                return valueDefault;
            }

            int outValue = 0;
            int.TryParse(value, out outValue);
            return outValue;
        }

        public static int ReplaceAndConvertInt(this string value, int valueDefault, string charWillBeReplace)
        {
            if (value.IsNullOrEmpty())
            {
                return valueDefault;
            }
            string valueBeforeReplace = value.Substring(0, 1);
            int outValue = 0;
            int.TryParse(value, out outValue);
            return outValue;
        }

        public static int ToInt(this object value, int valueDefault)
        {
            if (value == null)
            {
                return valueDefault;
            }

            int outValue = 0;
            int.TryParse(value.ToString(), out outValue);
            return outValue;
        }

        public static int? ToInt(this object value)
        {
            if (value == null)
            {
                return null;
            }

            int outValue = 0;
            int.TryParse(value.ToString(), out outValue);
            return outValue;
        }

        public static double? ToDouble(this string value)
        {
            if (value.IsNullOrEmpty())
            {
                return null;
            }

            double outValue = 0;
            double.TryParse(value.ToString(), out outValue);
            return outValue;
        }

        public static string ConvertToString(this object value)
        {
            if (value == null)
            {
                return null;
            }

            return value.ToString();
        }

        public static bool ConvertToBoolean(this object value)
        {
            if (value == null)
            {
                return false;
            }

            return bool.Parse(value.ToString());
        }

        public static string ConvertToString(this object value, string defaultValue)
        {
            if (value == null)
            {
                return defaultValue;
            }

            return value.ToString();
        }

        public static string IsToString(this string value)
        {
            if (value.IsNullOrEmpty())
            {
                return string.Empty;
            }

            return value;
        }

        public static string IsTrim(this string value)
        {
            if (value.IsNullOrEmpty())
            {
                return string.Empty;
            }

            return value.Trim();
        }

        public static string IsToString(this object value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            return value.ToString();
        }
        public static string IsToString(this string value, object defaultValue)
        {
            if (value.IsNullOrEmpty())
            {
                return defaultValue.ConvertToString("0");
            }

            return value;
        }

        public static bool IsEquals(this string value, string compareString)
        {
            if (value.IsNullOrEmpty())
            {
                return false;
            }

            return value.IsTrim().Equals(compareString.IsTrim());
        }

        public static string FomatToString(this DateTime? value, string fomat)
        {
            if (!value.HasValue)
            {
                return string.Empty;
            }

            return value.Value.ToString(fomat);
        }

        public static bool IsDouble(this string value)
        {
          
            double check;
            return Double.TryParse(value, out check);
        }

        public static string IsSubString(this string value)
        {

            if (value.IsNullOrEmpty())
            {
                return string.Empty;
            }

            return value.Substring((value.Length - 1), 1);

        }

        public static bool IsInt(this string value)
        {
            int check;
            return int.TryParse(value, out check);
        }

        public static DateTime ToDateTime(this string value)
        {
            if (value.IsNullOrEmpty())
            {
                return DateTime.Now;
            }

            return DateTime.ParseExact(value, "dd/MM/yyyy", null);
        }

        public static string GetItemInArray(this string values, int index, char split = '_')
        {
            string value = string.Empty;
            if (values.IsNullOrEmpty())
            {
                return value;
            }
            var array = values.Split(split);
            if (array.Length == 0 || array.Length < index )
            {
                return value;
            }
            try 
	        {
                return array[index];
	        }
	        catch 
	        {

                return string.Empty;
	        }
        }
    
    }
}
