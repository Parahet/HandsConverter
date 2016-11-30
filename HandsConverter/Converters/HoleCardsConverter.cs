using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandsConverter.Converters
{
    public class HoleCardsConverter : Converter
    {
        public HoleCardsConverter(string str) : base(str)
        {
        }

        protected override string pattern
        {
            get { return @"\*\*\* HOLE CARDS \*\*\*"; }
        }

        public override string ConvertToParty()
        {
            return @"** Dealing down cards **";
        }

        public override void Initialize()
        {
        }
    }
}
