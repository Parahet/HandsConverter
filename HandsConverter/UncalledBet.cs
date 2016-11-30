using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandsConverter
{
    public class UncalledBet
    {
        public string playerName;
        public long uncalledBet;

        public UncalledBet(string playerName, long uncalledBet)
        {
            this.playerName = playerName;
            this.uncalledBet = uncalledBet;
        }
    }
}
