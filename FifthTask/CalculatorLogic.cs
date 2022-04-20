using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifthTask
{
    public class CalculatorLogic : ICalculatorLogic
    {
        private string _exp;
        private string _InBrackets;
        private bool _divideByZero = false;
        private bool NotOperator(int j)
        {
            return !(_InBrackets[j] == '+' || _InBrackets[j] == '-' || _InBrackets[j] == '*' || _InBrackets[j] == '/');
        }

        private int GetLeftOperand(int i)
        {
            string LeftOperand = "";
            bool negative = false;
            for (int j = i - 1; j >= 0; j--)
            {
                if (_InBrackets[j] == '#')
                {
                    negative = true;
                    break;
                }
                if (NotOperator(j))
                {
                    LeftOperand = _InBrackets[j] + LeftOperand;
                }
                else
                {
                    break;
                }
            }
            int.TryParse(LeftOperand, out int result);
            if (negative)
            {
                return 0 - result;
            }
            return result;
        }

        private int GetRightOperand(int i)
        {
            string RightOperand = "";
            bool negative = false;
            for (int j = i + 1; j < _InBrackets.Length; j++)
            {
                if (_InBrackets[j] == '#')
                {
                    negative = true;
                }
                else if (NotOperator(j))
                {
                    RightOperand += _InBrackets[j];
                }
                else
                {
                    break;
                }
            }
            int.TryParse(RightOperand, out int result);
            if (negative)
            {
                return 0 - result;
            }
            return result;
        }

        private void ReplaceExp(int i, double ToThis)
        {
            int FromI = 0;
            int ToI = _InBrackets.Length - 1;
            for (int j = i - 1; j >= 0; j--)
            {
                if (NotOperator(j))
                {
                    FromI = j;
                }
                else
                {
                    break;
                }
            }
            for (int j = i + 1; j < _InBrackets.Length; j++)
            {
                if (NotOperator(j))
                {
                    ToI = j;
                }
                else
                {
                    break;
                }
            }
            _InBrackets = _InBrackets.Substring(0, FromI) + ToThis.ToString().Replace('-', '#') + _InBrackets.Substring(FromI + ToI - FromI + 1);
        }

        private void ReplaceMultiDivision(int i)
        {
            double MD;
            if (_InBrackets[i] == '*')
            {
                MD = GetLeftOperand(i) * GetRightOperand(i);
                ReplaceExp(i, MD);
                CaltulateBrackets();
            }
            else
            {
                _divideByZero = GetRightOperand(i) == 0;
                if (!_divideByZero)
                {
                    MD = GetLeftOperand(i) / GetRightOperand(i);
                    ReplaceExp(i, MD);
                    CaltulateBrackets();
                }
            }
        }

        private void ReplaceSumSubstraction(int i)
        {
            double AS;
            if (_InBrackets[i] == '+')
            {
                AS = GetLeftOperand(i) + GetRightOperand(i);
            }
            else
            {
                AS = GetLeftOperand(i) - GetRightOperand(i);
            }
            ReplaceExp(i, AS);
            CaltulateBrackets();
        }

        private void CaltulateBrackets()
        {
            int i;
            for (i = 0; i < _InBrackets.Length; i++)
            {
                if (_InBrackets[i] == '*' || _InBrackets[i] == '/')
                {
                    ReplaceMultiDivision(i);
                    return;
                }
            }
            for (i = 0; i < _InBrackets.Length; i++)
            {
                if (_InBrackets[i] == '+' || _InBrackets[i] == '-')
                {
                    ReplaceSumSubstraction(i);
                    return;
                }
            }
        }

        private bool FindBrackets(out int o)
        {
            o = 0;
            if (_exp.IndexOf('(') != -1)
            {
                int ClosedBracket = _exp.IndexOf(')');
                int OpenBracket = 0;
                for (int i = ClosedBracket - 1; i >= 0; i--)
                {
                    if (_exp[i] == '(')
                    {
                        OpenBracket = i;
                        _InBrackets = _exp.Substring(OpenBracket + 1, ClosedBracket - OpenBracket - 1);
                        o = OpenBracket;
                        _exp = _exp.Remove(OpenBracket, ClosedBracket - OpenBracket + 1);
                        break;
                    }
                }
                return true;
            }
            return false;
        }

        public List<string> Calculation(List<string> strings)
        {
            int stringWithError;
            List<string> result = new List<string>();
            for (int i = 0; i < strings.Count; i++)
            {
                if (!strings[i].Any(letter => Char.IsLetter(letter)))
                {
                    _exp = '(' + strings[i].Replace(" ", "") + ')';
                    int o;
                    while (FindBrackets(out o))
                    {
                        CaltulateBrackets();
                        _exp = _exp.Insert(o, _InBrackets);
                    }
                    _exp = _exp.Replace('#', '-');
                    if (!_divideByZero)
                    {
                        result.Add($"{strings[i]}: {_exp}");
                    }
                    else
                    {
                        result.Add($"{strings[i]}: Divide by Zero");
                        _divideByZero = false;
                    }
                }
                else
                {
                    stringWithError = i + 1;
                    result.Add($"{strings[i]}: Incorrect data");
                }
            }
            return result;
        }
    }
}
