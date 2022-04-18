using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifthTask
{
    class Reader : IReader
    {
        public string ReadStartChoise()
        {
            string choise = Console.ReadLine();
            return choise;
        }
        public string ReadConsoleString()
        {
            string problem = Console.ReadLine();
            return problem;
        }
        public List<string> ReadFileString(string path)
        {
            List<string> strings = new List<string>();
            foreach (string line in System.IO.File.ReadLines(path))
            {
                strings.Add(line);
            }
            return strings;
        }
        public string ReadFilePath()
        {
            string path = Console.ReadLine();
            return path;
        }
        public string ReadResultFilePath()
        {
            string path = Console.ReadLine();
            return path;
        }
        public string ReadExpression()
        {
            string expression = Console.ReadLine();
            return expression;
        }
    }
}
