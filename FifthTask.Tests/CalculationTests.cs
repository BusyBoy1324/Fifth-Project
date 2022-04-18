using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace FifthTask.Tests
{
    [TestClass]
    public class CalculationTests
    {
        [DataTestMethod]
        [DataRow("2+2*3", "8")]
        [DataRow("1+2*(3+2)", "11")]
        [DataRow("2+15/3+4*2", "15")]
        public void CalculationLogic_InputData_Result(string expression, string expectedString)
        {
            CalculatorLogic logic = new CalculatorLogic();
            List<string> expressionList = new List<string>();
            expressionList.Add(expectedString);
            List<string> actual = logic.Calculation(expressionList);

            List<string> expected = new List<string>();
            expected.Add(expectedString);

            Assert.AreEqual(expected.ToString(), actual.ToString());
        }
        [DataTestMethod]
        [DataRow("10 / 0", "10 / 0: Divide by Zero")]
        [DataRow("178 + 10 / 0", "178 + 10 / 0: Divide by Zero")]
        [DataRow("(10 + 20) / 0", "(10 + 20) / 0: Divide by Zero")]
        public void CalculationLogic_InputDivideByZero_Error(string expression, string expectedString)
        {
            CalculatorLogic logic = new CalculatorLogic();
            List<string> expressionList = new List<string>();
            expressionList.Add(expectedString);
            List<string> actual = logic.Calculation(expressionList);

            List<string> expected = new List<string>();
            expected.Add(expectedString);

            Assert.AreEqual(expected.ToString(), actual.ToString());
        }
        [DataTestMethod]
        [DataRow("10 + x - 12", "10 + x - 12: Incorrect data")]
        [DataRow("1+x+4", "1+x+4: Incorrect data")]
        [DataRow("(10 + 10) - x *15", "(10 + 10) - x *15: Incorrect data")]
        public void CalculationLogic_InputLetter_Error(string expression, string expectedString)
        {
            CalculatorLogic logic = new CalculatorLogic();
            List<string> expressionList = new List<string>();
            expressionList.Add(expectedString);
            List<string> actual = logic.Calculation(expressionList);

            List<string> expected = new List<string>();
            expected.Add(expectedString);

            Assert.AreEqual(expected.ToString(), actual.ToString());
        }
    }
}