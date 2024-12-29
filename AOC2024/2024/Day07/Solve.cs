using System.Linq;
using System.Net;
using HtmlAgilityPack;
using AOC2024;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices.Marshalling;

namespace AOC2024.Day07
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
            long count = 0;
            var equations = Input.LinesWithContent();
            foreach (var equation in equations)
            {
                List<long> elements = equation.Replace(":","").Split(' ').Select( x => Convert.ToInt64(x)).ToList();
                long result = elements[0];
                elements.RemoveAt(0);
                var possible = CalcTree(elements.First(),elements[1..]);
                if (possible.Any(x => x == result)) count+=result;

            }
            return $"{count}";
        }

        public List<long> CalcTree(long left, List<long> rights, bool part2 = false)
        {
            if (rights.Count==0) return [ left ] ;
            var plus = left + rights.First();
            var mult = left * rights.First();
            var concat = Convert.ToInt64(left.ToString() + rights.First().ToString());
            var subsp = CalcTree(plus, rights[1..], part2);
            var subsm = CalcTree(mult, rights[1..], part2);
            var subsc = CalcTree(concat, rights[1..], part2);
            List<long> result = subsp;
            result.AddRange(subsm);
            if (part2) result.AddRange(subsc);
            return result;
        }
        
        public string Part2()
        {
            long count = 0;
            var equations = Input.LinesWithContent();
            foreach (var equation in equations)
            {
                List<long> elements = equation.Replace(":","").Split(' ').Select( x => Convert.ToInt64(x)).ToList();
                long result = elements[0];
                elements.RemoveAt(0);
                var possible = CalcTree(elements.First(),elements[1..], true);
                if (possible.Any(x => x == result)) count+=result;

            }
            return $"{count}";
        }
        
    }

}

