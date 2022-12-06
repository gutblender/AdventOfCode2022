using System;
using System.Collections.Generic;
using System.IO;

namespace Day6_CommDevices
{
    internal class Program
    {
        static bool found(string window)
        {
            for (int i = 0; i < window.Length-1; i++)
            {
                for (int j = i + 1; j < window.Length; j++)
                {
                    if (window[i] == window[j])
                        return false;
                }
            }

            return true;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Day 6 Comm Devices!");

            const int n = 14; // 4 for part 1, 14 for part 2.
            string window;

            int cnt;

            using (var stream = File.OpenRead("input.txt"))
            using (var sr = new StreamReader(stream))
            {
                { // init
                    char[] buf = new char[n];
                    sr.ReadBlock(buf, 0, n);
                    window = new string(buf);
                    cnt = n;
                }

                for (char c; !found(window) && 0 < (c = (char)sr.Read()); window = window.Substring(1) + c, cnt++) ;
            }

            Console.WriteLine($"{cnt}");
        }
    }
}
