using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifthTask
{
    interface ICalculatorLogic
    {
        bool NotOperator(int j);
        double GetLeftOperand(int i);
        double GetRightOperand(int i);
        void ReplaceExp(int i, double toThis);
        void ReplaceMultiDivision(int i);
        void ReplaceSumSubstraction(int i);
        void CaltulateBrackets();
        bool FindBrackets(out int o);
        List<string> Calculation(List<string> strings);
    }
}
