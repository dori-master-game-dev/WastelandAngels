using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WLA.System.IO;

namespace WLA.System
{
    sealed public class Configuration : Serializer
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
