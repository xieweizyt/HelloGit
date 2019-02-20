using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagging.Document
{
    public interface IDocument
    {
        void AddFile(string sourcefile, string targetfile);

        List<string> GetDirectoryInfo(string sourcePath);

        List<string> GetDocument(string sourcePath);
    }
}
