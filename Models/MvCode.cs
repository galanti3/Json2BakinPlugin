using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json2BakinPlugin.Models
{
    public class MvCode
    {
        #region Properties
        public int code { get; set; }
        public int? indent { get; set; }

        //generic parameter arrays. stored as string
        public string parameters { get; set; }

        //id to distinguish different parameter conditions
        public int Subcode { get; set; }
        //Bakin command name and description
        public List<string> BakinCode { get; set; }

        public List<string> Params { get; set; }
        #endregion

        #region Methods
        public void ExtractEventCodeParameters()
        {
            List<string> output = new List<string>();
            int idx = 1;
            int start; //start index
                       //each element starts with character, "[" or "{";
            while (idx < parameters.Length - 1)
            {
                if (parameters[idx] == ',') //separator
                {
                    idx++;
                }
                else if (parameters[idx] == '{') //class
                {
                    start = idx;
                    idx = GetWithinBracketString(parameters, start, '{', '}');
                    output.Add(parameters.Substring(start, idx - start));
                    idx++;
                }
                else if (parameters[idx] == '[') //array
                {
                    start = idx;
                    idx = GetWithinBracketString(parameters, start, '[', ']');
                    output.Add(parameters.Substring(start, idx - start));
                    idx++;
                }
                else if (parameters[idx] == '\"') //string
                {
                    idx++;
                    start = idx;
                    while (parameters[idx] != '\"' || parameters[idx - 1] == '\\') idx++;
                    output.Add(parameters.Substring(start, Math.Max(idx - start, 0)));
                    idx++;
                }
                else
                {
                    start = idx;
                    while (idx < parameters.Length && parameters[idx] != ',' && parameters[idx] != ']') idx++;
                    output.Add(parameters.Substring(start, idx - start));
                }
            }
            Params = output;
        }

        //extract route parameters and replace with 505 code.
        public void ExtractRouteCode()
        {
            List<string> paras = parameters.Split(',').ToList();
            code = int.Parse(paras[0].Substring(paras[0].IndexOf(":") + 1));
            paras.RemoveAt(0);
            string oldpara = string.Join(",", paras);
            int bgn = oldpara.IndexOf(":"); //colon of "parameters"
            int end = oldpara.IndexOf(",\"indent\":");
            if (end - bgn < 0)
            {
                Params = null;
            }
            else
            {
                parameters = oldpara.Substring(bgn + 1, end - (bgn + 1));
                ExtractEventCodeParameters();
            }
        }

        public void GenerateSubCode(string moveChar = "0")
        {
            var scode = code * 100;
            if (scode != 0 && scode < 10000) //route
            {
                scode += moveChar == "0" ? 1 : 0;
            }
            else if (code == 111)
            {
                scode += GetSubcode111();
            }
            else if (code == 122)
            {
                scode += GetSubcode122();
            }
            Subcode = scode;
        }
        #endregion

        #region Privates
        private int GetSubcode122()
        {
            int id = int.Parse(Params[3]);
            if (id <= 1) //var
            {
                return 0;
            }
            else if (id == 2) //random var
            {
                return 1;
            }
            else if (id == 4) //script
            {
                return 8;
            }
            else
            {
                int id2 = int.Parse(Params[4]);
                return id2 <= 2 ? 2 : id2;
            }
        }

        private int GetSubcode111()
        {
            int id = int.Parse(Params[0]);
            if(id != 4)
            {
                return id + (id >= 5 ? 3 : 0);
            }
            else //if actor
            {
				int id2 = int.Parse(Params[2]);
				if (id2 == 2 || id2 == 3 || id2 == 6) //class, skill, state
                {
                    return 6;
                }
                else if (id2 == 4 || id2 == 5) //weapon, armor
                {
                    return 7;
                }
                else //id2 <= 1
                {
                    return 4 + id2;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text">string</param>
        /// <param name="start">index of open bracket</param>
        /// <param name="v">open bracket character</param>
        /// <param name="closev">close bracket character</param>
        /// <returns>index of close bracket</returns>
        private int GetWithinBracketString(string text, int start, char v, char closev)
        {
            int idx = start + 1;
            int numbracket = 0;
            while (text[idx] != closev || numbracket > 0)
            {
                if (text[idx] == v) numbracket++;
                else if (text[idx] == closev && numbracket > 0) numbracket--;
                idx++;
            }
            return idx + 1;
        }
        #endregion
    }
}
