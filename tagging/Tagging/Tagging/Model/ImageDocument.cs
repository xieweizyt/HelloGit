using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagging.Model
{
    public class ImageDocument : TaskCls
    {
        public ImageDocument()
        {
            Annotations = new List<ItemDocument>();
        }
        public string FileName { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
        
        public List<ItemDocument> Annotations { get; set; }
        //public override string Class
        //{
        //    get{ return base.Class; } 
        //}
    }
}
