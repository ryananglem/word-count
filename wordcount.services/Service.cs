using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wordcount.services
{
    public class Service : IService
    {
        private IRepo _repo;

        public Service (IRepo repository)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            _repo = repository;
        }

        // so.. dictionary was probably the word that 
        // I was missing in the interview
        public Dictionary<string, int> GetWordList()
        {
            var list = new Dictionary<string, int>();

            var start = 0;
            var text = _repo.GetChunkOfText(start);
            while(text != string.Empty)
            {
                var lastPosition = text.LastIndexOf(" ");
                if (lastPosition == -1 || lastPosition == 0)
                {
                    list = parseText(text.Substring(0, text.Length), list);
                    break;
                }
                list = parseText(text.Substring(0, lastPosition), list);
                
                text = _repo.GetChunkOfText(start + lastPosition);
                start = start + lastPosition;
            }
            return list;
        }

        private Dictionary<string, int> parseText(string textChunk, Dictionary<string, int> processedWords)
        {
            var words = textChunk.Split(' ');
            foreach(var word in words)
            {
                if (processedWords.ContainsKey(word))
                {
                    processedWords[word] = processedWords[word] + 1;
                }
                else
                {
                    processedWords.Add(word, 1);
                }
            }
            return processedWords;
        }
    }
}
