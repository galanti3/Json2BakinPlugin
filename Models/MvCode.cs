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

        //Bakin command name
        public string BakinCode { get; set; }

        public List<string> Params { get; set; }
		#endregion

		#region Functions
		public void SplitEventCodeParameters()
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
					idx = GetInBracketString(parameters, start, '{', '}');
					output.Add(parameters.Substring(start, idx - start));
					idx++;
				}
				else if (parameters[idx] == '[') //array
				{
					start = idx;
					idx = GetInBracketString(parameters, start, '[', ']');
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
				SplitEventCodeParameters();
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
		private int GetInBracketString(string text, int start, char v, char closev)
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
