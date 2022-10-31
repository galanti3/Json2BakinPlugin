using System;
using System.Text.Json;
using System.Collections.Generic;
using Json2BakinPlugin.Models;
using System.Linq;
using json2bakinPlugin.Models;

namespace Json2BakinPlugin.Services
{
    public class MvMapDataLoadService
    {
        #region functions
        public MvMap DeserializeMapData(string file)
        {
            //path = @"D:\Backups\gamedev\Somato\data\";
            //file = "Map001.json";
            string text = System.IO.File.ReadAllText(file);
            text = StringifyParameters(text);
            MvMap jsondata = JsonSerializer.Deserialize<MvMap>(text);
            SplitParams(jsondata);
            MergeParams(jsondata);
            return jsondata;
        }

        public void SplitParams(MvMap jsondata)
        {
            List<MvEvent> mvEvents = jsondata.events;
            foreach(MvEvent ev in mvEvents)
            {
                if(ev != null)
                {
                    foreach (MvEventPage page in ev.pages)
                    {
                        foreach (MvCode code in page.list)
                        {
                            code.Params = SplitEventCodeParameters(code.parameters);
                        }
                    }
                }
            }
        }

        public void MergeParams(MvMap jsondata)
        {
            List<MvEvent> mvEvents = jsondata.events;
            foreach (MvEvent ev in mvEvents)
            {
                if (ev != null)
                {
                    foreach (MvEventPage page in ev.pages)
                    {
                        MergeTextCodes(page);
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

            List<int> idx2Cand = new List<int>{text.IndexOf(paraendStrings[0]), text.IndexOf(paraendStrings[1])};
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

        public List<string> SplitEventCodeParameters(string text)
        {
            List<string> output = new List<string>();
            int idx = 1;
            int start; //start index
            //each element starts with character or "{";
            while(idx < text.Length-1)
            {
                if (text[idx] == ',') //separator
                {
                    idx++;
                }
                else if (text[idx] == '{') //class
                {
                    start = idx;
                    idx = GetInBracketString(text, start, '{', '}');
                    output.Add(text.Substring(start, idx - start));
                    idx++;
                }
                else if (text[idx] == '[') //array
                {
                    start = idx;
                    idx = GetInBracketString(text, start, '[', ']');
                    output.Add(text.Substring(start, idx - start));
                    idx++;
                }
                else if (text[idx] == '\"') //string
                {
                    idx++;
                    start = idx;
                    while (text[idx] != '\"' || text[idx - 1] == '\\') idx++;
                    output.Add(text.Substring(start, Math.Max(idx - start, 0)));
                    idx++;
                }
                else
                {
                    start = idx;
                    while (idx < text.Length && text[idx] != ',' && text[idx] != ']') idx++;
                    output.Add(text.Substring(start, idx - start));
                }
            }
            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text">string</param>
        /// <param name="start">index of open bracket</param>
        /// <param name="v">open bracket character</param>
        /// <param name="closev">close bracket character</param>
        /// <returns>index of close bracket</returns>
        private int GetInBracketString(string text, int start, char v, char closev)
        {
            int idx = start+1;
            int numbracket = 0;
            while (text[idx] != closev || numbracket > 0)
            {
                if (text[idx] == v) numbracket++;
                else if (text[idx] == closev && numbracket > 0) numbracket--;
                idx++;
            }
            return idx + 1;
        }

        public void MergeTextCodes(MvEventPage page)
        {
            List<MvCode> codes = new List<MvCode>();
            int i = 0;
            while (i < page.list.Count)
            {
                if (page.list[i].code == 101 || page.list[i].code == 105 || page.list[i].code == 108 || page.list[i].code == 355)
                {
                    int j = 1;
                    if (page.list[i].code == 101 || page.list[i].code == 105)
                    {
                        page.list[i].Params.Add(""); //Messaga & scroll texts has no text param, so add to the last.
                    }
                    while (page.list[i + j].code == page.list[i].code + 300)
                    {
                        string nl = ((page.list[i].code == 101 || page.list[i].code == 105) && j == 1) ? "" : "\\n";
                        page.list[i].Params[page.list[i].Params.Count - 1] += nl + page.list[i + j].Params[0];
                        j++;
                    }
                    codes.Add(page.list[i]);
                    i += j;
                }
                else
                {
                    codes.Add(page.list[i]);
                    i++;
                }
            }
        }
    }
    #endregion
}
