using System.Collections.Generic;

namespace Json2BakinPlugin.Models
{
    public class MvCodeList
    {
        #region Properties
        public int trigger { get; set; }
        public List<MvCode> list { get; set; }
        #endregion

        #region Methods
        public void MergeTextCodes()
        {
            List<MvCode> codes = new List<MvCode>();
            int i = 0;
            while (i < list.Count)
            {
                if (list[i].code == 101 || list[i].code == 105 || list[i].code == 108 || list[i].code == 355)
                {
                    int j = 1;
                    if (list[i].code == 101 || list[i].code == 105)
                    {
                        list[i].Params.Add(""); //Messaga & scroll texts has no text param, so add to the last.
                    }
                    while (list[i + j].code == list[i].code + 300)
                    {
                        string nl = ((list[i].code == 101 || list[i].code == 105) && j == 1) ? "" : "\\n";
                        list[i].Params[list[i].Params.Count - 1] += nl + list[i + j].Params[0];
                        j++;
                    }
                    codes.Add(list[i]);
                    i += j;
                }
                else
                {
                    codes.Add(list[i]);
                    i++;
                }
            }
        }
        #endregion
    }
}