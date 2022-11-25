using Json2BakinPlugin.Models;
using System.Collections.Generic;

namespace Json2BakinPlugin.Services
{
    public class MvDatabase
    {
        #region Properties
        public List<MvDatabaseItem> Actors { get; set; } = new List<MvDatabaseItem>();
        public List<MvDatabaseItem> Animations { get; set; } = new List<MvDatabaseItem>();
        public List<MvDatabaseItem> Armors { get; set; } = new List<MvDatabaseItem>();
        public List<MvDatabaseItem> Classes { get; set; } = new List<MvDatabaseItem>();
        public List<MvDatabaseItem> Enemies { get; set; } = new List<MvDatabaseItem>();
        public List<MvDatabaseItem> Items { get; set; } = new List<MvDatabaseItem>();
        public List<MvDatabaseItem> Skills { get; set; } = new List<MvDatabaseItem>();
        public List<MvDatabaseItem> States { get; set; } = new List<MvDatabaseItem>();
        public List<MvDatabaseItem> Troops { get; set; } = new List<MvDatabaseItem>();
        public List<MvDatabaseItem> Weapons { get; set; } = new List<MvDatabaseItem>();

        public List<string> Switches { get; set; } = new List<string>();
        public List<string> Variables { get; set; } = new List<string>();
        public List<MvDatabaseItem> MapInfos { get; set; } = new List<MvDatabaseItem>();
        #endregion

    }
}
