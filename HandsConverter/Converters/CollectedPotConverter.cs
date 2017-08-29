using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HandsConverter.Converters
{
	public class CollectedPotConverter : ConverterWithPlayerName
    {
        private long count;
        private string whichPot;
        public int sidePotNumber;
        public Dictionary<string, long> playersPutInAmount;

        public CollectedPotConverter(string str, int sidePotNumber, Dictionary<string, long> playersPutInAmount)
            : base(str)
        {
            this.sidePotNumber = sidePotNumber;
            this.playersPutInAmount = playersPutInAmount;
        }

		protected override string pattern
        {
            get { return @"(?<playerName>.*) collected (?<count>\d+) from (?<whichPot>|side |main )pot$"; } 
        }

        public override string ConvertToParty()
        {
            throw new NotImplementedException("use 'public string ConvertToParty(Dictionary<string, long> uncalledBets)'");
        }

	    public override string ConvertTo888()
	    {
		    sidePotNumber++;
			return $"{PlayerName} collected [ ${count.ToCommaSeparateString()} ]";
		}

	    public string ConvertToParty(Dictionary<string, long> uncalledBets)
        {
            if (uncalledBets.ContainsKey(playerName))
                count += uncalledBets[playerName];
            var combination = "";
            sidePotNumber++;
            if (whichPot == "side")
            {
                return
	                $"{PlayerName} wins {count.ToCommaSeparateString()} chips from the side pot {sidePotNumber}";
            }
            if (whichPot == "main")
            {
                return $"{PlayerName} wins {count.ToCommaSeparateString()} chips from the main pot";
            }
            return $"{PlayerName} wins {count.ToCommaSeparateString()} chips";
        }

		public override void Initialize()
        {
            var match = new Regex(pattern).Match(str);
            playerName = match.Groups["playerName"].Value;
            count = Int64.Parse(match.Groups["count"].Value);
            whichPot = match.Groups["whichPot"].Value.Trim();
        }
    }
}
