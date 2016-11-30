using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HandsConverter.Converters
{
    public class RaiseConverter : ConverterWithPlayerName
    {
        private long count;
        private long countTo;
        private bool isAllIn;

        private const string patternWithoutName = @": raises \d+ to \d+$";
        private const string pattern1 = @".*: raises ";
        private const string pattern2 = @" to \d+$";
        private const string pattern3 = @".*: raises \d+ to ";

        public Dictionary<string, long> playersPutInAmount;
        public RaiseConverter(string str, Dictionary<string, long> playersPutInAmount)
            : base(str)
        {
            this.playersPutInAmount = playersPutInAmount;
        }
        protected override string pattern
        {
            get { return @"(?<playerName>.*): raises (?<count>\d+) to (?<countTo>\d+)(?<isAllIn>| and is all-in)$"; }
        }
        public override string ConvertToParty()
        {
            countTo -= playersPutInAmount[playerName];
            playersPutInAmount[playerName] += countTo;
            if(isAllIn)
                return PlayerName + " is all-In  [" + countTo.ToCommaSeparateString() + "]";
            else
                return PlayerName + " raises [" + countTo.ToCommaSeparateString() + "]";
        }
        
        public override void Initialize()
        {
            var match = new Regex(pattern).Match(str);
            playerName = match.Groups["playerName"].Value;
            count = Int32.Parse(match.Groups["count"].Value);
            countTo = Int32.Parse(match.Groups["countTo"].Value);
            isAllIn = match.Groups["isAllIn"].Value != "";
        }
    }
}
