using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HandsConverter.Converters
{
	public class CallConverter : ConverterWithPlayerName
    {
        private int count;
        private bool isAllIn;
        public Dictionary<string, long> playersPutInAmount;

        public CallConverter(string str, Dictionary<string, long> playersPutInAmount)
            : base(str)
        {
            this.playersPutInAmount = playersPutInAmount;
        }
        protected override string pattern
        {
            get { return @"(?<playerName>.*): calls (?<count>\d+)(?<isAllIn>| and is all-in)$"; }
        }
        public override string ConvertToParty()
        {
            playersPutInAmount[playerName] += count;
            if (isAllIn)
                return PlayerName + " is all-In  [" + count.ToCommaSeparateString() + "]";
            else
                return PlayerName + " calls [" + count.ToCommaSeparateString() + "]";
        }

	    public override string ConvertTo888()
	    {
			return $"{PlayerName} calls [${count.ToCommaSeparateString()}]";
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
