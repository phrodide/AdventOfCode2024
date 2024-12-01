using System.Net;
using HtmlAgilityPack;

namespace AOC2024
{
    public static class GetDay
    {
        public static string GetMD(int year, int day)
        {
            var parent = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).GetDirectories().Where(x => x.Name=="Inputs").Single();

            if (System.IO.File.Exists(parent + $"/{year}_{day}_html.txt"))
            {
                return System.IO.File.ReadAllText(parent + $"/{year}_{day}_html.txt");
            }



            var sessionKey = System.IO.File.ReadAllLines(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()) + "/.session")[0];
            var baseAddress = new Uri("https://adventofcode.com");
            var cookieContainer = new CookieContainer();
            HttpClientHandler handler = new HttpClientHandler() { CookieContainer = cookieContainer };
            HttpClient client = new HttpClient(handler) { BaseAddress = baseAddress };
            cookieContainer.Add(baseAddress, new Cookie("session",sessionKey));
            var result = client.GetStringAsync($"/{year}/day/{day}");
            result.Wait();
            System.IO.File.WriteAllText(parent.FullName + $"/{year}_{day}_html.txt",result.Result);
            
            
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            var parse = System.IO.File.ReadAllText(parent + $"/{year}_{day}_html.txt");
            var doc = new HtmlDocument();
            doc.LoadHtml(parse);
            var article = (from a in doc.DocumentNode.Descendants("article") where a.Attributes["class"].Value == "day-desc" select a).Single();
            var title = (from c in article.ChildNodes where c.Name == "h2" select c.InnerText).First();
            string example = "";
            sb.AppendLine("# " + title);
            bool p_example = false;
            foreach (var child in article.ChildNodes)
            {
                if (child.Name == "p")
                {
                    if (child.InnerText.Contains("example"))
                    {                     
                        p_example = true;
                    }
                    else 
                        p_example = false;
                    sb.AppendLine(child.InnerHtml);
                    sb.AppendLine();

                }
                else if (child.Name == "pre")
                {
                    sb.AppendLine("```shell");
                    sb.AppendLine(child.InnerText);
                    sb.AppendLine("```");
                    if (p_example == true)
                    {
                        example = child.InnerText;
                    }
                }
            }
            System.IO.File.WriteAllText(parent + $"/{year}_{day}.md", sb.ToString());
            if (example.Length > 5)
            {
                System.IO.File.WriteAllText(parent + $"/{year}_{day}_test.txt", example);
            }

            return sb.ToString();

        }

        public static string GetTest(int year, int day)
        {
            var parent = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).GetDirectories().Where(x => x.Name=="Inputs").Single();

            if (System.IO.File.Exists(parent + $"/{year}_{day}_test.txt"))
            {
                return System.IO.File.ReadAllText(parent + $"/{year}_{day}_test.txt");
            }
            return "";
        }

        public static string GetInput(int year, int day)
        {
            var parent = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).GetDirectories().Where(x => x.Name=="Inputs").Single();

            if (System.IO.File.Exists(parent + $"/{year}_{day}_input.txt"))
            {
                return System.IO.File.ReadAllText(parent + $"/{year}_{day}_input.txt");
            }



            var sessionKey = System.IO.File.ReadAllLines(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()) + "/.session")[0];
            var baseAddress = new Uri("https://adventofcode.com");
            var cookieContainer = new CookieContainer();
            HttpClientHandler handler = new HttpClientHandler() { CookieContainer = cookieContainer };
            HttpClient client = new HttpClient(handler) { BaseAddress = baseAddress };
            cookieContainer.Add(baseAddress, new Cookie("session",sessionKey));
            var result = client.GetStringAsync($"/{year}/day/{day}/input");
            result.Wait();
            System.IO.File.WriteAllText(parent.FullName + $"/{year}_{day}_input.txt",result.Result);
            return result.Result;
        }
    }
}