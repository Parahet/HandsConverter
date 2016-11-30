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
        private const string TIME = "Saturday, July 16, 20:26:02 EET 2016";

        public HandHeaderConverter(string str, int ante)
            : base(str)
        {
            this.ante = ante;
        }

        protected override string pattern
        {
            get
            {
                return
                    @"PokerStars Hand #(?<handNumber>\d+): Tournament #(?<tournamentNumber>\d+), (?<buyIn>.*) Hold'em No Limit - Level (?<level>[I,V,X,L,C,D,M]+) \((?<sb>\d+)\/(?<bb>\d+)\) - (?<ET>.*)";
            }
        }

        public override string ConvertToParty()
        {
            var result = "Game #" + handNumber.ToString() + " starts.";
            result += Environment.NewLine;
            result += Environment.NewLine;
            result += @"#Game No : " + handNumber.ToString();
            result += Environment.NewLine;
            result += String.Format(@"***** Hand History for Game {0} *****", handNumber.ToString());
            result += Environment.NewLine;
            if (ante != 0)
                result += String.Format(
                    @"NL Texas Hold'em {0} Buy-in Trny:{1} Level:{2}  Blinds-Antes({3}/{4} -{5}) - {6}", buyIn,
                    tournamentNumber, arabicLevel, smallBlind.ToSeparateString(),
                    bigBlind.ToSeparateString(),
                    ante.ToSeparateString(), TIME.ToString());
            else
                result += String.Format(
                    @"NL Texas Hold'em {0} Buy-in Trny:{1} Level:{2}  Blinds-Antes({3}/{4}) - {5}", buyIn,
                    tournamentNumber, arabicLevel, smallBlind.ToSeparateString(),
                    bigBlind.ToSeparateString(), TIME.ToString());
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
