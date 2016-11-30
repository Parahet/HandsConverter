using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HandsConverter.Converters
{
    public class FoldConverter : ConverterWithPlayerName
    {
        private string shownCard;

        public FoldConverter(string str)
            : base(str)
        {
        }

        protected override string pattern
        {
            get { return @"(?<playerName>.*): folds(?<shownCard>.*)$"; }
        }

        public override string ConvertToParty()
        {
            return PlayerName + " folds";
        }

        public override void Initialize()
        {
            var match = new Regex(pattern).Match(str);
            playerName = match.Groups["playerName"].Value;
            shownCard = match.Groups["shownCard"].Value;
        }
    }
}
