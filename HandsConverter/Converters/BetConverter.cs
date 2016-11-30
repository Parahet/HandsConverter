using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HandsConverter.Converters
{
    public class BetConverter : ConverterWithPlayerName
    {
        private int count;
        private bool isAllIn;
        public Dictionary<string, long> playersPutInAmount;


        public BetConverter(string str, Dictionary<string, long> playersPutInAmount)
            : base(str)
        {
            this.playersPutInAmount = playersPutInAmount;
        }
        protected override string pattern
        {
            get { return @"(?<playerName>.*): bets (?<count>\d+)(?<isAllIn>| and is all-in)$"; }
        }
        public override string ConvertToParty()
        {
            playersPutInAmount[playerName] += count;
            if (isAllIn)
                return PlayerName + " is all-In  [" + count.ToCommaSeparateString() + "]";
            else
                return PlayerName + " bets [" + count.ToCommaSeparateString() + "]";
        }

        public override void Initialize()
        {
            var match = new Regex(pattern).Match(str);
            playerName = match.Groups["playerName"].Value;
            count = Int32.Parse(match.Groups["count"].Value);
            isAllIn = match.Groups["isAllIn"].Value != "";
        }
    }
}
