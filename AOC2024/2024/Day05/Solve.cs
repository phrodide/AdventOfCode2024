using System.Linq;
using System.Net;
using HtmlAgilityPack;
using AOC2024;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices.Marshalling;
using System.Text.Json.Serialization;

namespace AOC2024.Day05
{

    public class Solve
    {
        public bool Test { get; set; } = false;
        public int Year { get; set; } = 2024;
        public int Day { get; set; }

        public string[] Input { get; set; } = [];

        private Dictionary<int, List<int>> befores = [];

        private List<int[]> part2 = [];

        public Solve(int day)
        {
            Day = day;
            GetDay.GetMD(Year, Day);
            string input = GetDay.GetInput(Year, Day);
            if (Test) input = GetDay.GetTest(Year, Day);
            //Input = input;
            Input = input.StringToMultilineContent();
            foreach (var item in Input[0].LinesWithContent())
            {
                var rule = item.Split('|').Select(x => int.Parse(x)).ToArray();
                if (!befores.ContainsKey(rule[1]))
                {
                    befores[rule[1]] = new List<int>();
                }
                befores[rule[1]].Add(rule[0]);
            }

        }

        public string Part1()
        {
            long count = 0;
            foreach (var array in from item in Input[1].LinesWithContent()
                                  let array = item.Split(',').Select(x => int.Parse(x)).ToArray()
                                  select array)
            {
                if (Passes(array))
                {
                    count += array[array.Length / 2];
                }
                else
                {
                    part2.Add(array);
                }
            }

            return $"{count}";
        }

        private bool Passes(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                var inspectionItem = array[i];
                if (befores.ContainsKey(inspectionItem))
                {
                    foreach (var inspectionValue in befores[inspectionItem])
                    {
                        for (int j = 0; j < array.Length; j++)
                        {
                            if (array[j] == inspectionValue && j > i)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public string Part2()
        {
            long count = 0;
            foreach (var test in from item in part2
                                 let test = new List<int>(item).ToArray()
                                 select test)
            {
                while (!Passes(test))
                {
                    bool retest = false;
                    for (int i = 0; i < test.Length; i++)
                    {
                        var inspectionItem = test[i];
                        if (befores.ContainsKey(inspectionItem))
                        {
                            foreach (var inspectionValue in befores[inspectionItem])
                            {
                                for (int j = 0; j < test.Length; j++)
                                {
                                    if (test[j] == inspectionValue && j > i)
                                    {
                                        (test[i], test[j]) = (test[j], test[i]);
                                        retest = true;
                                        break;
                                    }
                                }
                                if (retest) break;
                            }
                        }
                        if (retest) break;
                    }
                }

                int len = (test.Length / 2);
                count += test[len];
            }

            return $"{count}";
        }
    }

}

