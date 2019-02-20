using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagging.Document
{
    public class DocumentInfo : IDocument
    {
        public void AddFile(string sourcefile, string targetfile)
        {
            File.Copy(sourcefile, targetfile, true);
        }

        public List<string> GetDirectoryInfo(string sourcePath)
        {
            List<string> DirectoryList = new List<string>();
            DirectoryInfo TheFolder = new DirectoryInfo(sourcePath);
            //遍历文件夹
            foreach (DirectoryInfo NextFolder in TheFolder.GetDirectories())
                DirectoryList.Add(NextFolder.Name);

            return DirectoryList;
        }

        public List<string> GetDocument(string sourcePath)
        {
            List<string> DocumentList = new List<string>();
            DirectoryInfo TheFolder = new DirectoryInfo(sourcePath);
            //遍历文件
            foreach (FileInfo NextFile in TheFolder.GetFiles())
                DocumentList.Add(NextFile.Name);
            return DocumentList;
        }
    }
}
