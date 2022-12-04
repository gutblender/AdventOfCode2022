using System;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Day4_Camping
{
    internal class Program
    {
        struct Pair
        {
            public int left, right;
        }

        static bool Overlaps(int l1, int l2, int r1, int r2)
        {
            if (l1 <= l2 && r1 >= r2)
                return true;
            else if (l2 <= l1 && r2 >= r1)
                return true;

            return false;
        }
        static bool Overlaps(Pair p1, Pair p2)
        {
            return Overlaps(p1.left, p2.left, p1.right, p2.right);
        }

        static bool InRange(int rangeLeft, int rangeRight, int value)
        {
            return value >= rangeLeft && value <= rangeRight;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Day 4 Camping!");

            int nTotallyEncompassed = 0;
            int nAnyOverlap = 0;

            using (var stream = File.OpenRead("input.txt"))
            using (var sr = new StreamReader(stream))
            {

                for (string line;
                    null != (line = sr.ReadLine());)
                {
                    var match = System.Text.RegularExpressions.Regex.Match(line,
                        "(?<l1>[0-9]+)-(?<r1>[0-9]+),(?<l2>[0-9]+)-(?<r2>[0-9]+)"
                    );

                    if (!match.Success)
                        break;

                    Pair p1, p2;
                    p1.left = int.Parse(match.Groups["l1"].Value);
                    p1.right = int.Parse(match.Groups["r1"].Value);
                    p2.left = int.Parse(match.Groups["l2"].Value);
                    p2.right = int.Parse(match.Groups["r2"].Value);

                    if (Overlaps(p1, p2))
                        nTotallyEncompassed++;
                    else if (InRange(p1.left, p1.right, p2.left)
                        || InRange(p1.left, p1.right, p2.right)
                        || InRange(p2.left, p2.right, p1.left)
                        || InRange(p2.left, p2.right, p1.right))
                        nAnyOverlap++;
                }
            }

            Console.WriteLine($"{nTotallyEncompassed}");
            Console.WriteLine($"{nAnyOverlap + nTotallyEncompassed}");
        }
    }
}
