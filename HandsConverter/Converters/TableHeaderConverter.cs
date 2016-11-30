using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
        protected override string pattern
        {
            get { return @"Table '(?<tournamentNumber>\d+) (?<tableNumber>\d+)' (?<maxSeat>\d)-max Seat #(?<buttonNumber>\d) is the button"; }
        }

        public override string ConvertToParty()
        {
            var result = String.Format(@"Table Parahet. $0,000 Gtd {0}-Max ({1}) Table #{2} (Real Money)",
                maxSeat, tournamentNumber, tableNumber);
            result += Environment.NewLine;
            result += String.Format(@"Seat {0} is the button", buttonNumber);
            result += Environment.NewLine;
            result += String.Format(@"Total number of players : {0}/{1} ", numberOfPlayers, maxSeat);
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
