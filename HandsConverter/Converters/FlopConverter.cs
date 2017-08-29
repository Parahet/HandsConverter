using System.Text.RegularExpressions;

namespace HandsConverter.Converters
{
	public class FlopConverter : Converter
    {
        private string card1;
        private string card2;
        private string card3;

        private const string patternBeforeCard1 = @"\*\*\* FLOP \*\*\* \[";
        private const string patternAfterCard1 = " " + Consts.Card + " " + Consts.Card + @"\]";
        private const string patternBeforeCard2 = @"\*\*\* FLOP \*\*\* \[" + Consts.Card + " ";
        private const string patternAfterCard2 = " " + Consts.Card + @"\]";
        private const string patternBeforeCard3 = @"\*\*\* FLOP \*\*\* \[" + Consts.Card + " " + Consts.Card + " ";
        private const string patternAfterCard3 = @"\]";

        public FlopConverter(string str) : base(str)
        {
        }

        protected override string pattern => @"\*\*\* FLOP \*\*\* \[" + Consts.Card + " " + Consts.Card + " " +
                                             Consts.Card + @"\]";

	    public override string ConvertToParty() => $"** Dealing Flop ** [ {card1}, {card2}, {card3} ]";
	    public override string ConvertTo888() => $"** Dealing flop ** [ {card1}, {card2}, {card3} ]";

		public override void Initialize()
        {
            card1 = Regex.Replace(Regex.Replace(str, patternBeforeCard1, ""), patternAfterCard1, "");
            card2 = Regex.Replace(Regex.Replace(str, patternBeforeCard2, ""), patternAfterCard2, "");
            card3 = Regex.Replace(Regex.Replace(str, patternBeforeCard3, ""), patternAfterCard3, "");
        }
    }
}
