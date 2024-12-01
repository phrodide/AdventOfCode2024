using System.Linq;
using System.Net;
using HtmlAgilityPack;

namespace AOC2024.Day01
{

    public class Solve
    {
        public bool Test { get; set; } = false;
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
            input = input.Replace("\r\n","\n").Replace("\n\n","\n");
            input = input.Substring(0,input.Length-1);
            var left = input.Split('\n').Select(x => int.Parse(x.Split(' ')[0])).OrderBy(x => x).ToArray();
            var right = input.Split('\n').Select(x => int.Parse(x.Split(' ',StringSplitOptions.RemoveEmptyEntries)[1])).OrderBy(x => x).ToArray();

            List<int> diffs = new();
            int sum = 0;
            for (int i = 0; i < left.Count(); i++)
            {
                diffs.Add(Math.Abs(left[i] - right[i]));
                sum += Math.Abs(left[i] - right[i]);
            }
            

            return $"{sum}";
        }
        
        public string Part2()
        {
            GetDay.GetMD(Year,Day);
            string input = GetDay.GetInput(Year,Day);
            if (Test) input = GetDay.GetTest(Year,Day);
            input = input.Replace("\r\n","\n").Replace("\n\n","\n");
            input = input.Substring(0,input.Length-1);
            var left = input.Split('\n').Select(x => int.Parse(x.Split(' ')[0])).OrderBy(x => x).ToArray();
            var right = input.Split('\n').Select(x => int.Parse(x.Split(' ',StringSplitOptions.RemoveEmptyEntries)[1])).OrderBy(x => x).ToArray();

            List<int> diffs = new();
            int sum = 0;
            for (int i = 0; i < left.Count(); i++)
            {
                var count = (from r in right where r == left[i] select r).Count();
                sum += Math.Abs(left[i] * count);
            }
            

            return $"{sum}";
        }
    }

}

