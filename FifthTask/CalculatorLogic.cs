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
        private static string exp;
        private static string InBrackets;
        private static bool divideByZero = false;
        NumberStyles style = NumberStyles.AllowDecimalPoint;
        public bool NotOperator(int j)
        {
            return InBrackets[j] != '+' && InBrackets[j] != '-' && InBrackets[j] != '*' && InBrackets[j] != '/';
        }

        public double GetLeftOperand(int i)
        {
            string LeftOperand = "";
            for (int j = i - 1; j >= 0; j--)
            {
                if (NotOperator(j))
                {
                    LeftOperand = InBrackets[j] + LeftOperand;
                }
                else
                {
                    break;
                }
            }
            var englishCulture = CultureInfo.GetCultureInfo("en-US");
            double.TryParse(LeftOperand, style, englishCulture, out double result);
            return result;
        }

        public double GetRightOperand(int i)
        {
            string RightOperand = "";
            for (int j = i + 1; j < InBrackets.Length; j++)
            {
                if (NotOperator(j))
                {
                    RightOperand += InBrackets[j];
                }
                else
                {
                    break;
                }
            }
            var englishCulture = CultureInfo.GetCultureInfo("en-US");
            double.TryParse(RightOperand, style, englishCulture, out double result);
            return result;
        }

        public void ReplaceExp(int i, double ToThis)
        {
            int FromI = 0;
            int ToI = InBrackets.Length - 1;
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
            for (int j = i + 1; j < InBrackets.Length; j++)
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
            InBrackets = InBrackets.Replace(InBrackets.Substring(FromI, ToI - FromI + 1), ToThis.ToString());
        }

        public void ReplaceMultiDivision(int i)
        {
            double MD;
            if (InBrackets[i] == '*')
            {
                MD = GetLeftOperand(i) * GetRightOperand(i);
                ReplaceExp(i, MD);
                CaltulateBrackets();
            }
            else
            {
                divideByZero = GetRightOperand(i) == 0;
                if (!divideByZero)
                {
                    MD = GetLeftOperand(i) / GetRightOperand(i);
                    ReplaceExp(i, MD);
                    CaltulateBrackets();
                }
            }
        }

        public void ReplaceSumSubstraction(int i)
        {
            double AS;
            if (InBrackets[i] == '+')
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

        public void CaltulateBrackets()
        {
            int i;
            for (i = 0; i < InBrackets.Length; i++)
            {
                if (InBrackets[i] == '*' || InBrackets[i] == '/')
                {
                    ReplaceMultiDivision(i);
                    return;
                }
            }
            for (i = 0; i < InBrackets.Length; i++)
            {
                if (InBrackets[i] == '+' || InBrackets[i] == '-')
                {
                    ReplaceSumSubstraction(i);
                    return;
                }
            }
        }

        public bool FindBrackets(out int o)
        {
            o = 0;
            if (exp.IndexOf('(') != -1)
            {
                int ClosedBracket = exp.IndexOf(')');
                int OpenBracket = 0;
                for (int i = ClosedBracket - 1; i >= 0; i--)
                {
                    if (exp[i] == '(')
                    {
                        OpenBracket = i;
                        InBrackets = exp.Substring(OpenBracket + 1, ClosedBracket - OpenBracket - 1);
                        o = OpenBracket;
                        exp = exp.Remove(OpenBracket, ClosedBracket - OpenBracket + 1);
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
                    exp = '(' + strings[i].Replace(" ", "") + ')';
                    int o;
                    while (FindBrackets(out o))
                    {
                        CaltulateBrackets();
                        exp = exp.Insert(o, InBrackets);
                    }
                    if (!divideByZero)
                    {
                        result.Add($"{strings[i]}: {exp}");
                    }
                    else 
                    {
                        result.Add($"{strings[i]}: Divide by Zero");
                        divideByZero = false;
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
