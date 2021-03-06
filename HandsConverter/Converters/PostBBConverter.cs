﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HandsConverter.Converters
{
	public class PostBBConverter : ConverterWithPlayerName
    {
        private int count;
        public Dictionary<string, long> playersPutInAmount;


        public PostBBConverter(string str, Dictionary<string, long> playersPutInAmount)
            : base(str)
        {
            this.playersPutInAmount = playersPutInAmount;
        }

        protected override string pattern => @"(?<playerName>.*): posts big blind (?<bb>\d+)";

	    public override string ConvertToParty()
        {
            playersPutInAmount[playerName] += count;
            return PlayerName + " posts big blind [" + count.ToCommaSeparateString() + "].";
        }

	    public override string ConvertTo888()
	    {
			playersPutInAmount[playerName] += count;
			return $"{PlayerName} posts big blind [${count.ToCommaSeparateString()}]";
		}

	    public override void Initialize()
        {
            var match = new Regex(pattern).Match(str);
            playerName = match.Groups["playerName"].Value;
            count = Int32.Parse(match.Groups["bb"].Value);
        }
    }
}
