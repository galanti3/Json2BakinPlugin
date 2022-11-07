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
		#region Privates
		MvMap _map;
		MvDatabase _database;
        #endregion

        #region Methods
        public void LoadDatabase(string path)
		{
			MvDatabase database = new MvDatabase();
			List<string> fileList = new List<string> { "Actors", "Animations", "Armors", "Classes", "Enemies", "Items", 
													"Mapinfos", "Skills", "States", "Troops", "Weapons", "Switches", "Variables" };
			foreach(string file in fileList)
            {
				try
				{
					string text = System.IO.File.ReadAllText(path + "/" + file + ".json");
					//elminate se for animation
					List<string> list = Regex.Matches(text, @",""name"":.*?[^\\]""").Cast<Match>()
											.Select(d => d.Value.Replace(@",""name"":", "")).ToList();
				}
				catch
				{
					database.GetType().GetProperty(file).SetValue(database, new List<string>());
				}
			}
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
		
		public void DeserializeMapData(string file, string id)
        {
            string text = System.IO.File.ReadAllText(file);
            text = StringifyParameters(text);
            MvMap jsondata = JsonSerializer.Deserialize<MvMap>(text);
			SplitWithinCodeParams(jsondata);
			MergeInterCodeParams(jsondata);
            _map = jsondata;
			_map.IdString = id;
        }

		public void SplitWithinCodeParams(MvMap jsondata)
		{
			List<MvEvent> mvEvents = jsondata.events;
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

        public void MergeInterCodeParams(MvMap jsondata)
		{
			List<MvEvent> mvEvents = jsondata.events;
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

		public string StringifyParameters(string text)
		{
			List<string> paraendStrings = new List<string> { "]},", "]}]" }; //parameter endpos for middle of list or last of list
			string outtext = String.Empty;
			int idx0 = 0;
			int idx1 = text.IndexOf("\"parameters\":"); //parameter start pos

			List<int> idx2Cand = new List<int> { text.IndexOf(paraendStrings[0]), text.IndexOf(paraendStrings[1]) };
			int idx2 = idx2Cand.Min(); //parameter end pos

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
