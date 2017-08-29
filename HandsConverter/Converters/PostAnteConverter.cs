using System;
using System.Text.RegularExpressions;

namespace HandsConverter.Converters
{
	public class PostAnteConverter : ConverterWithPlayerName
    {
        public int ante;


        public PostAnteConverter(string str) : base(str)
        {
        }

        protected override string pattern => @"(?<playerName>.*): posts the ante (?<ante>\d+)";

	    public override string ConvertToParty()
        {
            return PlayerName + " posts ante [" + ante.ToCommaSeparateString() + "]";
        }

	    public override string ConvertTo888()
	    {
		    return $"{PlayerName} posts ante [${ante.ToCommaSeparateString()}]";
	    }

	    public override void Initialize()
        {
            var match = new Regex(pattern).Match(str);
            playerName = match.Groups["playerName"].Value;
            ante = Int32.Parse(match.Groups["ante"].Value);
        }
    }
}
