using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HandsConverter.Converters
{
    public class PostSBConverter : ConverterWithPlayerName
    {
        private int count;
        public Dictionary<string, long> playersPutInAmount;

        public PostSBConverter(string str, Dictionary<string, long> playersPutInAmount)
            : base(str)
        {
            this.playersPutInAmount = playersPutInAmount;
        }

        protected override string pattern
        {
            get { return @"(?<playerName>.*): posts small blind (?<sb>\d+)"; }
        }

        public override string ConvertToParty()
        {
            playersPutInAmount[playerName] += count;
            return PlayerName + " posts small blind [" + count.ToCommaSeparateString() + "].";
        }

        public override void Initialize()
        {
            var match = new Regex(pattern).Match(str);
            playerName = match.Groups["playerName"].Value;
            count = Int32.Parse(match.Groups["sb"].Value);
        }
    }
}
