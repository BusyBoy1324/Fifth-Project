using System;

namespace FifthTask
{
    class Program
    {
        public static void Main(string[] args)
        {
            var calculator = new Calculator(new Reader(), new Writer(), new CalculatorLogic());
            calculator.Calculate();
        }
    }
}