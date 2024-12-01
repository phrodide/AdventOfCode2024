using System.Linq;
using System.Net;
using HtmlAgilityPack;

namespace AOC2024.Day01
{

    public class Solve
    {
        public bool Test { get; set; } = true;
        public int Year { get; set; } = 2024;
        public int Day { get; set; }
        public Solve(int day)
        {
            Day = day;
        }

        public string Part1()
        {
            GetDay.GetMD(Year,Day);
            string input = GetDay.GetInput(Year,Day);
            if (Test) input = GetDay.GetTest(Year,Day);
            

            return "";
        }
        
        public string Part2()
        {
            return "";
        }
    }

}

