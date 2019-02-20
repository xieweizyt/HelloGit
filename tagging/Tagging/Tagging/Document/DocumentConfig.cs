using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tagging.Model;

namespace Tagging.Document
{
    public class DocumentConfig
    {
        //数据文件夹路径
        public static string _Path = string.Empty;

        public static Dictionary<string,TaskCls> MainDocuments;
    }
}
