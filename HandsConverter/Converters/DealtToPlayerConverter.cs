using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HandsConverter.Converters
{
    //TODO: need to redefine pattern
    public class DealtToPlayerConverter : ConverterWithPlayerName
    {
        private string card1;
        private string card2;

        private const string patternBeforeName = @"Dealt to ";
        private const string patternAfterName = @" \[" + Consts.Card + " " + Consts.Card + @"\]";

        private const string patternBeforeCard1 = @"Dealt to .* \[";
        private const string patternAfterCard1 = " " + Consts.Card + @"\]";
        private const string patternBeforeCard2 = @"Dealt to .* \[" + Consts.Card + " ";
        private const string patternAfterCard2 = @"\]";

        public DealtToPlayerConverter(string str) : base(str)
        {
        }

        protected override string pattern
        {
            get { return @"Dealt to .* \[" + Consts.Card + " " + Consts.Card + @"\]"; }
        }

        public override string ConvertToParty()
        {
            return "Dealt to " + PlayerName + " [ " + card1 + " " + card2 + " ]";
        }

        public override void Initialize()
        {
            playerName = Regex.Replace(Regex.Replace(str, patternBeforeName, ""), patternAfterName, "");
            card1 = Regex.Replace(Regex.Replace(str, patternBeforeCard1, ""), patternAfterCard1, "");
            card2 = Regex.Replace(Regex.Replace(str, patternBeforeCard2, ""), patternAfterCard2, "");
        }
    }
}
