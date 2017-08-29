using System;
using System.Text.RegularExpressions;

namespace HandsConverter.Converters
{
    public class HandHeaderConverter : Converter
    {
        public long handNumber;
        public long tournamentNumber;
        public string romanLevel;
        public int arabicLevel; 
        public int smallBlind;
        public int bigBlind;
        public int ante;
        public string buyIn;
        public string ET;
        private const string TimeForParty = "Saturday, July 16, 20:26:02 EET 2016";
	    private const string TimeFor888 = "25 08 2017 04:16:36";

		public HandHeaderConverter(string str, int ante)
            : base(str)
        {
            this.ante = ante;
        }

        protected override string pattern => @"PokerStars Hand #(?<handNumber>\d+): Tournament #(?<tournamentNumber>\d+), (?<buyIn>.*) Hold'em No Limit - Level (?<level>[I,V,X,L,C,D,M]+) \((?<sb>\d+)\/(?<bb>\d+)\) - (?<ET>.*)";

	    public override string ConvertToParty()
        {
            var result = "Game #" + handNumber + " starts.";
            result += Environment.NewLine;
            result += Environment.NewLine;
            result += @"#Game No : " + handNumber;
            result += Environment.NewLine;
            result += $@"***** Hand History for Game {handNumber} *****";
            result += Environment.NewLine;
            if (ante != 0)
                result +=
		                $@"NL Texas Hold'em {buyIn} Buy-in Trny:{tournamentNumber} Level:{arabicLevel}  Blinds-Antes({
			                smallBlind.ToSeparateString()
		                }/{bigBlind.ToSeparateString()} -{ante.ToSeparateString()}) - {TimeForParty}";
            else
                result +=
		                $@"NL Texas Hold'em {buyIn} Buy-in Trny:{tournamentNumber} Level:{arabicLevel}  Blinds-Antes({
			                smallBlind.ToSeparateString()
		                }/{bigBlind.ToSeparateString()}) - {TimeForParty}";
            return result;
        }

	    public override string ConvertTo888()
	    {
		    var result = $"#Game No : {handNumber}";
		    result += Environment.NewLine;
		    result += $"***** 888poker Hand History for Game {handNumber} *****";
		    result += Environment.NewLine;
		    result += $"${smallBlind.ToSeparateString()}/${bigBlind.ToSeparateString()} Blinds No Limit Holdem - *** {TimeFor888}";
		    return result;
		}

		public override void Initialize()
        {
            var match = new Regex(pattern).Match(str);
            handNumber = Int64.Parse(match.Groups["handNumber"].Value);
            tournamentNumber = Int64.Parse(match.Groups["tournamentNumber"].Value);
            buyIn = match.Groups["buyIn"].Value;
            romanLevel = match.Groups["level"].Value;
            smallBlind = Int32.Parse(match.Groups["sb"].Value);
            bigBlind = Int32.Parse(match.Groups["bb"].Value);
            ET = match.Groups["ET"].Value;

            arabicLevel = FromRomanToAbaric(romanLevel);
        }

        public int FromRomanToAbaric(string romanNumerals)
        {
            string rim = romanNumerals;
            int summa = 0;

            for (int i = 0; i < rim.Length; i++)
            {
                if (i < rim.Length - 1 && rim[i] == 'I' && rim[i + 1] != 'I') continue;
                else if (rim[i] == 'I')
                    summa += 1;
                else if (rim[i] == 'V')
                {
                    if (i > 0 && rim[i - 1] == 'I') summa += 4;
                    else summa += 5;
                }

                else if (rim[i] == 'X')
                {
                    if (i > 0 && rim[i - 1] == 'I') summa += 9;
                    else if (i < rim.Length - 1 && (rim[i + 1] == 'L' || rim[i + 1] == 'C')) continue;
                    else summa += 10;
                }

                else if (rim[i] == 'L')
                {
                    if (i > 0 && rim[i - 1] == 'X') summa += 40;
                    else summa += 50;
                }

                else if (rim[i] == 'C')
                {
                    if (i > 0 && rim[i - 1] == 'X') summa += 90;
                    else if (i < rim.Length - 1 && (rim[i + 1] == 'D' || rim[i + 1] == 'M')) continue;
                    else summa += 100;
                }
                else if (rim[i] == 'D')
                {
                    if (i > 0 && rim[i - 1] == 'C') summa += 400;
                    else summa += 500;
                }

                else if (rim[i] == 'M')
                {
                    if (i > 0 && rim[i - 1] == 'C') summa += 900;
                    else summa += 1000;
                }
            }
            return summa;
        }
    }
}
