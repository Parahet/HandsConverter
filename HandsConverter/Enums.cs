using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandsConverter
{
    public class Enums
    {
        public enum StringType
        {
            PlayerPreview = 0,
            PostsTheAnte = 1, 
            HoleCards = 4,
            Summary = 5,
            DealtCards = 6,
            Action = 7,
            TableSummary = 8,
            HandSummary =9,
            Unknown = 10
        }

        public enum State
        {
            PreFlop = 0,
            Flop = 1,
            Turn = 2,
            River = 3
        }
    }
}
