using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HandsConverter.Converters
{
    public class MuckHandConverter : ConverterWithPlayerName
    {
        private string patternWithoutName = @": doesn't show hand";
        public MuckHandConverter(string str) : base(str)
        {
        }

        protected override string pattern
        {
            get { return @".*" + patternWithoutName; } 
        }

        public override string ConvertToParty()
        {
            return String.Format("{0} does not show cards.", PlayerName);
        }

        public override void Initialize()
        {
            playerName = Regex.Replace(str, patternWithoutName, "");
        }
    }
}
