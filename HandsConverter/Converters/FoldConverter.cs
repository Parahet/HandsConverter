using System.Text.RegularExpressions;

namespace HandsConverter.Converters
{
	public class FoldConverter : ConverterWithPlayerName
    {
        private string shownCard;

        public FoldConverter(string str)
            : base(str)
        {
        }

        protected override string pattern => @"(?<playerName>.*): folds(?<shownCard>.*)$";

	    public override string ConvertToParty()
        {
            return PlayerName + " folds";
        }

	    public override string ConvertTo888()
	    {
			return PlayerName + " folds";
		}

	    public override void Initialize()
        {
            var match = new Regex(pattern).Match(str);
            playerName = match.Groups["playerName"].Value;
            shownCard = match.Groups["shownCard"].Value;
        }
    }
}
