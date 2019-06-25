using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_App.API
{
    public class Sproeier
    {
        public int teeltbed { get; set; }
        public float waarde { get; set; }
        public float hoog { get; set; }
        public float laag { get; set; }
        public bool status { get; set; }
    }
}
