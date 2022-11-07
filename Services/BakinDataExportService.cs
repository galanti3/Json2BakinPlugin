using Json2BakinPlugin.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace Json2BakinPlugin.Services
{
    public class BakinDataExportService
    {
        #region Variables
        private MvMapDataLoadService _loadService;
        private Json2BakinConvertService _convertService;
        private string _moveChar;
        private bool _isCommonEvent;
        #endregion

        #region Methods
        public void Preprocess()
        {
            foreach (MvEvent ev in _loadService.GetMap().events)
            {
                if (ev != null)
                {
                    foreach (MvEventPage page in ev.pages)
                    {
                        _convertService.ConvertRouteCodesToDestinationCode(page);
                    }
                }
            }
        }

        public void Postprocess()
        {
            foreach (MvEvent ev in _loadService.GetMap().events)
            {
                if (ev != null)
                {
                    foreach (MvEventPage page in ev.pages)
                    {
                        foreach (MvCode code in page.list)
                        {
                            if (code.BakinCode != null && code.BakinCode[0].Contains("COMMENT"))
                            {
                                code.Params.Add(code.BakinCode[1]); //for comment, text is added to the parameters list.
                            }
                        }
                    }
                }
            }
        }

        public void RegisterBakinCodes()
        {
            foreach(MvEvent ev in _loadService.GetMap().events)
            {
                if(ev != null)
                {
                    foreach (MvEventPage page in ev.pages)
                    {
                        foreach (MvCode code in page.list)
                        {
							if (code.code == 205)
							{
								_moveChar = code.Params[0];
							}
							else if (code.code == 505) //route
                            {
                                code.ExtractRouteCode();
							}
							code.GenerateSubCode(_moveChar);
                            code.BakinCode = MvBakinCodeDictionary.Code(code.Subcode);
                        }
                    }
                }
            }
        }

        public void ExportMap(string path)
        {
            string mapName = _loadService.GetMapName();
            _isCommonEvent = false;
            foreach (MvEvent ev in _loadService.GetMapEvents())
            {
                if (ev != null)
                {
                    string evName = GetEventName(ev);
                    string fileName = string.Format("{0}_{1}.txt", mapName, evName);
                    string otext = WriteEventInfo(evName);
                    foreach (MvEventPage page in ev.pages)
                    {
                        otext += WritePage(page);
                    }
                    File.WriteAllText(path + "\\" + fileName, otext);
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
            //normal:trigger 0=talk, 1=player touch, 2=event touch, 3=autorun, 4=parallel; common: 0=none, 1=switch auto, 2=switch parallel
            otext += "\t\t開始条件\t" + GetTriggerCode(page.trigger) + "\n";
            otext += "\t\t高さ無視\tFalse\n";
            otext += "\t\t判定拡張\tFalse\n";
            foreach (MvCode code in page.list)
            {
                otext += ExportCode(_convertService.ConvertToBakinCode(code));
            }
            otext += "スクリプト終了\nシート終了";
            return otext;
        }

        private string GetTriggerCode(int mvCode)
        {
            if(_isCommonEvent)
            {
                switch (mvCode)
                {
                    case 1:
                        return "AUTO"; //debug
                    case 2:
                        return "PARA"; //debug
                    default:
                        return "NONE";
                }
            }
            else
            {
                switch (mvCode)
                {
                    case 1:
                        return "PTOUCH"; //debug
                    case 2:
                        return "ETouch"; //debug
                    case 3:
                        return "AUTO"; //debug
                    case 4:
                        return "Para"; //debug
                    default:
                        return "TALK";
                }
            }
        }

        private string GetConditions(MvEventConditions cond, MvDatabase vardata)
        {
            //codition header
            string otext = "";

            //selfSwitchCh(selfSwitchValid)
            if (cond.selfSwitchValid)
            {
                otext += "sw\n" + "L:" + cond.selfSwitchCh;
            }
            //switch1Id(switch1Valid)
            if (cond.switch1Valid)
            {
                otext += "sw\n" + "N:[S" + cond.switch1Id + "]" + vardata.Switches[cond.switch1Id] + "\n";
            }
            //switch2Id(switch2Valid)
            if (cond.switch1Valid)
            {
                otext += "sw\n" + "N:[S" + cond.switch2Id + "]" + vardata.Switches[cond.switch2Id] + "\n";
            }
            //variableId(variableValid)
            //variableValue
            if (cond.variableValid)
            {
                otext += "var\n" + "N:[V" + cond.variableId + "]" + vardata.Variables[cond.variableId] + "\n";
                //otext + cond.variableValue;
            }
            //actorId(actorValid)
            if (cond.actorValid)
            {
                otext += "actor\n" + Guid.Empty.ToString() + "\t(" + vardata.Actors[cond.actorId] + ")\n";
                //otext + cond.variableValue;
            }
            //itemId(itemValid)
            if (cond.itemValid)
            {
                otext += "item\n" + Guid.Empty.ToString() + "\t(" + vardata.Items[cond.itemId] + ")\n";
                //otext + cond.variableValue;
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
            otext += "\t移動速度\t" + (page.moveSpeed - 4).ToString() + "\n";
            otext += "\t移動頻度\t" + (page.moveFrequency - 3).ToString() + "\n";
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

        #region Initialize
        public BakinDataExportService(MvMapDataLoadService loadService, Json2BakinConvertService convertService)
        {
            _loadService = loadService;
            _convertService = convertService;
        }
        #endregion
    }
}
