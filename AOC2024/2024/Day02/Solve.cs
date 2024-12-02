using System.Linq;
using System.Net;
using HtmlAgilityPack;
using AOC2024;

namespace AOC2024.Day02
{

    public class Solve
    {
        public bool Test { get; set; } = false;
        public int Year { get; set; } = 2024;
        public int Day { get; set; }

        public string Input { get; set; }
        public Solve(int day)
        {
            Day = day;
            GetDay.GetMD(Year,Day);
            string input = GetDay.GetInput(Year,Day);
            if (Test) input = GetDay.GetTest(Year,Day);
            Input = input;
        }

        public string Part1()
        {
            var left = Input.LinesWithContent().Select(x => int.Parse(x.Split(' ')[0])).OrderBy(x => x).ToArray();
            var right = Input.LinesWithContent().Select(x => int.Parse(x.Split(' ',StringSplitOptions.RemoveEmptyEntries)[1])).OrderBy(x => x).ToArray();

            int sum = 0;
            for (int i = 0; i < left.Length; i++)
            {
                sum += Math.Abs(left[i] - right[i]);
            }
            

            return $"{sum}";
        }
        
        public string Part2()
        {
            var left = Input.LinesWithContent().Select(x => int.Parse(x.Split(' ')[0])).OrderBy(x => x).ToArray();
            var right = Input.LinesWithContent().Select(x => int.Parse(x.Split(' ',StringSplitOptions.RemoveEmptyEntries)[1])).OrderBy(x => x).ToArray();

            int sum = 0;
            for (int i = 0; i < left.Length; i++)
            {
                var count = (from r in right where r == left[i] select r).Count();
                sum += Math.Abs(left[i] * count);
            }
            

            return $"{sum}";
        }
    }

}

