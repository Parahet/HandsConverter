using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HandsConverter.Converters
{
    public abstract class Converter
    {
        protected string str;
        protected Converter(string str)
        {
            this.str = str;
        }

        protected abstract string pattern { get;}
        public bool IsMatch()
        {
            if (Regex.IsMatch(str, pattern))
            {
                Initialize();
                return true;
            }
            return false;
        }

        public abstract string ConvertToParty();
        public abstract void Initialize();
    }
}
