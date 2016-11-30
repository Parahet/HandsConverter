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

        private string patternBefore = String.Format(@"\*\*\* TURN \*\*\* \[{0} {1} {2}\] \[", Consts.Card, Consts.Card, Consts.Card);
        private string patternAfter = @"\]";
        public TurnConverter(string str) : base(str)
        {
        }

        protected override string pattern
        {
            get { return String.Format(@"\*\*\* TURN \*\*\* \[{0} {1} {2}\] \[{3}\]", Consts.Card, Consts.Card, Consts.Card, Consts.Card); }
        }

        public override string ConvertToParty()
        {
            return String.Format("** Dealing Turn ** [ {0} ]", turnCard);
        }

        public override void Initialize()
        {
            turnCard = Regex.Replace(Regex.Replace(str, patternBefore, ""), patternAfter, "");
        }
    }
}
