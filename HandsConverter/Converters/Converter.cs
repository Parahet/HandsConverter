using System.Text.RegularExpressions;

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
	    public abstract string ConvertTo888();
		public abstract void Initialize();
    }
}
