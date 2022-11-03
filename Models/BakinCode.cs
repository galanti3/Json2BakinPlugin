using System;
using System.Collections.Generic;

namespace Json2BakinPlugin.Models
{
    public class BakinCode
    {
        #region Properties
        public string Code { get; set; }
        public List<BakinParameter> Params { get; set; }
        #endregion
    }

    public class BakinParameter
    {
        #region Properties
        public string Type { get; set; }
        public string Value { get; set; }

        public string Description { get; set; }

        #endregion
        #region Constructor
        public BakinParameter(string type, string desc)
        {
            Type = type;
            Description = desc;
            switch(Type)
            {
                case "コマンド": //command name
                case "コマンド終了": //command end
                    Value = desc;
                    Description = "";
                    break;
                case "整数":
                case "小数":
                    Value = "0";
                    break;
                case "文字列":
                    Value = "";
                    break;
                case "Guid":
                    Value = Guid.Empty.ToString();
                    break;
                case "変数":
                    Value = "";
                    break;
                case "ローカル変数":
                    Value = "A";
                    break;
                case "スポット": //マップGuid | スポットID | X位置（変数可）| Y位置 | Z位置（変数可）
                    Value = Guid.Empty.ToString() + "|1001|0|0|0";
                    break;
                default:
                    Value = "";
                    break;
            }
        }

        public BakinParameter(string type, string desc, string value)
        {
            Type = type;
            Description = desc;
            Value = value;
        }
        #endregion
    }
}
