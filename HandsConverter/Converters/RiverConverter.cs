using System.Text.RegularExpressions;

namespace HandsConverter.Converters
{
	public class RiverConverter : Converter
    {
        private string riverCard;

        private string patternBefore = $@"\*\*\* RIVER \*\*\* \[{Consts.Card} {Consts.Card} {Consts.Card} {Consts.Card}\] \[";
        private string patternAfter = @"\]";
        public RiverConverter(string str) : base(str)
        {
        }

        protected override string pattern => $@"\*\*\* RIVER \*\*\* \[{Consts.Card} {Consts.Card} {Consts.Card} {Consts.Card}\] \[{Consts.Card}\]";

	    public override string ConvertToParty() => $"** Dealing River ** [ {riverCard} ]";

	    public override string ConvertTo888() => $"** Dealing river ** [ {riverCard} ]";

	    public override void Initialize()
        {
            riverCard = Regex.Replace(Regex.Replace(str, patternBefore, ""), patternAfter, "");
        }
    }
}
