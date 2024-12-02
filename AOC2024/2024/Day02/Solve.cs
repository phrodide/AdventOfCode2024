using System.Linq;
using System.Net;
using HtmlAgilityPack;
using AOC2024;
using System.Security.Cryptography.X509Certificates;

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
            var reports = Input.LinesWithContent();
            int count = 0;
            foreach (var report in reports)
            {
                var array = report.Split(' ').Select(x => int.Parse(x)).ToArray();
                bool known = false;
                bool isIncreasing = true;
                bool safe = true;
                for (int i = 1; i < array.Length; i++)
                {
                    int diff = array[i] - array[i-1];
                    int abs_diff = Math.Abs(diff);
                    if (diff < 0)
                    {
                        //decreasing
                        if (known==true && isIncreasing==true)
                        {
                            safe = false;
                            break;
                        }
                        known = true;
                        isIncreasing = false;
                    }
                    if (diff > 0)
                    {
                        //increasing
                        if (known==true && isIncreasing==false)
                        {
                            safe = false;
                            break;
                        }
                        known = true;
                        isIncreasing = true;
                    }
                    if (abs_diff < 1 || abs_diff > 3)
                    {
                        safe = false;
                        break;
                    }
                    
                    
                }
                if (safe) count++;
            }
            

            return $"{count}";
        }
        
        public string Part2()
        {
            var reports = Input.LinesWithContent();
            int count = 0;
            foreach (var report in reports)
            {
                var array = report.Split(' ').Select(x => int.Parse(x)).ToArray();
                bool safe = isReportSafe(array);
                if (safe) count++;
            }

          

            return $"{count}";
        }
        public bool isReportSafe(int[] array, bool initial = true)
        {
                bool known = false;
                bool isIncreasing = true;
                bool safe = true;
                for (int i = 1; i < array.Length; i++)
                {
                    int diff = array[i] - array[i-1];
                    int abs_diff = Math.Abs(diff);
                    if (diff < 0)
                    {
                        //decreasing
                        if (known==true && isIncreasing==true)
                        {
                            if (initial==true)
                            {
                                //try to remove one item and see if it works...
                                for (int j = 0; j < array.Length; j++)
                                {
                                    List<int> a = new(array);
                                    a.RemoveAt(j);
                                    if (isReportSafe(a.ToArray(), false))
                                        return true;
                                }
                            }
                            safe = false;
                            break;
                        }
                        known = true;
                        isIncreasing = false;
                    }
                    if (diff > 0)
                    {
                        //increasing
                        if (known==true && isIncreasing==false)
                        {
                            if (initial==true)
                            {
                                //try to remove one item and see if it works...
                                for (int j = 0; j < array.Length; j++)
                                {
                                    List<int> a = new(array);
                                    a.RemoveAt(j);
                                    if (isReportSafe(a.ToArray(), false))
                                        return true;
                                }
                            }
                            safe = false;
                            break;
                        }
                        known = true;
                        isIncreasing = true;
                    }
                    if (abs_diff < 1 || abs_diff > 3)
                    {
                            if (initial==true)
                            {
                                //try to remove one item and see if it works...
                                for (int j = 0; j < array.Length; j++)
                                {
                                    List<int> a = new(array);
                                    a.RemoveAt(j);
                                    if (isReportSafe(a.ToArray(), false))
                                        return true;
                                }
                            }
                        safe = false;
                        break;
                    }
                    
                    
                }
                return safe;
        }

    }

}

