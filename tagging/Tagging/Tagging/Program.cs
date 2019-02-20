using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tagging.Document;
using Tagging.Model;

namespace Tagging
{
    static class Program
    {
        static IDocument _document = new DocumentInfo();
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            DocumentConfig._Path = Application.StartupPath + "\\TaggingData";
            //创建数据文件夹
            if (!Directory.Exists(DocumentConfig._Path))
            {
                Directory.CreateDirectory(DocumentConfig._Path);
            }
            DocumentConfig.MainDocuments = new Dictionary<string, TaskCls>();
            //读取历史数据
            List<string> TaskList = _document.GetDirectoryInfo(DocumentConfig._Path);
            List<string> DocumentList = new List<string>();
            foreach (string item in TaskList)
            {
                DocumentConfig.MainDocuments.Add(item, new TaskCls() { Class = item });
                DocumentList = _document.GetDocument($"{DocumentConfig._Path}\\{item}");

                //读文件
                string fp = $"{DocumentConfig._Path}\\{item}.json";
                TaskCls obji = JsonConvert.DeserializeObject<TaskCls>(File.ReadAllText(fp));
                if (File.Exists(fp))
                {                    
                    DocumentConfig.MainDocuments[item].Class = obji.Class;
                    DocumentConfig.MainDocuments[item].Channels = obji.Channels;
                    DocumentConfig.MainDocuments[item].DType = obji.DType;
                    DocumentConfig.MainDocuments[item].Type = obji.Type;
                }

                foreach (string documentItem in DocumentList)
                {
                    DocumentConfig.MainDocuments[item].ImageList.Add(documentItem, new ImageDocument() { FileName = documentItem });
                    //读文件
                    fp = $"{DocumentConfig._Path}\\{item}\\{(documentItem.Contains(".") ? documentItem.Substring(0, documentItem.IndexOf('.')) : documentItem)}.json";

                    if (File.Exists(fp))
                    {
                        ImageDocument imgobj = JsonConvert.DeserializeObject<ImageDocument>(File.ReadAllText(fp));
                        DocumentConfig.MainDocuments[item].ImageList[documentItem].Class = imgobj.Class;
                        DocumentConfig.MainDocuments[item].ImageList[documentItem].Channels = imgobj.Channels;
                        DocumentConfig.MainDocuments[item].ImageList[documentItem].DType = imgobj.DType;
                        DocumentConfig.MainDocuments[item].ImageList[documentItem].Type = imgobj.Type;
                        DocumentConfig.MainDocuments[item].ImageList[documentItem].FileName = imgobj.FileName;
                        DocumentConfig.MainDocuments[item].ImageList[documentItem].Width = imgobj.Width;
                        DocumentConfig.MainDocuments[item].ImageList[documentItem].Height = imgobj.Height;
                        DocumentConfig.MainDocuments[item].ImageList[documentItem].Annotations = imgobj.Annotations;
                    }
                    else
                    {
                        DocumentConfig.MainDocuments[item].ImageList[documentItem].Class = obji.Class;
                        DocumentConfig.MainDocuments[item].ImageList[documentItem].Channels = obji.Channels;
                        DocumentConfig.MainDocuments[item].ImageList[documentItem].DType = obji.DType;
                        DocumentConfig.MainDocuments[item].ImageList[documentItem].Type = obji.Type;
                    }
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
