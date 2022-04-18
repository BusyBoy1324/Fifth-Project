using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifthTask
{
    class Calculator
    {
        IReader _reader;
        IWriter _writer;
        ICalculatorLogic _logic;
        public Calculator(IReader reader, IWriter writer, ICalculatorLogic logic)
        {
            _reader = reader;
            _writer = writer;
            _logic = logic;
        }
        public void Calculate()
        {
            string startChoise = String.Empty, path = String.Empty, resultFilePath = String.Empty, expression = String.Empty;
            List<string> strings = new List<string>();
            List<string> result = new List<string>();
            bool check = true;
            while (check)
            {
                _writer.WriteStart();
                startChoise = _reader.ReadStartChoise();
                if (startChoise == "console")
                {
                    _writer.WriteEnterExpression();
                    expression = _reader.ReadExpression();
                    strings.Add(expression);
                    result = _logic.Calculation(strings);
                    _writer.WriteExpressionResult(result);
                    check = false;
                }
                else if (startChoise == "file")
                {
                    _writer.WritePath();
                    path = _reader.ReadFilePath();
                    while (!File.Exists(path))
                    {
                        _writer.WriteFileNotFounded();
                        path = _reader.ReadFilePath();
                    }
                    strings = _reader.ReadFileString(path);
                    result = _logic.Calculation(strings);
                    _writer.AskForResultFilePath();
                    resultFilePath = _reader.ReadResultFilePath();
                    while (!File.Exists(resultFilePath))
                    {
                        _writer.WriteFileNotFounded();
                        resultFilePath = _reader.ReadResultFilePath();
                    }
                    _writer.WriteResultToTheFile(result, resultFilePath);
                    check = false;
                }
            }
        }
    }
}
