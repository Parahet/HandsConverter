using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HandsConverter
{
    public class Helper
    {
        private static Dictionary<string, string> players;

        public static Dictionary<string, string> Players
        {
            get
            {
                if (players == null)
                {
                    players = Helper.GetPartyPlayers();
                }
                return players;
            }
        }

        public static Dictionary<string, string> GetPartyPlayers()
        {
            var dir = new DirectoryInfo(Environment.CurrentDirectory);
            var files = dir.GetFiles("*.csv");
            var file = files.Any(f => f.Name.ToLower().Contains("players"))
                ? files.First(f => f.Name.ToLower().Contains("players"))
                : files.First();
            return new CSVParser(file.FullName).GetPartyPlayers();
        }
    }
}
