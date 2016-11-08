using System;
using System.IO;
using System.Text;
using wordcount.services;

namespace wordcount.textfilerepo
{
    public class TextFileDocumentRepo : IDocumentRepo
    {
        private readonly int _chunkSize;
        private readonly string _fileName;

        public TextFileDocumentRepo(int chunkSize, string fileName)
        {
            if (chunkSize == 0) throw new ArgumentException(nameof(chunkSize));
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentException(nameof(fileName));

            _chunkSize = chunkSize;
            _fileName = fileName;
        }

        public string GetChunkOfText(long start)
        {
            var chunkSize = _chunkSize;
            using (FileStream fs = new FileStream(_fileName, FileMode.Open, FileAccess.Read))
            {
                if (fs.Length < start + chunkSize)
                {
                    chunkSize = (int) (fs.Length - start);
                }
                fs.Seek(start, SeekOrigin.Begin);

                byte[] b = new byte[chunkSize];
                fs.Read(b, 0, (int)(chunkSize));

                return Encoding.UTF8.GetString(b);
            }
        }
    }
}
