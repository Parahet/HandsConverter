using System;
using System.Text.RegularExpressions;

namespace HandsConverter.Converters
{
	public class TableHeaderConverter : Converter
    {
        public long tournamentNumber;
        public int tableNumber;
        public int maxSeat;
        public int buttonNumber;
        public int numberOfPlayers;

        public TableHeaderConverter(string str, int numberOfPlayers)
            : base(str)
        {
            this.numberOfPlayers = numberOfPlayers;
        }
        //Table '1607627129 44' 6-max Seat #2 is the button
        //Table '1607627129 44' 6-max Seat #1 is the button
        protected override string pattern => @"Table '(?<tournamentNumber>\d+) (?<tableNumber>\d+)' (?<maxSeat>\d)-max Seat #(?<buttonNumber>\d) is the button";

	    public override string ConvertToParty()
        {
            var result =
	            $"Table Parahet. $0,000 Gtd {maxSeat}-Max ({tournamentNumber}) Table #{tableNumber} (Real Money)";
            result += Environment.NewLine;
            result += $"Seat {buttonNumber} is the button";
            result += Environment.NewLine;
            result += $"Total number of players : {numberOfPlayers}/{maxSeat} ";
            return result;
        }

	    public override string ConvertTo888()
	    {
		    var result = $"Tournament #{tournamentNumber} $666 + $66 - Table #{tableNumber} {maxSeat} Max (Real Money)";
		    result += Environment.NewLine;
		    result += $"Seat {buttonNumber} is the button";
		    result += Environment.NewLine;
		    result += $"Total number of players : {numberOfPlayers}";
		    return result;
		}

	    public override void Initialize()
        {
            var match = new Regex(pattern).Match(str);
            tournamentNumber = Int64.Parse(match.Groups["tournamentNumber"].Value);
            tableNumber = Int32.Parse(match.Groups["tableNumber"].Value);
            maxSeat = Int32.Parse(match.Groups["maxSeat"].Value);
            buttonNumber = Int32.Parse(match.Groups["buttonNumber"].Value);
        }
    }
}
