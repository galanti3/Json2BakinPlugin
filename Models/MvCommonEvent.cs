using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json2BakinPlugin.Models
{
    internal class MvCommonEvent
    {
        #region Properties
        public int id { get; set; }
        public string name { get; set; }
        public int switchId { get; set; }
        public int trigger { get; set; }
        public List<MvCode> list { get; set; }
        public string TriggerCode
        {
            get
            {
                switch (trigger)
                {
                    case 1:
                        return "AUTO_REPEAT";
                    case 2:
                        return "PARALLEL";
                    default:
                        return "TALK";
                }
            }
        }
        #endregion
    }
}
