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
            string input = text[0];
            Dictionary<string,string> polymer = new();
            Dictionary<char, long> result = new();

            for(int i = 2; i < text.Length; i++)
            {
                string[] temp = text[i].Split(" -> ");
                polymer.Add(temp[0],temp[1]);
            }

            string str = new(input);
            int count = 0;

            for(;count < 10; count++)
            {
                string next = "";
                for(int i = 0; i < str.Length;i++)
                {
                    if(i != str.Length-1)
                    {
                        string temp = "" + str[i] + str[i+1];
                        if(polymer.ContainsKey(temp))
                        {
                            next += str[i] + polymer[temp];
                        }
                        else
                        {
                            next+= str[i];
                        }
                    }
                    else
                    {
                        next+= str[i];
                    }
                }
                str = new (next);
            }
            Console.WriteLine(str.Length);
            for(int i = 0; i < str.Length; i++)
            {
                if(!result.ContainsKey(str[i]))
                {
                    result.Add(str[i],0);
                }
                result[str[i]]++;
            }
            Console.WriteLine(result.Values.Max() - result.Values.Min());
            foreach(var item in result)
            {
                Console.WriteLine($"{item.Key} equals {item.Value}");
            }
        }
    }
}
