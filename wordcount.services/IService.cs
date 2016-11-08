using System.Collections.Generic;

namespace wordcount.services
{
    public interface IService
    {
        Dictionary<string, int> CountWordsInDocument();
    }
}
