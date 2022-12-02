using System;
using System.IO;
using System.Linq;

namespace Day1_ElvesSnacksCalories
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            int[] max = new int[3];
            using (var stream = File.OpenRead("input.txt"))
            using (var sr = new StreamReader(stream))
            {

                for (bool ok = true; ok;)
                { // this loop is per elf.
                    int total = 0; // total cals from this elf

                    string line; // local var just to hold the line for scanning
                    for (int cals; // this loop is per-meal, per-line.
                        null != (line = sr.ReadLine())   // read the line then
                            && int.TryParse(line, out cals);        // try to parse a number from it
                        total += cals) ;                             // add up the total

                    // keep the N largest totals. To do this:
                    // displace the highest OR leftmost max.
                    // though if you do, pick up that old max and see if you should keep it
                    // by carrying it forward.
                    // the total variable can be thrashed for this purpose
                    // since it will have been recorded already.
                    for (int i = 0; i < max.Length; i++)
                    { 
                        if (max[i] < total)
                        {
                            int old = max[i];
                            max[i] = total;
                            total = old;
                        }
                    }

                    // blank line = ok to continue, or if end of stream quit.
                    ok = line != null;
                }
            }

            // write to console
            Console.WriteLine($"Max: {max[0]}");
            Console.WriteLine($"Sum of Top {max.Length}: {max.Sum()}");
        }
    }
}
