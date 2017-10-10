using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HandsConverter
{
	public class Helper
    {
        private static Dictionary<string, string> partyPlayers;

	    public static Dictionary<string, string> Players
	    {
		    get
		    {
			    if ((partyPlayers == null) &&(_888Players != null))
				    return _888Players;
			    if ((partyPlayers != null) && (_888Players == null))
				    return _888Players;
				throw new Exception("Need to Initialize Players");
			}
	    }


		public static Dictionary<string, string> PartyPlayers
        {
            get
            {
                if (partyPlayers == null)
                {
                    partyPlayers = GetPartyPlayers();
	                _888Players = null;
                }
                return partyPlayers;
            }
        }
	    private static Dictionary<string, string> _888Players;

	    public static Dictionary<string, string> Players888
	    {
		    get
		    {
			    if (_888Players == null)
			    {
				    _888Players = Get888Players();
				    partyPlayers = null;
			    }
			    return _888Players;
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
	    public static Dictionary<string, string> Get888Players()
	    {
		    var dir = new DirectoryInfo(Environment.CurrentDirectory);
		    var files = dir.GetFiles("*.csv");
		    var file = files.Any(f => f.Name.ToLower().Contains("players"))
			    ? files.First(f => f.Name.ToLower().Contains("players"))
			    : files.First();
		    return new CSVParser(file.FullName).Get888Players();
	    }
	}
}
