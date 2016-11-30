using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HandsConverter.Converters
{
    public class UncalledBetConverter : ConverterWithPlayerName
    {
        public long uncalledBet;

        public UncalledBetConverter(string str) : base(str)
        {
        }

        protected override string pattern
        {
            get { return @"Uncalled bet \((?<uncalledBet>\d+)\) returned to (?<playerName>.*)"; }
        }

        public override string ConvertToParty()
        {
            return "";
        }

        public override void Initialize()
        {
            var match = new Regex(pattern).Match(str);
            playerName = match.Groups["playerName"].Value;
            uncalledBet = Int64.Parse(match.Groups["uncalledBet"].Value);
        }
    }
}
