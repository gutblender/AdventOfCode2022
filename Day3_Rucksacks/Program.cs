using System;
using System.Collections.Generic;
using System.IO;

namespace Day3_Rucksacks
{
    internal class Program
    {
        static int priority(char c)
        {
            if (c >= 'a' && c <= 'z')
                return 1 + c - 'a';
            else if (c >= 'A' && c <= 'Z')
                return 27 + c - 'A';
            return 0;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Day 3 Rucksacks!");

            int total = 0, groupTotal = 0;
            const int GROUPSIZE = 3;
            string[] group = new string[GROUPSIZE];

            using (var stream = File.OpenRead("input.txt"))
            using (var sr = new StreamReader(stream))
            {
                for (bool ok = true; ok;)
                {
                    string line;

                    int g;
                    for (g = 0; g < GROUPSIZE && (ok = null != (line = sr.ReadLine())); g++)
                    {
                        group[g] = line;

                        string left, right;

                        int middle = line.Length / 2;
                        left = line.Substring(0, middle);
                        right = line.Substring(middle);

                        HashSet<char> set = new HashSet<char>();
                        foreach (char c in left)
                            set.Add(c);

                        foreach (char c in set)
                        {
                            if (right.Contains(c))
                                total += priority(c);
                        }
                    }

                    if (!ok || g < GROUPSIZE)
                        break;

                    HashSet<char> all = new HashSet<char>();
                    foreach (char c in "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ")
                        all.Add(c);

                    for (int i = 0; i < GROUPSIZE; i++)
                    {
                        HashSet<char> set = new HashSet<char>();
                        foreach (char c in group[i])
                            set.Add(c);
                        all.IntersectWith(set);
                    }

                    // should only be one char remaining
                    foreach (char c in all)
                    {
                        groupTotal += priority(c);
                        break;
                    }
                }
            }

            Console.WriteLine($"Total: {total}");
            Console.WriteLine($"Group Badge Total: {groupTotal}");
        }
    }
}
