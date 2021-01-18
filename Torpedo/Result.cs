using System;
using System.Collections.Generic;
using System.Text;

namespace Torpedo
{
   public class Result
    {
        public Result(string Name, string Rounds, string Result)
        {
            username = Name;
            rounds = Rounds;
            result = Result;
        }
        public string username { get; set; }
        public string rounds { get; set; }
        public string result { get; set; }
    }
}
