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
            foreach (var item in Input[1].LinesWithContent())
            {
                var array = item.Split(',').Select(x => int.Parse(x)).ToArray();
                var passes = Passes(array);
                if (passes)
                {
                    int len = (array.Length / 2);
                    count += array[len];
                }
            }
            return $"{count}";
        }

        private bool Passes(int[] array, bool addToPart = true)
        {
            bool passes = true;
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
                                if (addToPart) part2.Add(array);
                                passes = false;
                                break;
                            }
                        }
                        if (passes == false)
                            break;
                    }
                }
                if (passes == false)
                    break;
            }
            return passes;
        }

        public string Part2()
        {
            long count = 0;
            foreach (var item in part2)
            {
                var test = new List<int>(item).ToArray();
                while (!Passes(test, false))
                {
                    //find first error.  
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
                                        var temp = test[j];
                                        test[j] = test[i];
                                        test[i] = temp;
                                        //test = Bubble(test, i);
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

                //we need to bubble the first failure and see if it passes...
                //var b = Bubble(item, 0);
                //var c = Bubble(item, 1);
                //var d = Bubble(item, 2);
            }
            return $"{count}";
        }

        private int[] Bubble(int[] a, int offset)
        {
            //0 moves 1 to 0, 0 to 1, and appends everything else.
            //1 prepends 0 to 0, 1 to 2, 2 to 1, and appends everything else.
            //n prepends everything, and should never happen.
            List<int> newList = [];
            for (int i = 0; i < offset; i++)
            {
                newList.Add(a[i]);
            }
            newList.Add(a[offset + 1]);
            newList.Add(a[offset]);
            //(a[offset + 1], a[offset]) = (a[offset], a[offset + 1]);
            for (int i = offset + 2; i < a.Length; i++)
            {
                newList.Add(a[i]);
            }
            return newList.ToArray();
        }

    }

}

