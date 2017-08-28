using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HandsConverter.Converters
{
    public class TurnConverter : Converter
    {
        private string turnCard;

        private string patternBefore = $@"\*\*\* TURN \*\*\* \[{Consts.Card} {Consts.Card} {Consts.Card}\] \[";
        private string patternAfter = @"\]";
        public TurnConverter(string str) : base(str)
        {
        }

        protected override string pattern => $"\\*\\*\\* TURN \\*\\*\\* \\[{Consts.Card} {Consts.Card} {Consts.Card}\\] \\[{Consts.Card}\\]";

	    public override string ConvertToParty()
        {
            return $"** Dealing Turn ** [ {turnCard} ]";
        }

	    public override string ConvertTo888()
	    {
		    return $"** Dealing turn ** [ {turnCard} ]";
	    }

	    public override void Initialize()
        {
            turnCard = Regex.Replace(Regex.Replace(str, patternBefore, ""), patternAfter, "");
        }
    }
}
