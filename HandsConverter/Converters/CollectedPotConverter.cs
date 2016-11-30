using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HandsConverter.Converters
{
    public class CollectedPotConverter : ConverterWithPlayerName
    {
        private long count;
        private string whichPot;
        public int sidePotNumber;
        public Dictionary<string, long> playersPutInAmount;

        //Fieldmice1 collected 12375 from main pot
        //Fieldmice1 collected 12446 from side pot
        //Fitness_g collected 4119 from pot

        //cheesebag wins 14,440 chips from the main pot with two pairs, Aces and Sixes.
        //BeesKiss wins 23,968 chips from the side pot 1 with two pairs, Kings and Jacks.
        //Vic_Biggs wins 9,556 chips

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

        public string ConvertToParty(Dictionary<string, long> uncalledBets)
        {
            if (uncalledBets.ContainsKey(playerName))
                count += uncalledBets[playerName];
            var combination = "";
            sidePotNumber++;
            if (whichPot == "side")
            {
                return String.Format("{0} wins {1} chips from the side pot {2}"/* with {3}"*/, PlayerName, count.ToCommaSeparateString(), sidePotNumber.ToString()/*, combination*/);
            }
            if (whichPot == "main")
            {
                return String.Format("{0} wins {1} chips from the main pot"/* with {3}"*/, PlayerName, count.ToCommaSeparateString()/*, combination*/);
            }
            return String.Format("{0} wins {1} chips", PlayerName, count.ToCommaSeparateString());
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
