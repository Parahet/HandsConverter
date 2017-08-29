using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HandsConverter
{
	public class CSVParser
    {
        private string csvPath;
        public CSVParser(string filePath)
        {
            csvPath = filePath;
        }

        public Dictionary<string, string> GetPartyPlayers()
        {
            var result = new Dictionary<string, string>();
            using (var reader = new StreamReader(csvPath))
            {
                var headers = reader.ReadLine().Split(',');
                int psCol=-1;
                int partyCol=-1;
                for (int i = 0; i < headers.Count(); i++)
                {
                    if (headers[i].ToLower() == "pokerstars")
                        psCol = i;
                    else if (headers[i].ToLower() == "party")
                        partyCol = i;
                }

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    if (values[partyCol].Trim() != "")
                        result.Add(values[psCol].Trim(), values[partyCol].Trim()); 
                }
            }
            return result;
        }
	    public Dictionary<string, string> Get888Players()
	    {
		    var result = new Dictionary<string, string>();
		    using (var reader = new StreamReader(csvPath))
		    {
			    var headers = reader.ReadLine().Split(',');
			    int psCol = -1;
			    int _888Col = -1;
			    for (int i = 0; i < headers.Count(); i++)
			    {
				    if (headers[i].ToLower() == "pokerstars")
					    psCol = i;
				    else if (headers[i].ToLower() == "888")
					    _888Col = i;
			    }

			    while (!reader.EndOfStream)
			    {
				    var line = reader.ReadLine();
				    var values = line.Split(',');
				    if (values[_888Col].Trim() != "")
					    result.Add(values[psCol].Trim(), values[_888Col].Trim());
			    }
		    }
		    return result;
	    }
	}
}
