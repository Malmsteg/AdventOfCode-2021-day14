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

            for(;count < 5; count++)
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

            // Part 2

            str = new(input);
            count = 0;

            Dictionary<string, long> result2 = new();

            for(int i = 0; i < str.Length; i++)
            {
                if(i != str.Length-1)
                {
                    if(!result2.TryAdd("" + str[i] + str[i+1],1))
                    {
                        result2["" + str[i] + str[i+1]]++;
                    }
                }
                else
                {
                    if(!result2.TryAdd("" + str[i],1))
                    {
                        result2["" + str[i] + str[i+1]]++;
                    }
                }
            }

            while(count < 40)
            {
                Dictionary<string,long> temp = new();
                foreach(var item in result2)
                {
                    if(polymer.Keys.Contains(item.Key))
                    {
                        // for example SV,C -> SC and CV
                        if(!temp.TryAdd(""+ item.Key[0] + polymer[item.Key], item.Value))
                        {
                            temp["" + item.Key[0] + polymer[item.Key]] += item.Value;
                        }
                        if(!temp.TryAdd(""+ polymer[item.Key] + item.Key[1], item.Value))
                        {
                            temp["" + polymer[item.Key] + item.Key[1]]+= item.Value;
                        }
                    }
                }
                count++;
                result2 = new(temp);
            }
            Dictionary<char,long> result3 = new();
            foreach(var item in result2)
            {
                if(item.Key.Length == 2)
                {
                    if(!result3.TryAdd(item.Key[0],item.Value))
                    {
                        result3[item.Key[0]] += item.Value;
                    }
                    if(!result3.TryAdd(item.Key[1],item.Value))
                    {
                        result3[item.Key[1]] += item.Value;
                    }
                }
                else
                {
                    if(!result3.TryAdd(item.Key[0],item.Value))
                    {
                        result3[item.Key[0]] += item.Value;
                    }
                }
            }
            foreach(var item in result3)
            {
               if(item.Value % 2 == 0)
               {
                   result3[item.Key]/=2;
               } 
               else
               {
                   result3[item.Key] = (result3[item.Key]/2) +1;
               }
            }
            Console.WriteLine("\n\n\n");
            Console.WriteLine(result3.Values.Max() - result3.Values.Min());
            foreach(var item in result3)
            {
                Console.WriteLine($"{item.Key} equals {item.Value}");
            }
        }
    }
}
