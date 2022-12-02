using System;
using System.IO;
using System.Collections.Generic;

namespace _2021_day25
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("2021 Day 25!");

            char[][] old, next;
            using (var stream = File.OpenRead("input.txt"))
            using (var sr = new StreamReader(stream))
            {
                var list1 = new List<char[]>();
                var list2 = new List<char[]>();

                for (string line = null;  null != (line = sr.ReadLine()); )   // read the lines
                {
                    // different, but similarly stuffed, arrays
                    list1.Add(line.ToCharArray());
                    list2.Add(line.ToCharArray());
                }

                old = list1.ToArray();
                next = list2.ToArray();
            }

            // old contains the data. next just gives us room to double-buffer the state

            int steps;
            for (steps = 0; ; steps++)
            {
                bool anyMove = false;

                // analyze eastwards
                for (int r = 0; r < old.Length; r++)
                {
                    for (int c = 0; c < old[r].Length; c++)
                    {
                        next[r][c] = old[r][c];
                        if (old[r][c] == '>' && old[r][(c + 1) % old[r].Length] == '.')
                        { // move east
                            next[r][(c + 1) % old[r].Length] = '>';
                            next[r][c] = '.';
                            anyMove = true;
                            c++;
                        }
                    }
                }

                // perform the move
                { // swap the two for next processing
                    char[][] temp;
                    temp = old;
                    old = next;
                    next = temp;
                }

                // analyze southward
                for (int c = 0; c < old[0].Length; c++)
                {
                    for (int r = 0; r < old.Length; r++)
                    {
                        next[r][c] = old[r][c];
                        if (old[r][c] == 'v' && old[(r + 1) % old.Length][c] == '.')
                        { // move south
                            next[(r + 1) % old.Length][c] = 'v';
                            next[r][c] = '.';
                            anyMove = true;
                            r++; // skip that one now
                        }
                    }
                }

                if (!anyMove)
                    break;

                // perform the move
                { // swap the two for next processing
                    char[][] temp;
                    temp = old;
                    old = next;
                    next = temp;
                }

                PrintBoard(old);
            }

            PrintBoard(old);
            //Console.WriteLine($"The Sea Cucumbers stopped moving after {steps+1} steps!");
        }

        static void PrintBoard(char[][] board)
        {
            for (int r = 0; r < board.Length; r++)
            {
                Console.WriteLine(new string(board[r]));
            }
        }
    }
}
