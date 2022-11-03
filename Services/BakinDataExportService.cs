using Json2BakinPlugin.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace Json2BakinPlugin.Services
{
    public class BakinDataExportService
    {
        private Json2BakinConvertService convertService = new Json2BakinConvertService();
        private MvBakinCodeDictionary codeDic = new MvBakinCodeDictionary();

        private bool nullCommand = false;

        #region Methods
        public void Preprocess(MvMap map)
        {
            foreach (MvEvent ev in map.events)
            {
                if (ev != null)
                {
                    foreach (MvEventPage page in ev.pages)
                    {
                        convertService.ConvertRouteCodesToDestinationCode(page);
                        //debug; merge shop items
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
                            if(code.code == 505) //route
                            {
                                code.ExtractRouteCode();
                            }
                            code.GenerateSubCode();
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
        #endregion

        #region Privates
        private string WriteEventInfo(string evName)
        {
            string otext = "";
            otext += "Guid\t" + Guid.Empty + "\n";
            otext += "イベント名\t" + evName + "\n";
            return otext;
        }

        private string WritePage(MvEventPage page)
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
            string otext = "";
            foreach (BakinParameter p in code.Params)
            {
                string desc = p.Description == "" ? "" : "\t(" + p.Description + ")";
                otext += p.Type.Contains("コマンド") ? "\t\t" : "\t\t\t";
                otext += p.Type + "\t" + p.Value +  desc + "\n";
            }
            return otext;
        }

        #endregion

    }
}
