using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagging.Model
{
    public class TaskCls
    {
        public TaskCls()
        {
            ImageList = new Dictionary<string, ImageDocument>();
        }

        public virtual string Class { get; set; }

        public string Type { get; set; }

        public string Channels { get; set; }

        public string DType { get; set; }

        [Newtonsoft.Json.JsonIgnore()]
        public Dictionary<string,ImageDocument> ImageList { get; set; }
        
        
    }
}
