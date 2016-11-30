using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HandsConverter.Converters
{
    public class CheckConverter : ConverterWithPlayerName
    {
        private const string patternWithoutName = @": checks";
        public CheckConverter(string str) : base(str)
        {
            }
        protected override string pattern
        {
            get { return @".*"+patternWithoutName;}
        }
        public override string ConvertToParty()
        {
            return PlayerName + " checks";
        }

        public override void Initialize()
        {
            playerName = Regex.Replace(str, patternWithoutName, "");
        }
    }
}
