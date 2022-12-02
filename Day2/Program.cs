using System.IO;
using System.Text.RegularExpressions;

namespace Day2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 2!");

            using (var stream = File.OpenRead("input.txt"))
            using (var sr = new StreamReader(stream))
            {
                int total = 0;
                int total2 = 0;

                string line; // local var just to hold the line for scanning
                for (int score, score2; // this loop is per-meal, per-line.
                    null != (line = sr.ReadLine());
                    total += score, total2 += score2)
                {
                    var match = Regex.Match(line, "(?<a>[ABC]) (?<b>[XYZ])");
                    var a = match.Groups["a"].Value;
                    var b = match.Groups["b"].Value;

                    score = b[0] - 'X' + 1;

                    switch ((a[0] - 'A' - (b[0] - 'X') + 3) % 3)
                    {
                        case 0: // draw
                            score += 3;
                            break;

                        case 1: // loss
                            score += 0;
                            break;

                        case 2: // win
                            score += 6;
                            break;
                    }

                    // the other interpretation is that X is a loss, Y is draw, Z is victory
                    score2 = 3 * (b[0] - 'X'); // the outcome
                    score2 += 1 + (a[0] - 'A' + (b[0] - 'X' + 2) % 3) % 3;

                }

                Console.WriteLine($"Total - Part 1: {total}");
                Console.WriteLine($"Total - Part 2: {total2}");
            }
        }
    }
}