using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Json2BakinPlugin.Models;
using System.Dynamic;
using System;

namespace Json2BakinPlugin.Services
{
    public class MvMapDataLoadService
    {
        #region Variables
        MvMap _map;
        List<MvCommonEvent> _common;
        MvDatabase _database;
        #endregion

        #region Methods
        public List<string> LoadDatabase(string path)
        {
            MvDatabase database = new MvDatabase();
            List<string> dataList = new List<string> { "Actors", "Animations", "Armors", "Classes", "Enemies", "Items", 
                                                    "Skills", "States", "Troops", "Weapons", "MapInfos"};
            List<string> noExist = new List<string>();
            string text;
            List<string> names;
            List<string> ids;

            foreach (string data in dataList)
            {
                try
                {
                    string file =  (data == "Switches" || data == "Variables") ? "System" : data;
                    text = System.IO.File.ReadAllText(path + "/" + file + ".json");
                    names = Regex.Matches(text, @",""name"":.*?[^\\]""").Cast<Match>()
                                            .Select(d => d.Value.Replace(@",""name"":", "").Replace("\"", ""))
                                            .ToList();
                    ids = Regex.Matches(text, @"""id"":.*?[^\\]""").Cast<Match>()
                                            .Select(d => d.Value.Replace(@"""id"":", "").Replace(",", "").Replace("\"", ""))
                                            .ToList();
                    List<MvDatabaseItem> list = ids.Zip(names, (id, name) => new MvDatabaseItem { Id = int.Parse(id), Name = name }).ToList();
                    database.GetType().GetProperty(data).SetValue(database, list);
                }
                catch
                {
                    noExist.Add(data);
                }
            }
            try
            {
                //switches and variables
                text = System.IO.File.ReadAllText(path + "/SYSTEM.json");
                List<string> list = JsonSerializer.Deserialize<List<string>>(Regex.Matches(text, @",""switches"":\[.*?\]").Cast<Match>()
                                        .Select(d => d.Value.Replace(@",""switches"":", "")).ToList().First());
                database.Switches = list;
                list = JsonSerializer.Deserialize<List<string>>(Regex.Matches(text, @",""variables"":\[.*?\]").Cast<Match>()
                                        .Select(d => d.Value.Replace(@",""variables"":", "")).ToList().First());
                database.Variables = list;
            }
            catch {}

            _database = database;

            return noExist; //return nonexistent file names
        }

        public MvDatabase GetDatabase()
        {
            return _database;
        }

        public MvMap GetMap()
        {
            return _map;
        }

        public List<MvEvent> GetMapEvents()
        {
            return _map.events;
        }

        public string GetMapName(bool isMapNumber, bool isMapName)
        {
            string otext = "";
            if (isMapNumber || (!isMapNumber && (_map.displayName == null || _map.displayName == "")))
            {
                otext += "Map" + _map.IdString;
            }
            if (isMapName && _map.displayName != null && _map.displayName != "")
            {
                otext += (isMapNumber ? "_" : "") + _map.displayName;
            }
            return otext;
        }
        
        public List<MvCommonEvent> GetCommonEvents()
        {
            return _common;
        }

        public void DeserializeMapData(string file, string id)
        {
            string text = System.IO.File.ReadAllText(file);
            text = StringifyParameters(text);
            MvMap jsondata = JsonSerializer.Deserialize<MvMap>(text);
            SplitWithinCodeParams(jsondata.events);
            MergeInterCodeParams(jsondata.events);
            _map = jsondata;
            _map.IdString = id;
            _map.displayName = _database.MapInfos.Where(d => d.Id == int.Parse(id)).First().Name;
        }

        public void DeserializeCommonData(string file)
        {
            string text = System.IO.File.ReadAllText(file);
            text = StringifyParameters(text);
            List<MvCommonEvent> jsondata = JsonSerializer.Deserialize<List<MvCommonEvent>>(text);

            SplitWithinCodeParams(jsondata);
            MergeInterCodeParams(jsondata);
            _common = jsondata;
        }
        #endregion

        #region Privates
        private void SplitWithinCodeParams(List<MvEvent> mvEvents)
        {
            foreach (MvEvent ev in mvEvents)
            {
                if (ev != null)
                {
                    foreach (MvEventPage page in ev.pages)
                    {
                        foreach (MvCode code in page.list)
                        {
                            code.ExtractEventCodeParameters();
                        }
                    }
                }
            }
        }

        private void MergeInterCodeParams(List<MvEvent> mvEvents)
        {
            foreach (MvEvent ev in mvEvents)
            {
                if (ev != null)
                {
                    foreach (MvEventPage page in ev.pages)
                    {
                        page.MergeTextCodes();
                    }
                }
            }
        }

        private void SplitWithinCodeParams(List<MvCommonEvent> mvEvents)
        {
            foreach (MvCommonEvent ev in mvEvents)
            {
                if (ev != null)
                {
                    foreach (MvCode code in ev.list)
                    {
                        code.ExtractEventCodeParameters();
                    }
                }
            }
        }

        private void MergeInterCodeParams(List<MvCommonEvent> mvEvents)
        {
            foreach (MvCommonEvent ev in mvEvents)
            {
                if (ev != null)
                {
                    ev.MergeTextCodes();
                }
            }
        }

        private string StringifyParameters(string text)
        {
            string outtext = "";
            int idx0 = 0;
            int idx1 = text.IndexOf("\"parameters\":"); //parameter start pos
            if (idx1 == -1)
                return text;
            while (true)
            {
                outtext += text.Substring(idx0, idx1 - idx0); //skipped part;
                if (idx1 == text.Length) break;

                outtext += "\"parameters\":\"["; //"parameters":" 
                                                 //inside parameter. add a backslash to quote and backslash characters.
                var parastr = GetParameterChunkAsString(text.Substring(idx1 + 13));
                outtext += parastr.Item1;
                //close parameter.
                outtext += "]\"";
                //next chunk start.
                idx0 = idx1 + 13 + parastr.Item2 + 1;
                idx1 = text.Substring(idx0).IndexOf("\"parameters\":") + idx0;
                if (idx1 - idx0 == -1)
                {
                    idx1 = text.Length;
                }
            }
            return outtext;
        }

        private Tuple<string, int> GetParameterChunkAsString(string text)
        {
            string otext = "";
            //parameters are the string within "[]" blackets.
            int cnt = 0;
            int idx = 1; //0 should be "["
            while(true)
            {
                if (text[idx] == '[')
                {
                    cnt++;
                }
                else if (text[idx] == ']')
                {
                    if(--cnt < 0)
                    {
                        break;
                    }
                }
                otext += text[idx] == '"' ? "\\\"" : (text[idx] == '\\' ? "\\\\" : text[idx].ToString());
                idx++;
            }
            return new Tuple<string, int>(otext, idx);
        }

        #endregion
    }
}
