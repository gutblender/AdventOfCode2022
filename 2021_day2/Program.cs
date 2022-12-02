using System;
using System.IO;

namespace _2021_day2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("2021 Day 1!");
            int pos = 0, depth = 0;
            int p2 = 0, d2 = 0;

            using (var stream = File.OpenRead("input.txt"))
            using (var sr = new StreamReader(stream))
            {
                string line = null;

                for (int aim = 0
                    ;  null != (line = sr.ReadLine())
                    ; )
                {
                    var match = System.Text.RegularExpressions.Regex.Match(line, 
                        @"(?<dir>forward|down|up) (?<far>[0-9]+)");

                    int far = int.Parse(match.Groups["far"].Value);
                    switch (match.Groups["dir"].Value)
                    {
                        case "forward": pos += far; p2 += far; d2 += aim * far; break;
                        case "down": depth += far; aim += far;  break;
                        case "up": aim -= far; if (far <= depth) depth -= far; else depth = 0; break;
                    }
                }
            }

            Console.WriteLine($"Product: {pos * depth}");
            Console.WriteLine($"Part 2: {p2 * d2}");
        }
    }
}
