using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Json2BakinPlugin.Services
{
    public class MvDatabase
    {
        #region Properties
        public List<string> Actors { get; set; }
        public List<string> Animations { get; set; }
        public List<string> Armors { get; set; }
        public List<string> Classes { get; set; }
        public List<string> Enemies { get; set; }
        public List<string> Items { get; set; }
        public List<string> Mapinfos { get; set; }
        public List<string> Skills { get; set; }
        public List<string> States { get; set; }
        public List<string> Troops { get; set; }
        public List<string> Weapons { get; set; }

        public List<string> Switches { get; set; }
        public List<string> Variables { get; set; }
        #endregion

        public void StoreSwitchNames(string data)
        {
            int bgn = data.IndexOf("\"switches\":[") + 11;
            int leng = data.Substring(bgn).IndexOf("],") + 1;
            Switches = JsonSerializer.Deserialize<List<string>>(data.Substring(bgn, leng));
        }

        public void StoreVariableNames(string data)
        {
            int bgn = data.IndexOf("\"variables\":[") + 12;
            int leng = data.Substring(bgn).IndexOf("],") + 1;
            Variables = JsonSerializer.Deserialize<List<string>>(data.Substring(bgn, leng));
        }
    }
}
