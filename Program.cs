using System.IO;
using System.Collections.Generic;
using System;
using System.Linq;

namespace AdventOfCode_2021_day14
{
    public static class Program
    {
        public static void Main()
        {
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string file = Path.Combine(currentDirectory, @"..\..\..\input.txt");
            string path = Path.GetFullPath(file);
            string[] text = File.ReadAllText(path).Replace("\r", "").Split("\n");
        }
    }
}
