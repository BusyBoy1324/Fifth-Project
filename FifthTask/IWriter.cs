using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifthTask
{
    interface IWriter
    {
        void WriteStart();
        void WritePath();
        void WriteStartInputError();
        void WriteFileNotFounded();
        void WriteResultToTheFile(List<string> strings, string path);
        void AskForResultFilePath();
        void WriteEnterExpression();
        void WriteExpressionResult(List<string> result);
    }
}
