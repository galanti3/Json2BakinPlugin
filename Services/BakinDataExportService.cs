using json2bakinPlugin.Models;
using Json2BakinPlugin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Json2BakinPlugin.Services
{
    public class BakinDataExportService
    {
        private Json2BakinConvertService convertService = new Json2BakinConvertService();
        private MvBakinCodeDictionary codeDic = new MvBakinCodeDictionary();

        private bool nullCommand = false;

        #region Functions
        public void Preprocess(MvMap map)
        {
            foreach (MvEvent ev in map.events)
            {
                if (ev != null)
                {
                    foreach (MvEventPage page in ev.pages)
                    {
                        convertService.ConvertRouteCodesToDestinationCode(page);
                    }
                }
            }
        }

        public void Postprocess(MvMap map)
        {
            foreach (MvEvent ev in map.events)
            {
                if (ev != null)
                {
                    foreach (MvEventPage page in ev.pages)
                    {
                        foreach (MvCode code in page.list)
                        {
                            if (code.BakinCode != null && code.BakinCode.Contains("COMMENT") && !code.BakinCode.EndsWith("注釈"))
                            {
                                code.Params = new List<string> { code.BakinCode.Replace("COMMENT\t", "") };
                            }
                        }
                    }
                }
            }
        }

        public void RegisterBakinCodes(MvMap map)
        {
            foreach(MvEvent ev in map.events)
            {
                if(ev != null)
                {
                    foreach (MvEventPage page in ev.pages)
                    {
                        foreach (MvCode code in page.list)
                        {
                            code.BakinCode = codeDic.Code(code.code*100);
                        }
                    }
                }
            }
        }

        public void ExportMap(MvMap map, string path)
        {
            string mapName = GetMapName(map);
            foreach (MvEvent ev in map.events)
            {
                if (ev != null)
                {
                    string evName = GetEventName(ev);
                    int pidx = 1;
                    foreach (MvEventPage page in ev.pages)
                    {
                        string fileName = string.Format("{0}_{1}_Page{2:000}.txt", mapName, evName, pidx);
                        string otext = "";
                        otext += WriteEventInfo(evName);
                        otext += WritePage(page);
                        otext += "スクリプト終了\n\nシート終了";
                        File.WriteAllText(path + "\\" + fileName, otext);
                        pidx++;
                    }
                }
            }
        }

        private string WriteEventInfo(string evName)
        {
            string otext = "";
            otext += "Guid\t" + Guid.Empty + "\n";
            otext += "イベント名\t" + evName + "\n";
            return otext;
        }

        public string WritePage(MvEventPage page)
        {
            string otext = "";
            otext += WriteBasicPageInfo(page);
            otext += "\tスクリプト\n";
            otext += "\t\t開始条件\tTALK\n";
            otext += "\t\t高さ無視\tFalse\n";
            otext += "\t\t判定拡張\tFalse\n";
            foreach (MvCode code in page.list)
            {
                otext += ExportCode(convertService.ConvertToBakinCode(code));
            }
            return otext;
        }

        private string WriteBasicPageInfo(MvEventPage page)
        {
            string otext = "";
            otext += "シート\tイベントシート\n";
            otext += "\tグラフィック\t" + Guid.Empty + "\n";
            otext += "\tモーション\twait\n";
            otext += "\t向き\t-1\n";
            otext += "\t向き固定\t" + page.directionFix.ToString() + "\n";
            otext += "\t物理\tFalse\n";
            otext += "\t衝突判定\t" + (!page.through).ToString() + "\n";
            otext += "\tイベントと衝突\t" + (!page.through).ToString() + "\n";
            otext += "\t移動速度\t" + page.moveSpeed.ToString() + "\n";
            otext += "\t移動頻度\t" + page.moveFrequency.ToString() + "\n";
            otext += "\t移動タイプ\tNONE\n";
            otext += "\t押せる\tFalse\n";
            return otext;
        }
        #endregion

        #region Privates
        private string GetEventName(MvEvent ev)
        {
            if(ev.name != null && ev.name != "")
            {
                return ev.name;
            }
            else
            {
                return "Event" + ev.id.ToString("D3");
            }
        }

        private string GetMapName(MvMap map)
        {
            if(map.displayName != null && map.displayName != "")
            {
                return map.displayName;
            }
            else
            {
                return "Map" + map.IdString;
            }
        }

        private string ExportCode(BakinCode code)
        {
            return
            WriteCommand(code.Code)
            + WriteCommandParameters(code.Params)
            + WriteCommandEnd();
        }

        private string WriteCommand(string command)
        {
            nullCommand = command == null ? true : false;
            return nullCommand ? "" : "\t\tコマンド\t" + command.Replace("\t", "\t(") + ")" + "\n";
        }

        private string WriteCommandParameters(List<BakinParameter> paras)
        {
            string otext = "";
            foreach(BakinParameter p in paras)
            {
                string desc = p.Description == "" ? "" : "(" + p.Description + ")";
                otext += "\t\t\t" + p.Type + "\t" + p.Value + "\t" + desc + "\n";
            }
            return otext;
        }

        private string WriteCommandEnd()
        {
            return nullCommand ? "" : "\t\tコマンド終了\n";
        }
        #endregion

    }
}
