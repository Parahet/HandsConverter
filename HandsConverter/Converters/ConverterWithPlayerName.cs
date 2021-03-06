﻿namespace HandsConverter.Converters
{
	public abstract class ConverterWithPlayerName : Converter
    {
        public string playerName;

        protected ConverterWithPlayerName(string str) : base(str)
        {
        }
        public string PlayerName {
            get
            {
                var players = Helper.Players;
                return players.ContainsKey(playerName)
                    ? players[playerName]
                    : playerName.GetHashCode().ToString();
            }
        }
    }
}
