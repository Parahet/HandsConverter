using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HandsConverter.Converters
{
    public class SeatPreviewConverter : ConverterWithPlayerName
    {
        private int seatNumber;
        private int chipsCount;

        public SeatPreviewConverter(string str) : base(str)
        {
        }

        protected override string pattern
        {
            get { return @"Seat (?<seatNumber>\d): (?<playerName>.*) \((?<chipsCount>\d+) in chips\)"; }
        }

        public override string ConvertToParty()
        {
            return "Seat " + seatNumber.ToString() + ": " + PlayerName + " ( " + chipsCount.ToCommaSeparateString() + " )";
        }

        public override void Initialize()
        {
            var match = new Regex(pattern).Match(str);
            playerName = match.Groups["playerName"].Value;
            seatNumber = Int32.Parse(match.Groups["seatNumber"].Value);
            chipsCount = Int32.Parse(match.Groups["chipsCount"].Value);
        }
    }
}
