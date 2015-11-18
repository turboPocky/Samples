using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace Test
{
    public class Class1
    {
        [ExcludeFromCodeCoverage]
        public static void Main(params string[] args)
        {
            // print 1-100 
            // divisible by 3 = snap 
            // divisible by 5 = crackle 
            // divisible by 3 and 5 = snap crackle 

            var snapCrackle = new Library.SnapCrackle(DelegateMethod);

            var wordPairs = new List<Library.DenominatorWordPair>();
            wordPairs.Add(new Library.DenominatorWordPair { Denominator = 2, Word = "bob" });
            wordPairs.Add(new Library.DenominatorWordPair { Denominator = 3, Word = "sally" }); 

            snapCrackle.PrintResults(1, 100, wordPairs); 

            Console.ReadKey();
        }

        public static void DelegateMethod(string message)
        {
            System.Console.Write(message);
        }
    }
}

namespace Library
{
    public class DenominatorWordPair
    {
        public int Denominator { get; set; }
        public string Word { get; set; }
    }

    public class SnapCrackle
    {
        public delegate void OutputDelegate(string message);

        private OutputDelegate _logger { get; set; }

        public SnapCrackle(OutputDelegate logger)
        {
            _logger = logger; 
        }

        // pass in list of denominators and words 
        // 2 bob
        // 3 sam
        // 6 sally 

        // unit test this 
        // send to zack a link to github 
        
        public void PrintResults(int start, int end, IEnumerable<DenominatorWordPair> wordPairs) 
        {
            if (!(start < end))
            {
                throw new InvalidOperationException("start must be less than end"); 
            }

            if (wordPairs == null || wordPairs.Count() == 0)
            {
                throw new ArgumentException("wordPairs must have at least one item");
            }

            for (var i = start; i <= end; i++)
            {
                _logger(string.Format("{0} ", i));

                foreach (var wordPair in wordPairs)
                {
                    if (i % wordPair.Denominator == 0)
                    {
                        _logger(wordPair.Word); 
                    }
                }

                _logger(System.Environment.NewLine);
            }
        }
    }
}

