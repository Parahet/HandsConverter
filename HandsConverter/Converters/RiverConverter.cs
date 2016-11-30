using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HandsConverter.Converters
{
    public class RiverConverter : Converter
    {
        private string riverCard;

        private string patternBefore = String.Format(@"\*\*\* RIVER \*\*\* \[{0} {1} {2} {3}\] \[", Consts.Card, Consts.Card, Consts.Card, Consts.Card);
        private string patternAfter = @"\]";
        public RiverConverter(string str) : base(str)
        {
        }

        protected override string pattern
        {
            get
            {
                return String.Format(@"\*\*\* RIVER \*\*\* \[{0} {1} {2} {3}\] \[{4}\]", Consts.Card,
                    Consts.Card, Consts.Card, Consts.Card, Consts.Card);
            }
        }

        public override string ConvertToParty()
        {
            return String.Format("** Dealing River ** [ {0} ]", riverCard);
        }

        public override void Initialize()
        {
            riverCard = Regex.Replace(Regex.Replace(str, patternBefore, ""), patternAfter, "");
        }
    }
}
