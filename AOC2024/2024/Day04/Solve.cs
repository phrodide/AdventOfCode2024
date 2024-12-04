using System.Linq;
using System.Net;
using HtmlAgilityPack;
using AOC2024;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices.Marshalling;

namespace AOC2024.Day04
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
            var horizontal = Input.LinesWithContent();
            List<string> vertical = [];
            for (int i = 0; i < horizontal[0].Length; i++)
            {
                vertical.Add("");//[i] = "";
                for (int j = 0; j < horizontal.Length; j++)
                {
                    vertical[i] += horizontal[j][i];
                }
            }
            List<string> horizontal_mirror = [];
            for (int i = 0; i < horizontal.Length; i++)
            {
                //horizontal_mirror.Add("");
                string result = "";
                for (int j = 0; j < horizontal[0].Length; j++)
                {
                    result += horizontal[i][horizontal[0].Length-j-1];
                }
                horizontal_mirror.Add(result);
            }
            List<string> diagonal1 = [];
            List<string> diagonal2 = [];
            //0,9
            //0,8 1,9
            //0,7 1,8 2,9
            //0,6 1,7 2,8 3,9
            //0,5
            //0,4
            //0,3
            //0,2
            //0,1
            //0,0 1,1 2,2 3,3 4,4 5,5 6,6 7,7 8,8 9,9
            //1,0 2,1
            //2,0
            //3,0
            //4,0
            //5,0
            //6,0
            //7,0
            //8,0
            //9,0

            //0,0
            //1,0 0,1
            //2,0 1,1 0,2
            List<(int,int)> initials = [];
            for (int i = horizontal[0].Length-1; i > 0; i--)
            {
                initials.Add((0,i));
            }
            initials.Add((0,0));
            for (int i = 1; i < horizontal.Length; i++)
            {
                initials.Add((i,0));
            }
            foreach (var item in initials)
            {
                int x = item.Item1;
                int y = item.Item2;
                int maxLen = horizontal.Length;
                string result1 = "";
                string result2 = "";
                while (true)
                {
                    if (x >= maxLen || y >= maxLen) break;
                    result1 += horizontal[x][y];
                    result2 += horizontal_mirror[x][y];
                    x++;
                    y++;
                }
                diagonal1.Add(result1);
                diagonal2.Add(result2);
            }
            for (int x = 0; x < horizontal.Length; x++)
            {
                for (int i = 0; i < horizontal[0].Length-3; i++)
                {
                    if (horizontal[x][i..(i+4)]=="XMAS"|| horizontal[x][i..(i+4)]=="SAMX")
                    {
                        count++;
                    }
                }                
            }

            for (int x = 0; x < vertical.Count; x++)
            {
                for (int i = 0; i < vertical[0].Length-3; i++)
                {
                    if (vertical[x][i..(i+4)]=="XMAS"|| vertical[x][i..(i+4)]=="SAMX")
                    {
                        count++;
                    }
                }                
            }
            for (int x = 0; x < diagonal1.Count; x++)
            {
                for (int i = 0; i < diagonal1[x].Length-3; i++)
                {
                    if (diagonal1[x][i..(i+4)]=="XMAS"|| diagonal1[x][i..(i+4)]=="SAMX")
                    {
                        count++;
                    }
                }                
                for (int i = 0; i < diagonal2[x].Length-3; i++)
                {
                    if (diagonal2[x][i..(i+4)]=="XMAS"|| diagonal2[x][i..(i+4)]=="SAMX")
                    {
                        count++;
                    }
                }                
            }
            return $"{count}";
        }
        
        public string Part2()
        {
            long count = 0;
            var horizontal = Input.LinesWithContent();
            
            for (int i = 1; i < horizontal.Length-1; i++)
            {
                for (int j = 1; j < horizontal.Length-1; j++)
                {
                    if (horizontal[i][j]=='A')
                    {
                        char a = horizontal[i-1][j-1];
                        char b = horizontal[i-1][j+1];
                        char c = horizontal[i+1][j-1];
                        char d = horizontal[i+1][j+1];
                        if (a==b && c==d)
                        {
                            if (a=='M' && c=='S')
                            {
                                count++;
                            }
                            else if (a=='S' && c=='M')
                            {
                                count++;
                            }
                            //one
                        }
                        else if (a==c && b==d)
                        {
                            //two
                            if (a=='M' && b=='S')
                            {
                                count++;
                            }
                            else if (a=='S' && b=='M')
                            {
                                count++;
                            }
                        }
                    }
                }
            }
            
            
            return $"{count}";
        }
        
    }

}

