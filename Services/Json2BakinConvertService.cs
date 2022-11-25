using System;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Json2BakinPlugin.Models;
using static Json2BakinPlugin.Properties.Resources;

namespace Json2BakinPlugin.Services
{
    public class Json2BakinConvertService
    {
        #region Variables
        MvMapDataLoadService _loadService;

        string _moveChar;
        string _isStopStuck;
        string _isWaitMoving;
        bool _isAddReviceComment;
        List<string> _indentType = new List<string>();
        int _numChoices = 0;
        #endregion

        #region Methods
        public BakinCode ConvertToBakinCode(MvCode code)
        {
            BakinCode bakin = new BakinCode();
            bakin.Code = code.BakinCode;
            List<string> paras = code.Params;
            List<BakinParameter> p = new List<BakinParameter>();
            ConvertToBakinCodeCore(code, p);
            bakin.Params = p;
            return bakin;
        }

        private void ConvertToBakinCodeCore(MvCode code, List<BakinParameter> p)
        {
            string command = code.BakinCode != null ? code.BakinCode[0] : null;
            switch (command)
            {
                case "DIALOGUE": //101
                    AddCommandDialogue(p, code.Params);
                    break;
                case "MESSAGE": //101
                    AddCommandMessage(p, code.Params);
                    break;
                case "CHOICES": //102
                    AddCommandChoices(p, code.Params);
                    _indentType.Add("IF");
                    break;
                case "BRANCH": //402, 403
                    AddCommandBranch(p, code.Params);
                    break;
                case "CHANGE_STRING_VARIABLE": //103
                    break;
                case "ITEMMENU": //104
                    AddCommandItemmenu(p, code.Params);
                    _indentType.Add("IF");
                    break;
                case "TELOP": //105
                    AddCommandTelop(p, code.Params);
                    break;
                case "IFSWITCH": //111(00)
                    AddCommandIfswitch(p, code.Params);
                    _indentType.Add("IF");
                    break;
                case "IFVARIABLE": //111(01-02)
                    AddCommandIfvariable(p, code.Params);
                    _indentType.Add("IF");
                    break;
                //timer???
                case "IFPARTY": //111(04)
                    AddCommandIfparty(p, code.Params);
                    _indentType.Add("IF");
                    break;
                case "IF_STRING_VARIABLE": //111(05)
                    AddCommandIfstringvariable(p, code.Params);
                    _indentType.Add("IF");
                    break;
                case "IFITEM": //111(07, 11, 12, 13)
                    AddCommandIfitem(p, code.Params);
                    _indentType.Add("IF");
                    break;
                case "BTL_IFMONSTER": //111(08)
                    AddCommandBtlifmonster(p, code.Params);
                    _indentType.Add("IF");
                    break;
                case "IFMONEY": //111(10)
                    AddCommandIfmoney(p, code.Params);
                    _indentType.Add("IF");
                    break;
                case "ELSE": //411, 602, 603
                    if (_indentType.Count > 0 && _indentType.Last() != "IGNORE") //ELSE of non-converted IF.
                    {
                        AddCommandNoparams(p, "ELSE");
                    }
                    else
                    {
                        AddCommandNoticeComment(p, Cvt_NoConvert, Para_NoConvChoiceElse);
                    }
                    break;
                case "ENDIF":
                    AddCommandNoparams(p, "ENDIF");
                    break;
                case "LOOP": //112
                    AddCommandNoparams(p, "LOOP");
                    break;
                case "ENDLOOP": //413
                    AddCommandNoparams(p, "ENDLOOP");
                    break;
                case "BREAK": //113
                    AddCommandNoparams(p, "BREAK");
                    break;
                case "END": //115
                    AddCommandNoparams(p, "END");
                    break;
                case "EXEC": //117
                    AddCommandExec(p, code.Params);
                    break;
                case "SWITCH": //121, 123, 27, 28
                    AddCommandSwitch(code.code, p, code.Params);
                    break;
                case "HLVARIABLE": //122(00-05,07)
                    AddCommandHlvariable(p, code.Params);
                    break;
                case "TIMER": //124
                    AddCommandTimer(p, code.Params);
                    break;
                case "MONEY": //125
                    AddCommandMoney(p, code.Params);
                    break;
                case "ITEM": //126, 127, 128
                    AddCommandItem(p, code.Params);
                    break;
                case "PARTY": //129
                    AddCommandParty(p, code.Params);
                    break;
                case "SW_SAVE": //134
                    AddCommandSwsave(p, code.Params);
                    break;
                case "SW_MENU": //135
                    AddCommandSwmenu(p, code.Params);
                    break;
                case "SW_ENCOUNTING": //136
                    AddCommandSwencounting(p, code.Params);
                    break;
                case "CHANGE_LAYOUT": //138
                    AddCommandChangelayout(p, code.Params);
                    break;
                case "PLMOVE": //201
                    AddCommandPlmove(p, code.Params);
                    break;
                case "MOVE": //203
                    AddCommandMove(p, code.Params);
                    break;
                case "CAM_ANIMATION": //204
                    AddCommandCamanimation(p, code.Params);
                    break;
                case "MOVEROUTE": //205
                    AddCommandMoveRoute(p, code.Params);
                    break;
                case "EFFECT": //212, 337 no character choice
                    AddCommandEffect(code.code, p, code.Params);
                    break;
                case "EMOTE": //213 no character choice
                    AddCommandEmote(p, code.Params);
                    break;
                case "DESTROY_EVENT": //214
                                      //Erase Event
                    AddCommandNoparams(p, "DESTROY_EVENT");
                    break;
                case "WALK_IN_ROWS": //216
                    AddCommandWalkinrows(p, code.Params);
                    break;
                case "SCREEN_FADE": //221, 222
                    AddCommandScreenfade(code.code, p, code.Params);
                    break;
                case "SCREEN_COLOR": //223
                    AddCommandScreencolor(p, code.Params);
                    break;
                case "SCREEN_FLASH": //224
                    AddCommandScreenflash(p, code.Params);
                    break;
                case "SCREEN_SHAKE": //225
                    AddCommandScreenshake(p, code.Params);
                    break;
                case "WAIT": //230
                    AddCommandWait(p, code.Params);
                    break;
                case "SPPICTURE": //231
                    AddCommandSppicture(p, code.Params);
                    break;
                case "SPMOVE": //232
                    AddCommandSpmove(p, code.Params);
                    break;
                case "SPHIDE": //235
                    AddCommandSphide(p, code.Params);
                    break;
                case "CHANGE_RENDER": //236, 283, 284
                    AddCommandChangerender(code.code, p, code.Params);
                    break;
                case "PLAYBGM": //241, 242
                    AddCommandPlaybgm(code.code, p, code.Params);
                    break;
                case "PLAYBGS": //245, 246
                    AddCommandPlaybgs(code.code, p, code.Params);
                    break;
                case "PLAYJINGLE": //249
                    AddCommandPlayjingle(p, code.Params);
                    break;
                case "STOPSE": //251
                    AddCommandStopse(p, code.Params);
                    break;
                case "PLAYMOVIE": //261
                    AddCommandPlaymovie(p, code.Params);
                    break;
                case "GET_TERRAIN": //285 unable to get terraintag, eventid and regionid, position specified only by variables
                    AddCommandGetterrain(p, code.Params);
                    break;
                case "BOSSBATTLE": //301
                    AddCommandBossbattle(p, code.Params);
                    _indentType.Add("IF");
                    break;
                case "SHOP": //302　code.Params[1] and code.Params[3] should be arrays containing item ids and prices
                    AddCommandShop(p, code.Params);
                    break;
                case "CHANGE_HERO_NAME": //303
                    AddCommandChangeheroname(p, code.Params);
                    break;
                case "CHG_HPMP": //311, 312
                    AddCommandChghpmp(code.code, p, code.Params);
                    break;
                case "CHG_STTAILM": //313
                    AddCommandChgsttailm(p, code.Params);
                    break;
                case "FULLRECOV": //314 not all but only 1 actor? CHG_HPMP??
                    break;
                case "CHG_EXP": //315
                    AddCommandChgexp(p, code.Params);
                    break;
                case "STATUS": //317 unable for mdef and luk change
                    AddCommandStatus(p, code.Params);
                    break;
                case "CHG_SKILL": //318
                    AddCommandChgskill(p, code.Params);
                    break;
                case "EQUIP": //319
                    AddCommandEquip(p, code.Params);
                    break;
                case "STRING_VARIABLE": //320
                    AddCommandStringvariable(p, code.Params);
                    break;
                case "CHANGE_JOB": //321
                    AddCommandChangejob(p, code.Params);
                    break;
                case "PLGRAPHIC": //322
                    AddCommandPlgraphic(p, code.Params);
                    break;
                case "BTL_HEAL": //331, 332
                    AddCommandBtlheal(code.code, p, code.Params);
                    break;
                case "BTL_STATUS": //333
                    AddCommandBtlstatus(p, code.Params);
                    break;
                case "BTL_APPEAR": //335
                    AddCommandBtlappear(p, code.Params);
                    break;
                case "BTL_ACTION": //339
                    AddCommandBtlaction(p, code.Params);
                    break;
                case "BTL_STOP": //340
                    AddCommandNoparams(p, "BTL_STOP");
                    break;
                case "SHOW_SCORE_BOARD": //351 unusable??
                    AddCommandShowscoreboard(p, code.Params);
                    break;
                case "SAVE": //352
                    AddCommandNoparams(p, "SAVE");
                    break;
                case "PLWALK": //1-13
                    var plSteps = code.Params.Count == 0 ? "1" : code.Params.Last();
                    AddCommandWalk(p, code.code, plSteps);
                    break;
                case "WALK": //1-13
                    string steps = code.Params.Count == 0 ? "1" : code.Params.Last();
                    AddCommandWalk(p, code.code, steps);
                    break;
                case "PLWALK_TGT": //converted from consecutive player move commands
                    AddCommandWalktgt(p, code.Params);
                    break;
                case "EVWALK_TGT": //converted from consecutive event move commands
                    AddCommandWalktgt(p, code.Params);
                    break;
                case "ADDFORCEPL": //14
                    AddCommandAddforce(p, code.Params);
                    break;
                case "ADDFORCE": //14
                    AddCommandAddforce(p, code.Params);
                    break;
                case "PLROTATE": //16-26
                    AddCommandRotate(p, code.code);
                    break;
                case "ROTATE": //16-26
                    AddCommandRotate(p, code.code);
                    break;
                case "PLWALKSPEED": //29
                    AddCommandWalkspeed(p, code.Params);
                    break;
                case "WALKSPEED": //29
                    AddCommandWalkspeed(p, code.Params);
                    break;
                case "GRAPHIC": //31, 32, 41
                    AddCommandGraphic(p, code.Params);
                    break;
                case "PLMOTION": //33, 34
                    AddCommandPlmotion(p, code.Params);
                    break;
                case "MOTION": //33, 34
                    AddCommandMotion(p, code.Params);
                    break;
                case "SW_PLLOCKROTATE": //35, 36
                    AddCommandSwpllockrotate(p, new List<string> { code.code == 35 ? "1" : "0" });
                    break;
                case "CHANGE_PLAYER_MOVABLE": //37, 38
                    AddCommandChangemovable(p, new List<string> { code.code == 37 ? "1" : "0" });
                    break;
                case "CHANGE_MOVABLE": //37, 38
                    AddCommandChangemovable(p, new List<string> { code.code == 37 ? "1" : "0" });
                    break;
                case "PLHIDE": //39, 40
                    AddCommandHide(p, new List<string> { code.code == 39 ? "1" : "0" });
                    break;
                case "EVHIDE": //39, 40, 211
                    AddCommandHide(p, new List<string> { code.code == 39 ? "1" : code.code == 40 ? "0" : code.Params[0] });
                    break;
                case "PLAYSE": //44, 250
                    AddCommandPlayse(p, code.Params);
                    break;
                case "WALK_TGT": //50
                    AddCommandWalktgt(p, code.Params);
                    break;
                case "COMMENT":
                    AddCommandComment(p, code.Params);
                    break;
                case "CLOSE":
                    AddCommandClose(p, code.Params);
                    break;
                case "ERROR": //impossible to convert
                    AddCommandNoticeComment(p, Cvt_NoConvert, code.BakinCode[1]);
                    break;
                case "ERROR_WITH_CONTENT": //impossible to convert
                    AddCommandNoticeCommentWithContent(p, Cvt_NoConvert, code.BakinCode[1], code.Params);
                    break;
                case "ERROR_IF": //impossible to convert. Ignore corresponding close tag.
                    AddCommandNoticeComment(p, Cvt_NoConvert, code.BakinCode[1]);
                    _indentType.Add("IGNORE");
                    break;

                    #region Follwing commands are not used in MV.
                    //case "VARIABLE":
                    //	break;
                    //case "FACEEMOTION":
                    //	p.Add(new BakinParameter("整数", "変更対象（0：プレイヤー、1：イベント）"));    //target 0=player 1=this
                    //	p.Add(new BakinParameter("文字列", "簡易設定"));   //template name
                    //	p.Add(new BakinParameter("整数", "詳細設定：目（変数可）")); //eye id
                    //	p.Add(new BakinParameter("整数", "詳細設定：眉（変数可）")); //eyebrow id
                    //	p.Add(new BakinParameter("整数", "詳細設定：口（変数可）")); //lip id
                    //	break;
                    //case "SPTEXT":
                    //	p.Add(new BakinParameter("整数", "イメージの管理番号（変数可）"));  //image id
                    //	p.Add(new BakinParameter("文字列", "表示する文字列"));    //txt
                    //	p.Add(new BakinParameter("整数", "拡大率（変数可）"));    //zoom %
                    //	p.Add(new BakinParameter("整数", "文字色")); //color dec -> ARGB hex
                    //	p.Add(new BakinParameter("整数", ""));
                    //	p.Add(new BakinParameter("整数", "X位置（変数可）"));    //x pos
                    //	p.Add(new BakinParameter("整数", "Y位置（変数可）"));    //y pos
                    //	p.Add(new BakinParameter("整数", "テキスト揃え（0：左揃え、1：中央揃え、2：右揃え）"));  //text align 0=left 1=center 2=right
                    //	break;
                    //case "CHANGE_GAMEOVER_ACTION":
                    //	p.Add(new BakinParameter("整数", "動作（0：タイトルへ戻る、1：その場で復活、2：高度な設定）"));  //0=title 1=resurrect 2=detail
                    //	p.Add(new BakinParameter("Guid", ""));
                    //	p.Add(new BakinParameter("整数", "X座標")); //xpos
                    //	p.Add(new BakinParameter("整数", "Z座標")); //zpos
                    //	p.Add(new BakinParameter("整数", "復活範囲（0：先頭のみ、1：全員）"));   //range 1=all 0=onlytop
                    //	p.Add(new BakinParameter("整数", "復活時のHP（‐1：1、または指定値％）"));    //hp -1=1 or in %
                    //	p.Add(new BakinParameter("整数", "復活時のMP（‐1：変更しない、または指定値％）"));    //mp -1=1 or in %
                    //	p.Add(new BakinParameter("Guid", "移動後に実行する共通イベントGuid"));    //common ev guid
                    //	break;
                    //case "SW_CAMLOCK":
                    //	p.Add(new BakinParameter("整数", "カメラ操作の禁止"));    //camera_lock flag
                    //	p.Add(new BakinParameter("整数", ""));
                    //	p.Add(new BakinParameter("整数", "横回転操作"));   //horizontal rotate
                    //	p.Add(new BakinParameter("整数", "縦回転操作"));   //vertical rotate
                    //	p.Add(new BakinParameter("整数", "ズーム操作"));   //zoom
                    //	break;
                    //case "BTL_SW_CAMERA":
                    //	p.Add(new BakinParameter("整数", "バトル開始時カメラ演出")); //on_battle_start
                    //	p.Add(new BakinParameter("整数", "攻撃時カメラ演出"));    //on_attack
                    //	p.Add(new BakinParameter("整数", "スキル使用時カメラ演出")); //on_skill
                    //	p.Add(new BakinParameter("整数", "アイテム使用時カメラ演出"));    //on_item
                    //	p.Add(new BakinParameter("整数", "リザルト時カメラ演出"));  //on_result
                    //	break;
                    //case "CHANGE_PLAYER_HEIGHT":
                    //	p.Add(new BakinParameter("整数", "Y座標の指定方法（2：プレイヤーの現在地のY座標から、3：Y座標の絶対値）"));   //2=relative 3=absolute
                    //	p.Add(new BakinParameter("小数", "変更するY座標（変数可）"));    //ychange
                    //	p.Add(new BakinParameter("小数", "Y座標の変更にかける時間（変数可）"));   //time
                    //	p.Add(new BakinParameter("整数", "移動速度（0：等速、1：加速、2：減速）"));    //speed 0=const 1=accel 2=decel
                    //	p.Add(new BakinParameter("整数", "Y座標変更の完了を待つ")); //wait complete
                    //	break;
                    //case "FALL_PLAYER":
                    //	p.Add(new BakinParameter("小数", "重力加速度（変数可）"));  //accel amount
                    //	p.Add(new BakinParameter("整数", "落下の完了を待つ"));    //wait complete
                    //	break;
                    //case "CHANGE_PLAYER_SCALE":
                    //	p.Add(new BakinParameter("小数", "変更するスケール（変数可）"));   //scale size
                    //	p.Add(new BakinParameter("小数", "スケールの変更にかける時間（変数可）"));  //time in sec
                    //	p.Add(new BakinParameter("整数", "スケール変更の完了を待つ"));    //wait complete
                    //	break;
                    //case "JOINT_WEAPON":
                    //	p.Add(new BakinParameter("整数", "変更するキャスト（0：指定、1：n番目）"));    //0=specify_cast 1=n-th member
                    //	p.Add(new BakinParameter("Guid", "指定キャストGuid"));    //cast guid
                    //	p.Add(new BakinParameter("整数", "パーティのn番目（n-1）"));   //member number, N-1
                    //	p.Add(new BakinParameter("整数", "ジョイント（0：すべて外す、1：装備しているアイテムに応じたモデルをジョイント、2：任意のモデルをジョイント）")); //joint type 0=removeall 1=attachequipped 2=specify
                    //	p.Add(new BakinParameter("[整数", "任意モデル用パラメータ？"));   //for specify
                    //	p.Add(new BakinParameter("整数", "右手モデルジョイント（0：変更、2：変更しない）"));    //for specify right, 0=change 2=nochange
                    //	p.Add(new BakinParameter("[Guid", "右手モデルGuid"));    //right model guid]
                    //	p.Add(new BakinParameter("整数", "左手モデルジョイント（0：変更、2：変更しない）"));    //for specify left, 0=change 2=nochange
                    //	p.Add(new BakinParameter("[Guid", "左手モデルGuid"));    //left model guid]]
                    //	break;
                    //case "INVINCIBLE":
                    //	p.Add(new BakinParameter("小数", "無敵時間（変数可）"));   //time in sec
                    //	p.Add(new BakinParameter("整数", "無敵中はグラフィックを点滅させる"));    //flash gra flag
                    //	p.Add(new BakinParameter("整数", "無敵中は体当たりで敵にダメージを与えない"));    //no damage to enemy flag
                    //	break;
                    //case "PLSNAP":
                    //	break;
                    //case "WALK_IN_ROWS_ORDER":
                    //	p.Add(new BakinParameter("整数", "隊列の人数"));   //number of casts
                    //	p.Add(new BakinParameter("整数", "並び替え後1番目のタイプ（0：メンバー、1：グラフィック、2：キャスト）"));    //1st 0=member 1=graphic 2=cast
                    //	p.Add(new BakinParameter("整数", "並び替え後1番目の現在の順番（n-1）")); //1st current order, N-1
                    //	p.Add(new BakinParameter("整数", "並び替え後2番目のタイプ（0：メンバー、1：グラフィック、2：キャスト）"));    //2nd 0=member 1=graphic 2=cast
                    //	p.Add(new BakinParameter("整数", "並び替え後2番目の現在の順番（n-1）")); //2nd current order, N-1
                    //	p.Add(new BakinParameter("整数", "並び替え後3番目のタイプ（0：メンバー、1：グラフィック、2：キャスト）"));    //3rd 0=member 1=graphic 2=cast
                    //	p.Add(new BakinParameter("整数", "並び替え後3番目の現在の順番（n-1）")); //3rd current order, N-1
                    //	p.Add(new BakinParameter("整数", "並び替え後4番目のタイプ（0：メンバー、1：グラフィック、2：キャスト）"));    //4th 0=member 1=graphic 2=cast
                    //	p.Add(new BakinParameter("整数", "並び替え後4番目の現在の順番（n-1）")); //4th current order, N-1
                    //	p.Add(new BakinParameter("[整数", "並び替え後5番目のタイプ（0：メンバー、1：グラフィック、2：キャスト）"));   //5th 0=member 1=graphic 2=cast
                    //	p.Add(new BakinParameter("Guid", "並び替え後5番目のGuid")); //added cast guid]
                    //	break;
                    //case "ROTATEPL_XYZ":
                    //	p.Add(new BakinParameter("整数", "回転（0：絶対値、1：現在の向きからの相対値）")); //0=absolute 1=relative
                    //	p.Add(new BakinParameter("整数", "X回転（変数可）"));    //xrotate +-360
                    //	p.Add(new BakinParameter("整数", "Y回転（変数可）"));    //yrotate +-360
                    //	p.Add(new BakinParameter("整数", "Z回転（変数可）"));    //zrotate +-360
                    //	break;
                    //case "PLSUBGRP":
                    //	p.Add(new BakinParameter("整数", ""));    //??
                    //	p.Add(new BakinParameter("Guid", ""));  //??
                    //	p.Add(new BakinParameter("整数", ""));
                    //	p.Add(new BakinParameter("整数", "変更するサブグラフィックの番号（変数可）"));    //subgraphic number
                    //	p.Add(new BakinParameter("整数", "表示フラグ"));   //display flag
                    //	p.Add(new BakinParameter("小数", "変更にかける時間（変数可）"));   //time in sec
                    //	break;
                    //case "ITEM_THROW_OUT":
                    //	break;
                    //case "CHANGE_HEIGHT":
                    //	p.Add(new BakinParameter("整数", "Y座標の指定方法（2：プレイヤーの現在地のY座標から、3：Y座標の絶対値）"));   //2=relative 3=absolute
                    //	p.Add(new BakinParameter("小数", "変更するY座標（変数可）"));    //ychange
                    //	p.Add(new BakinParameter("小数", "Y座標の変更にかける時間（変数可）"));   //time
                    //	p.Add(new BakinParameter("整数", "移動速度（0：等速、1：加速、2：減速）"));    //speed 0=const 1=accel 2=decel
                    //	p.Add(new BakinParameter("整数", "Y座標変更の完了を待つ")); //wait complete
                    //	break;
                    //case "FALL_EVENT":
                    //	p.Add(new BakinParameter("小数", "重力加速度（変数可）"));  //accel amount
                    //	p.Add(new BakinParameter("整数", "落下の完了を待つ"));    //wait complete
                    //	break;
                    //case "CHANGE_SCALE":
                    //	p.Add(new BakinParameter("小数", "変更するスケール（変数可）"));   //scale size
                    //	p.Add(new BakinParameter("小数", "スケールの変更にかける時間（変数可）"));  //time in sec
                    //	p.Add(new BakinParameter("整数", "スケール変更の完了を待つ"));    //wait complete
                    //	break;
                    //case "EVSNAP":
                    //	break;
                    //case "ROTATE_XYZ":
                    //	p.Add(new BakinParameter("整数", "回転（0：絶対値、1：現在の向きからの相対値）")); //0=absolute 1=relative
                    //	p.Add(new BakinParameter("整数", "X回転（変数可）"));    //xrotate +-360
                    //	p.Add(new BakinParameter("整数", "Y回転（変数可）"));    //yrotate +-360
                    //	p.Add(new BakinParameter("整数", "Z回転（変数可）"));    //zrotate +-360
                    //	break;
                    //case "SUBGRP":
                    //	p.Add(new BakinParameter("整数", "変更するサブグラフィックの番号（変数可）"));    //subgraphic number
                    //	p.Add(new BakinParameter("整数", "表示フラグ"));   //display flag
                    //	p.Add(new BakinParameter("小数", "変更にかける時間（変数可）"));   //time in sec
                    //	break;
                    //case "HLSTRVARIABLE":
                    //	p.Add(new BakinParameter("変数", "文字列変数の番号"));   //to; type":name N=numeric, S=string, A=array
                    //	p.Add(new BakinParameter("整数", "タイプ（0：文字列変数の内容、1：現在のマップ名、2：キャストのステータス）"));  //0=string var 1=current map name 2=cast status
                    //	p.Add(new BakinParameter("[Guid", "キャストGuid")); //cast guid
                    //	p.Add(new BakinParameter("(none for current map name)", ""));
                    //	p.Add(new BakinParameter("変数", "代入元の文字列変数の番号"));   //from; type":name N=numeric, S=string, A=array]
                    //	p.Add(new BakinParameter("[変数", "キャストのステータス（0：名前、1：職業、2：副業、3：武器、4：腕防具、5：頭防具、6：体防具、7：装飾品1、8：装飾品2）"));    //cast status 0=name 1=job 2=subjob 3=weapon 4=armor 5=head 6=body 7=acces1 8=acces2]
                    //	p.Add(new BakinParameter("整数", "代入（0：上書き、1：先頭に追加、2：最後尾に追加）"));  //0=overwrite 1=addfirst 2=addlast
                    //	break;
                    //case "REPLACE_STRING_VARIABLE":
                    //	p.Add(new BakinParameter("変数", "文字列変数の番号"));   //to; type":name N=numeric, S=string, A=array
                    //	p.Add(new BakinParameter("文字列", "置き換え前の文字列")); //from
                    //	p.Add(new BakinParameter("文字列", "置き換え後の文字列")); //to
                    //	break;
                    //case "SW_PLLOCK":
                    //	p.Add(new BakinParameter("整数", "プレイヤー捜査の禁止"));  //control disable flag
                    //	break;
                    //case "SW_DASH":
                    //	p.Add(new BakinParameter("整数", "プレイヤーのダッシュの禁止"));   //dash disable flag
                    //	break;
                    //case "SW_JUMP":
                    //	p.Add(new BakinParameter("整数", "ジャンプの禁止")); //jump disable flag
                    //	break;
                    //case "INN":
                    //	p.Add(new BakinParameter("[変数", "宿泊料（変数可）"));   //price in var
                    //	p.Add(new BakinParameter("整数", "(absolute"));   //absolute price]
                    //	p.Add(new BakinParameter("整数", "状態変化を回復")); //recover state flag
                    //	p.Add(new BakinParameter("整数", "先頭不能を回復")); //recover dead flag
                    //	p.Add(new BakinParameter("整数", "選択肢の位置（0：左上、1：上、2：右上、3：左、4：中央、5：右、6：左下、7：下、8：右下）"));    //pos 0=upleft 1=up 2=upright 3=left 4=center 5=right 6=botleft 7=bottom 8=botright
                    //	break;
                    //case "IF_INVENTORY_EMPTY":
                    //	p.Add(new BakinParameter("[変数", "アイテム袋空き数（変数可）")); //type":name N=numeric, S=string, A=array
                    //	p.Add(new BakinParameter("整数", ""));
                    //	break;
                    //case "COL_CONTACT":
                    //	p.Add(new BakinParameter("整数", "チェック元（0：プレイヤー、1：このイベント）")); //0=player 1=this
                    //	p.Add(new BakinParameter("整数", "チェック先（0：地形（着地状態かどうか）、1：物体、2：プレイヤー、3：イベント"));    //0=land 1=obj 2=player 3=event
                    //	p.Add(new BakinParameter("整数", "接触したチェック先の名称を取得")); //get contact name flag
                    //	p.Add(new BakinParameter("[変数", "何個目を取得するか（変数可）"));    //type":name N=numeric, S=string, A=array
                    //	p.Add(new BakinParameter("整数", "(N-th"));   //N-th obj]
                    //	p.Add(new BakinParameter("変数", "代入先変数名")); //name to; type":name N=numeric, S=string, A=array
                    //	break;
                    //case "COL_RAYCAST":
                    //	p.Add(new BakinParameter("整数", "チェック元（0：プレイヤー、1：このイベント）")); //0=player 1=this
                    //	p.Add(new BakinParameter("整数", "チェック先（0：地形、1：物体、2：プレイヤー、3：イベント"));  //0=land 1=obj 2=player 3=event
                    //	p.Add(new BakinParameter("整数", "向き（0：正面z+、1：左x-、2：右x+、3：上y+、4：下y-、5：後方z-、6：任意の角度）"));   //0=z+ 1=x- 2=x+ 3=y+ 4=y- 5=z- 6=deg
                    //	p.Add(new BakinParameter("小数", "何マス先までチェックするか（変数可）"));  //distance
                    //	p.Add(new BakinParameter("[変数", "角度X（変数可）"));  //type":name N=numeric, S=string, A=array
                    //	p.Add(new BakinParameter("小数", "(xdegree)]"));  //xdegree]
                    //	p.Add(new BakinParameter("[変数", "角度Y（変数可）"));  //type":name N=numeric, S=string, A=array
                    //	p.Add(new BakinParameter("小数", "(ydegree)]"));  //ydegree]
                    //	p.Add(new BakinParameter("整数", "基準（0：ローカル、1：ワールド）"));   //0=local 1=world
                    //	break;
                    //case "SHOT_EVENT":
                    //	p.Add(new BakinParameter("Guid", "生成されるイベントGuid")); //generated event guid
                    //	p.Add(new BakinParameter("整数", "発射元（0：プレイヤー、1：このイベント）"));   //from 0=player 1=this
                    //	p.Add(new BakinParameter("[整数", "0度の基準（0：下方向、1：向いている方向、2：生成元から見たプレイヤー、3：生成元から見たこのイベント、4＋：各イベント）")); //0deg base 0=down 1=face 2=toplayer 3=tothis
                    //	p.Add(new BakinParameter("Guid", "発射先イベントGuid"));   //toevent guid]
                    //	p.Add(new BakinParameter("[変数", "角度（変数可）"));   //type":name N=numeric, S=string, A=array
                    //	p.Add(new BakinParameter("整数", "(degree)]"));   //degree]
                    //	p.Add(new BakinParameter("整数", "発射数（変数可）"));    //num of shots
                    //	p.Add(new BakinParameter("整数", "生成ごとにばらす角度（変数可）")); //each shift deg +-180
                    //	p.Add(new BakinParameter("整数", "角度のランダム幅（変数可）"));   //deg rand 0-360
                    //	p.Add(new BakinParameter("小数", "生成ごとの待ち時間（変数可）"));  //interval in sec
                    //	p.Add(new BakinParameter("整数", "発射完了を待つ")); //wait complete shot
                    //	break;
                    //case "EXIT":
                    //	p.Add(new BakinParameter("整数", "終了方法（0：タイトル画面に戻る、1：ゲームオーバー）")); //0=totitle 1=gameover
                    //	break;
                    #endregion
            }
        }

        public List<MvCode> MergeConsecutiveSameMovements(List<MvCode> list)
        {
            List<MvCode> codes = new List<MvCode>();
            int i = 0;
            while (i < list.Count)
            {
                if (list[i].code == 205) //set move route
                {
                    codes.Add(list[i]);
                    i++;
                    while (i < list.Count && list[i].code < 100) //contents of code 205
                    {
                        if (list[i].code >= 1 && list[i].code <= 8) //walk command
                        {
                            codes.Add(list[i]);
                            int stepCount = 0;
                            int code = list[i].code;
                            while (list[i].code == code) //get steps while walking
                            {
                                stepCount++;
                                i++;
                            }
                            if (stepCount > 1) //if route only 1 step, not converted to destination
                            {
                                codes.Last().Params = new List<string> { stepCount.ToString() };
                            }
                        }
                        else //non walk command. skip
                        {
                            codes.Add(list[i]);
                            i++;
                        }
                    }
                }
                else
                {
                    codes.Add(list[i]);
                    i++;
                }
            }
            return codes;
        }

        public List<MvCode> ConvertRouteCodesToDestinationCode(List<MvCode> list)
        {
            List<MvCode> codes = new List<MvCode>();
            int i = 0;
            while (i < list.Count)
            {
                if (list[i].code == 205) //set move route
                {
                    codes.Add(list[i]);
                    i++;
                    int x = 0, y = 0;
                    while (i < list.Count && list[i].code < 100)
                    {
                        if (list[i].code >= 1 && list[i].code <= 8) //walk command
                        {
                            int count = 0;
                            while (i < list.Count && list[i].code >= 1 && list[i].code <= 8) //get steps while walking
                            {
                                int code = list[i].code;
                                if (code == 2 || code == 5 || code == 7)
                                {
                                    x++;
                                }
                                else if (code == 3 || code == 6 || code == 8)
                                {
                                    x--;
                                }
                                if (code == 1 || code == 5 || code == 6)
                                {
                                    y++;
                                }
                                else if (code == 4 || code == 7 || code == 8)
                                {
                                    y--;
                                }
                                count++;
                                i++;
                            }
                            codes.Add(list[i - 1]);
                            if (count > 1) //if route only 1 step, not converted to destination
                            {
                                codes.Last().code = 50; //code for moving to distination
                                codes.Last().Params = new List<string> { x.ToString(), y.ToString() };
                            }
                        }
                        else //non walk command. skip
                        {
                            codes.Add(list[i]);
                            i++;
                        }
                    }
                }
                else
                {
                    codes.Add(list[i]);
                    i++;
                }
            }
            return codes;
        }
        #endregion

        #region Privates
        public void SetReviceCommentMode(bool isAddReviceComment)
        {
            _isAddReviceComment = isAddReviceComment;
        }

        //Adding command parameters

        //101 Show Text: 0:face graphic, 1:face index, 2:background type(0:norm, 1:dark, 2:trans), 3:position type(0:up, 1:middle, 2:down),
        //4:speaker name(mz) 5:(concatenated text)
        private void AddCommandDialogue(List<BakinParameter> p, List<string> paras)
        {
            AddCommandHeader(p, "DIALOGUE");
            p.Add(new BakinParameter("文字列", Para_DisplayText, paras.Last())); //text
            p.Add(new BakinParameter("整数", Para_WindowPos, paras[3])); //window pos 0=up 1=middle 2=buttom
            p.Add(new BakinParameter("整数", ""));
            p.Add(new BakinParameter("Guid", Para_DisplayCast + "1Guid")); //cast1 sprite guid
            p.Add(new BakinParameter("文字列", Para_DisplayCast + "1" + Para_Expression)); //cast1 face expression
            p.Add(new BakinParameter("Guid", Para_DisplayCast + "2Guid")); //cast2 sprite guid
            p.Add(new BakinParameter("文字列", Para_DisplayCast + "2" + Para_Expression)); //cast2 face expression
            p.Add(new BakinParameter("整数", Para_SpeakingCast)); //who's talking [0,1]
            p.Add(new BakinParameter("整数", Para_Cast + "1" + Para_Flip)); //cast1 flip
            p.Add(new BakinParameter("整数", Para_Cast + "2" + Para_Flip, "1")); //cast2 flip
            p.Add(new BakinParameter("整数", Para_UseMapLightSource, "1")); //use map light source
            p.Add(new BakinParameter("整数", Para_Cast + "1" + Para_Bilboard)); //cast1 bilboard
            p.Add(new BakinParameter("整数", Para_Cast + "2" + Para_Bilboard)); //cast2 bilboard
            AddCommandEnd(p);
        }

        //Unused
        private void AddCommandMessage(List<BakinParameter> p, List<string> paras)
        {
            AddCommandHeader(p, "MESSAGE");
            p.Add(new BakinParameter("文字列", Para_DisplayText, paras.Last()));   //text
            p.Add(new BakinParameter("整数", Para_WindowPos, paras[3]));  //window pos 0=up 1=middle 2=buttom
            p.Add(new BakinParameter("整数", Para_ShowWindow, paras[2] != "2" ? "1" : "0"));    //show window flag
            AddCommandEnd(p);
        }

        //102 Show Choices: 0:choice list(array), 1:cancel type, 2:default type, 3:position type, 4:background type
        private void AddCommandChoices(List<BakinParameter> p, List<string> paras)
        {
            AddCommandHeader(p, "CHOICES");
            List<string> labels = JsonSerializer.Deserialize<List<string>>(paras[0]);
            if (paras[1] == "-2") //check if cancel branch exists
            {
                labels.Add(Para_Cancel);
            }
            p.Add(new BakinParameter("整数", Para_NumChoces, labels.Count.ToString()));   //num of choices
            _numChoices = 0;
            foreach (string label in labels)
            {
                p.Add(new BakinParameter("文字列", Para_ChoiceLabel, label)); //choice label
            }
            //pos 0=upleft 1=up 2=upright 3=left 4=center 5=right 6=botleft 7=bottom 8=botright
            string pos = int.Parse(paras[3]) == 0 ? "3" : int.Parse(paras[3]) == 1 ? "4" : "5";
            p.Add(new BakinParameter("整数", Para_ChoicePos, pos));
            AddCommandEnd(p);
        }

        //402 Branches of the choices
        private void AddCommandBranch(List<BakinParameter> p, List<string> paras)
        {
            _indentType.Add("BRANCH");
            if (_numChoices > 0) //first choice is skipped
            {
                AddCommandHeader(p, "BRANCH");
                p.Add(new BakinParameter("整数", Para_ChoiceN, _numChoices.ToString())); //choice N-1
                AddCommandEnd(p);
            }
            _numChoices++;
        }

        //103 Input Number: 0:Variable ID, 1:number of digits
        private void AddCommandChangestringvariable(List<BakinParameter> p, List<string> paras)
        {
            AddCommandHeader(p, "CHANGE_STRING_VARIABLE");
            p.Add(new BakinParameter("変数", Para_StrVarNum, GetBknVarName(paras[0], "N"))); //type":name N=numeric, S=string, A=array
            p.Add(new BakinParameter("整数", Para_MaxNumInput, paras[1])); //maxchar
            p.Add(new BakinParameter("整数", Para_WindowPos)); //pos 0=top 1=center 2=bottom
            p.Add(new BakinParameter("文字列", Para_DisplayKeys + "1", "0123456789"));   //input1
            p.Add(new BakinParameter("文字列", Para_DisplayKeys + "2"));   //input2
            p.Add(new BakinParameter("文字列", Para_DisplayKeys + "3"));
            p.Add(new BakinParameter("文字列", Para_DisplayKeys + "4"));
            AddCommandEnd(p);
        }

        //104 Select Item: 0:Variable ID, 1:item type
        private void AddCommandItemmenu(List<BakinParameter> p, List<string> paras)
        {
            AddCommandHeader(p, "ITEMMENU");
            p.Add(new BakinParameter("Guid", Para_UseLayout + "Guid")); //layout guid
            AddCommandEnd(p);
        }

        //105 Show Scrolling Text: 0:Scroll speed, 1:no fast forward(boolean), 2:(concatenated text)
        private void AddCommandTelop(List<BakinParameter> p, List<string> paras)
        {
            AddCommandHeader(p, "TELOP");
            p.Add(new BakinParameter("文字列", Para_DisplayText, paras.Last())); //text
            p.Add(new BakinParameter("整数", Para_Back, "2")); //background 0=black 1=picture 2=none
            if (p.Last().Value == "1")
            {
                p.Add(new BakinParameter("Guid", Para_GraphicGuid)); //if bg=picture, sprite guid
            }
            p.Add(new BakinParameter("整数", Para_ScrollTelop, "1")); //scroll
            AddCommandEnd(p);
        }

        //111(00) If switch: 0:0, 1:id, 2:flag
        //111(02) If selfswitch: 0:2, 1:letter, 2:flag
        private void AddCommandIfswitch(List<BakinParameter> p, List<string> paras)
        {
            AddCommandHeader(p, "IFSWITCH");
            p.Add(new BakinParameter("変数", Para_EventSwitchName, GetBknVarName(paras[1], "B"))); //type:B=bool
            p.Add(new BakinParameter("整数", Para_CondOnOff, paras[2] == "on" ? "0" : "1")); //0=on 1=off
            AddCommandEnd(p);
        }

        //111(01) If variable: 0:1, 1:id, 2:type(const, var), 3:value or id, 4:operation(==,>=,<=,>,<,!=)
        //111(03) If timer 0:3	1:operator(>=,<=) 2:time in sec
        private void AddCommandIfvariable(List<BakinParameter> p, List<string> paras)
        {
            AddCommandHeader(p, "IFVARIABLE");
            if (paras[0] == "1") //if variable
            {
                p.Add(new BakinParameter("変数", Para_VarName, GetBknVarName(paras[1], "N"))); //type:name N=numeric
                Tuple<string, string> varval = GetBknVarNameOrVal(paras[2], paras[3]);
                p.Add(new BakinParameter(varval.Item1, Para_VariableValue, varval.Item2));
                string tmp = ToInt(paras[4]) <= 2 ? paras[4] : paras[4] == "5" ? "3" : ToStr(ToInt(paras[4]) - 1);
                p.Add(new BakinParameter("整数", Para_CondOp, tmp)); //0== 1=>= 2=<= 3=!= 4=> 5=<
            }
            else //if timer. Timer common event must be implemented
            {
                p.Add(new BakinParameter("変数", Para_TimerVarName, "N:TimerInfo")); //type:name N=numeric
                p.Add(new BakinParameter("整数", Para_TimerSec, paras[2]));
                p.Add(new BakinParameter("整数", Para_CondOp, ToStr(ToInt(paras[1]) + 1))); //0== 1=>= 2=<= 3=!= 4=> 5=<
            }
            AddCommandEnd(p);
        }

        //111(04) If actor in party: 0:4, 1:actor id, 2:0(party)
        private void AddCommandIfparty(List<BakinParameter> p, List<string> paras)
        {
            AddCommandHeader(p, "IFPARTY");
            p.Add(new BakinParameter("Guid", Para_ObjCast + "Guid" + "(ID:" + paras[1] + ")")); //member guid
            p.Add(new BakinParameter("整数", Para_CondInParty, "0")); //0=with 1=without
            AddCommandEnd(p);
        }

        //111(05) If actor name: 0:4, 1:actor id, 2:1(name), 3:name
        private void AddCommandIfstringvariable(List<BakinParameter> p, List<string> paras)
        {
            AddCommandHeader(p, "IF_STRING_VARIABLE");
            p.Add(new BakinParameter("整数", Para_StrVarName, "-1")); //type: -1=actor name
            p.Add(new BakinParameter("文字列", Para_CompStr, paras[3])); //string
            p.Add(new BakinParameter("Guid", Para_ObjCast + "Guid" + "(ID:" + paras[1] + ")")); //member guid
            p.Add(new BakinParameter("整数", Para_CompCond, "0")); //0=equal 1=startwith 2=endwith 3=include
            AddCommandEnd(p);
        }

        //111(07) If actor weapon,armor: 0:4, 1:actor id, 2:4,5(weapon,armor), 3:id
        //111(11) If item: 0:8, 1:id
        //111(12) If weapon: 0:9, 1:id, 2:include equip flag
        //111(13) If armor: 0:10, 1:id, 2:include equip flag
        private void AddCommandIfitem(List<BakinParameter> p, List<string> paras)
        {
            AddCommandHeader(p, "IFITEM");
            p.Add(new BakinParameter("Guid", Para_ObjItem + "Guid" + "(ID:" + paras[0] == "4" ? paras[3] : paras[1] + ")")); //item guid
            p.Add(new BakinParameter("整数", Para_Num, "1"));
            p.Add(new BakinParameter("整数", Para_CondHave, "0")); //0=having 1=not having
            p.Add(new BakinParameter("整数", Para_WithoutEquipItem, "0")); //exclude equipped flag
            AddCommandEnd(p);
        }

        //111(08) If enemy: 0:5, 1:enemy id, 2:check(visible, state), 3:state id
        private void AddCommandBtlifmonster(List<BakinParameter> p, List<string> paras)
        {
            if (paras[2] == "1")
            {
                AddCommandNoticeComment(p, Cvt_NoConvert, Para_CondEnemyStatus + "(ID:" + paras[3] + ")");
            }
            else
            {
                AddCommandHeader(p, "BTL_IFMONSTER");
                p.Add(new BakinParameter("Guid", Para_ObjMonster + "Guid" + "(ID:" + paras[1] + ")")); //item guid
                p.Add(new BakinParameter("整数", Para_Nth, "1")); //only 1st monster is accessed...
                AddCommandEnd(p);
            }
        }

        //111(10) If gold: 0:7, 1:amount, 2:operator(>=,<=,<)
        private void AddCommandIfmoney(List<BakinParameter> p, List<string> paras)
        {
            AddCommandHeader(p, "IFMONEY");
            p.Add(new BakinParameter("整数", Para_MoneyAmount, paras[1])); //amount
            p.Add(new BakinParameter("整数", Para_CondHave, paras[2] == "0" ? "0" : "1")); //0=having 1=not having
            AddCommandEnd(p);
        }

        //117 Common Event: 0:Common Event ID
        private void AddCommandExec(List<BakinParameter> p, List<string> paras)
        {
            AddCommandHeader(p, "EXEC");
            p.Add(new BakinParameter("Guid", Para_CommonEvent + "Guid(ID:" + paras[0] + ")")); //common event guid
            p.Add(new BakinParameter("整数", Para_WaitExecEvent, "1")); //wait complete
            AddCommandEnd(p);
        }

        //121 Control Switches: 0:Single switch ID or starting ID, 1:range end ID, 2:operation(on, off)
        //123 Control Self Switch: 0:Self switch letter, 1:value(on,off)
        //27 switch on
        //28 switch off
        private void AddCommandSwitch(int code, List<BakinParameter> p, List<string> paras)
        {
            AddCommandHeader(p, "SWITCH");
            if (code == 121)
            {
                for (var i = ToInt(paras[0]); i <= ToInt(paras[1]); i++)
                {
                    if (i > ToInt(paras[0]))
                    {
                        AddCommandEnd(p);
                        AddCommandHeader(p, "SWITCH");
                    }
                    p.Add(new BakinParameter("変数", Para_EventSwitchName, GetBknVarName(i.ToString(), "B"))); //to; B=Bool
                    p.Add(new BakinParameter("整数", Para_StateOnOff, paras[2] == "on" ? "0" : "1")); //0=on 1=off
                }
            }
            else if (code == 123)
            {
                p.Add(new BakinParameter("ローカル変数", Para_EventSwitchName, GetBknVarName(paras[0], "B"))); //to; B=Bool
                p.Add(new BakinParameter("整数", Para_StateOnOff, paras[1] == "on" ? "0" : "1")); //0=on 1=off
            }
            else
            {
                p.Add(new BakinParameter("変数", Para_EventSwitchName, GetBknVarName(paras[0], "B"))); //to; B=Bool
                p.Add(new BakinParameter("整数", Para_StateOnOff, code == 27 ? "0" : "1")); //0=on 1=off
            }
            AddCommandEnd(p);
        }

        //def & mdef are treated as the same parameter. luck is treated as evasion rate.
        //122 Control Variables common: 0:Single variable ID or starting ID, 1:range end ID, 2:operation(set,add,sub,mul,div,mod)
        //122(00) Control Variables: 3:operand(const,var), 4:value or varid
        //122(1) Control Variables random: 3:2, 4:randmin, 5:randmax
        //122(02) Control Variables item: 3:3, 4:type(item, weapon, armor), 5:id
        //122(03) Control Variables actor: 3:3, 4:3, 5:id, 6:val(level, exp, hp, mp, maxhp, maxmp, atk, def, matk, mdef, agi, luk)
        //122(04) Control Variables enemy: 3:3, 4:4, 5:id, 6:val(hp, mp, maxhp, maxmp, atk, def, matk, mdef, agi, luk)
        //122(05) Control Variables character: 3:3, 4:5, 5:id, 6:val(mapx, mapy, dir, screenx, screeny)
        //122(07) Control Variables gamedata: 3:3, 4:7, 5:val(mapid, nummember, gold, step, playtime, timer, savecount, battlecount, windount, escapecount)
        private void AddCommandHlvariable(List<BakinParameter> p, List<string> paras)
        {
            if (ToInt(paras[3]) == 3 && ToInt(paras[4]) == 5 && ToInt(paras[5]) >= 1)
            {
                AddCommandNoticeComment(p, Cvt_NoConvert, Para_VarCharStatus); //character; only player or this event.
            }
            else if (ToInt(paras[3]) == 3 && ToInt(paras[4]) == 7 && ToInt(paras[5]) != 1 && ToInt(paras[5]) != 2 && ToInt(paras[5]) != 4)
            {
                List<string> str = new List<string> { Para_MapId, "", "", Para_NumSteps, "", Para_Timer, Para_NumSave, Para_NumBtl, Para_NumWin, Para_NumEscape };
                AddCommandNoticeComment(p, Cvt_NoConvert, Para_VarGameData + "(" + str[ToInt(paras[5])] + ")"); //gamedata; only membernum, gold and playtime info can be fetched
            }
            else
            {
                string notice;
                if (ToInt(paras[3]) == 3 && ToInt(paras[4]) <= 4)
                {
                    notice = paras[4] == "0" ? Para_Item + "Guid" : paras[4] == "1" ? Para_Weapon + "Guid" : paras[4] == "2" ? Para_Armor + "Guid" :
                            paras[4] == "3" ? Para_ActorInfo : Para_EnemyInfo;
                    notice += "(ID:" + paras[5] + (ToInt(paras[3]) >= 3 ? (", " + Para_Status + "ID:" + paras[6]) : "") + ")";
                    AddCommandNoticeComment(p, Cvt_NeedRevise, notice);
                }

                List<string> status;
                AddCommandHeader(p, "HLVARIABLE");
                for (var i = ToInt(paras[0]); i <= ToInt(paras[1]); i++)
                {
                    if (i > ToInt(paras[0]))
                    {
                        AddCommandEnd(p);
                        AddCommandHeader(p, "HLVARIABLE");
                    }
                    p.Add(new BakinParameter("整数", ""));
                    p.Add(new BakinParameter("変数", Para_VarNum, GetBknVarName(ToStr(i), "N"))); //type:N=numeric
                    if (ToInt(paras[3]) <= 1) //variable
                    {
                        Tuple<string, string> varval = GetBknVarNameOrVal(paras[3], paras[4]);
                        p.Add(new BakinParameter("整数", Para_FromType, paras[3] == "0" ? "0" : "2"));
                        p.Add(new BakinParameter(varval.Item1, Para_InputValue, varval.Item2));
                    }
                    else if (ToInt(paras[3]) == 2) //random
                    {
                        p.Add(new BakinParameter("整数", Para_FromType, "1"));
                        p.Add(new BakinParameter("整数", Para_RandMin, paras[4]));
                        p.Add(new BakinParameter("整数", Para_RandMax, paras[5]));
                    }
                    else if (ToInt(paras[4]) <= 2) //item
                    {
                        p.Add(new BakinParameter("整数", Para_FromType, "5"));
                        p.Add(new BakinParameter("Guid", Para_ObjItem + "Guid(ID:" + paras[5] + ")"));
                    }
                    else if (ToInt(paras[4]) == 3 || ToInt(paras[4]) == 4) //actor, enemy
                    {
                        p.Add(new BakinParameter("整数", Para_FromType, "6"));
                        p.Add(new BakinParameter("Guid", Para_ObjCast + "Guid(ID:" + paras[5] + ")"));
                        if (paras[4] == "3") //actor
                        {
                            //(level, exp, hp, mp, maxhp, maxmp, atk, def, matk, mdef, agi, luk)
                            status = new List<string> { "0", "10", "1", "2", "3", "4", "5", "6", "11", "6", "9", "8" };
                        }
                        else //enemy
                        {
                            //(hp, mp, maxhp, maxmp, atk, def, matk, mdef, agi, luk)
                            status = new List<string> { "1", "2", "3", "4", "5", "6", "11", "6", "9", "8" };
                        }
                        p.Add(new BakinParameter("整数", Para_StatusType, status[ToInt(paras[6])]));
                    }
                    else if (ToInt(paras[4]) == 5) //character
                    {
                        if (paras[5] == "-1") //player
                        {
                            //(mapx, mapy, dir, screenx, screeny, posx, posy)
                            status = new List<string> { "17", "18", "21", "29", "30", "17", "18" };
                        }
                        else //this event
                        {
                            status = new List<string> { "14", "15", "20", "27", "28", "14", "15" };
                        }
                        p.Add(new BakinParameter("整数", Para_FromType, status[ToInt(paras[6])]));
                    }
                    else //game data
                    {
                        p.Add(new BakinParameter("整数", Para_FromType, paras[6] == "1" ? "31" : paras[6] == "2" ? "4" : "8"));
                        //nummember = 31, gold = 4, playtime(hour) = 8
                        p.Add(new BakinParameter("整数", Para_InputValue));
                        if (paras[6] == "4") //playtime
                        {
                            p.Add(new BakinParameter("整数", Para_TimeUnit, "4")); //4=hour, 5=minute, 6=second
                        }
                    }
                    string op = paras[2] == "5" ? "6" : paras[2];
                    p.Add(new BakinParameter("整数", Para_Calc0to7, op)); //0=overwrite 1=add 2=sub 3=mult 4=div 6=mod 7=floor
                }
                AddCommandEnd(p);
            }
        }

        //125 Change Gold: 0:Operation(+, -), 1:type(const, var), 2:value or id
        private void AddCommandMoney(List<BakinParameter> p, List<string> paras)
        {
            AddCommandHeader(p, "MONEY");
            p.Add(new BakinParameter("整数", ""));
            Tuple<string, string> varval = GetBknVarNameOrVal(paras[1], paras[2]);
            p.Add(new BakinParameter(varval.Item1, Para_MoneyAmountVar, varval.Item2)); //amount
            p.Add(new BakinParameter("整数", Para_Change + Para_IncrDecr12, paras[0] == "0" ? "1" : "2")); //1=increase 2=decrease
            AddCommandEnd(p);
        }

        //126 Change Items: 0:item id, 1:Operation(+, -), 2:type(const, var), 3:value or id
        //127 Change Weapons: 0:weapon id, 1:Operation(+, -) 2:type(const, var) 3:value or id, 4:include equipment flag
        //128 Change Armors: 0:armor id, 1:Operation(+, -), 2:type(const, var), 3:value or id, 4:include equipment flag
        private void AddCommandItem(List<BakinParameter> p, List<string> paras)
        {
            AddCommandHeader(p, "ITEM");
            p.Add(new BakinParameter("Guid", Para_ObjItem + "Guid" + "(ID：" + paras[0] + ")")); //item guid
            p.Add(new BakinParameter("整数", ""));
            Tuple<string, string> varval = GetBknVarNameOrVal(paras[2], paras[3]);
            p.Add(new BakinParameter(varval.Item1, Para_NumVar, varval.Item2)); //amount
            p.Add(new BakinParameter("整数", Para_Change + Para_IncrDecr12, paras[1] == "0" ? "1" : "2")); //1=increase 2=decrease
            AddCommandEnd(p);
        }

        //129 Change Party Member: 0:actor id, 1:operation(add, remove), 2:initialize flag
        private void AddCommandParty(List<BakinParameter> p, List<string> paras)
        {
            AddCommandHeader(p, "PARTY");
            p.Add(new BakinParameter("整数", Para_ObjCast + Para_SpecifyOrN, "0")); //0=specify_cast 1=n-th member
            p.Add(new BakinParameter("Guid", Para_ObjCast + "Guid" + "(ID：" + paras[0] + ")")); //cast guid
            p.Add(new BakinParameter("整数", Para_NthInParty));   //member number, N-1
            p.Add(new BakinParameter("整数", Para_JoinState, paras[1])); //0=join 1=remove
            p.Add(new BakinParameter("整数", Para_InheritStatus, paras[2] == "0" ? "1" : "0")); //inherit previous status
            AddCommandEnd(p);
        }

        //134 Change Save Access 0:Disable/enable
        private void AddCommandSwsave(List<BakinParameter> p, List<string> paras)
        {
            AddCommandHeader(p, "SW_SAVE");
            p.Add(new BakinParameter("整数", Para_NoSave, paras[0] == "0" ? "1" : "0")); //save disable flag
            AddCommandEnd(p);
        }

        //135 Change Menu Access: 0:Disable/enable
        private void AddCommandSwmenu(List<BakinParameter> p, List<string> paras)
        {
            AddCommandHeader(p, "SW_MENU");
            p.Add(new BakinParameter("整数", Para_NoMenu, paras[0] == "0" ? "1" : "0")); //menu disable flag
            AddCommandEnd(p);
        }

        //136 Change Encounter: 0:Disable/enable
        private void AddCommandSwencounting(List<BakinParameter> p, List<string> paras)
        {
            AddCommandHeader(p, "SW_ENCOUNTING");
            p.Add(new BakinParameter("整数", Para_NoEmerge, paras[0] == "0" ? "1" : "0")); //monster disable flag
            AddCommandEnd(p);
        }

        //138 Change Window Color: 0:RGB array
        private void AddCommandChangelayout(List<BakinParameter> p, List<string> paras)
        {
            AddCommandHeader(p, "CHANGE_LAYOUT");
            p.Add(new BakinParameter("Guid", Para_UseLayout + "Guid(" + Para_WindowColor + ":" + paras[0] + ")")); //layout guid
            AddCommandEnd(p);
        }

        //201 Transfer Player: 0:specify type(const, var), 1:map id value or id, 2:x value or id, 3:y value or id,
        //	4:direction(0=nochange, 2=down, 4=left, 6=right, 8=up), 5:fade type(black, white)
        private void AddCommandPlmove(List<BakinParameter> p, List<string> paras)
        {
            AddCommandNoticeComment(p, Cvt_NeedRevise);
            AddCommandHeader(p, "PLMOVE");
            //map_guid|spot_id_from_1001|xpos|ypos|zpos
            p.Add(new BakinParameter("スポット", Para_Spot, GetSpot(paras[0], paras[2], paras[3])));
            //direction 0=nochange 1=up 2=down 3=left 4=right
            string d = paras[4] == "4" ? "3" : paras[4] == "6" ? "4" : paras[4];
            p.Add(new BakinParameter("整数", Para_SpecDir, d));
            AddCommandEnd(p);
        }

        //203 Set Event Location: 0:id, 1:specify type(const, var, swap), 2:x; value or id or swapevent id, 3:y value or id,
        //	4:direction
        private void AddCommandMove(List<BakinParameter> p, List<string> paras)
        {
            if (paras[1] == "2")
            {
                AddCommandNoticeComment(p, Cvt_NoConvert, Para_NoSwap);
            }
            else
            {
                AddCommandHeader(p, "MOVE");
                //map_guid|spot_id_from_1001|xpos|ypos|zpos
                p.Add(new BakinParameter("スポット", Para_Spot, GetSpot(paras[1], paras[2], paras[3])));
                p.Add(new BakinParameter("Guid", Para_Event + "Guid" + "(ID:" + paras[0] + ")")); //event guid
                                                                                                  //direction 0=nochange 1=up 2=down 3=left 4=right
                string d = paras[4] == "4" ? "3" : paras[4] == "6" ? "4" : paras[4];
                p.Add(new BakinParameter("整数", Para_SpecDir));
                AddCommandEnd(p);
            }
        }

        //204 Scroll Map: 0:Scroll direction, 1:distance, 2:speed, 3:wait flag(mz)
        private void AddCommandCamanimation(List<BakinParameter> p, List<string> paras)
        {
            AddCommandNoticeComment(p, Cvt_NeedRevise);
            AddCommandHeader(p, "CAM_ANIMATION");
            p.Add(new BakinParameter("Guid", Para_Camera + "Guid" +
                "(" + Para_Dir + ":" + paras[0] + "," + Para_Dist + ":" + paras[1] + "," + Para_Speed + ":" + paras[2] + ")")); //camera_anim guid
            string flag = paras.Count == 4 ? paras[3] : "0";
            p.Add(new BakinParameter("整数", Para_WaitComplete, flag)); //wait complete
            AddCommandEnd(p);
        }

        //212 Show Animation: 0:char id, 1:animation id, 2:wait flag
        //337 Show Battle Animation: 0:Enemy index, 1:animation ID, 2:entire troop flag
        private void AddCommandEffect(int code, List<BakinParameter> p, List<string> paras)
        {
            AddCommandNoticeComment(p, Cvt_NeedRevise, Para_Effect + "ID, " + Para_Range);
            AddCommandHeader(p, "EFFECT");
            string tmp = code == 337 ? "2" : (paras[0] == "-1" ? "1" : paras[0] == "0" ? "0" : "2");
            p.Add(new BakinParameter("整数", Para_DispCenter0123, tmp)); //pos 0=this 1=player
            p.Add(new BakinParameter("Guid", Para_Effect + "Guid" + "(ID:" + paras[1] + ")")); //effect guid
            p.Add(new BakinParameter("整数", Para_WaitComplete, code == 212 ? paras[2] : "1")); //wait complete
            AddCommandEnd(p);
        }

        //213 Show Balloon Icon: 0:char id, 1:balloon id, 2:wait flag
        private void AddCommandEmote(List<BakinParameter> p, List<string> paras)
        {
            AddCommandNoticeComment(p, Cvt_NeedRevise, Para_Emote + "ID, " + Para_ObjEvent);
            AddCommandHeader(p, "EMOTE");
            string tmp = paras[0] == "-1" ? "1" : paras[0] == "0" ? "0" : "2";
            p.Add(new BakinParameter("整数", Para_DispCenter01, tmp)); //pos 0=this 1=player
            p.Add(new BakinParameter("Guid", Para_Emote + "Guid(ID:" + paras[1] + ")")); //emote guid
            p.Add(new BakinParameter("整数", Para_WaitComplete, paras[2])); //wait complete
            AddCommandEnd(p);
        }

        //216 Change Player Followers: 0:(on, off)
        private void AddCommandWalkinrows(List<BakinParameter> p, List<string> paras)
        {
            AddCommandHeader(p, "WALK_IN_ROWS");
            p.Add(new BakinParameter("整数", Para_AllowRow, paras[0])); //walk row flag
            AddCommandEnd(p);
        }

        //221 Fadeout Screen:
        //222 Fadein Screen:
        private void AddCommandScreenfade(int code, List<BakinParameter> p, List<string> paras)
        {
            AddCommandHeader(p, "SCREEN_FADE");
            p.Add(new BakinParameter("小数", Para_TimeToChange, "0.4")); //time in sec
            p.Add(new BakinParameter("整数", Para_Effect01, code == 221 ? "1" : "0")); //fade 0=in 1=out
            p.Add(new BakinParameter("整数", Para_WaitComplete, "1")); //wait complete
            AddCommandEnd(p);
        }

        //Currently unused. Probably not working as expected.. changing rendering is better.
        //223 Tint Screen: 0:colour object(RGB, grey), 1:duration in frames, 2:wait flag
        private void AddCommandScreencolor(List<BakinParameter> p, List<string> paras)
        {
            AddCommandHeader(p, "SCREEN_COLOR");
            p.Add(new BakinParameter("小数", Para_TimeToChange, Frame2Time(paras[1]))); //time in sec
            p.Add(new BakinParameter("整数", Para_ScreenColor)); //color dec -> ARGB hex
            p.Add(new BakinParameter("整数", Para_WaitComplete, paras[2]));    //wait complete
            AddCommandEnd(p);
        }

        //224 Flash Screen: 0:colour object(RGB, grey), 1:duration in frames, 2:wait flag
        private void AddCommandScreenflash(List<BakinParameter> p, List<string> paras)
        {
            AddCommandHeader(p, "SCREEN_FLASH");
            p.Add(new BakinParameter("小数", Para_FlashTime, Frame2Time(paras[1])));  //time in sec
            AddCommandEnd(p);
        }

        //225 Shake Screen: 0:Power(1 - 9), 1:speed(1 - 9), 2:duration in frames, 3:wait flag
        private void AddCommandScreenshake(List<BakinParameter> p, List<string> paras)
        {
            AddCommandHeader(p, "SCREEN_SHAKE");
            p.Add(new BakinParameter("小数", Para_ShakeTime, Frame2Time(paras[2])));  //time in sec
            string tmp = ToInt(paras[0]) <= 3 ? "0" : ToInt(paras[0]) <= 6 ? "1" : "2";
            p.Add(new BakinParameter("整数", Para_ShakeStrength, tmp));  //strength 0=weak 1=middle 2=strong
            p.Add(new BakinParameter("整数", Para_WaitComplete, paras[3]));    //wait complete
            AddCommandEnd(p);
        }

        //230 Wait: 0:Wait time in frames
        private void AddCommandWait(List<BakinParameter> p, List<string> paras)
        {
            AddCommandHeader(p, "WAIT");
            p.Add(new BakinParameter("小数", Para_Time, Frame2Time(paras[0]))); //time
            p.Add(new BakinParameter("整数", ""));
            AddCommandEnd(p);
        }

        //231 Show Picture: 0:Picture number, 1:image filename, 2:origin(upleft, center), 3:specify type(const, var),
        //	4:x value or id, 5:y value or id, 6:scale X, 7:scale Y, 8:opacity, 9:blend mode(normal, add, mult, screen)
        private void AddCommandSppicture(List<BakinParameter> p, List<string> paras)
        {
            AddCommandHeader(p, "SPPICTURE");
            p.Add(new BakinParameter("整数", Para_ImageNum, paras[0]));
            p.Add(new BakinParameter("Guid", Para_Image + "Guid(" + paras[1] + ")"));
            p.Add(new BakinParameter("整数", Para_ZoomX, paras[6]));
            p.Add(new BakinParameter("整数", Para_HalfTrans, OpacToColor(paras[8])));
            p.Add(new BakinParameter("整数", ""));
            Tuple<string, string> varval = GetBknVarNameOrVal(paras[3], paras[4]);
            p.Add(new BakinParameter(varval.Item1, Para_PosX, varval.Item2));
            varval = GetBknVarNameOrVal(paras[3], paras[5]);
            p.Add(new BakinParameter(varval.Item1, Para_PosY, varval.Item2));
            p.Add(new BakinParameter("文字列", Para_Motion));
            p.Add(new BakinParameter("整数", Para_ShowFade));
            p.Add(new BakinParameter("整数", Para_ZoomY, paras[7]));
            p.Add(new BakinParameter("整数", Para_Rot));
            AddCommandEnd(p);
        }

        //232 Move Picture: 0:Picture number, 1:image filename, 2:origin(upleft, center), 3:specify type(const, var),
        //	4:x value or id, 5:y value or id, 6:scale X, 7:scale Y, 8:opacity, 9:blend mode(normal, add, mult, screen),
        //	10:duration, 11:wait flag
        private void AddCommandSpmove(List<BakinParameter> p, List<string> paras)
        {
            AddCommandHeader(p, "SPMOVE");
            p.Add(new BakinParameter("整数", Para_ImageNum, paras[0])); //image id
            p.Add(new BakinParameter("整数", Para_ZoomX, paras[6]));
            p.Add(new BakinParameter("小数", Para_TimeToRot, Frame2Time(paras[10]))); //move time in sec
            p.Add(new BakinParameter("整数", ""));
            p.Add(new BakinParameter("整数", ""));
            p.Add(new BakinParameter("整数", ""));
            Tuple<string, string> varval = GetBknVarNameOrVal(paras[3], paras[4]);
            p.Add(new BakinParameter(varval.Item1, Para_PosX, varval.Item2)); //xpos
            varval = GetBknVarNameOrVal(paras[3], paras[5]);
            p.Add(new BakinParameter(varval.Item1, Para_PosY, varval.Item2)); //ypos
            p.Add(new BakinParameter("整数", Para_ZoomY, paras[7]));
            p.Add(new BakinParameter("整数", Para_WaitComplete, paras[11])); //wait complete
            AddCommandEnd(p);
        }

        //235 Erase Picture: 0:Picture number
        private void AddCommandSphide(List<BakinParameter> p, List<string> paras)
        {
            AddCommandHeader(p, "SPHIDE");
            p.Add(new BakinParameter("整数", Para_ImageNum, paras[0])); //image id
            p.Add(new BakinParameter("整数", Para_HideFade, "0")); //fade flag
            AddCommandEnd(p);
        }

        //223 Tint Screen: 0:colour object(RGB, grey), 1:duration in frames, 2:wait flag
        //236 Change Weather: 0:Weather type, 1:power, 2:duration, 3:wait flag
        //283 Change Battle Background: 0:Battleback1   1:Battleback2
        //284 Change Parallax: 0:Image filename, 1:loop x flag, 2:3:loop y flag, 3:x shift, 4:y shift
        private void AddCommandChangerender(int code, List<BakinParameter> p, List<string> paras)
        {
            string tmp = "";
            if (code == 223)
            {
                AddCommandNoticeComment(p, Cvt_NeedRevise, Para_ScreenTone);
                string[] col = paras[0].Substring(1, paras[0].Length - 2).Split(',');
                tmp = Para_Tone + ":" + "R=" + col[0] + ",G=" + col[1] + ",B=" + col[2] + ",Grey=" + col[3];
            }
            else if (code == 236)
            {
                AddCommandNoticeComment(p, Cvt_NeedRevise, Para_Weather);
                tmp = Para_Weather + "ID:" + paras[0];
            }
            else if (code == 283)
            {
                AddCommandNoticeComment(p, Cvt_NeedRevise, Para_BtlBack);
                tmp = Para_BtlBack + "ID:" + paras[0] + "+" + paras[1];
            }
            else
            {
                AddCommandNoticeComment(p, Cvt_NeedRevise, Para_GameBack);
                tmp = Para_GameBack + "ID:" + paras[0];
            }
            AddCommandHeader(p, "CHANGE_RENDER");
            p.Add(new BakinParameter("Guid", Para_Rendering + "Guid" + "(" + tmp + ")")); //render guid
            p.Add(new BakinParameter("文字列", Para_RenderingName)); //render name
            p.Add(new BakinParameter("文字列", ""));
            AddCommandEnd(p);
        }

        //241 Play BGM: 0:{ name: BGM filename, volume: 0 - 100, pitch: 50 - 150, pan: -100 - 100 }
        //242 Fadeout BGM: 0:Duration in frames
        private void AddCommandPlaybgm(int code, List<BakinParameter> p, List<string> paras)
        {
            if (code == 241)
            {
                AddCommandNoticeComment(p, Cvt_NeedRevise, "BGM, " + Para_NoPan);
                AddCommandHeader(p, "PLAYBGM");
                MvAudio audio = JsonSerializer.Deserialize<MvAudio>(paras[0]);
                p.Add(new BakinParameter("Guid", "BGM Guid(" + audio.name + ")")); //bgm guid
                p.Add(new BakinParameter("整数", Para_Vol, ToStr(audio.volume))); //vol
                p.Add(new BakinParameter("整数", Para_Tempo, ToStr(audio.pitch))); //tempo
                p.Add(new BakinParameter("整数", ""));
                p.Add(new BakinParameter("小数", Para_FadeoutTime, "0")); //fadeout time
                p.Add(new BakinParameter("小数", Para_FadeinTime, "0")); //fadein time
            }
            else
            {
                AddCommandHeader(p, "PLAYBGM");
                p.Add(new BakinParameter("Guid", "BGM Guid"));  //bgm guid
                p.Add(new BakinParameter("整数", Para_Vol));   //vol
                p.Add(new BakinParameter("整数", Para_Tempo)); //tempo
                p.Add(new BakinParameter("整数", ""));
                p.Add(new BakinParameter("小数", Para_FadeoutTime, "0"));  //fadeout time
                p.Add(new BakinParameter("小数", Para_FadeinTime, Frame2Time(paras[0])));   //fadein time
            }
            AddCommandEnd(p);
        }

        //245 Play BGS: 0:{ name: BGM filename, volume: 0 - 100, pitch: 50 - 150, pan: -100 - 100 }
        //246 Fadeout BGS: 0:Duration in frames
        private void AddCommandPlaybgs(int code, List<BakinParameter> p, List<string> paras)
        {
            if (code == 245)
            {
                AddCommandNoticeComment(p, Cvt_NeedRevise, "BGS, " + Para_NoPan);
                AddCommandHeader(p, "PLAYBGS");
                MvAudio audio = JsonSerializer.Deserialize<MvAudio>(paras[0]);
                p.Add(new BakinParameter("Guid", "BGS Guid(" + audio.name + ")"));  //bgs guid
                p.Add(new BakinParameter("整数", Para_Vol, ToStr(audio.volume)));   //vol
                p.Add(new BakinParameter("整数", Para_Tempo, ToStr(audio.pitch))); //tempo 50-200
                p.Add(new BakinParameter("整数", ""));
                p.Add(new BakinParameter("小数", Para_FadeoutTime, "0"));  //fadeout time
                p.Add(new BakinParameter("小数", Para_FadeinTime, "0"));   //fadein time
            }
            else
            {
                AddCommandHeader(p, "PLAYBGS");
                p.Add(new BakinParameter("Guid", "BGS Guid"));  //bgs guid
                p.Add(new BakinParameter("整数", Para_Vol));   //vol
                p.Add(new BakinParameter("整数", Para_Tempo)); //tempo 50-200
                p.Add(new BakinParameter("整数", ""));
                p.Add(new BakinParameter("小数", Para_FadeoutTime));  //fadeout time
                p.Add(new BakinParameter("小数", Para_FadeinTime, Frame2Time(paras[0])));   //fadein time
            }
            AddCommandEnd(p);
        }

        //249 Play ME: 0:{ name: BGM filename, volume: 0 - 100, pitch: 50 - 150, pan: -100 - 100 }
        private void AddCommandPlayjingle(List<BakinParameter> p, List<string> paras)
        {
            AddCommandNoticeComment(p, Cvt_NeedRevise, "ME, " + Para_NoPan);
            AddCommandHeader(p, "PLAYJINGLE");
            MvAudio audio = JsonSerializer.Deserialize<MvAudio>(paras[0]);
            p.Add(new BakinParameter("Guid", "ME Guid(" + audio.name + ")"));   //fanfare guid
            p.Add(new BakinParameter("整数", Para_Vol, ToStr(audio.volume)));   //vol
            p.Add(new BakinParameter("整数", Para_Tempo, ToStr(audio.pitch))); //tempo
            p.Add(new BakinParameter("整数", Para_WaitComplete, "0")); //wait complete
            AddCommandEnd(p);
        }

        //251 Stop SE
        private void AddCommandStopse(List<BakinParameter> p, List<string> paras)
        {
            AddCommandHeader(p, "STOPSE");
            p.Add(new BakinParameter("Guid", Para_StopSeGuid));   //stop audio guid, all if 0
            AddCommandEnd(p);
        }

        //261 Play Movie: 0:Video filename
        private void AddCommandPlaymovie(List<BakinParameter> p, List<string> paras)
        {
            AddCommandNoticeComment(p, Cvt_NeedRevise, Para_MovieName);
            AddCommandHeader(p, "PLAYMOVIE");
            p.Add(new BakinParameter("Guid", Para_PlayMovie + "Guid(" + paras[0] + ")"));    //movie guid
            AddCommandEnd(p);
        }

        //285 Get Location Info, 0:Variable ID, 1:type(terraintag, eventid, tileid(layer1 - 4), regionid),
        //	2:specify type(const, var), 3:x value or id, 4:y value or id
        private void AddCommandGetterrain(List<BakinParameter> p, List<string> paras)
        {
            if (paras[2] == "0" || paras[1] != "2") //unable to specify absolute position. unable to get terraintag, eventid or regionid.
            {
                AddCommandNoticeComment(p, Cvt_NoConvert, Para_Parallax);
            }
            else
            {
                AddCommandHeader(p, "GET_TERRAIN");
                p.Add(new BakinParameter("整数", Para_AxesToGet, "2")); //0=currentpos 1=eventpos 2=var
                p.Add(new BakinParameter("変数", Para_AxXNum, GetBknVarNameOrVal("1", paras[3]).Item2)); //xpos
                p.Add(new BakinParameter("変数", Para_AxYNum, GetBknVarNameOrVal("1", paras[4]).Item2)); //ypos
                p.Add(new BakinParameter("整数", Para_ParallaxInfo, "0")); //0=land res name 1=height
                p.Add(new BakinParameter("変数", Para_VarNumToParallaxInfo, paras[0])); //to; type":name N=numeric, S=string, A=array
                AddCommandEnd(p);
            }
        }

        //301 Battle Processing: 0:specify type(const, var, rand), 1:troop value or id, 2:can escape flag, 3:can lose flag
        private void AddCommandBossbattle(List<BakinParameter> p, List<string> paras)
        {
            Tuple<string, string> varval = GetBknVarNameOrVal(paras[0], paras[1]);
            AddCommandNoticeComment(p, Cvt_NeedRevise, Para_Troop + "(ID:" + paras[0] != "2" ? varval.Item2 : Para_Rand + "), BGM Guid");
            AddCommandHeader(p, "BOSSBATTLE");
            p.Add(new BakinParameter("整数", Para_NumEnemy, "")); //num of monsters
                                                                //p.Add(new BakinParameter("Guid", "モンスターnのGuid"));
            p.Add(new BakinParameter("整数", Para_NoGameover, paras[3])); //no gameover flag
            p.Add(new BakinParameter("整数", Para_NoEscape, paras[2] == "0" ? "1" : "0")); //no escape flag
            p.Add(new BakinParameter("整数", ""));
            p.Add(new BakinParameter("Guid", "BGM Guid")); //bgm guid
            p.Add(new BakinParameter("Guid", Para_BtlMap + "Guid")); //battle map guid
            p.Add(new BakinParameter("整数", Para_MapCenterSection, "1000")); //1000": map center section
            p.Add(new BakinParameter("整数", Para_MapCenterX, "12")); //battle map centerx
            p.Add(new BakinParameter("整数", Para_MapCenterZ, "12")); //battle map centerz
            p.Add(new BakinParameter("整数", Para_EnemyPosSection, "1001")); //1001": monster pos section, rel to center
                                                                           //p.Add(new BakinParameter("整数", "モンスターn位置X座標ｘ1000")); //monster1 posx*1000
                                                                           //p.Add(new BakinParameter("整数", "モンスターn位置Z座標ｘ1000")); //monster1 posz*1000
            p.Add(new BakinParameter("整数", Para_LevelSection, "1002")); //1002": level section
                                                                        //p.Add(new BakinParameter("整数", "モンスターnレベル")); //monster1 level
            p.Add(new BakinParameter("整数", Para_MemberPosSection, "1005")); //1005": mem pos section, rel to center
            p.Add(new BakinParameter("整数", Para_NumMember, "1")); //num of members
            p.Add(new BakinParameter("整数", Para_NthPosX, "3000")); //mem1 posx*1000
            p.Add(new BakinParameter("整数", Para_NthPosZ, "3000")); //mem1 posz*1000
            p.Add(new BakinParameter("整数", Para_EmergeMessageFlagSection, "1006")); //1006": emerge message flag section
            p.Add(new BakinParameter("整数", Para_NoEmergeMessage, "0")); //no emerge message flag
            AddCommandEnd(p);
        }

        //302 Shop Processing: 0:type(item, weapon, armor), 1:id, 2:price type(defprice, specify), 3:specific price,
        //	4:purchase only flag
        private void AddCommandShop(List<BakinParameter> p, List<string> paras)
        {
            AddCommandNoticeComment(p, Cvt_NeedRevise, Para_ItemList);
            AddCommandHeader(p, "SHOP");
            p.Add(new BakinParameter("整数", Para_NumItem, "1")); //num of items
            p.Add(new BakinParameter("Guid", Para_Item + "Guid(ID:" + paras[1] + ")")); //item guid
            p.Add(new BakinParameter("整数", Para_ItemPrice + "(" + paras[3] + ")")); //item price
            p.Add(new BakinParameter("整数", Para_ChoicePos, "2130706432")); //pos 2130706432-N 0=upleft 1=up 2=upright 3=left 4=center 5=right 6=botleft 7=bottom 8=botright
            AddCommandEnd(p);
            // else and endif
            AddCommandNoparams(p, "ELSE");
            AddCommandNoparams(p, "ENDIF");
        }

        //303 Name Input Processing: 0:Actor ID, 1:max characters
        private void AddCommandChangeheroname(List<BakinParameter> p, List<string> paras)
        {
            AddCommandHeader(p, "CHANGE_HERO_NAME");
            p.Add(new BakinParameter("整数", Para_ObjCast + Para_SpecifyOrN, "0")); //0=specify_cast 1=n-th member
            p.Add(new BakinParameter("Guid", Para_ObjCast + "Guid" + "(ID:" + paras[0] + ")"));    //cast guid
            p.Add(new BakinParameter("整数", Para_NthInParty));   //member number, N-1
            p.Add(new BakinParameter("整数", Para_MaxNumInput, paras[1])); //input chars number": max10
            p.Add(new BakinParameter("整数", Para_WindowPos)); //windowpos 0=up 1=center 2=bottom
            p.Add(new BakinParameter("文字列", Para_DisplayKeys + "1", Para_TypeKeys1)); //input1
            p.Add(new BakinParameter("文字列", Para_DisplayKeys + "2", Para_TypeKeys2));   //input2
            p.Add(new BakinParameter("文字列", Para_DisplayKeys + "3"));
            p.Add(new BakinParameter("文字列", Para_DisplayKeys + "4"));
            AddCommandEnd(p);
        }

        //311 Change HP: 0:specify type(const, var), 1:actor value or id, 2:Operation(+, -), 3:type(const, var), 4:value or id, 5:allow death flag
        //312 Change MP: 0:specify type(const, var), 1:actor value or id, 2:Operation(+, -), 3:type(const, var), 4:value or id
        private void AddCommandChghpmp(int code, List<BakinParameter> p, List<string> paras)
        {
            Tuple<string, string> varval = GetBknVarNameOrVal(paras[0], paras[1]);
            AddCommandNoticeComment(p, Cvt_NeedRevise, Para_ObjCast + "(ID:" + varval.Item2 + ")");
            AddCommandHeader(p, "CHG_HPMP");
            p.Add(new BakinParameter("整数", Para_ObjCast + Para_SpecifyOrN, "1")); //0=specify_cast 1=n-th member
            p.Add(new BakinParameter("Guid", "指定キャストGuid" + "(ID:" + varval.Item2 + ")")); //cast guid
            p.Add(new BakinParameter("整数", Para_NthInParty, "0")); //member number, N-1
            p.Add(new BakinParameter("整数", Para_EffectObj01, code == 311 ? "0" : "1")); //0=hp 1=mp
            varval = GetBknVarNameOrVal(paras[3], paras[4]);
            p.Add(new BakinParameter(varval.Item1, Para_VariableValue, varval.Item2)); //amount
            p.Add(new BakinParameter("整数", Para_Change + Para_IncrDecr01, paras[2])); //0=increase 1=decrease
            AddCommandEnd(p);
        }

        //313 Change State: 0:specify type(const, var), 1:actor value or id, 2:operation(add, remove), 3:state id
        private void AddCommandChgsttailm(List<BakinParameter> p, List<string> paras)
        {
            Tuple<string, string> varval = GetBknVarNameOrVal(paras[0], paras[1]);
            AddCommandNoticeComment(p, Cvt_NeedRevise, Para_ObjCast + "(ID:" + varval.Item2 + ")," + Para_StateChange + "(ID:" + paras[3] + ")");
            AddCommandHeader(p, "CHG_STTAILM");
            p.Add(new BakinParameter("整数", Para_ObjCast + Para_SpecifyOrN, "1")); //0=specify_cast 1=n-th member
            p.Add(new BakinParameter("Guid", "指定キャストGuid")); //cast guid, all if 0
            p.Add(new BakinParameter("整数", Para_NthInParty, "0")); //member number, N-1
            p.Add(new BakinParameter("Guid", Para_StateChange + "Guid")); //state guid
            p.Add(new BakinParameter("整数", Para_StateAddRemove, paras[2])); //0=add 1=remove
            AddCommandEnd(p);
        }

        //314 Recover All: 0:specify type(const, var), 1:actor value or id (-1:all)
        private void AddCommandFullrecov(List<BakinParameter> p, List<string> paras)
        {
            if (paras[1] == "-1")
            {
                AddCommandNoparams(p, "FULLRECOV");
            }
            else
            {
                Tuple<string, string> varval = GetBknVarNameOrVal(paras[0], paras[1]);
                AddCommandNoticeComment(p, Cvt_NeedRevise, Para_ObjCast + "(ID:" + varval.Item2 + ")");
                AddCommandChghpmp(311, p, new List<string> { "0", "1", "0", "0", ToStr(int.MaxValue) });
                AddCommandChghpmp(312, p, new List<string> { "0", "1", "0", "0", ToStr(int.MaxValue) });
            }
        }

        //315 Change Exp: 0:specify type(const, var), 1:actor value or id, 2:Operation(+, -), 3:type(const, var),
        //	4:value or id, 5:show level up flag
        private void AddCommandChgexp(List<BakinParameter> p, List<string> paras)
        {
            Tuple<string, string> varval = GetBknVarNameOrVal(paras[0], paras[1]);
            AddCommandNoticeComment(p, Cvt_NeedRevise, Para_ObjCast + "(ID:" + varval.Item2 + ")");
            AddCommandHeader(p, "CHG_EXP");
            p.Add(new BakinParameter("整数", Para_ObjCast + Para_SpecifyOrN, "0")); //0=specify_cast 1=n-th member
            p.Add(new BakinParameter("Guid", Para_ObjCast + "Guid")); //cast guid, all if 0
            p.Add(new BakinParameter("整数", Para_NthInParty)); //member number, N-1
            varval = GetBknVarNameOrVal(paras[3], paras[4]);
            p.Add(new BakinParameter(varval.Item1, Para_Exp, varval.Item2)); //amount
            p.Add(new BakinParameter("整数", Para_Change + Para_IncrDecr01, paras[2])); //0=increase 1=decrease
            AddCommandEnd(p);
        }

        //317 Change Parameter: 0:specify type(const, var), 1:actor value or id, 2:param ID, 3:Operation(+, -),
        //	4:type(const, var), 5:value or id
        private void AddCommandStatus(List<BakinParameter> p, List<string> paras)
        {
            if (paras[2] == "5" || paras[2] == "7")
            {
                AddCommandNoticeComment(p, Cvt_NoConvert, Para_ChangeMDefLuc);
            }
            else
            {
                AddCommandHeader(p, "STATUS");
                p.Add(new BakinParameter("整数", Para_ObjCast + Para_SpecifyOrN, "0")); //0=specify_cast 1=n-th member
                Tuple<string, string> varval = GetBknVarNameOrVal(paras[0], paras[1]);
                p.Add(new BakinParameter("Guid", Para_ObjCast + "Guid" + "(ID:" + varval.Item2 + ")")); //cast guid
                p.Add(new BakinParameter("整数", Para_NthInParty)); //member number, N-1
                string tmp = paras[2] == "6" ? "5" : paras[2];
                p.Add(new BakinParameter("整数", Para_EffectObj012345, tmp)); //0=maxhp 1=maxmp 2=attack 3=defence 4=magic 5=agility
                varval = GetBknVarNameOrVal(paras[4], paras[5]);
                p.Add(new BakinParameter(varval.Item1, Para_VariableValue, varval.Item2)); //amount
                p.Add(new BakinParameter("整数", Para_ChangeUpDown, paras[3])); //0=increase 1=decrease
                AddCommandEnd(p);
            }
        }

        //318 Change Skill: 0:specify type(const, var), 1:actor value or id, 2:operation(add, remove), 3:skill id
        private void AddCommandChgskill(List<BakinParameter> p, List<string> paras)
        {
            Tuple<string, string> varval = GetBknVarNameOrVal(paras[0], paras[1]);
            AddCommandNoticeComment(p, Cvt_NeedRevise, Para_ObjCast + "(ID:" + varval.Item2 + ")");
            AddCommandHeader(p, "CHG_SKILL");
            p.Add(new BakinParameter("整数", Para_ObjCast + Para_SpecifyOrN, "0")); //0=specify_cast 1=n-th member
            p.Add(new BakinParameter("Guid", Para_ObjCast + "Guid")); //cast guid
            p.Add(new BakinParameter("整数", Para_NthInParty)); //member number, N-1
            p.Add(new BakinParameter("Guid", Para_ChangingSkill + "Guid(ID:" + paras[3] + ")")); //skill guid
            p.Add(new BakinParameter("整数", Para_LearnForget, paras[3])); //0=get 1=forget
            AddCommandEnd(p);
        }

        //319 Change Equipment: 0:Actor ID, 1:equip(weapon, shield, head, body, acces), 2:weapon or armor ID
        private void AddCommandEquip(List<BakinParameter> p, List<string> paras)
        {
            AddCommandNoticeComment(p, Cvt_NeedRevise, Para_ObjCast + "(ID:" + paras[0] + "), " + Para_EquipItem + "(ID:" + paras[2] + ")");
            AddCommandHeader(p, "EQUIP");
            p.Add(new BakinParameter("整数", Para_ObjCast + Para_SpecifyOrN, "0")); //0=specify_cast 1=n-th member
            p.Add(new BakinParameter("Guid", Para_ObjCast + "Guid")); //cast guid
            p.Add(new BakinParameter("整数", Para_NthInParty)); //member number, N-1
            p.Add(new BakinParameter("整数", Para_EquipPart, paras[1])); //part 0=weapon 1=armor 2=head 3=body 4=acces1 5=acces2
            p.Add(new BakinParameter("Guid", Para_EquipItem + "Guid")); //item guid
            AddCommandEnd(p);
        }

        //320 Change Name: 0:Actor ID, 1:name
        private void AddCommandStringvariable(List<BakinParameter> p, List<string> paras)
        {
            AddCommandNoticeComment(p, Cvt_NeedRevise, Para_ChangeCast + "(ID:" + paras[0] + ")");
            AddCommandHeader(p, "STRING_VARIABLE");
            p.Add(new BakinParameter("整数", "", "-1"));
            p.Add(new BakinParameter("文字列", Para_NewCastName, paras[1]));
            p.Add(new BakinParameter("整数", Para_Input, "0")); //0=overwrite 1=addfirst 2=addlast
            p.Add(new BakinParameter("Guid", Para_ChangeCast + "Guid")); //cast guid
            AddCommandEnd(p);
        }

        //321 Change Class: 0:Actor ID, 1:class ID, 2:save EXP flag
        private void AddCommandChangejob(List<BakinParameter> p, List<string> paras)
        {
            AddCommandNoticeComment(p, Cvt_NeedRevise, Para_ObjCast + "(ID:" + paras[0] + "), " + Para_Job + "(ID:" + paras[1] + ")");
            AddCommandHeader(p, "CHANGE_JOB");
            p.Add(new BakinParameter("整数", Para_ObjCast + Para_SpecifyOrN, "0")); //0=specify_cast 1=n-th member
            p.Add(new BakinParameter("Guid", Para_ObjCast + "Guid")); //cast guid
            p.Add(new BakinParameter("整数", Para_NthInParty)); //member number, N-1
            p.Add(new BakinParameter("Guid", Para_Job + "Guid")); //job guid
            p.Add(new BakinParameter("整数", Para_ChangeObjJob, "0")); //object 0=job 1=subjob
            p.Add(new BakinParameter("整数", Para_InheritGrownStatus, paras[2])); //inherit status flag
            p.Add(new BakinParameter("整数", Para_SubjobToJob)); //change subjob to job
            AddCommandEnd(p);
        }

        //322 Change Actor Images: 0:Actor ID, 1:walking graphic filename, 2:walking graphic index, 3:face filename,
        //	4:face index, 5:battler filename
        private void AddCommandPlgraphic(List<BakinParameter> p, List<string> paras)
        {
            AddCommandNoticeComment(p, Cvt_NeedRevise, Para_ObjCast + "(ID:" + paras[0] + "), " +
                Para_OnMapGraphic + "(" + paras[1] + ":" + paras[2] + ")、" +
                Para_LayoutGraphic + paras[3] + ":" + paras[4] + ")");
            AddCommandHeader(p, "PLGRAPHIC");
            p.Add(new BakinParameter("整数", Para_ObjCast + Para_SpecifyOrN, "0")); //0=specify_cast 1=n-th member
            p.Add(new BakinParameter("Guid", Para_ObjCast + "Guid")); //cast guid
            p.Add(new BakinParameter("整数", Para_NthInParty));   //member number, N-1
            p.Add(new BakinParameter("Guid", Para_OnMapGraphic + "Guid"));  //mapchip guid
            p.Add(new BakinParameter("Guid", Para_LayoutGraphic + "Guid")); //layout_gra guid
            p.Add(new BakinParameter("文字列", Para_MotionName)); //motion name?
            p.Add(new BakinParameter("整数", Para_WaitMotion)); //wait until motion_complete
            AddCommandEnd(p);
        }

        //331 Change Enemy HP: 0:Enemy ID, 1:Operation(+,-), 2:type(const, var), 3:value or id, 4:allow death flag
        //332 Change Enemy MP: 0:Enemy ID, 1:Operation(+,-), 2:type(const, var), 3:value or id
        private void AddCommandBtlheal(int code, List<BakinParameter> p, List<string> paras)
        {
            Tuple<string, string> varval = GetBknVarNameOrVal(paras[2], paras[3]);
            AddCommandNoticeComment(p, Cvt_NeedRevise, Para_ObjMonster + "(ID:" + varval.Item2 + ")");
            AddCommandHeader(p, "BTL_HEAL");
            p.Add(new BakinParameter("Guid", Para_ObjMonster + "Guid")); //cast guid
            p.Add(new BakinParameter("整数", Para_EffectObj01, code == 331 ? "0" : "1")); //0=HP 1=MP
            p.Add(new BakinParameter(varval.Item1, Para_VariableValue, varval.Item2)); //amount
            p.Add(new BakinParameter("整数", Para_ChangeUpDown, paras[1] == "1" ? "2" : "0")); //0=increase 2=decrease
            p.Add(new BakinParameter("整数", "", "0")); //0=specify_cast 1=n-th member
            p.Add(new BakinParameter("整数", "", "0")); //0=specify_cast 1=n-th member
            p.Add(new BakinParameter("整数", Para_ShowDamage, "1")); //display damage number
            AddCommandEnd(p);
        }

        //333 Change Enemy State: 0:Enemy ID, 1:operation(add, remove), 2:state id
        private void AddCommandBtlstatus(List<BakinParameter> p, List<string> paras)
        {
            AddCommandNoticeComment(p, Cvt_NeedRevise, Para_ObjMonster + "(ID:" + paras[0] + "), " + Para_State + "(ID:" + paras[2] + ")");
            AddCommandHeader(p, "BTL_STATUS");
            p.Add(new BakinParameter("整数", Para_BtlStateObj, "2")); //1=party 2=troop
            p.Add(new BakinParameter("整数", Para_ObjMonster, paras[0])); //cast guid
            p.Add(new BakinParameter("Guid", Para_State + "Guid")); //state guid
            p.Add(new BakinParameter("整数", Para_StateAddRemove, paras[1])); //0=add 2=remove
            AddCommandEnd(p);
        }

        //335 Enemy Appear: 0:Enemy index
        private void AddCommandBtlappear(List<BakinParameter> p, List<string> paras)
        {
            AddCommandNoticeComment(p, Cvt_NeedRevise, Para_ObjMonster + "(ID:" + paras[0] + "), " + Para_InGroupNum);
            AddCommandHeader(p, "BTL_APPEAR");
            p.Add(new BakinParameter("Guid", Para_ObjMonster + "Guid")); //cast guid
            p.Add(new BakinParameter("整数", Para_InGroupNum, "1")); //n-th monster
            AddCommandEnd(p);
        }

        //339 Force Action: 0:battler(enemy, actor), 1:enemy or actor id, 2:skill id, 3:target id(-2:last, -1:rand, 0-:cast)
        private void AddCommandBtlaction(List<BakinParameter> p, List<string> paras)
        {
            string tmp = paras[0] == "0" ? Para_ObjMonster : Para_ObjCast;
            AddCommandNoticeComment(p, Cvt_NeedRevise, tmp + "(ID:" + paras[1] + "), " + Para_Action + "(ID:" + paras[2] + ")");
            AddCommandHeader(p, "BTL_ACTION");
            p.Add(new BakinParameter("整数", ""));
            p.Add(new BakinParameter("Guid", Para_ObjCast + "Guid")); //cast guid
            p.Add(new BakinParameter("整数", "0")); //action type
            p.Add(new BakinParameter("Guid", Para_Target + "Guid")); //cast guid
            AddCommandEnd(p);
        }

        //debug		//351 Open Menu Screen??????
        private void AddCommandShowscoreboard(List<BakinParameter> p, List<string> paras)
        {
            AddCommandHeader(p, "SHOW_SCORE_BOARD");
            p.Add(new BakinParameter("整数", Para_DisplayFlag, "1")); //display flag
            p.Add(new BakinParameter("Guid", Para_UseLayout + "Guid"));
            AddCommandEnd(p);
        }

        //124 Control Timer 0:Operation(start, stop) 1:time in seconds
        private void AddCommandTimer(List<BakinParameter> p, List<string> paras)
        {
            AddCommandTemporaryVariable(p, "TimerTrigger", paras[0], 0);
            if (paras[0] == "0")
            {
                AddCommandTemporaryVariable(p, "TimerInfo", paras[1], 0);
            }
            else
            {
                AddCommandTemporaryVariable(p, "TimerInfo", "0", 0);
            }
        }

        private void AddCommandNoparams(List<BakinParameter> p, string command)
        {
            AddCommandHeader(p, command);
            AddCommandEnd(p);
        }

        //205 
        //setting move route parameter values to global variables.
        private void AddCommandMoveRoute(List<BakinParameter> p, List<string> paras)
        {
            if (ToInt(paras[0]) >= 1)
            {
                paras[0] = "0";
                AddCommandNoticeComment(p, Cvt_NeedRevise, Para_ObjEvent + "(ID:" + paras[0] + "), " + Para_ThisEventMove);
            }
            _moveChar = paras[0];
            //list data are the same as follwoing move route command.
            paras[1] = Regex.Replace(paras[1], ".*\\\"repeat", "{\"list\":\"\",\"repeat");
            MvEventMoveRouteHeader route = JsonSerializer.Deserialize<MvEventMoveRouteHeader>(paras[1]);
            _isStopStuck = route.skippable ? "1" : "0";

            if (!route.wait)
            {
                AddCommandNoticeComment(p, Cvt_NeedRevise, Para_WaitProcess);
            }
            _isWaitMoving = route.wait ? "1" : "0";
        }

        //1-13 1:down, 2:left, 3:right, 4:up, 5:dl, 6:dr, 7:ul, 8:ur, 9:random, 10:toward, 11:away, 12:forward, 13:backward
        private void AddCommandWalk(List<BakinParameter> p, int code, string steps)
        {
            if (code == 13)
            {
                AddCommandNoticeComment(p, Cvt_NeedRevise, Para_StepBack);
            }
            AddCommandHeader(p, _moveChar == "-1" ? "PLWALK" : "WALK");
            //0=up 1=down 2=left 3=right 4=rand 5=face 6=away 8=toward 10=deg
            List<string> dir = new List<string> { "", "1", "2", "3", "0", "10", "10", "10", "10", "4", "5", "6", "8", "6" };
            p.Add(new BakinParameter("整数", Para_Dir0to10, dir[code]));
            p.Add(new BakinParameter("小数", Para_NumSteps, steps)); //step size
            p.Add(new BakinParameter("変数", ""));
            p.Add(new BakinParameter("整数", Para_FixDir, "0")); //fix_direction flag
            p.Add(new BakinParameter("整数", Para_StopStuck, _isStopStuck)); //stop if stuck flag
            p.Add(new BakinParameter("整数", Para_ThroughEvent, "0")); //through flag
            p.Add(new BakinParameter("整数", Para_StepOver, "0")); //step over flag
            p.Add(new BakinParameter("整数", Para_NoMotionChange, "0")); //no motion change flag
            var tmp = code == 5 ? "315" : code == 6 ? "45" : code == 7 ? "225" : code == 8 ? "135" : "0";
            p.Add(new BakinParameter("整数", Para_Angle, tmp)); //degree
            p.Add(new BakinParameter("整数", ""));
            p.Add(new BakinParameter("整数", Para_SmoothTurn, "1")); //smooth rotate flag
            tmp = (code >= 5 && code <= 8) ? "0" : "1";
            p.Add(new BakinParameter("整数", Para_Fix4Dir, tmp)); //round-to-4direction flag
            AddCommandEnd(p);
        }

        private void AddCommandWalktgt(List<BakinParameter> p, List<string> paras)
        {
            AddCommandNoticeComment(p, Cvt_NeedRevise, Para_MoveRouteTime);
            //first get position X & Z
            AddCommandTemporaryGamedataVariable(p, "Tmp1", 0, _moveChar == "-1" ? 17 : 14); //xpos
            AddCommandTemporaryVariable(p, "Tmp1", ToInt(paras[0]), 1);
            AddCommandTemporaryGamedataVariable(p, "Tmp2", 0, _moveChar == "-1" ? 18 : 15); //ypos
            AddCommandTemporaryVariable(p, "Tmp2", ToInt(paras[1]), 1);

            AddCommandHeader(p, _moveChar == "-1" ? "PLWALK_TGT" : "EVWALK_TGT");
            p.Add(new BakinParameter("スポット", Para_Spot, GetSpot("0", "Tmp1", "Tmp2"))); //map_guid|spot_id_from_1001|xpos|ypos|zpos
            p.Add(new BakinParameter("整数", Para_DirAtMoveEnd, "1")); //0=deg 1=toward
            p.Add(new BakinParameter("小数", Para_Angle)); //degree
            p.Add(new BakinParameter("小数", Para_TimeToRot, ToStr((float)0.5 * (Math.Abs(ToFlo(paras[0])) + Math.Abs(ToFlo(paras[1])))))); //time in sec
            p.Add(new BakinParameter("整数", Para_InterP, "0")); //interp 0=const 1=accel 2=decel 3=accel-decel
            p.Add(new BakinParameter("整数", Para_WaitComplete, _isWaitMoving)); //wait complete
            p.Add(new BakinParameter("整数", Para_NoMotionChange, "0")); //no motion change
            p.Add(new BakinParameter("整数", Para_CurveInterp, "0")); //curve interp
            AddCommandEnd(p);
        }

        private void AddCommandAddforce(List<BakinParameter> p, List<string> paras)
        {
            AddCommandNoticeComment(p, Cvt_NeedRevise, Para_JumpDist + "(X:" + paras[0] + ", Z:" + paras[1] + ")");
            AddCommandHeader(p, _moveChar == "-1" ? "ADDFORCEPL" : "ADDFORCE");
            p.Add(new BakinParameter("整数", Para_PhysX, "0")); //xforce
            p.Add(new BakinParameter("整数", Para_PhysY, "300")); //yforce
            p.Add(new BakinParameter("整数", Para_PhysZ, "0")); //zforce
            AddCommandEnd(p);
        }

        //16=down, 17=left, 18=right, 19=up, 20=r90, 21=l90, 22=180, 23=lr90 24=rand, 25=toward, 26=away
        private void AddCommandRotate(List<BakinParameter> p, int code)
        {
            //0=up 1=down 2=left 3=right 4=rand 5=face 6=away 7=rotate 8=deg
            List<string> dir = new List<string> { "", "1", "2", "3", "0", "8", "8", "8", "8", "4", "5", "6" };
            List<string> dir2 = new List<string> { "", "D", "L", "R", "U", "R90", "L90", "180", "L/R90", "Rand", "Toward", "Away" };
            AddCommandNoticeComment(p, Cvt_NeedRevise, Para_RotDir + "(" + dir2[code - 15] + ")");
            AddCommandHeader(p, _moveChar == "-1" ? "PLROTATE" : "ROTATE");
            p.Add(new BakinParameter("整数", Para_Dir0to8, dir[code - 15]));
            p.Add(new BakinParameter("整数", Para_Angle)); //degree
            AddCommandEnd(p);
        }

        private void AddCommandWalkspeed(List<BakinParameter> p, List<string> paras)
        {
            AddCommandHeader(p, _moveChar == "-1" ? "PLWALKSPEED" : "WALKSPEED");
            p.Add(new BakinParameter("整数", Para_MoveSpeed, ToStr(ToInt(paras[0]) - 4))); //speed -3 to 3
            AddCommandEnd(p);
        }

        private void AddCommandGraphic(List<BakinParameter> p, List<string> paras)
        {
            if(paras.Count > 0)
            {
                AddCommandNoticeComment(p, Cvt_NeedRevise, Para_Graphic + "(" + paras[0] + ")");
            }
            else
            {
                AddCommandNoticeComment(p, Cvt_NeedRevise, Para_Graphic + "(???)");
            }
            AddCommandHeader(p, "GRAPHIC");
            p.Add(new BakinParameter("Guid", Para_ObjCast + "Guid"));  //mapchip guid
            p.Add(new BakinParameter("文字列", Para_MotionGraphicName));
            AddCommandEnd(p);
        }

        private void AddCommandPlmotion(List<BakinParameter> p, List<string> paras)
        {
            AddCommandHeader(p, "PLMOTION");
            p.Add(new BakinParameter("整数", ""));
            p.Add(new BakinParameter("Guid", ""));  //cast guid?
            p.Add(new BakinParameter("整数", ""));
            p.Add(new BakinParameter("Guid", Para_OnMapGraphic + "Guid"));  //mapchip_guid
            p.Add(new BakinParameter("文字列", Para_MotionGraphicName));   //pic name
            p.Add(new BakinParameter("整数", Para_WaitMotion)); //wait motion complete
            AddCommandEnd(p);
        }

        private void AddCommandMotion(List<BakinParameter> p, List<string> paras)
        {
            AddCommandHeader(p, "MOTION");
            p.Add(new BakinParameter("Guid", Para_ObjCast + "Guid"));  //mapchip guid
            p.Add(new BakinParameter("文字列", Para_MotionGraphicName));   //pic name
            AddCommandEnd(p);
        }

        private void AddCommandSwpllockrotate(List<BakinParameter> p, List<string> paras)
        {
            AddCommandHeader(p, "SW_PLLOCKROTATE");
            p.Add(new BakinParameter("整数", Para_NoPlayerTurn, paras[0]));  //fix direction flag
            AddCommandEnd(p);
        }

        //through
        private void AddCommandChangemovable(List<BakinParameter> p, List<string> paras)
        {
            AddCommandHeader(p, _moveChar == "-1" ? "CHANGE_PLAYER_MOVABLE" : "CHANGE_MOVABLE");
            p.Add(new BakinParameter("整数", Para_Through, paras[0])); //through flag
            AddCommandEnd(p);
        }

        private void AddCommandHide(List<BakinParameter> p, List<string> paras)
        {
            AddCommandHeader(p, _moveChar == "-1" ? "PLHIDE" : "EVHIDE");
            p.Add(new BakinParameter("整数", Para_Trans, paras[0]));  //transparent flag
            AddCommandEnd(p);
        }

        private void AddCommandPlayse(List<BakinParameter> p, List<string> paras)
        {
            AddCommandHeader(p, "PLAYSE");
            p.Add(new BakinParameter("Guid", "BGS Guid"));  //se guid
            p.Add(new BakinParameter("整数", Para_Vol));   //vol
            p.Add(new BakinParameter("整数", Para_Tempo)); //tempo
            p.Add(new BakinParameter("整数", Para_3dSound)); //3d sound flag
            AddCommandEnd(p);
        }

        private void AddCommandComment(List<BakinParameter> p, List<string> paras)
        {
            AddCommandHeader(p, "COMMENT");
            p.Add(new BakinParameter("文字列", Para_Text, paras.Last()));
            AddCommandEnd(p);
        }

        private void AddCommandNoticeComment(List<BakinParameter> p, string pre, string text = "")
        {
            if (_isAddReviceComment || pre == Cvt_NoConvert)
            {
                AddCommandHeader(p, "COMMENT");
                p.Add(new BakinParameter("文字列", Para_Text, pre + text));
                AddCommandEnd(p);
            }
        }

        private void AddCommandNoticeCommentWithContent(List<BakinParameter> p, string pre, string text, List<string> paras)
        {
            AddCommandHeader(p, "COMMENT");
            p.Add(new BakinParameter("文字列", Para_Text, pre + text + "\\n" + paras.Last()));
            AddCommandEnd(p);
        }

        private void AddCommandHeader(List<BakinParameter> p, string command)
        {
            p.Add(new BakinParameter(Para_Command, command));
        }

        private void AddCommandEnd(List<BakinParameter> p)
        {
            p.Add(new BakinParameter(Para_CommandEnd, ""));
        }


        //Use temporary variable within Bakin event sheet. The Name could be "Tmp1", "Tmp2" etc.
        //Operand: 0=set, 1=add, 2=sub, 3=mult, 4=div, 5=addrand
        private void AddCommandTemporaryVariable(List<BakinParameter> p, string name, int value, int operand)
        {
            AddCommandHeader(p, "VARIABLE");
            p.Add(new BakinParameter("整数", ""));
            p.Add(new BakinParameter("変数", Para_VarNum, "N:" + name)); //type":name N=numeric
            p.Add(new BakinParameter("整数", ""));
            p.Add(new BakinParameter("整数", Para_Value, value.ToString())); //value
            p.Add(new BakinParameter("整数", Para_Calc, operand.ToString()));
            AddCommandEnd(p);
        }
        private void AddCommandTemporaryVariable(List<BakinParameter> p, string name, string var, int operand)
        {
            AddCommandHeader(p, "VARIABLE");
            p.Add(new BakinParameter("整数", ""));
            p.Add(new BakinParameter("変数", Para_VarNum, "N:" + name)); //type":name N=numeric
            p.Add(new BakinParameter("整数", ""));
            p.Add(new BakinParameter("変数", Para_Value, "N:" + var)); //value
            p.Add(new BakinParameter("整数", Para_Calc, operand.ToString()));
            AddCommandEnd(p);
        }

        private void AddCommandTemporaryGamedataVariable(List<BakinParameter> p, string varname, int op, int id)
        {
            //player:(mapx, mapy, dir, screenx, screeny, posx, posy) = [ 17, 18, 21, 29, 30, 17, 18 ];
            //event:(mapx, mapy, dir, screenx, screeny, posx, posy) = [ 14, 15, 20, 27, 28, 14, 15 ];
            AddCommandHeader(p, "HLVARIABLE");
            p.Add(new BakinParameter("整数", ""));
            p.Add(new BakinParameter("変数", Para_VarNum, "N:" + varname)); //type":name N=numeric
            //if (type < 0)
            //{
            //    p.Add(new BakinParameter("整数", ""));
            //    p.Add(new BakinParameter("整数", Para_InfoType, ToStr(id)));
            //}
            //else //game data
            //{
            //nummember = 31, gold = 4, playtime(hour) = 8
            p.Add(new BakinParameter("整数", Para_DataType, ToStr(id)));
            //}
            p.Add(new BakinParameter("整数", Para_Calc0to7, ToStr(op))); //0=overwrite 1=add 2=sub 3=mult 4=div 6=mod 7=floor
            AddCommandEnd(p);
        }

        private void AddCommandClose(List<BakinParameter> p, List<string> paras)
        {
            if (_indentType.Count == 0)
            {
                return; //last of a page.
            }
            if (_indentType.Last() == "IF")
            {
                AddCommandNoparams(p, "ENDIF");
            }
            else if (_indentType.Last() == "CHOICE")
            {
                AddCommandNoparams(p, "ENDIF");
                _numChoices = 0;
            }
            else if (_indentType.Last() == "BRANCH")
            {
                //ignore
            }
            else if (_indentType.Last() == "IGNORE")
            {
                //close tag for non-converted command.
                AddCommandNoticeComment(p, Cvt_NoConvert, Para_NoConvChoiceEnd);
            }
            _indentType.RemoveAt(_indentType.Count - 1);
        }

        #endregion

        #region initialize
        public Json2BakinConvertService(MvMapDataLoadService loadService)
        {
            _loadService = loadService;
        }
        #endregion

        #region Helpers
        private string GetSpot(string type, string x, string z)
        {
            Tuple<string, string> valvar = GetBknVarNameOrVal(type, x);
            Tuple<string, string> valvar2 = GetBknVarNameOrVal(type, z);
            return Guid.Empty.ToString() + "|-1|" + valvar.Item2 + "|0|" + valvar2.Item2;
        }

        private string OpacToColor(string opac)
        {
            string hex = int.Parse(opac).ToString("X2");
            return Convert.ToInt32("0x" + hex + hex + hex + hex, 16).ToString();
        }

        private string Frame2Time(string val)
        {
            return (float.Parse(val) / 60.0).ToString();
        }

        private int ToInt(string val)
        {
            return int.Parse(val);
        }

        private float ToFlo(string val)
        {
            return float.Parse(val);
        }

        private string ToStr(int val)
        {
            return val.ToString();
        }

        private string ToStr(float val)
        {
            return val.ToString();
        }

        private Tuple<string, string> GetBknVarNameOrVal(string type, string val)
        {
            string label = type == "0" ? "整数" : "変数";
            if (type == "0")
            {
                label = val.Contains(".") ? "小数" : "整数";
                return new Tuple<string, string>(label, val);
            }
            else if (val == "A" || val == "B" || val == "C" || val == "D")
            {
                return new Tuple<string, string>("ローカル変数", val);
            }
            else if (!Regex.IsMatch(val, @"^\d+$")) //non number: variable name
            {
                return new Tuple<string, string>("変数", "N:" + val);
            }
            else
            {
                return new Tuple<string, string>("変数", GetBknVarName(val, "N"));
            }
        }

        private string GetBknVarName(string key, string type)
        {
            //type: N for numeric, B for bool(switch).

            if (key == "A" || key == "B" || key == "C" || key == "D")
            {
                return key;
            }
            else
            {
                MvDatabase varData = _loadService.GetDatabase();
                return "N:" + (type == "B" ? "[S" : "[V") + int.Parse(key).ToString("D3") + "]" + //"variable" or "switch"
                    (type == "B" ? varData.Switches[int.Parse(key)] : varData.Variables[int.Parse(key)]);
            }
        }
        #endregion
    }
}