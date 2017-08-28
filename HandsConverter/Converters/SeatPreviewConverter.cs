using System;
using System.Text.RegularExpressions;

namespace HandsConverter.Converters
{
	public class SeatPreviewConverter : ConverterWithPlayerName
    {
        private int seatNumber;
        private int chipsCount;

        public SeatPreviewConverter(string str) : base(str)
        {
        }

        protected override string pattern => @"Seat (?<seatNumber>\d): (?<playerName>.*) \((?<chipsCount>\d+) in chips\)";

	    public override string ConvertToParty() => "Seat " + seatNumber.ToString() + ": " + PlayerName + " ( " + chipsCount.ToCommaSeparateString() + " )";

	    public override string ConvertTo888() => $"Seat {seatNumber}: {PlayerName} ( {chipsCount.ToCommaSeparateString()} )";

	    public override void Initialize()
        {
            var match = new Regex(pattern).Match(str);
            playerName = match.Groups["playerName"].Value;
            seatNumber = Int32.Parse(match.Groups["seatNumber"].Value);
            chipsCount = Int32.Parse(match.Groups["chipsCount"].Value);
        }
    }
}
