using Json2BakinPlugin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using Yukar.Engine;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
            otext += WriteConditionInfo(page);
            otext += "\tスクリプト\n";
            //normal:trigger 0=talk, 1=player touch, 2=event touch, 3=autorun, 4=parallel
            otext += "\t\t開始条件\t" + page.TriggerCode + "\n";
            otext += "\t\t高さ無視\tFalse\n";
            otext += "\t\t判定拡張\tFalse\n";
            foreach (MvCode code in page.list)
            {
                otext += ExportCode(_convertService.ConvertToBakinCode(code));
            }
            otext += "スクリプト終了\nシート終了";
            return otext;
        }

        private string WriteCommonEvent(MvCommonEvent common)
        {
            string otext = "";
            otext += WriteCommonBasicPageInfo();
            otext += WriteCommonConditionInfo(common);
            otext += "\tスクリプト\n";
            //normal:trigger 0=none, 1=switch auto, 2=switch parallel
            otext += "\t\t開始条件\t" + common.TriggerCode + "\n";
            otext += "\t\t高さ無視\tFalse\n";
            otext += "\t\t判定拡張\tFalse\n";
            foreach (MvCode code in common.list)
            {
                otext += ExportCode(_convertService.ConvertToBakinCode(code));
            }
            otext += "スクリプト終了\nシート終了";
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

        private string WriteCommonBasicPageInfo()
        {
            return "シート\tイベントシート\n\tグラフィック\t" + Guid.Empty + "\n\t向き\t-1\n"
                    + "\t向き固定\tFalse\n\t物理\tFalse\n\t衝突判定\tTrue\n\tイベントと衝突\tTrue\n"
                    + "\t移動速度\t0\n\t移動頻度\t0\n\t移動タイプ\tNONE\n\t押せる\tTrue\n";
        }

        private string WriteConditionInfo(MvEventPage page)
        {
            MvEventConditions cond = page.conditions;
            MvDatabase vardata = _loadService.GetDatabase();
            string otext = "";

            if (!cond.switch1Valid && !cond.switch2Valid && !cond.selfSwitchValid && 
                !cond.variableValid && !cond.actorValid && !cond.itemValid)
            {
                return "";
            }

            //selfSwitchCh(selfSwitchValid)
            if (cond.selfSwitchValid)
            {
                otext += "\t条件\tCOND_TYPE_SWITCH\n"
                        + "\t\t比較演算子\tEQUAL\n\t\tインデックス\t-1\n\t\tオプション\t0\n"
                        + "\t\tローカル参照\tTrue\n\t\t参照名\t" + "L:" + cond.selfSwitchCh + "\n"
                        + "\t条件終了\n";
            }
            //switch1Id(switch1Valid)
            if (cond.switch1Valid)
            {
                otext += "\t条件\tCOND_TYPE_SWITCH\n"
                        + "\t\t比較演算子\tEQUAL\n\t\tインデックス\t-1\n\t\tオプション\t0\n"
                        + "\t\tローカル参照\tFalse\n\t\t参照名\t" + "N:[S" + cond.switch1Id + "]" + vardata.Switches[cond.switch1Id] + "\n"
                        + "\t条件終了\n";
            }
            //switch2Id(switch2Valid)
            if (cond.switch2Valid)
            {
                otext += "\t条件\tCOND_TYPE_SWITCH\n"
                        + "\t\t比較演算子\tEQUAL\n\t\tインデックス\t-1\n\t\tオプション\t0\n"
                        + "\t\tローカル参照\tFalse\n\t\t参照名\t" + "N:[S" + cond.switch2Id + "]" + vardata.Switches[cond.switch2Id] + "\n"
                        + "\t条件終了\n";
            }
            //variableId(variableValid)
            if (cond.variableValid)
            {
                otext += "\t条件\tCOND_TYPE_VARIABLE\n"
                        + "\t\t比較演算子\tEQUAL_GREATER\n\t\tインデックス\t-1\n\t\tオプション\t" + cond.variableValue + "\n"
                        + "\t\tローカル参照\tFalse\n\t\t参照名\t" + "N:[V" + cond.variableId + "]" + vardata.Variables[cond.variableId] + "\n"
                        + "\t条件終了\n";
            }
            //actorId(actorValid)
            if (cond.actorValid)
            {
                otext += "\t条件\tCOND_TYPE_HERO\n"
                        + "\t\t比較演算子\tEQUAL_GREATER\n\t\tインデックス\t-1\n\t\tオプション\t0\n"
                        + "\t\tローカル参照\tFalse\n\t\t参照名\t\n\t\tGuid参照\t" + Guid.Empty.ToString()
                        + "\t(" + vardata.Actors[cond.actorId] + ")\n"
                        + "\t条件終了\n";
            }
            //itemId(itemValid)
            if (cond.itemValid)
            {
                otext += "\t条件\tCOND_TYPE_ITEM_WITH_EQUIPMENT\n"
                        + "\t\t比較演算子\tEQUAL_GREATER\n\t\tインデックス\t-1\n\t\tオプション\t1\n"
                        + "\t\tローカル参照\tFalse\n\t\t参照名\t\n\t\tGuid参照\t" + Guid.Empty.ToString()
                        + "\t(" + vardata.Items[cond.itemId] + ")\n"
                        + "\t条件終了\n";
            }
            return otext;
        }

        private string WriteCommonConditionInfo(MvCommonEvent common)
        {
            List<string> swdata = _loadService.GetDatabase().Switches;
            if(common.trigger >= 1)
            {
                return "\t条件\tCOND_TYPE_SWITCH\n"
                        + "\t\t比較演算子\tEQUAL\n\t\tインデックス\t-1\n\t\tオプション\t0\n"
                        + "\t\tローカル参照\tFalse\n\t\t参照名\t" + "N:[S" + common.switchId + "]" + swdata[common.switchId] + "\n"
                        + "\t条件終了\n";
            }
            else
            {
                return "";
            }
        }

        private string WriteConditionInfoCore(string command, string operand, string option, string local, string name, string guid)
        {
            string otext = "\t条件\t" + command + "\n\t\t比較演算子\t" + operand + "\n\t\tインデックス\t-1\n\t\tオプション\t" + option +
                    "\n\t\tローカル参照\t" + local + "\n\t\t参照名\t" + name;
            otext += "\n\t\tGuid参照\t" + Guid.Empty.ToString() + "\t(" + guid + ")\n";
            otext += "\t条件終了\n";
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
