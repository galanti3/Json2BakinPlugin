using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json2BakinPlugin.Models
{
    public class MvEventCodeParameter
    {
        #region Properties
        public List<string> Strings { get; set; }
        public List<MvEventMoveRoute> Routes { get; set; }
        public List<MvAudio> Audios { get; set; }
        public string DataType { get; set; } //Strings, Codes or Audios
        public List<MvEventCodeParamType> Types { get; set; }
        #endregion
    }

    public enum MvEventCodeParamType
    {
        STRING,
        INT,
        FLOAT,
        ROUTE,
        AUDIO,
    }
}
