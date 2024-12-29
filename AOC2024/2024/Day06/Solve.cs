using System.Linq;
using System.Net;
using HtmlAgilityPack;
using AOC2024;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices.Marshalling;
using System.Text.Json.Serialization;

namespace AOC2024.Day06
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
            List<(int x, int y)> visited = [];
            (int x, int y, Facing facing) guard = (0,0,Facing.Up);
                //find the guard
            for (int i = 0; i < Input.Length; i++)
            {
                for (int j = 0; j < Input[i].Length; j++)
                {
                    if (Input[i][j]!='.' && Input[i][j]!='#')
                    {
                        char direction = Input[i][j];
                        Facing f = Facing.Up;
                        switch (direction)
                        {
                            case '^': f = Facing.Up; break;
                            case 'v': f = Facing.Down; break;
                            case '>': f = Facing.Right; break;
                            case '<': f = Facing.Left; break;
                            default: break;
                        }
                        guard = (i,j,f);
                        break;
                    }                    
                }
            }
            bool leftArea = false;
            while (leftArea==false)
            {
                //put in list
                visited.Add((guard.x,guard.y));
                //move according to the rules
                switch (guard.facing)
                {
                    case Facing.Up:
                        if (guard.x-1 < 0)
                        {
                            leftArea = true;
                        }
                        else if (Input[guard.x-1][guard.y] == '#') 
                        {
                            guard = (guard.x,guard.y,Facing.Right);
                        }
                        else 
                        {
                            guard = (guard.x-1, guard.y, guard.facing);
                        }
                        break;
                    case Facing.Down:
                        if (guard.x+1 >= Input.Length)
                        {
                            leftArea = true;
                        }
                        else if (Input[guard.x+1][guard.y] == '#') 
                        {
                            guard = (guard.x,guard.y,Facing.Left);
                        }
                        else 
                        {
                            guard = (guard.x+1, guard.y, guard.facing);
                        }
                        break;
                    case Facing.Left:
                        if (guard.y-1 < 0)
                        {
                            leftArea = true;
                        }
                        else if (Input[guard.x][guard.y-1] == '#') 
                        {
                            guard = (guard.x,guard.y,Facing.Up);
                        }
                        else 
                        {
                            guard = (guard.x, guard.y-1, guard.facing);
                        }
                        break;
                    case Facing.Right:
                        if (guard.y+1 >= Input[0].Length)
                        {
                            leftArea = true;
                        }
                        else if (Input[guard.x][guard.y+1] == '#') 
                        {
                            guard = (guard.x,guard.y,Facing.Down);
                        }
                        else 
                        {
                            guard = (guard.x, guard.y+1, guard.facing);
                        }
                        break;
                }
                //exit if it left the area

            }
            count = visited.Distinct().Count();
            return $"{count}";
        }


        public string Part2()
        {
            long count = 0;
            for (int i = 0; i < Input.Length; i++)
            {
                for (int j = 0; j < Input[0].Length; j++)
                {
                    if (Input[i][j]=='.')
                    {
                        //ok to substitute
                        if (GuardWalk(i,j))
                        {
                            count++;
                        }
                    }
                }
            }
            return $"{count}";
        }

        public bool GuardWalk(int NewX, int NewY)
        {
            string[] Input2 = Input.ToArray();
            var b = Input2[NewX].ToCharArray();
            b[NewY] = '#';
            Input2[NewX] = new string(b);
            long count = 0;
            List<(int x, int y)> visited = [];
            (int x, int y, Facing facing) guard = (0,0,Facing.Up);
                //find the guard
            for (int i = 0; i < Input2.Length; i++)
            {
                for (int j = 0; j < Input2[i].Length; j++)
                {
                    if (Input2[i][j]!='.' && Input2[i][j]!='#')
                    {
                        char direction = Input2[i][j];
                        Facing f = Facing.Up;
                        switch (direction)
                        {
                            case '^': f = Facing.Up; break;
                            case 'v': f = Facing.Down; break;
                            case '>': f = Facing.Right; break;
                            case '<': f = Facing.Left; break;
                            default: break;
                        }
                        guard = (i,j,f);
                        break;
                    }                    
                }
            }
            bool leftArea = false;
            while (leftArea==false && count < 10000)
            {
                //put in list
                count ++;
                visited.Add((guard.x,guard.y));
                //move according to the rules
                switch (guard.facing)
                {
                    case Facing.Up:
                        if (guard.x-1 < 0)
                        {
                            leftArea = true;
                        }
                        else if (Input2[guard.x-1][guard.y] == '#') 
                        {
                            guard = (guard.x,guard.y,Facing.Right);
                        }
                        else 
                        {
                            guard = (guard.x-1, guard.y, guard.facing);
                        }
                        break;
                    case Facing.Down:
                        if (guard.x+1 >= Input2.Length)
                        {
                            leftArea = true;
                        }
                        else if (Input2[guard.x+1][guard.y] == '#') 
                        {
                            guard = (guard.x,guard.y,Facing.Left);
                        }
                        else 
                        {
                            guard = (guard.x+1, guard.y, guard.facing);
                        }
                        break;
                    case Facing.Left:
                        if (guard.y-1 < 0)
                        {
                            leftArea = true;
                        }
                        else if (Input2[guard.x][guard.y-1] == '#') 
                        {
                            guard = (guard.x,guard.y,Facing.Up);
                        }
                        else 
                        {
                            guard = (guard.x, guard.y-1, guard.facing);
                        }
                        break;
                    case Facing.Right:
                        if (guard.y+1 >= Input2[0].Length)
                        {
                            leftArea = true;
                        }
                        else if (Input2[guard.x][guard.y+1] == '#') 
                        {
                            guard = (guard.x,guard.y,Facing.Down);
                        }
                        else 
                        {
                            guard = (guard.x, guard.y+1, guard.facing);
                        }
                        break;
                }
                //exit if it left the area

            }
            if (count < 10000)
            {
                return false;
            }
            return true;

        }


    }

    public enum Facing
    {
        Up = 0,
        Right = 90,
        Down = 180,
        Left = 270
    }

}

