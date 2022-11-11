using System.Collections.Generic;

namespace Json2BakinPlugin.Services
{
    public class MvDatabase
    {
        #region Properties
        public List<string> Actors { get; set; } = new List<string>();
        public List<string> Animations { get; set; } = new List<string>();
        public List<string> Armors { get; set; } = new List<string>();
        public List<string> Classes { get; set; } = new List<string>();
        public List<string> Enemies { get; set; } = new List<string>();
        public List<string> Items { get; set; } = new List<string>();
        public List<string> Skills { get; set; } = new List<string>();
        public List<string> States { get; set; } = new List<string>();
        public List<string> Troops { get; set; } = new List<string>();
        public List<string> Weapons { get; set; } = new List<string>();

        public List<string> Switches { get; set; } = new List<string>();
        public List<string> Variables { get; set; } = new List<string>();
        #endregion

    }
}
