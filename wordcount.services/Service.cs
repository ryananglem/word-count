using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wordcount.services
{
    public class Service : IService
    {
        private readonly IDocumentRepo _documentRepository;

        public Service (IDocumentRepo documentRepository)
        {
            if (documentRepository == null) throw new ArgumentNullException(nameof(documentRepository));

            _documentRepository = documentRepository;
        }

        public Dictionary<string, int> CountWordsInDocument()
        {
            var list = new Dictionary<string, int>();
            var start = 0;
            var text = _documentRepository.GetChunkOfText(start)
                                .StripExtraCharacters()
                                .ToLower();
            
            while (text != string.Empty)
            {
                var lastPosition = text.LastIndexOf(" ", StringComparison.Ordinal);
                if (lastPosition == -1 || lastPosition == 0)
                {
                    list = parseText(text.Substring(0, text.Length), list);
                    break;
                }
                list = parseText(text.Substring(0, lastPosition), list);
                
                text = _documentRepository.GetChunkOfText(start + lastPosition)
                         .StripExtraCharacters()
                         .ToLower();
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
