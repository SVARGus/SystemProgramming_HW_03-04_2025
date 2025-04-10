using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryWorldFinder_WPF
{
    public class FileSearchResult
    {
        public string fileName { get; set; }
        public string filePath { get; set; }
        public int wordCount { get; set; }

        public FileSearchResult(string fileName, string filePath, int wordCount)
        {
            this.fileName = fileName;
            this.filePath = filePath;
            this.wordCount = wordCount;
        }
        public FileSearchResult() { }
    }
}
