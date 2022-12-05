using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day5_Crane
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 5 Crane!");

            List<string> stacks = new List<string>();

            using (var stream = File.OpenRead("input.txt"))
            using (var sr = new StreamReader(stream))
            {
                string line;
                for (bool ok = true;
                    ok && null != (line = sr.ReadLine());)
                {
                    for (int i = 0, col = 0; i < line.Length; i += 4, col++)
                    {
                        string crate = line.Substring(i, 3);
                        if (String.IsNullOrWhiteSpace(crate))
                            continue;

                        var match = System.Text.RegularExpressions.Regex.Match(crate, @"\[(?<name>[A-Z]+)\]");
                        if (!match.Success)
                        {
                            ok = false;
                            break;
                        }

                        while (stacks.Count <= col)
                            stacks.Add("");

                        stacks[col] += match.Groups["name"];
                    }
                }

                for (int i = 0; i < stacks.Count; i++)
                {
                    stacks[i] = new string(stacks[i].Reverse().ToArray());
                }

                // skip blanks
                while (String.IsNullOrEmpty(line = sr.ReadLine()));

                do
                {
                    var match = System.Text.RegularExpressions.Regex.Match(line, @"move (?<cnt>[0-9]+) from (?<src>[0-9]+) to (?<dest>[0-9]+)");

                    int cnt = int.Parse(match.Groups["cnt"].Value);
                    int src = int.Parse(match.Groups["src"].Value);
                    int dest = int.Parse(match.Groups["dest"].Value);

                    string move = stacks[src - 1].Substring(stacks[src - 1].Length - cnt, cnt);
                    stacks[src - 1] = stacks[src - 1].Remove(stacks[src - 1].Length - cnt);
                    stacks[dest - 1] += move; // new string(move.Reverse().ToArray()); // COMMENTED IS PART-1

                } while (null != (line = sr.ReadLine()));

                Console.WriteLine(new string(stacks.Select(s => s.Last()).ToArray()));
            }
        }
    }
}
