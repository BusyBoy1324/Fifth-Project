using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifthTask
{
    interface IReader
    {
        string ReadStartChoise();
        string ReadConsoleString();
        List<string> ReadFileString(string path);
        string ReadFilePath();
        string ReadResultFilePath();
        string ReadExpression();
    }
}
