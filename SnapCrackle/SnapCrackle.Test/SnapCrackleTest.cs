using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using NUnit.Common;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SnapCrackle.Test
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class SnapCrackleTest
    {
        public static void OutputDelegate(string message)
        {
           // Debug.Write(message);
        }

        [TestMethod]
        public void Test_PrintResultsThrowsIfStartNotLessThanEnd()
        {
            var snapCrackle = new Library.SnapCrackle(SnapCrackleTest.OutputDelegate);

            var start = 10;
            var end = 8;
            var denominatorList = new List<Library.DenominatorWordPair>(new[] { new Library.DenominatorWordPair { Word = "Test", Denominator = 2 }});

            NUnit.Framework.Assert.Throws<InvalidOperationException>(() => snapCrackle.PrintResults(start, end, denominatorList));
        }

        [TestMethod]
        public void Test_PrintResultsThrowsIfDenominatorListIsNull()
        {
            var snapCrackle = new Library.SnapCrackle(SnapCrackleTest.OutputDelegate);

            var start = 1;
            var end = 100;
            
            NUnit.Framework.Assert.Throws<ArgumentException>(() => snapCrackle.PrintResults(start, end, null));
        }

        [TestMethod]
        public void Test_PrintResultsThrowsIfDenominatorListIsEmpty()
        {
            var snapCrackle = new Library.SnapCrackle(SnapCrackleTest.OutputDelegate);

            var start = 1;
            var end = 100;
            var denominatorList = new List<Library.DenominatorWordPair>();

            NUnit.Framework.Assert.Throws<ArgumentException>(() => snapCrackle.PrintResults(start, end, denominatorList));
        }
                
        [TestMethod]
        public void Test_PrintResultsOutputsDenominatorWords()
        {
            var outputLog = new StringBuilder();
            var snapCrackle = new Library.SnapCrackle((s) => { outputLog.Append(s); });

            var start = 1;
            var end = 10;
            var denominatorList = new List<Library.DenominatorWordPair>();
            denominatorList.Add(new Library.DenominatorWordPair { Denominator = 2, Word = "bob" });
            denominatorList.Add(new Library.DenominatorWordPair { Denominator = 3, Word = "sally" });

            snapCrackle.PrintResults(start, end, denominatorList);

            var output = outputLog.ToString().Split(new[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            /* expected: 
                    1 
                    2 bob
                    3 sally
                    ... 
                    6 bobsally*/

            NUnit.Framework.Assert.AreEqual(output[0], "1 ");
            NUnit.Framework.Assert.AreEqual(output[1], "2 bob");
            NUnit.Framework.Assert.AreEqual(output[2], "3 sally");
            NUnit.Framework.Assert.AreEqual(output[5], "6 bobsally");
        }

        [TestMethod]
        public void Test_PrintResultsOutputsCorrectNumberOfLines()
        {
            var outputLog = new StringBuilder();
            var snapCrackle = new Library.SnapCrackle((s) => { outputLog.Append(s); });

            var start = 1;
            var end = 10;
            var denominatorList = new List<Library.DenominatorWordPair>();
            denominatorList.Add(new Library.DenominatorWordPair { Denominator = 2, Word = "bob" });
            denominatorList.Add(new Library.DenominatorWordPair { Denominator = 3, Word = "sally" });

            snapCrackle.PrintResults(start, end, denominatorList);

            var output = outputLog.ToString().Split(new[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            NUnit.Framework.Assert.AreEqual(output.Length, 10);
        }
    }
}
