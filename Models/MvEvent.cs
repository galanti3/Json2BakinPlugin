using System.Collections.Generic;

namespace Json2BakinPlugin.Models
{
    public class MvEvent
    {
        #region Properties
        public int id { get; set; }
        public string name { get; set; }
        public string note { get; set; }
        public int x { get; set; }
        public int y { get; set; }

        public List<MvEventPage> pages { get; set; }
        #endregion
    }
}