using System.Linq;
using System.Net;
using HtmlAgilityPack;
using AOC2024;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices.Marshalling;
using System.Text.Json.Serialization;

namespace AOC2024.Day08
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
            Input = input.LinesWithContent();

        }

        public string Part1()
        {
            long count = 0;
            List<(int x, int y, int a)> items = [];
            for (int i = 0; i < Input.Length; i++)
            {
                for (int j = 0; j < Input[i].Length; j++)
                {
                    if (Input[i][j]!='.' && Input[i][j]!='#')
                    {
                        items.Add((i,j,Input[i][j]));
                    }
                }
            }
            List<(int x, int y)> antinodes = [];
            var frequencies = from f in items group f by f.a;
            foreach (var frequency in frequencies)
            {
                var list = frequency.ToArray();
                for (int i = 0; i < list.Length; i++)
                {
                    for (int j = i+1; j < list.Length; j++)
                    {
                        //first test is list[i]
                        //second test is list[j]
                        //this will not duplicate. if the antinode is in scope, add to the antinode list.
                        int dx = list[i].x - list[j].x;
                        int dy = list[i].y - list[j].y;
                        int newX = list[i].x + dx;
                        int newX2 = list[j].x - dx;
                        int newY = list[i].y + dy;
                        int newY2 = list[j].y - dy;
                        if (newX >= 0 && newX < Input.Length && newY >= 0 && newY < Input[0].Length)
                        {
                            antinodes.Add((newX,newY));
                        }
                        if (newX2 >= 0 && newX2 < Input.Length && newY2 >= 0 && newY2 < Input[0].Length)
                        {
                            antinodes.Add((newX2,newY2));
                        }
                    }
                }
            }
            count = antinodes.Distinct().Count();
            return $"{count}";
        }


        public string Part2()
        {
            long count = 0;
            List<(int x, int y, int a)> items = [];
            for (int i = 0; i < Input.Length; i++)
            {
                for (int j = 0; j < Input[i].Length; j++)
                {
                    if (Input[i][j]!='.' && Input[i][j]!='#')
                    {
                        items.Add((i,j,Input[i][j]));
                    }
                }
            }
            List<(int x, int y)> antinodes = [];
            var frequencies = from f in items group f by f.a;
            foreach (var frequency in frequencies)
            {
                var list = frequency.ToArray();
                for (int i = 0; i < list.Length; i++)
                {
                    for (int j = i+1; j < list.Length; j++)
                    {
                        //first test is list[i]
                        //second test is list[j]
                        //this will not duplicate. if the antinode is in scope, add to the antinode list.
                        antinodes.Add((list[i].x,list[i].y));
                        antinodes.Add((list[j].x,list[j].y));
                        int dx = list[i].x - list[j].x;
                        int dy = list[i].y - list[j].y;
                        int newX = list[i].x;
                        int newY = list[i].y;
                        while (true)
                        {
                            newX += dx;
                            newY += dy;
                            if (newX >= 0 && newX < Input.Length && newY >= 0 && newY < Input[0].Length)
                            {
                                antinodes.Add((newX,newY));
                            }
                            else
                            {
                                break;
                            }
                        }                        
                        newX = list[j].x;
                        newY = list[j].y;
                        while (true)
                        {
                            newX -= dx;
                            newY -= dy;
                            if (newX >= 0 && newX < Input.Length && newY >= 0 && newY < Input[0].Length)
                            {
                                antinodes.Add((newX,newY));
                            }
                            else
                            {
                                break;
                            }
                        }                        
                    }
                }
            }
            count = antinodes.Distinct().Count();
            return $"{count}";
        }

    }


}

