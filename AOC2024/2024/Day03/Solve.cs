using System.Linq;
using System.Net;
using HtmlAgilityPack;
using AOC2024;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices.Marshalling;

namespace AOC2024.Day03
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
            var Instructions = Input.Replace("mul(","`").Split('`').Select(x => x.Substring(0,x.IndexOf(')')==-1 ? 0 : x.IndexOf(')')));
            long count = 0;
            foreach (var instr in Instructions)
            {
                if (instr.Length==0) continue;
                var nums = instr.Split(',');
                if (nums.Length!=2) continue;
                bool invalid = false;
                foreach(char c in instr)
                {
                    switch (c)
                    {
                        case '0':
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                        case '6':
                        case '7':
                        case '8':
                        case '9':
                        case ',':
                            break;
                        default:
                            invalid = true;
                            break;
                    }
                }    
                if (!invalid)
                {
                    //passed first sanity check. Now let's try to parse it.
                    string num1 = instr.Split(',')[0];
                    string num2 = instr.Split(',')[1];
                    if (num1.Length >= 1 && num1.Length <= 3 && num2.Length >= 1 && num2.Length <= 3 && int.TryParse(num1, out int inum1) && int.TryParse(num2, out int inum2))
                    {
                        count += inum1*inum2;
                    }
                }            
            }
            

            return $"{count}";
        }
        
        public string Part2()
        {
            var Instructions = Input.Replace("mul(","`").Replace("do(","`˜").Replace("don't(","`|").Split('`').Select(x => x.Substring(0,x.IndexOf(')')==-1 ? 0 : x.IndexOf(')')));
            long count = 0;
            bool enable = true;
            foreach (var instr in Instructions)
            {
                if (instr.Length==0) continue;
                if (instr[0]=='˜')
                {
                    enable = true;
                    continue;
                } 
                if (instr[0]=='|')
                {
                    enable = false;
                    continue;
                }
                var nums = instr.Split(',');
                if (nums.Length!=2) continue;
                bool invalid = false;
                foreach(char c in instr)
                {
                    switch (c)
                    {
                        case '0':
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                        case '6':
                        case '7':
                        case '8':
                        case '9':
                        case ',':
                            break;
                        default:
                            invalid = true;
                            break;
                    }
                }    
                if (!invalid)
                {
                    //passed first sanity check. Now let's try to parse it.
                    string num1 = instr.Split(',')[0];
                    string num2 = instr.Split(',')[1];
                    if (num1.Length >= 1 && num1.Length <= 3 && num2.Length >= 1 && num2.Length <= 3 && int.TryParse(num1, out int inum1) && int.TryParse(num2, out int inum2))
                    {
                        if (enable)
                            count += inum1*inum2;
                    }
                }            
            }
            

            return $"{count}";
        }
        
    }

}

