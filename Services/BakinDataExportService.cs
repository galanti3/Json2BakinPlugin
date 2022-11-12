using System;
using System.IO;
using System.Collections.Generic;
using Json2BakinPlugin.Models;

namespace Json2BakinPlugin.Services
{
    public class BakinDataExportService
    {
        #region Variables
        private MvMapDataLoadService _loadService;
        private Json2BakinConvertService _convertService;
        private string _moveChar;
        private bool _isTimerUsed = false;
        #endregion

        #region Methods
        public void Preprocess()
        {
            foreach (MvEvent ev in _loadService.GetMapEvents())
            {
                if (ev != null)
                {
                    foreach (MvEventPage page in ev.pages)
                    {
                        foreach(MvCode code in page.list)
                        {
                            code.ExtractMoveRoute();
                        }
                        //deactivated. Won't work..
                        //page.list = _convertService.ConvertRouteCodesToDestinationCode(page.list);
                        page.list = _convertService.MergeConsecutiveSameMovements(page.list);
                    }
                }
            }
        }

        public void Postprocess()
        {
            foreach (MvEvent ev in _loadService.GetMapEvents())
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
                            else if (code.Subcode == 11103|| code.Subcode == 12400) //timer related command
                            {
                                _isTimerUsed = true;
                            }
                        }
                    }
                }
            }
        }

        public void PreprocessCommon()
        {
            foreach (MvCommonEvent ev in _loadService.GetCommonEvents())
            {
                if (ev != null)
                {
                    foreach (MvCode code in ev.list)
                    {
                        code.ExtractMoveRoute();
                    }
                    //deactivated. Won't work..
                    //ev.list = _convertService.ConvertRouteCodesToDestinationCode(ev.list);
                }
            }
        }

        public void PostprocessCommon()
        {
            foreach (MvCommonEvent ev in _loadService.GetCommonEvents())
            {
                if (ev != null)
                {
                    foreach (MvCode code in ev.list)
                    {
                        if (code.BakinCode != null && code.BakinCode[0].Contains("COMMENT"))
                        {
                            code.Params.Add(code.BakinCode[1]); //for comment, text is added to the parameters list.
                        }
                    }
                }
            }
        }

        public void RegisterBakinCodes()
        {
            foreach(MvEvent ev in _loadService.GetMapEvents())
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
                            code.GenerateSubCode(_moveChar);
                            code.BakinCode = MvBakinCodeDictionary.Code(code.Subcode);
                        }
                    }
                }
            }
        }

        public void RegisterBakinCodesCommon()
        {
            foreach (MvCommonEvent ev in _loadService.GetCommonEvents())
            {
                if (ev != null)
                {
                    foreach (MvCode code in ev.list)
                    {
                        if (code.code == 205)
                        {
                            _moveChar = code.Params[0];
                        }
                        code.GenerateSubCode(_moveChar);
                        code.BakinCode = MvBakinCodeDictionary.Code(code.Subcode);
                    }
                }
            }
        }

        public void ExportMap(string path)
        {
            string mapName = _loadService.GetMapName();
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

        public void ExportCommonEvents(string path)
        {
            string otext = "";
            foreach (MvCommonEvent ev in _loadService.GetCommonEvents())
            {
                if (ev != null)
                {
                    otext = WriteEventInfo(string.Format("CommonEvents{0:D3}", ev.id));
                    otext += WriteCommonEvent(ev);
                    string evName = ev.name != "" ? "_" + ev.name : "";
                    string fileName = string.Format("CommonEvents{0:D3}", ev.id) + evName + ".txt";
                    File.WriteAllText(path + "\\" + fileName, otext);
                }
            }
            if (_isTimerUsed)
            {
                otext = CountDownTimer.WriteCommonEvent();
                File.WriteAllText(path + "\\" + "CommonEvent_Timer.txt", otext);
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
                otext += WriteConditionInfoCore("COND_TYPE_SWITCH", "EQUAL", "0", "True", cond.selfSwitchCh);
            }
            //switch1Id(switch1Valid)
            if (cond.switch1Valid)
            {
                otext += WriteConditionInfoCore("COND_TYPE_SWITCH", "EQUAL", "0", "False",
                    "[S" + cond.switch1Id.ToString("D3") + "]" + vardata.Switches[cond.switch1Id]);
            }
            //switch2Id(switch2Valid)
            if (cond.switch2Valid)
            {
                otext += WriteConditionInfoCore("COND_TYPE_SWITCH", "EQUAL", "0", "False",
                    "[S" + cond.switch2Id.ToString("D3") + "]" + vardata.Switches[cond.switch2Id]);
            }
            //variableId(variableValid)
            if (cond.variableValid)
            {
                otext += WriteConditionInfoCore("COND_TYPE_VARIABLE", "EQUAL_GREATER", cond.variableValue.ToString(), "False",
                    "[V" + cond.variableId.ToString("D3") + "]" + vardata.Variables[cond.variableId]);
            }
            //actorId(actorValid)
            if (cond.actorValid)
            {
                otext += WriteConditionInfoCore("COND_TYPE_HERO", "EQUAL", "0", "False", "", Guid.Empty.ToString());
            }
            //itemId(itemValid)
            if (cond.itemValid)
            {
                return WriteConditionInfoCore("COND_TYPE_ITEM_WITH_EQUIPMENT", "EQUAL", "1", "False", "", Guid.Empty.ToString());
            }
            return otext;
        }

        private string WriteCommonConditionInfo(MvCommonEvent common, string swname= "")
        {
            List<string> swdata = _loadService.GetDatabase().Switches;
            if(swname == "")
            {
                swname = "[S" + common.switchId.ToString("D3") + "]" + swdata[common.switchId];
            }
            if (common.trigger >= 1)
            {
                return WriteConditionInfoCore("COND_TYPE_SWITCH", "EQUAL", "0", "False", swname);
            }
            else
            {
                return "";
            }
        }

        private string WriteConditionInfoCore(string command, string operand, string option, string local, string name, string guid="")
        {
            string otext = "\t条件\t" + command + "\n" + 
                "\t\t比較演算子\t" + operand +
                "\n\t\tインデックス\t-1\n" +
                "\t\tオプション\t" + option +
                "\n\t\tローカル参照\t" + local +
                "\n\t\t参照名\t" + name;
            if (guid != "")
            {
                otext += "\n\t\tGuid参照\t" + Guid.Empty.ToString() + "\t(" + guid + ")";
            }
            otext += "\n\t条件終了\n";
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
