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

        protected override string pattern => @".*" + patternWithoutName;

	    public override string ConvertToParty() => $"{PlayerName} does not show cards.";
	    public override string ConvertTo888() => $"{PlayerName} did not show his hand";

	    public override void Initialize()
        {
            playerName = Regex.Replace(str, patternWithoutName, "");
        }
    }
}
