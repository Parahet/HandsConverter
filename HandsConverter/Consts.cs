using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandsConverter
{
    public class Consts
    {
        public const string Card = @"[2-9,A,K,Q,J,T][c,s,d,h]";
        public const string HHPattern = @"HH\d+ .*.txt";

        public const long tournamentDelta = 1000000000;
        public const long handNumberDelta = 155929861006;
    }
}
