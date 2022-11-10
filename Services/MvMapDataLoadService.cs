using System;
using System.Text.Json;
using System.Collections.Generic;
using Json2BakinPlugin.Models;
using System.Linq;
using System.Text.RegularExpressions;

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
        public void LoadDatabase(string path)
		{
			MvDatabase database = new MvDatabase();
			List<string> dataList = new List<string> { "Actors", "Animations", "Armors", "Classes", "Enemies", "Items", 
													"Skills", "States", "Troops", "Weapons"};
			string text;
			List<string> list;
			foreach(string data in dataList)
            {
				try
				{
					string file =  (data == "Switches" || data == "Variables") ? "System" : data;
					text = System.IO.File.ReadAllText(path + "/" + file + ".json");
					list = Regex.Matches(text, @",""name"":.*?[^\\]""").Cast<Match>()
											.Select(d => d.Value.Replace(@",""name"":", "").Replace("\"", ""))
											.ToList();
                    database.GetType().GetProperty(data).SetValue(database, list);
                }
                catch {}
			}
			try
			{
                //switches and variables
                text = System.IO.File.ReadAllText(path + "/SYSTEM.json");
                list = JsonSerializer.Deserialize<List<string>>(Regex.Matches(text, @",""switches"":\[.*?\]").Cast<Match>()
                                        .Select(d => d.Value.Replace(@",""switches"":", "")).ToList().First());
                database.Switches = list;
                list = JsonSerializer.Deserialize<List<string>>(Regex.Matches(text, @",""variables"":\[.*?\]").Cast<Match>()
                                        .Select(d => d.Value.Replace(@",""variables"":", "")).ToList().First());
                database.Variables = list;
            }
            catch {}

            _database = database;
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

		public string GetMapName()
		{
			if (_map.displayName != null && _map.displayName != "")
			{
				return _map.displayName;
			}
			else
			{
				return "Map" + _map.IdString;
			}
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

        public void SplitWithinCodeParams(List<MvEvent> mvEvents)
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

        public void MergeInterCodeParams(List<MvEvent> mvEvents)
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

        public void SplitWithinCodeParams(List<MvCommonEvent> mvEvents)
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

        public void MergeInterCodeParams(List<MvCommonEvent> mvEvents)
        {
            foreach (MvCommonEvent ev in mvEvents)
            {
                if (ev != null)
                {
					ev.MergeTextCodes();
                }
            }
        }

        public string StringifyParameters(string text)
		{
			List<string> paraendStrings = new List<string> { "]},", "]}]" }; //parameter endpos for middle of list or last of list
			string outtext = "";
			int idx0 = 0;
			int idx1 = text.IndexOf("\"parameters\":"); //parameter start pos

			List<int> idx2Cand = new List<int> { text.IndexOf(paraendStrings[0]), text.IndexOf(paraendStrings[1]) };
            int idx2 = idx2Cand.Min() == -1 ? idx2Cand.Max() : idx2Cand.Min(); //parameter end pos

			while (true)
			{
				outtext += text.Substring(idx0, idx1 - idx0); //skipped part;
				if (idx1 == text.Length) break;

				outtext += "\"parameters\":\""; //"parameters":" 
												//inside parameter. add a backslash to quote and backslash characters.
				for (int i = idx1 + 13; i < idx2; i++)
				{
					outtext += text[i] == '"' ? "\\\"" : (text[i] == '\\' ? "\\\\" : text[i].ToString());
				}
				//close parameter.
				outtext += "]\"";
				//next chunk start.
				idx0 = idx2 + 1;
				idx1 = text.Substring(idx0, text.Length - idx0).IndexOf("\"parameters\":") + idx0;
				if (idx1 - idx0 == -1)
				{
					idx1 = text.Length;
				}
				else
				{
					string substr = text.Substring(idx1, text.Length - idx1);
					idx2Cand = new List<int> { substr.IndexOf(paraendStrings[0]), substr.IndexOf(paraendStrings[1]) };
					idx2 = (idx2Cand.Min() == -1 ? idx2Cand.Max() : idx2Cand.Min()) + idx1;
				}
			}
			return outtext;
		}
        #endregion
    }
}
