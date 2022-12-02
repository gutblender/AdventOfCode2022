using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace _2021_day3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("2021 Day 1!");
            int[] counts = default;
            int nbits = 0, numLines = 0;
            List<int> list = new List<int>();

            using (var stream = File.OpenRead("input.txt"))
            using (var sr = new StreamReader(stream))
            {
                for (string line; null != (line = sr.ReadLine()); numLines++)
                {
                    if (counts == null)
                    {
                        counts = new int[nbits = line.Length];
                    }
                    else if (counts.Length != line.Length)
                    { // big problem

                    }

                    int num = 0;
                    // parse the line
                    for (int i = 0; i < line.Length; i++)
                    {
                        num <<= 1;
                        if (line[i] != '0')
                        {
                            counts[i]++;
                            num |= 1;
                        }
                    }
                    list.Add(num);
                }
            }

            int gamma = 0;
            for (int i = 0; i < counts.Length; i++)
                if (counts[i] > (numLines / 2))
                    gamma |= (1 << (nbits - 1 - i));
            int epsilon = ~gamma & ((1 << nbits) - 1);

            Console.WriteLine($"Product: {epsilon * gamma}");

            List<int> list2 = new List<int>(list); // make a copy for CO2 scrubber

            int oxy = 0;
            int co2 = 0;

            // for each bit...
            for (int bit = nbits - 1, mask = 1 << bit; bit >= 0 && list.Count > 1; bit--, mask >>= 1)
            {
                int h1 = list.Count(n => (n & mask) != 0);
                if (h1 >= ((list.Count + 1) / 2)) // if 1s are more common than otherwise...
                    oxy |= mask; // add 1 at that bit to the criterion

                // cull all the minority nums
                for (int i = list.Count - 1; i >= 0; i--)
                    if ((list[i] & mask) != (oxy & mask))
                        list.RemoveAt(i);
            }
            for (int bit = nbits - 1, mask = 1 << bit; bit >= 0 && list2.Count > 1; bit--, mask >>= 1)
            { 
                int h2 = list2.Count(n => (n & mask) != 0);
                if (h2 < ((list2.Count + 1) / 2)  // if 1s are LESS common than otherwise...
                    || h2 == list2.Count) // BUT you cannot take the road less traveled if it's not traveled at all
                    // because 0 numbers would match the qualification.
                    co2 |= mask; // add 1 at that bit to the criterion

                // cull all the majority nums
                for (int i = list2.Count - 1; i >= 0; i--)
                    if ((list2[i] & mask) != (co2 & mask))
                        list2.RemoveAt(i);
            }

            // now there should be only one number remaining in each collection
            oxy = list[0];
            co2 = list2[0];
            Console.WriteLine($"Life Support Rating: {oxy * co2}");

        }
    }
}
