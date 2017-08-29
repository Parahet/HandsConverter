using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HandsConverter.Converters
{
	public class RaiseConverter : ConverterWithPlayerName
    {
        private long count;
        private long countTo;
        private bool isAllIn;

        public Dictionary<string, long> playersPutInAmount;
        public RaiseConverter(string str, Dictionary<string, long> playersPutInAmount)
            : base(str)
        {
            this.playersPutInAmount = playersPutInAmount;
        }
        protected override string pattern => @"(?<playerName>.*): raises (?<count>\d+) to (?<countTo>\d+)(?<isAllIn>| and is all-in)$";

	    public override string ConvertToParty()
        {
            countTo -= playersPutInAmount[playerName];
            playersPutInAmount[playerName] += countTo;
            if(isAllIn)
                return PlayerName + " is all-In  [" + countTo.ToCommaSeparateString() + "]";
            else
                return PlayerName + " raises [" + countTo.ToCommaSeparateString() + "]";
        }

	    public override string ConvertTo888()
	    {
		    countTo -= playersPutInAmount[playerName];
		    playersPutInAmount[playerName] += countTo;
		    return $"{PlayerName} raises [${countTo.ToCommaSeparateString()}]";
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
