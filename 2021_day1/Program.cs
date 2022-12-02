using System;
using System.IO;

namespace _2021_day1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("2021 Day 1!");
            int L = 0, S = 0;
            int sumGreater = 0;

            using (var stream = File.OpenRead("input.txt"))
            using (var sr = new StreamReader(stream))
            {
                string line = null;

                var window = new int[3];
                int sum = 0, sumPrior = 0;
                for (int prior = -1, w = 0, cnt = 0; 
                    null != (line = sr.ReadLine())
                        && int.TryParse(line, out int meas); 
                    prior = meas, window[w] = meas, w = (w + 1) % window.Length, sumPrior = sum, cnt++)
                {
                    sum += meas - window[w];

                    if (prior < 0) continue;

                    if (meas > prior)
                        L++;
                    else if (meas < prior)
                        S++;

                    if (cnt >= window.Length && sum > sumPrior)
                        sumGreater++;
                }
            }

            Console.WriteLine($"Larger: {L}");
            Console.WriteLine($"sumGreater: {sumGreater}");
        }
    }
}
