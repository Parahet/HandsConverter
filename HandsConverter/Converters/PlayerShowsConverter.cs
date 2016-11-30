using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HandsConverter.Converters
{
    public class PlayerShowsConverter : ConverterWithPlayerName
    {
        private string card1;
        private string card2;
        private string combination;

        public PlayerShowsConverter(string str) : base(str)
        {
        }

        protected override string pattern
        {
            get { return @"(?<playerName>.*): shows \[(?<card1>"+Consts.Card+@") (?<card2>"+Consts.Card+@")\] \((?<combination>.*)\)"; }
        }

        public override string ConvertToParty()
        {
            var combinationForParty = GetPartyCombination(combination);
            return String.Format(@"{0} shows [ {1}, {2} ]{3}.", PlayerName, card1, card2, combinationForParty);
        }

        private string GetPartyCombination(string comb)
        {

            if (comb.Contains("kicker"))
            {
                if (comb.Contains(" - lower kicker"))
                    return comb.Replace(" - lower kicker", "");
                return comb.Replace("-", "with");
            }
            return combination;
        }
        public override void Initialize()
        {
            var match = new Regex(pattern).Match(str);
            playerName = match.Groups["playerName"].Value;
            card1 = match.Groups["card1"].Value;
            card2 = match.Groups["card2"].Value; 
            combination = match.Groups["combination"].Value;
        }
    }
}
