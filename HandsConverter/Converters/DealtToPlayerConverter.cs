using System;
using System.Text.RegularExpressions;

namespace HandsConverter.Converters
{
	//TODO: need to redefine pattern
	public class DealtToPlayerConverter : ConverterWithPlayerName
    {
        private string card1;
        private string card2;

	    private string newPattern = $@"Dealt to (?<playerName>.*) \[(?<card1>{Consts.Card}) (?<card2>{Consts.Card})\]";
		
		private const string patternBeforeName = @"Dealt to ";
        private const string patternAfterName = @" \[" + Consts.Card + " " + Consts.Card + @"\]";

        private const string patternBeforeCard1 = @"Dealt to .* \[";
        private const string patternAfterCard1 = " " + Consts.Card + @"\]";
        private const string patternBeforeCard2 = @"Dealt to .* \[" + Consts.Card + " ";
        private const string patternAfterCard2 = @"\]";

        public DealtToPlayerConverter(string str) : base(str)
        {
        }

        protected override string pattern => @"Dealt to .* \[" + Consts.Card + " " + Consts.Card + @"\]";

	    public override string ConvertToParty()
        {
            return "Dealt to " + PlayerName + " [ " + card1 + " " + card2 + " ]";
        }

	    public override string ConvertTo888()
	    {
		    return $"Dealt to {PlayerName} [ {card1}, {card2} ]";
	    }

	    public override void Initialize()
	    {
		    var playerNameOld = Regex.Replace(Regex.Replace(str, patternBeforeName, ""), patternAfterName, "");
		    var card1Old = Regex.Replace(Regex.Replace(str, patternBeforeCard1, ""), patternAfterCard1, "");
		    var card2Old = Regex.Replace(Regex.Replace(str, patternBeforeCard2, ""), patternAfterCard2, "");

		    var match = new Regex(newPattern).Match(str);
		    playerName = match.Groups["playerName"].Value;
		    card1 = match.Groups["card1"].Value;
		    card2 = match.Groups["card2"].Value;

		    if (playerNameOld != playerName)
			    throw new Exception($"Player name is incorrect. Old: {playerNameOld}, now: {playerName}");
		    if (card1 != card1Old) throw new Exception($"card1 is incorrect. Old: {card1}, now: {card1Old}");
		    if (card2 != card2Old) throw new Exception($"card2 is incorrect. Old: {card2}, now: {card2Old}");
	    }
    }
}
