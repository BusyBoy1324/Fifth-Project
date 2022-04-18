using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifthTask
{
    class Writer : IWriter
    {
        public void WritePath()
        {
            Console.WriteLine("Enter file path");
        }
        public void WriteStart()
        {
            Console.WriteLine("Select function which you want to use 'console' or 'file'");
        }
        public void WriteStartInputError()
        {
            Console.WriteLine("You must write 'console' or 'file' to continue!");
        }
        public void WriteFileNotFounded()
        {
            Console.WriteLine("File wasn`t founded, enter path again");
        }
        public void WriteResultToTheFile(List <string> strings, string path)
        {
            File.WriteAllLines(path, strings);
        }
        public void AskForResultFilePath()
        {
            Console.WriteLine("Enter a path to the result file");
        }
        public void WriteEnterExpression()
        {
            Console.WriteLine("Enter your expression");
        }
        public void WriteExpressionResult(List<string> result)
        {
            for (int i = 0; i < result.Count; i++)
            {
                Console.WriteLine(result[i]);
            }
        }
    }
}
