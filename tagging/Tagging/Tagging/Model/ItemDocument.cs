using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagging.Model
{
    public class ItemDocument
    {
        public string Class { get; set; }

        public double X_Min { get; set; }

        public double Y_Min { get; set; }

        public double X_Max { get; set; }

        public double Y_Max { get; set; }

        public string Utf8_str { get; set; }
    }
}
