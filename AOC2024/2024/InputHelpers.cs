using System.Linq;

namespace AOC2024{
    public static class InputSanitation
    {
        public static string[] LinesWithContent(this string input)
        {
            var output = from s in input.Replace("\r\n","\n").Split('\n') where s.Trim().Length!=0 select s.Trim();
            return output.ToArray();
        }

        public static string[] StringToMultilineContent(this string input)
        {
            var output = from s in input.Replace("\r\n","\n").Replace("\n\n","˜").Split('˜') where s.Trim().Length!=0 select s.Trim();
            return output.ToArray();
        }

        public static Dictionary<string,string> LineArrayToDictionary(this string[] input, char delimiter)
        {
            Dictionary<string,string> output = [];

            foreach (var line in input)
            {
                int offset = line.IndexOf(':');
                if (offset == -1) continue;

                output.Add(line[..offset], line[(offset + 1)..]);
            }
            return output;        
        }

        public static Dictionary<string,T> ConvertTo<T>(this string[] input, char delimiter) where T : IInputConvertTo,new()
        {
            Dictionary<string,T> output = [];
            foreach (var line in input)
            {
                int offset = line.IndexOf(':');
                if (offset == -1) continue;
                T data = new()
                {
                    Input = line[(offset + 1)..]
                };
                output.Add(line[..offset],data);
            }
            return output;
        }

        public static Dictionary<string,List<string>> DictionaryToArray(this Dictionary<string,string> input, char delimiter)
        {
            Dictionary<string,List<string>> output = [];

            foreach (var entry in input)
            {
                List<string> array = [.. entry.Value.Split(delimiter,StringSplitOptions.RemoveEmptyEntries)];
                output.Add(entry.Key,array);
            }
            return output;
        }

        //2023 Days:
        //1 = single token per line (had to further sanitize)
        //2 = header followed by token list (header:array;)    Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
        //3 = bitmap. Best if you can set rules against the bitmap    467..114..
        //4 = header followed by two token lists (header:array | array )     Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
        //5 = multiline
        //6 = header followed by a list of tokens

    }

    public interface IInputConvertTo
    {
        public string Input { get; set; }
    }
}