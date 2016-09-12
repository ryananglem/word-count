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
            
            var words = wordCountService.GetWordList();

            // presentation layer
            foreach (KeyValuePair<string, int> word in words)
            {
                Console.WriteLine(String.Format("{0}  - {1}", word.Key, word.Value));
            }

            Console.ReadKey();
        }
    }
}
