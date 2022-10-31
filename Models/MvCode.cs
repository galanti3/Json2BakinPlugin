using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace json2bakinPlugin.Models
{
    public class MvCode
    {
        public int code { get; set; }
        public int? indent { get; set; }

        //generic parameter arrays. stored as string
        public string parameters { get; set; }

        //Bakin command name
        public string BakinCode { get; set; }

        public List<string> Params { get; set; }
    }
}
