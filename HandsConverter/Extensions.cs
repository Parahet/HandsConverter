using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandsConverter
{
    public static class Extensions
    {
        public static string ToCommaSeparateString(this long count)
        {
            return count.ToSeparateString().Replace(" ", ",");
        }

        public static string ToSeparateString(this long count)
        {
            return count.ToString("### ### ### ### ### ### ### ###").Trim();
        }
        public static string ToCommaSeparateString(this int count)
        {
            return count.ToSeparateString().Replace(" ", ",");
        }
        public static string ToSeparateString(this int count)
        {
            return count.ToString("### ### ### ### ### ### ### ###").Trim();
        }

        public static string ToNewName(this string playerName)
        {
            return playerName.GetHashCode().ToString();
        }

    }
}
