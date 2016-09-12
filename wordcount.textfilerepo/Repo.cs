using System;
using System.IO;
using System.Text;
using wordcount.services;

namespace wordcount.textfilerepo
{
    public class Repo : IRepo
    {
        private long _chunkSize;
        private string _fileName;

        public Repo(int chunkSize, string fileName)
        {
            if (chunkSize == 0) throw new ArgumentException(nameof(chunkSize));
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentException(nameof(fileName));

            _chunkSize = chunkSize;
            _fileName = fileName;
        }

        // start could be made type long for very long files
        public string GetChunkOfText(int start)
        {
            var chunkSize = _chunkSize;
            using (FileStream fs = new FileStream(_fileName, FileMode.Open, FileAccess.Read))
            {
                if (fs.Length < start + chunkSize)
                {
                    chunkSize = fs.Length - start;
                }
                fs.Seek(start, SeekOrigin.Begin);

                byte[] b = new byte[chunkSize];
                fs.Read(b, 0, (int)(chunkSize));

                return Encoding.UTF8.GetString(b);
            }
        }
    }
}
