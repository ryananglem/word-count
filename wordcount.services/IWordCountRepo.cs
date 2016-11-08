using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wordcount.services
{
    public interface IWordCountRepo
    {
        IQueryable GetWordList();

        void AddWord(string word);

    }
}
