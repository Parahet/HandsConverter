using System.Text.RegularExpressions;

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

        protected override string pattern => @"(?<playerName>.*): shows \[(?<card1>"+Consts.Card+@") (?<card2>"+Consts.Card+@")\] \((?<combination>.*)\)";

	    public override string ConvertToParty()
        {
            var combinationForParty = GetPartyCombination(combination);
            return $"{PlayerName} shows [ {card1}, {card2} ]{combinationForParty}.";
        }

	    public override string ConvertTo888()
	    {
		    return $"{PlayerName} shows [ {card1}, {card2} ]";
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
