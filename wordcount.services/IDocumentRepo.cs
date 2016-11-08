
namespace wordcount.services
{
    public interface IDocumentRepo
    {
        string GetChunkOfText(long start);
    }
}
