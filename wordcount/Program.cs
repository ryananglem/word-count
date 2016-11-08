using Ninject;
using System;
using System.Collections.Generic;

namespace wordcount
{
    class Program
    {
        static void Main(string[] args)
        {
            // bind dependencies
            IKernel kernel = new StandardKernel(new WordCountModule());

            var wordCountService = kernel.Get<services.Service>();
            
            var words = wordCountService.CountWordsInDocument();
            var sortedWords = new SortedDictionary<string, int>(words);

            // presentation layer
            foreach (var word in sortedWords)
            {
                Console.WriteLine($"{word.Key}  - {word.Value}");
            }

            Console.WriteLine(String.Empty);
            Console.WriteLine("Press any key to end..");
            Console.ReadKey();
        }
    }
}
