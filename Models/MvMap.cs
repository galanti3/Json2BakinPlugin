using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json2BakinPlugin.Models
{
    public class MvMap
    {
        #region properties
        //map ID. created from file name.
        public string IdString { get; set; }
        public bool autoplayBgm {get; set;}
        public bool autoplayBgs { get; set; }
        public string battleback1Name { get; set; }
        public string battleback2Name { get; set; }

        public MvAudio bgm { get; set; }
        public MvAudio bgs { get; set; }
        public bool disableDashing { get; set; }
        public string displayName { get; set; }
        public List<int> encounterList { get; set; }
        public int height { get; set; }
        public string note { get; set; }
        public bool parallaxLoopX { get; set; }
        public bool parallaxLoopY { get; set; }
        public string parallaxName { get; set; }
        public bool parallaxShow { get; set; }
        public int parallaxSx { get; set; }
        public int parallaxSy { get; set; }
        public int scrollType { get; set; }
        public bool specifyBattleback { get; set; }
        public int tilesetId { get; set; }
        public int width { get; set; }

        public List<int> data { get; set; }

        public List<MvEvent> events { get; set; }
        #endregion
    }
}
