using System.Linq;
using System.Net;
using HtmlAgilityPack;
using AOC2024;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices.Marshalling;
using System.Text.Json.Serialization;

namespace AOC2024.Day09
{

    public class Solve
    {
        public bool Test { get; set; } = false;
        public int Year { get; set; } = 2024;
        public int Day { get; set; }

        public string Input { get; set; } = "";

        private Dictionary<int, List<int>> befores = [];

        private List<int[]> part2 = [];

        public Solve(int day)
        {
            Day = day;
            GetDay.GetMD(Year, Day);
            string input = GetDay.GetInput(Year, Day);
            if (Test) input = GetDay.GetTest(Year, Day);
            Input = input.Replace("\n","").Replace("\r","");

        }

        public string Part1()
        {
            long count = 0;
            List<int> array = [];
            int offset = 0;
            for (int i = 0; i < Input.Length; i+=2)
            {
                int len = Input[i] - '0';
                int len2 = i+1 != Input.Length ? Input[i+1] - '0' : 0;
                for (int k = 0; k < len; k++)
                {
                    array.Add(offset);
                }
                offset++;
                for (int k = 0; k < len2; k++)
                {
                    array.Add(-1);
                }
            }
            while (array.Contains(-1))
            {
                int pos = array.IndexOf(-1);
                int block = array.Last();
                array.RemoveAt(array.Count-1);
                array[pos] = block;
            }
            for (int i = 0; i < array.Count; i++)
            {
                count += (i * array[i]);
            }
            return $"{count}";
        }


        public string Part2()
        {
            long count = 0;
            List<int> array = [];
            int offset = 0;
            for (int i = 0; i < Input.Length; i+=2)
            {
                int len = Input[i] - '0';
                int len2 = i+1 != Input.Length ? Input[i+1] - '0' : 0;
                for (int k = 0; k < len; k++)
                {
                    array.Add(offset);
                }
                offset++;
                for (int k = 0; k < len2; k++)
                {
                    array.Add(-1);
                }
            }
            while (offset!=0)
            {
                offset--;
                int blockLen = array.Where(x => x==offset).Count();
                int blockOffset = array.IndexOf(offset);
                int emptyCount = 0;
                for (int i = 0; i < blockOffset; i++)
                {
                    if (array[i]==-1)
                        emptyCount++;
                    else
                        emptyCount = 0;
                    if (emptyCount==blockLen)
                    {
                        for (int j = 0; j < blockLen; j++)
                        {
                            array[blockOffset + j] = -1;
                        }
                        for (int j = 0; j < blockLen; j++)
                        {
                            array[i-j] = offset;
                        }
                        break;
                    }
                }
            }
            for (int i = 0; i < array.Count; i++)
            {
                if (array[i]==-1) continue;
                count += (i * array[i]);
            }
            return $"{count}";
        }

    }


}

