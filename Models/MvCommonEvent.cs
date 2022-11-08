using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json2BakinPlugin.Models
{
    internal class MvCommonEvent
    {
        public int id { get; set; }
        public string name { get; set; }
        public int switchId { get; set; }
        public int trigger { get; set; }
        public List<MvCode> list { get; set; }

    }
}
