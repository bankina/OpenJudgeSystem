﻿namespace OJS.Workers.Checkers.Tests
{
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using OJS.Workers.Common;

    [TestClass]
    public class SortCheckerTests
    {
        [TestMethod]
        public void SortCheckerShouldReturnTrueWhenGivenExactStrings()
        {
            string receivedOutput = "Ивайло";
            string expectedOutput = "Ивайло";
            var checker = new SortChecker();

            var checkerResult = checker.Check(string.Empty, receivedOutput, expectedOutput);

            Assert.IsNotNull(checkerResult);
            Assert.IsTrue(checkerResult.IsCorrect);
            Assert.IsTrue(checkerResult.ResultType.HasFlag(CheckerResultType.Ok));
        }

        [TestMethod]
        public void SortCheckerShouldReturnTrueWhenGivenExactStringsWithDifferentNewLineEndings()
        {
            string receivedOutput = "Ивайло\n";
            string expectedOutput = "Ивайло";
            var checker = new SortChecker();

            var checkerResult = checker.Check(string.Empty, receivedOutput, expectedOutput);

            Assert.IsNotNull(checkerResult);
            Assert.IsTrue(checkerResult.IsCorrect);
            Assert.IsTrue(checkerResult.ResultType.HasFlag(CheckerResultType.Ok));
        }

        [TestMethod]
        public void SortCheckerShouldReturnTrueWhenGivenExactMultiLineStrings()
        {
            string receivedOutput = "Ивайло\nFoo\nBar";
            string expectedOutput = "Ивайло\nFoo\nBar";
            var checker = new SortChecker();

            var checkerResult = checker.Check(string.Empty, receivedOutput, expectedOutput);

            Assert.IsNotNull(checkerResult);
            Assert.IsTrue(checkerResult.IsCorrect);
            Assert.IsTrue(checkerResult.ResultType.HasFlag(CheckerResultType.Ok));
        }

        [TestMethod]
        public void SortCheckerShouldReturnTrueWhenGivenExactMultiLineStringsWithDifferentNewLineEndings()
        {
            string receivedOutput = "Ивайло\nFoo\nBar";
            string expectedOutput = "Ивайло\nFoo\nBar\n";
            var checker = new SortChecker();

            var checkerResult = checker.Check(string.Empty, receivedOutput, expectedOutput);

            Assert.IsNotNull(checkerResult);
            Assert.IsTrue(checkerResult.IsCorrect);
            Assert.IsTrue(checkerResult.ResultType.HasFlag(CheckerResultType.Ok));
        }

        [TestMethod]
        public void SortCheckerShouldRespectsTextCasing()
        {
            string receivedOutput = "Ивайло";
            string expectedOutput = "ивайло";
            var checker = new SortChecker();

            var checkerResult = checker.Check(string.Empty, receivedOutput, expectedOutput);

            Assert.IsNotNull(checkerResult);
            Assert.IsFalse(checkerResult.IsCorrect);
            Assert.IsTrue(checkerResult.ResultType.HasFlag(CheckerResultType.WrongAnswer));
        }

        [TestMethod]
        public void SortCheckerShouldRespectsDecimalSeparators()
        {
            string receivedOutput = "1,1";
            string expectedOutput = "1.1";
            var checker = new SortChecker();

            var checkerResult = checker.Check(string.Empty, receivedOutput, expectedOutput);

            Assert.IsNotNull(checkerResult);
            Assert.IsFalse(checkerResult.IsCorrect);
            Assert.IsTrue(checkerResult.ResultType.HasFlag(CheckerResultType.WrongAnswer));
        }

        [TestMethod]
        public void SortCheckerShouldReturnFalseWhenGivenDifferentStrings()
        {
            string receivedOutput = "Foo";
            string expectedOutput = "Bar";
            var checker = new SortChecker();

            var checkerResult = checker.Check(string.Empty, receivedOutput, expectedOutput);

            Assert.IsNotNull(checkerResult);
            Assert.IsFalse(checkerResult.IsCorrect);
            Assert.IsTrue(checkerResult.ResultType.HasFlag(CheckerResultType.WrongAnswer));
        }

        [TestMethod]
        public void SortCheckershouldReturnInvalidNumberOfLinesWhenReceivedOutputHasMoreLines()
        {
            string receivedOutput = "Bar\nFoo";
            string expectedOutput = "Bar";
            var checker = new SortChecker();

            var checkerResult = checker.Check(string.Empty, receivedOutput, expectedOutput);

            Assert.IsNotNull(checkerResult);
            Assert.IsFalse(checkerResult.IsCorrect);
            Assert.IsTrue(checkerResult.ResultType.HasFlag(CheckerResultType.InvalidNumberOfLines));
        }

        [TestMethod]
        public void SortCheckerShouldReturnInvalidNumberOfLinesWhenExpectedOutputHasMoreLines()
        {
            string receivedOutput = "Bar";
            string expectedOutput = "Bar\nFoo";
            var checker = new SortChecker();

            var checkerResult = checker.Check(string.Empty, receivedOutput, expectedOutput);

            Assert.IsNotNull(checkerResult);
            Assert.IsFalse(checkerResult.IsCorrect);
            Assert.IsTrue(checkerResult.ResultType.HasFlag(CheckerResultType.InvalidNumberOfLines));
        }

        [TestMethod]
        public void SortCheckerShouldReturnIncorrectNumberOfLinesWhenGivenDifferentMultiLineStringsWithSameText()
        {
            string receivedOutput = "Bar\nFoo\n\n";
            string expectedOutput = "Bar\nFoo";
            var checker = new SortChecker();

            var checkerResult = checker.Check(string.Empty, receivedOutput, expectedOutput);

            Assert.IsNotNull(checkerResult);
            Assert.IsFalse(checkerResult.IsCorrect);
            Assert.IsTrue(checkerResult.ResultType.HasFlag(CheckerResultType.InvalidNumberOfLines));
        }

        [TestMethod]
        public void SortCheckerShouldReturnInvalidNumberOfLinesWhenGivenDifferentMultiLineStringsWithSameTextDifferentBlankLines()
        {
            string receivedOutput = "Bar\nFoo\n\n";
            string expectedOutput = "\n\nBar\nFoo";
            var checker = new SortChecker();

            var checkerResult = checker.Check(string.Empty, receivedOutput, expectedOutput);

            Assert.IsNotNull(checkerResult);
            Assert.IsFalse(checkerResult.IsCorrect);
            Assert.IsTrue(checkerResult.ResultType.HasFlag(CheckerResultType.InvalidNumberOfLines));
        }

        [TestMethod]
        public void SortCheckerShouldReturnCorrectAnswerInBiggerSameTextsTest()
        {
            StringBuilder receivedOutput = new StringBuilder();

            for (int i = 0; i < 100000; i++)
            {
                receivedOutput.AppendLine(i.ToString());
            }

            StringBuilder expectedOutput = new StringBuilder();

            for (int i = 0; i < 100000; i++)
            {
                expectedOutput.AppendLine(i.ToString());
            }

            var checker = new SortChecker();

            var checkerResult = checker.Check(string.Empty, receivedOutput.ToString(), expectedOutput.ToString());

            Assert.IsNotNull(checkerResult);
            Assert.IsTrue(checkerResult.IsCorrect);
            Assert.IsTrue(checkerResult.ResultType.HasFlag(CheckerResultType.Ok));
        }

        [TestMethod]
        public void SortCheckerShouldReturnWrongAnswerInBiggerDifferentTextsTest()
        {
            StringBuilder receivedOutput = new StringBuilder();

            for (int i = 0; i < 10; i++)
            {
                receivedOutput.AppendLine(i.ToString());
            }

            StringBuilder expectedOutput = new StringBuilder();

            for (int i = 10; i > 0; i--)
            {
                expectedOutput.AppendLine(i.ToString());
            }

            var checker = new SortChecker();

            var checkerResult = checker.Check(string.Empty, receivedOutput.ToString(), expectedOutput.ToString());

            Assert.IsNotNull(checkerResult);
            Assert.IsFalse(checkerResult.IsCorrect);
            Assert.IsTrue(checkerResult.ResultType.HasFlag(CheckerResultType.WrongAnswer));
        }

        [TestMethod]
        public void SortCheckerShouldReturnInvalidNumberOfLinesInBiggerDifferentNumberOfLinesTest()
        {
            StringBuilder receivedOutput = new StringBuilder();

            for (int i = 0; i < 10000; i++)
            {
                receivedOutput.AppendLine(i.ToString());
            }

            StringBuilder expectedOutput = new StringBuilder();

            for (int i = 0; i < 100000; i++)
            {
                expectedOutput.AppendLine(i.ToString());
            }

            var checker = new SortChecker();

            var checkerResult = checker.Check(string.Empty, receivedOutput.ToString(), expectedOutput.ToString());

            Assert.IsNotNull(checkerResult);
            Assert.IsFalse(checkerResult.IsCorrect);
            Assert.IsTrue(checkerResult.ResultType.HasFlag(CheckerResultType.InvalidNumberOfLines));
        }

        [TestMethod]
        public void SortCheckerShouldReturnCorrectAnswerInBiggerReversedTextsTest()
        {
            StringBuilder receivedOutput = new StringBuilder();

            for (int i = 1; i <= 100000; i++)
            {
                receivedOutput.AppendLine(i.ToString());
            }

            StringBuilder expectedOutput = new StringBuilder();

            for (int i = 100000; i >= 1; i--)
            {
                expectedOutput.AppendLine(i.ToString());
            }

            var checker = new SortChecker();

            var checkerResult = checker.Check(string.Empty, receivedOutput.ToString(), expectedOutput.ToString());

            Assert.IsNotNull(checkerResult);
            Assert.IsTrue(checkerResult.IsCorrect);
            Assert.IsTrue(checkerResult.ResultType.HasFlag(CheckerResultType.Ok));
        }

        [TestMethod]
        public void SortCheckerShouldReturnTrueWhenGivenSameResultsUnsortedMultiLineStrings()
        {
            string receivedOutput = "Ивайло\nFoo\nBar";
            string expectedOutput = "Ивайло\nBar\nFoo";
            var checker = new SortChecker();

            var checkerResult = checker.Check(string.Empty, receivedOutput, expectedOutput);

            Assert.IsNotNull(checkerResult);
            Assert.IsTrue(checkerResult.IsCorrect);
            Assert.IsTrue(checkerResult.ResultType.HasFlag(CheckerResultType.Ok));
        }

        [TestMethod]
        public void SortCheckerShouldReturnTrueWhenReceivedIsSortedAndExpectedIsNotStrings()
        {
            string receivedOutput = "1\n2\n3";
            string expectedOutput = "2\n3\n1";
            var checker = new SortChecker();

            var checkerResult = checker.Check(string.Empty, receivedOutput, expectedOutput);

            Assert.IsNotNull(checkerResult);
            Assert.IsTrue(checkerResult.IsCorrect);
            Assert.IsTrue(checkerResult.ResultType.HasFlag(CheckerResultType.Ok));
        }

        [TestMethod]
        public void SortCheckerShouldReturnTrueWhenReceivedIsUnSortedAndExpectedIsSortedStrings()
        {
            string receivedOutput = "2\n1\n3";
            string expectedOutput = "1\n2\n3";
            var checker = new SortChecker();

            var checkerResult = checker.Check(string.Empty, receivedOutput, expectedOutput);

            Assert.IsNotNull(checkerResult);
            Assert.IsTrue(checkerResult.IsCorrect);
            Assert.IsTrue(checkerResult.ResultType.HasFlag(CheckerResultType.Ok));
        }

        [TestMethod]
        public void SortCheckerShouldReturnTrueWhenBothTextsAreUnsortedStrings()
        {
            string receivedOutput = "2\n1\n3";
            string expectedOutput = "3\n1\n2";
            var checker = new SortChecker();

            var checkerResult = checker.Check(string.Empty, receivedOutput, expectedOutput);

            Assert.IsNotNull(checkerResult);
            Assert.IsTrue(checkerResult.IsCorrect);
            Assert.IsTrue(checkerResult.ResultType.HasFlag(CheckerResultType.Ok));
        }

        [TestMethod]
        public void SortCheckerShouldReturnTrueWhenReceivedIsSortedAndExpectedIsAlmostStrings()
        {
            string receivedOutput = "1\n2\n3\n4\n5\n6\n7\n8\n9";
            string expectedOutput = "1\n2\n3\n4\n6\n5\n7\n8\n9";
            var checker = new SortChecker();

            var checkerResult = checker.Check(string.Empty, receivedOutput, expectedOutput);

            Assert.IsNotNull(checkerResult);
            Assert.IsTrue(checkerResult.IsCorrect);
            Assert.IsTrue(checkerResult.ResultType.HasFlag(CheckerResultType.Ok));
        }

        [TestMethod]
        public void SortCheckerShouldReturnTrueWhenReceivedIsAlmostSortedAndExpectedIsFullSortedStrings()
        {
            string receivedOutput = "1\n2\n3\n4\n6\n5\n7\n8\n9";
            string expectedOutput = "1\n2\n3\n4\n5\n6\n7\n8\n9";
            var checker = new SortChecker();

            var checkerResult = checker.Check(string.Empty, receivedOutput, expectedOutput);

            Assert.IsNotNull(checkerResult);
            Assert.IsTrue(checkerResult.IsCorrect);
            Assert.IsTrue(checkerResult.ResultType.HasFlag(CheckerResultType.Ok));
        }

        [TestMethod]
        public void SortCheckerShouldReturnTrueWhenBothAreTotallyUnsortedStrings()
        {
            string receivedOutput = "9\n2\n3\n4\n5\n8\n6\n1\n7";
            string expectedOutput = "1\n6\n3\n9\n5\n2\n8\n7\n4";
            var checker = new SortChecker();

            var checkerResult = checker.Check(string.Empty, receivedOutput, expectedOutput);

            Assert.IsNotNull(checkerResult);
            Assert.IsTrue(checkerResult.IsCorrect);
            Assert.IsTrue(checkerResult.ResultType.HasFlag(CheckerResultType.Ok));
        }
    }
}
