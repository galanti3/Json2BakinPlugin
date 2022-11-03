using Json2BakinPlugin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text.Json;

namespace Json2BakinPlugin.Services
{
	public class Json2BakinConvertService
	{
#region Methods
		public BakinCode ConvertToBakinCode(MvCode code)
		{
			BakinCode bakin = new BakinCode();
			bakin.Code = code.BakinCode;
			List<string> paras = code.Params;
			List<BakinParameter> p = new List<BakinParameter>();
			string command = code.BakinCode != null ? code.BakinCode.Split('\t')[0] : null;
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
					break;
				case "BRANCH": //402, 403
					AddCommandBranch(p, code.Params);
					break;
				case "CHANGE_STRING_VARIABLE": //103
					break;
				case "ITEMMENU": //104
					AddCommandItemmenu(p, code.Params);
					break;
				case "TELOP": //105
					AddCommandTelop(p, code.Params);
					break;
				case "IFSWITCH": //111(00)
					AddCommandIfswitch(p, code.Params);
					break;
				case "IFVARIABLE": //111(01-02)
					AddCommandIfvariable(p, code.Params);
					break;
				//timer???
				case "IFPARTY": //111(04)
					AddCommandIfparty(p, code.Params);
					break;
				case "IF_STRING_VARIABLE": //111(05)
					AddCommandIfstringvariable(p, code.Params);
					break;
				case "IFITEM": //111(07, 11, 12, 13)
					AddCommandIfitem(p, code.Params);
					break;
//debug. BTL_IFMONSTER //111(08)
				case "IFMONEY": //111(10)
					AddCommandIfmoney(p, code.Params);
					break;
				case "ELSE": //411, 602, 603
					AddCommandNoparams(p, "ELSE");
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
				case "VARIABLE": //122(00) : using Tmp variable for random value calc.
					AddCommandVariable(p, code.Params);
					break;
				case "HLVARIABLE": //122(01-07)
					AddCommandHlvariable(p, code.Params);
					break;
				case "MONEY": //125
					AddCommandMoney(p, code.Params);
					break;
				case "ITEM": //126, 127, 128
					AddCommandItem(p, code.Params);
					break;
				case "PARTY": //129(0)
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
				case "BTL_HEAL": //331, 332 ???
					AddCommandBtlheal(p, code.Params);
					break;
				case "BTL_STATUS": //333 ???
					AddCommandBtlstatus(p, code.Params);
					break;
				case "BTL_APPEAR": //335 ???
					AddCommandBtlappear(p, code.Params);
					break;
				case "BTL_ACTION": //339 ???
					AddCommandBtlaction(p, code.Params);
					break;
				case "BTL_STOP": //340
					AddCommandBtlstop(p, code.Params);
					break;
				case "SHOW_SCORE_BOARD": //351 unusable??
					AddCommandShowscoreboard(p, code.Params);
					break;
				case "SAVE": //352
					AddCommandNoparams(p, "SAVE");
					break;
				case "PLWALK": //1-13
					AddCommandPlwalk(p, code.Params);
					break;
				case "WALK": //1-13
					AddCommandWalk(p, code.Params);
					break;
				case "PLWALK_TGT": //converted from consecutive player move commands
					AddCommandPlwalktgt(p, code.Params);
					break;
				case "EVWALK_TGT": //converted from consecutive event move commands
					AddCommandEvwalktgt(p, code.Params);
					break;
				case "ADDFORCEPL": //14
					AddCommandAddforcepl(p, code.Params);
					break;
				case "ADDFORCE": //14
					AddCommandAddforce(p, code.Params);
					break;
				case "PLROTATE": //16-26
					AddCommandPlrotate(p, code.Params);
					break;
				case "ROTATE": //16-26
					AddCommandRotate(p, code.Params);
					break;
				case "PLWALKSPEED": //29
					AddCommandPlwalkspeed(p, code.Params);
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
					AddCommandSwpllockrotate(p, code.Params);
					break;
				case "CHANGE_PLAYER_MOVABLE": //37, 38
					AddCommandChangeplayermovable(p, code.Params);
					break;
				case "CHANGE_MOVABLE": //37, 38
					AddCommandChangemovable(p, code.Params);
					break;
				case "PLHIDE": //39, 40
					AddCommandPlhide(p, code.Params);
					break;
				case "EVHIDE": //39, 40, 211
					AddCommandEvhide(p, code.Params);
					break;
				case "PLAYSE": //44, 250
					AddCommandPlayse(p, code.Params);
					break;
				case "COMMENT":
					AddCommandComment(p, code.Params);
					break;

					#region Follwing commands are not used in MV.
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
			bakin.Params = p;
			return bakin;
		}

		public void ConvertRouteCodesToDestinationCode(MvEventPage page)
		{
			List<MvCode> codes = new List<MvCode>();
			int i = 0;
			while (i < page.list.Count)
			{
				if (page.list[i].code == 205) //set move route
				{
					int target = int.Parse(page.list[i].Params[0]);
					codes.Add(page.list[i]);
					i++;
					int j = 0;
					int x = 0, y = 0;
					while (page.list[i + j].code >= 1 && page.list[i + j].code <= 8)
					{
						switch (page.list[i + j].code)
						{
							case 2:
							case 5:
							case 7:
								x++;
								break;
							case 3:
							case 6:
							case 8:
								x--;
								break;
						}
						switch (page.list[i + j].code)
						{
							case 1:
							case 5:
							case 6:
								y++;
								break;
							case 4:
							case 7:
							case 8:
								y--;
								break;
						}
						j++;
						if (j > 1) //if route only 1 step, not converted to destination
						{
							codes.Add(page.list[i]);
							codes.Last().BakinCode = target == -1 ? "PLWALK_TGT" : "EVWALK_TGT";
							codes.Last().Params = new List<string> { x.ToString(), y.ToString() };
							i += j;
						}
					}
				}
				else
				{
					codes.Add(page.list[i]);
					i++;
				}
			}
			page.list = codes;
		}
        #endregion

        #region Privates
        //Adding command parameters
        private void AddCommandDialogue(List<BakinParameter> p, List<string> paras)
		{
			AddCommandHeader(p, "DIALOGUE");
			p.Add(new BakinParameter("文字列", "表示するテキスト", paras.Last()));   //text
			p.Add(new BakinParameter("整数", "ウィンドウ表示位置", paras[3]));  //window pos 0=up 1=middle 2=buttom
			//p.Add(new BakinParameter("整数", "吹き出し対象（4096：プレイヤー、4097：このイベント）"));  //bubble focus 4096=player 4097=this &
			//p.Add(new BakinParameter("Guid", "吹き出し対象Guid"));    //bubble focus event]
			p.Add(new BakinParameter("整数", ""));
			p.Add(new BakinParameter("Guid", "表示するキャスト1Guid")); //cast1 sprite guid
			p.Add(new BakinParameter("文字列", "表示するキャスト1表情"));    //cast1 face expression
			p.Add(new BakinParameter("Guid", "表示するキャスト2Guid")); //cast2 sprite guid
			p.Add(new BakinParameter("文字列", "表示するキャスト2表情"));    //cast2 face expression
			p.Add(new BakinParameter("整数", "喋らせるキャスト（0：キャスト1、1：キャスト2）"));   //who's talking [0,1]
			p.Add(new BakinParameter("整数", "キャスト1左右反転"));   //cast1 flip
			p.Add(new BakinParameter("整数", "キャスト2左右反転", "1"));   //cast2 flip
			p.Add(new BakinParameter("整数", "マップの光源を使用する", "1")); //use map light source
			p.Add(new BakinParameter("整数", "キャスト1ビルボード"));  //cast1 bilboard
			p.Add(new BakinParameter("整数", "キャスト2ビルボード"));  //cast2 bilboard
			AddCommandEnd(p);
		}

		private void AddCommandMessage(List<BakinParameter> p, List<string> paras)
		{
			AddCommandHeader(p, "MESSAGE");
			p.Add(new BakinParameter("文字列", "表示するテキスト", paras.Last()));   //text
			p.Add(new BakinParameter("整数", "ウィンドウ表示位置", paras[3]));  //window pos 0=up 1=middle 2=buttom
			//p.Add(new BakinParameter("整数", "吹き出し対象（4096：プレイヤー、4097：このイベント）"));  //bubble focus 4096=player 4097=this &
			//p.Add(new BakinParameter("Guid", "吹き出し対象Guid"));    //bubble focus event]
			string tmp = paras[2] != "2" ? "1" : "0";
			p.Add(new BakinParameter("整数", "ウィンドウを表示", tmp));    //show window flag
			AddCommandEnd(p);
		}

		private void AddCommandChoices(List<BakinParameter> p, List<string> paras)
        {
			AddCommandHeader(p, "CHOICES");
			List<string> labels = JsonSerializer.Deserialize<List<string>>(paras[0]);
			p.Add(new BakinParameter("整数", "選択肢の数", labels.Count.ToString()));   //num of choices
			foreach (string label in labels)
			{
				p.Add(new BakinParameter("文字列", "選択肢のラベル", label)); //choice label
			}
			int mvpos = int.Parse(paras[3]);
			string pos = mvpos == 0 ? "3" : mvpos == 1 ? "4" : "5";
			p.Add(new BakinParameter("整数", "選択肢の位置（0：左上、1：上、2：右上、3：左、4：中央、5：右、6：左下、7：下、8：右下）", pos));    //pos 0=upleft 1=up 2=upright 3=left 4=center 5=right 6=botleft 7=bottom 8=botright
			AddCommandEnd(p);
		}
		private void AddCommandBranch(List<BakinParameter> p, List<string> paras)
		{
			AddCommandHeader(p, "BRANCH");
			p.Add(new BakinParameter("整数", "選択肢n番号（n-1）")); //choice N-1
			AddCommandEnd(p);
		}

		private void AddCommandChangestringvariable(List<BakinParameter> p, List<string> paras)
		{
			AddCommandHeader(p, "CHANGESTRINGVARIABLE");
			p.Add(new BakinParameter("変数", "文字列変数の番号", GetBknVarName(paras[0], "N"))); //type":name N=numeric, S=string, A=array
			p.Add(new BakinParameter("整数", "入力可能な最大文字数（変数可）", paras[1])); //maxchar
			p.Add(new BakinParameter("整数", "ウィンドウ表示位置（0：上、1：中央、2：下）")); //pos 0=top 1=center 2=bottom
			p.Add(new BakinParameter("文字列", "入力画面に表示する文字1", "0123456789"));   //input1
			p.Add(new BakinParameter("文字列", "入力画面に表示する文字2"));   //input2
			p.Add(new BakinParameter("文字列", "入力画面に表示する文字3"));
			p.Add(new BakinParameter("文字列", "入力画面に表示する文字4"));
			AddCommandEnd(p);
		}

		private void AddCommandItemmenu(List<BakinParameter> p, List<string> paras)
		{
			AddCommandHeader(p, "ITEMMENU");
			p.Add(new BakinParameter("Guid", "使用するレイアウトGuid")); //layout guid
			AddCommandEnd(p);
		}

		private void AddCommandTelop(List<BakinParameter> p, List<string> paras)
		{
			AddCommandHeader(p, "TELOP");
			p.Add(new BakinParameter("文字列", "表示するテキスト", paras.Last()));   //text
			p.Add(new BakinParameter("整数", "背景（0：黒、1：画像、2：なし）", "2"));   //background 0=black 1=picture 2=none
			if (p.Last().Value == "1")
			{
				p.Add(new BakinParameter("Guid", "画像Guid"));   //if bg=picture, sprite guid
			}
			p.Add(new BakinParameter("整数", "テロップをスクロールさせる", "1"));   //scroll
			AddCommandEnd(p);
		}
		private void AddCommandIfswitch(List<BakinParameter> p, List<string> paras)
		{
			AddCommandHeader(p, "IFSWITCH");
			p.Add(new BakinParameter("変数", "イベントスイッチ名", GetBknVarName(paras[1], "B")));  //type":name N=numeric, S=string, A=array
			string mvsw = paras[2] == "on" ? "0" : "1";
			p.Add(new BakinParameter("整数", "条件（0：オン、1：オフ）", mvsw));   //0=on 1=off
			AddCommandEnd(p);
		}
		private void AddCommandIfvariable(List<BakinParameter> p, List<string> paras)
		{
			AddCommandHeader(p, "IFVARIABLE");
			p.Add(new BakinParameter("変数", "変数名", GetBknVarName(paras[1], "N"))); //type":name N=numeric, S=string, A=array
			Tuple<string, string> varval = GetBknVarNameOrVal(paras[2], paras[3]);
			p.Add(new BakinParameter(varval.Item1, "数値（変数可）", varval.Item2));   //type":name N=numeric, S=string, A=array
			p.Add(new BakinParameter("整数", "比較条件（0：=、1：>=、2：<=、3：!=、4：>、5：<）"));    //0== 1=>= 2=<= 3=!= 4=> 5=<
			AddCommandEnd(p);
		}
		private void AddCommandIfstringvariable(List<BakinParameter> p, List<string> paras)
		{
			AddCommandHeader(p, "IFSTRINGVARIABLE");
			p.Add(new BakinParameter("整数", "文字列変数名", "-1")); //type":name N=numeric, S=string, A=array, -1=actor name
			p.Add(new BakinParameter("文字列", "比較文字列", paras[3]));  //string
			p.Add(new BakinParameter("Guid", "チェックするキャストGuid"));    //member guid
			p.Add(new BakinParameter("整数", "比較条件（0：同じ、1：先頭が同じ、2：最後尾が同じ、3：含む）"));    //0=equal 1=startwith 2=endwith 3=include
			AddCommandEnd(p);
		}
		private void AddCommandIfparty(List<BakinParameter> p, List<string> paras)
		{
			AddCommandHeader(p, "IFPARTY");
			p.Add(new BakinParameter("Guid", "チェックするキャストGuid"));    //member guid
			p.Add(new BakinParameter("整数", "条件（0：パーティにいる、1：パーティにいない）"));    //0=with 1=without
			AddCommandEnd(p);
		}
		private void AddCommandIfitem(List<BakinParameter> p, List<string> paras)
		{
			AddCommandHeader(p, "IFITEM");
			p.Add(new BakinParameter("Guid", "チェックするアイテムGuid"));    //item guid
			p.Add(new BakinParameter("整数", "個数（変数可）", "1")); //type":name N=numeric, S=string, A=array
			p.Add(new BakinParameter("整数", "条件（0：持っている、1：持っていない）"));    //0=having 1=not having
			p.Add(new BakinParameter("整数", "装備中のアイテムを含めない"));   //exclude equipped flag
			AddCommandEnd(p);
		}
		private void AddCommandIfmoney(List<BakinParameter> p, List<string> paras)
		{
			AddCommandHeader(p, "IFMONEY");
			p.Add(new BakinParameter("整数", "チェックする金額（変数可）", paras[1]));   //amount
			var op = paras[2] == "0" ? "0" : "1";
			p.Add(new BakinParameter("整数", "条件（0：持っている、1：持っていない）", op));    //0=having 1=not having
			AddCommandEnd(p);
		}
		private void AddCommandExec(List<BakinParameter> p, List<string> paras)
		{
			AddCommandHeader(p, "EXEC");
			p.Add(new BakinParameter("Guid", "共通イベントGuid")); //common event guid
			p.Add(new BakinParameter("整数", "実行先のイベントが完了するまで待つ", "1"));   //wait complete
			AddCommandEnd(p);
		}
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
					p.Add(new BakinParameter("変数", "イベントスイッチ名", GetBknVarName(i.ToString(), "B")));  //to; type":name N=numeric, S=string, A=array
					p.Add(new BakinParameter("整数", "状態(0：オン、1：オフ）", paras[2] == "on" ? "0" : "1"));   //0=on 1=off
				}
			}
			else if (code == 123)
			{
				p.Add(new BakinParameter("ローカル変数", "イベントスイッチ名", GetBknVarName(paras[0], "B")));  //to; type":name N=numeric, S=string, A=array
				p.Add(new BakinParameter("整数", "状態(0：オン、1：オフ）", paras[1] == "on" ? "0" : "1"));   //0=on 1=off
			}
			else
			{
				p.Add(new BakinParameter("変数", "イベントスイッチ名", GetBknVarName(paras[0], "B")));  //to; type":name N=numeric, S=string, A=array
				p.Add(new BakinParameter("整数", "状態(0：オン、1：オフ）", paras[1] == "on" ? "0" : "1"));   //0=on 1=off
			}
			AddCommandEnd(p);
		}
		private void AddCommandVariable(List<BakinParameter> p, List<string> paras)
		{
			//Control Variables: 0:Single variable ID or starting ID, 1:range end ID, 2:operation(set,add,sub,mul,div,mod)
			//					 3:operand(const,var,rand), 4:value or varid or randmin, 5:randmax
			AddCommandHeader(p, "VARIABLE");
			for (var i = ToInt(paras[0]); i <= ToInt(paras[1]); i++)
			{
				if (i > ToInt(paras[0]))
				{
					AddCommandEnd(p);
					AddCommandHeader(p, "VARIABLE");
				}
				//reset Tmp variable
				p.Add(new BakinParameter("整数", ""));    //?
				p.Add(new BakinParameter("変数", "変数ボックスの番号", "N:Tmp"));  //type":name N=numeric
				p.Add(new BakinParameter("整数", ""));    //?
				p.Add(new BakinParameter("整数", "数値", "0"));   //value
				p.Add(new BakinParameter("整数", "計算（0：代入）", "0")); //0=overwrite
				AddCommandEnd(p);
				AddCommandHeader(p, "VARIABLE");
				//add rand value to Tmp(=0)
				p.Add(new BakinParameter("整数", ""));    //?
				p.Add(new BakinParameter("変数", "変数ボックスの番号", "N:Tmp"));  //type":name N=numeric
				p.Add(new BakinParameter("整数", ""));    //?
				p.Add(new BakinParameter("整数", "数値", paras[5]));   //value
				p.Add(new BakinParameter("整数", "計算（5：乱数を足す（0～数値））", "5")); //5=addrand
				AddCommandEnd(p);
				//operate specified var with Tmp
				AddCommandHlvariable(p, new List<string> { ToStr(i), ToStr(i), paras[2], "1", "Tmp" });
			}
			AddCommandEnd(p);
		}
		private void AddCommandHlvariable(List<BakinParameter> p, List<string> paras)
		{
			//Control Variables common:		0:Single variable ID or starting ID, 1:range end ID, 2:operation(set,add,sub,mul,div,mod)
			//Control Variables:			3:operand(const,var), 4:value or varid
			//Control Variables item:		3:3, 4:type(item, weapon, armor), 5:id
			//Control Variables actor:		3:3, 4:3, 5:id, 6:val(level, exp, hp, mp, maxhp, maxmp, atk, def, matk, mdef, agi, luk)
			//Control Variables enemy:		3:3, 4:4, 5:id, 6:val(hp, mp, maxhp, maxmp, atk, def, matk, mdef, agi, luk)
			//Control Variables character:	3:3, 4:5, 5:id, 6:val(mapx, mapy, dir, screenx, screeny)
			//Control Variables party:		3:3, 4:6, 5:id
			//Control Variables gamedata:	3:3, 4:7, 5:val(mapid, nummember, gold, step, playtime, timer, savecount, battlecount, windount, escapecount)
			AddCommandHeader(p, "HLVARIABLE");
			for (var i = ToInt(paras[0]); i <= ToInt(paras[1]); i++)
			{
				if (i > ToInt(paras[0]))
				{
					AddCommandEnd(p);
					AddCommandHeader(p, "HLVARIABLE");
				}
				p.Add(new BakinParameter("整数", ""));
				p.Add(new BakinParameter("変数", "変数ボックスの番号", GetBknVarName(ToStr(i), "N")));  //type":name N=numeric
				if (ToInt(paras[3]) <= 1) //variable
				{
					Tuple<string, string> varval = GetBknVarNameOrVal(paras[3], paras[4]);
					p.Add(new BakinParameter(varval.Item1, "データタイプ", varval.Item2));
				}
				else if (ToInt(paras[4]) <= 2 || ToInt(paras[4]) == 6) //item
				{
					paras[5]; //item id
					p.Add(new BakinParameter("整数", "データタイプ", ???));
				}
				else if (ToInt(paras[4]) >= 3 && ToInt(paras[4]) <= 6) //actor, enemy, character
				{
					paras[5]; //id
							  //parameter name
					p.Add(new BakinParameter("整数", "データタイプ", ???));
				}
				else //game data
				{
					//parameter name
					p.Add(new BakinParameter("整数", "データタイプ", ???));
				}
				p.Add(new BakinParameter("整数", "数値"));  //value
				string op = paras[2] == "5" ? "6" : paras[2];
				p.Add(new BakinParameter("整数", "計算（0：代入、1：足す、2：引く、3：かける、4：割る、6：割った余りを代入、7：小数点以下を切り捨てて代入）", op)); //0=overwrite 1=add 2=sub 3=mult 4=div 6=mod 7=floor
			}
			AddCommandEnd(p);
		}
		private void AddCommandMoney(List<BakinParameter> p, List<string> paras)
		{
			//Change Gold: 0:Operation(+, -), 1:type(const, var), 2:value or id
			AddCommandHeader(p, "MONEY");
			p.Add(new BakinParameter("整数", ""));    //??
			Tuple<string, string> varval = GetBknVarNameOrVal(paras[1], paras[2]);
			p.Add(new BakinParameter(varval.Item1, "金額（変数可）", varval.Item2)); //amount
			p.Add(new BakinParameter("整数", "変化（1：増やす、2：減らす）", paras[0] == "0" ? "1" : "2")); //1=increase 2=decrease
			AddCommandEnd(p);
		}
		private void AddCommandItem(List<BakinParameter> p, List<string> paras)
		{
			//Change Items: 0:item id, 1:Operation(+, -), 2:type(const, var), 3:value or id
			//Change Weapons: 0:weapon id, 1:Operation(+, -) 2:type(const, var) 3:value or id, 4:include equipment flag
			//Change Armors: 0:armor id, 1:Operation(+, -), 2:type(const, var), 3:value or id, 4:include equipment flag
			AddCommandHeader(p, "ITEM");
			p.Add(new BakinParameter("Guid", "アイテムGuid"));  //item guid
			p.Add(new BakinParameter("整数", ""));    //??
			Tuple<string, string> varval = GetBknVarNameOrVal(paras[2], paras[3]);
			p.Add(new BakinParameter(varval.Item1, "個数（変数可）", varval.Item2)); //amount
			p.Add(new BakinParameter("整数", "変化（1：増やす、2：減らす）", paras[1] == "0" ? "1" : "2")); //1=increase 2=decrease
			AddCommandEnd(p);
		}
		private void AddCommandParty(List<BakinParameter> p, List<string> paras)
		{
			//Change Party Member: 0:actor id, 1:operation(add, remove), 2:initialize flag
			AddCommandHeader(p, "PARTY");
			p.Add(new BakinParameter("整数", "変更するキャスト（0：指定、1：n番目）", "0"));    //0=specify_cast 1=n-th member
			p.Add(new BakinParameter("Guid", "指定キャストGuid" + "(キャストID：" + paras[0] + ")"));    //cast guid
			p.Add(new BakinParameter("整数", "パーティのn番目（n-1）"));   //member number, N-1
			p.Add(new BakinParameter("整数", "参加状態（0：参加、1：外す）", paras[1])); //0=join 1=remove
			p.Add(new BakinParameter("整数", "前回参加時のステータスを引き継ぐ", paras[2] == "0" ? "1" : "0"));    //inherit previous status
			AddCommandEnd(p);
		}
		private void AddCommandSwsave(List<BakinParameter> p, List<string> paras)
		{
			//Change Save Access 0:Disable/enable
			AddCommandHeader(p, "SWSAVE");
			p.Add(new BakinParameter("整数", "セーブの禁止", paras[0] == "0" ? "1" : "0"));  //save disable flag
			AddCommandEnd(p);
		}
		private void AddCommandSwmenu(List<BakinParameter> p, List<string> paras)
		{
			//Change Menu Access: 0:Disable/enable
			AddCommandHeader(p, "SWMENU");
			p.Add(new BakinParameter("整数", "メニュー画面の表示の禁止", paras[0] == "0" ? "1" : "0"));    //menu disable flag
			AddCommandEnd(p);
		}
		private void AddCommandSwencounting(List<BakinParameter> p, List<string> paras)
		{
			//Change Encounter: 0:Disable/enable
			AddCommandHeader(p, "SWENCOUNTING");
			p.Add(new BakinParameter("整数", "モンスターの出現の禁止", paras[0] == "0" ? "1" : "0")); //monster disable flag
			AddCommandEnd(p);
		}
		private void AddCommandChangelayout(List<BakinParameter> p, List<string> paras)
		{
			//Change Window Color: 0:RGB array
			AddCommandHeader(p, "CHANGELAYOUT");
			p.Add(new BakinParameter("Guid", "使用するレイアウトGuid" + "(ウィンドウ色:" + paras[0] + ")")); //layout guid
			AddCommandEnd(p);
		}
		private void AddCommandPlmove(List<BakinParameter> p, List<string> paras)
		{
			//Transfer Player 201     場所移動 PLMOVE  0:specify type(const, var)	1:map id; value or id   2:x; value or id    3:y; value or id    4:4 5:fade type(black, white)
					varval = GetBknVarNameOrVal(paras[]);
			AddCommandHeader(p, "PLMOVE");
			p.Add(new BakinParameter("[スポット", "マップGuid|スポットID|X位置（変数可）|Y位置|Z位置（変数可）")); //map_guid|spot_id_from_1001|xpos|ypos|zpos
			p.Add(new BakinParameter("変数", "X位置"));    //xpos type":name N=numeric, S=string, A=array&
			p.Add(new BakinParameter("変数", "Z位置")); //zpos
			p.Add(new BakinParameter("ローカル変数", "(x/zpos)]"));   //x/zpos]
			p.Add(new BakinParameter("整数", "向きを指定（0：変更しない、1：上向き、2：下向き、3：左向き、4：右向き）"));  //direction 0=nochange 1=up 2=down 3=left 4=right
			AddCommandEnd(p);
		}
		private void AddCommandMove(List<BakinParameter> p, List<string> paras)
		{
			//Set Event Location  203     イベントの位置設定 MOVE    0:id    1:specify type(const, var, swap)	2:x; value or id or swapevent id    3:y; value or id    4:direction
			varval = GetBknVarNameOrVal(paras[]);
			AddCommandHeader(p, "MOVE");
			p.Add(new BakinParameter("[スポット", "マップGuid|スポットID|X位置（変数可）|Y位置|Z位置（変数可）")); //map_guid|spot_id_from_1001|xpos|ypos|zpos
			p.Add(new BakinParameter("変数", "X位置"));    //xpos type":name N=numeric, S=string, A=array&
			p.Add(new BakinParameter("変数", "Z位置")); //zpos
			p.Add(new BakinParameter("ローカル変数", "(x/zpos)]"));   //x/zpos]
			p.Add(new BakinParameter("Guid", "イベントGuid"));  //event guid
			p.Add(new BakinParameter("整数", "向きを指定（0：変更しない、1：上向き、2：下向き、3：左向き、4：右向き）"));  //direction 0=nochange 1=up 2=down 3=left 4=right
			AddCommandEnd(p);
		}
		private void AddCommandCamanimation(List<BakinParameter> p, List<string> paras)
		{
			//Scroll Map: 0:Scroll direction, 1:distance, 2:speed, 3:wait flag(mz)
			AddCommandHeader(p, "CAMANIMATION");
			p.Add(new BakinParameter("Guid", "カメラGuid" + "(方向:" + paras[0] + " 距離:" + paras[1] + " 速度:" + paras[2] + ")"));   //camera_anim guid
			string flag = paras.Count == 4 ? paras[3] : "0";
			p.Add(new BakinParameter("整数", "完了するまで待つ", flag)); //wait complete
			AddCommandEnd(p);
		}
		private void AddCommandEffect(int code, List<BakinParameter> p, List<string> paras)
		{
			//Show Animation: 0:char id, 1:animation id, 2:wait flag
			//Show Battle Animation: 0:Enemy index, 1:animation ID, 2:entire troop flag
			AddCommandHeader(p, "EFFECT");
			string tmp = code != 337 ? "2" : (paras[0] == "-1" ? "1" : paras[0] == "0" ? "0" : "2");
			p.Add(new BakinParameter("整数", "表示中心位置（0：イベント、1：プレイヤー、2：画面、3：イメージ）", tmp));  //pos 0=this 1=player
			p.Add(new BakinParameter("Guid", "エフェクトGuid" + "(ID:" + paras[1] + ")")); //effect guid
			p.Add(new BakinParameter("整数", "完了するまで待つ", paras[2]));    //wait complete
			AddCommandEnd(p);
		}
		private void AddCommandEmote(List<BakinParameter> p, List<string> paras)
		{
			//Show Balloon Icon: 0:char id, 1:balloon id, 2:wait flag
			AddCommandHeader(p, "EMOTE");
			string tmp = paras[0] == "-1" ? "1" : paras[0] == "0" ? "0" : "2";
			p.Add(new BakinParameter("整数", "表示中心位置（0：イベント、1：プレイヤー）", tmp));  //pos 0=this 1=player
			p.Add(new BakinParameter("Guid", "感情マークGuid" + "(ID:" + paras[1] + ")")); //emote guid
			p.Add(new BakinParameter("整数", "完了するまで待つ", paras[2]));    //wait complete
			AddCommandEnd(p);
		}
		private void AddCommandWalkinrows(List<BakinParameter> p, List<string> paras)
		{
			//Change Player Followers: 0:(on, off)
			AddCommandHeader(p, "WALKINROWS");
			p.Add(new BakinParameter("整数", "隊列歩行の許可", paras[0])); //walk row flag
			AddCommandEnd(p);
		}
		private void AddCommandScreenfade(int code, List<BakinParameter> p, List<string> paras)
		{
			//Fadeout Screen:
			//Fadein Screen:
			AddCommandHeader(p, "SCREENFADE");
			p.Add(new BakinParameter("小数", "変更までにかかる時間", "0.4")); //time in sec
			string tmp = code == 221 ? "1" : "0";
			p.Add(new BakinParameter("整数", "効果（0：明るくする、1：暗くする）", tmp));  //fade 0=in 1=out
			p.Add(new BakinParameter("整数", "完了するまで待つ", "1"));    //wait complete
			AddCommandEnd(p);
		}
		private void AddCommandScreencolor(List<BakinParameter> p, List<string> paras)
		{
			//Tint Screen 223     画面の色調変更 SCREEN_COLOR    0:colour object(RGB, grey) 1:duration in frames    2:wait flag
					varval = GetBknVarNameOrVal(paras[]);
			AddCommandHeader(p, "SCREENCOLOR");
			p.Add(new BakinParameter("小数", "変更までにかかる時間", Frame2Time(paras[1]))); //time in sec
			p.Add(new BakinParameter("整数", "画面色")); //color dec -> ARGB hex
			p.Add(new BakinParameter("整数", "完了するまで待つ", paras[2]));    //wait complete
			AddCommandEnd(p);
		}
		private void AddCommandScreenflash(List<BakinParameter> p, List<string> paras)
		{
			//Flash Screen: 0:colour object(RGB, grey), 1:duration in frames, 2:wait flag
			AddCommandHeader(p, "SCREENFLASH");
			p.Add(new BakinParameter("小数", "フラッシュする時間", Frame2Time(paras[1])));  //time in sec
			AddCommandEnd(p);
		}
		private void AddCommandScreenshake(List<BakinParameter> p, List<string> paras)
		{
			//Shake Screen: 0:Power(1 - 9), 1:speed(1 - 9), 2:duration in frames, 3:wait flag
			AddCommandHeader(p, "SCREENSHAKE");
			p.Add(new BakinParameter("小数", "ゆらす時間", Frame2Time(paras[2])));  //time in sec
			string tmp = ToInt(paras[0]) <= 3 ? "0" : ToInt(paras[0]) <= 6 ? "1" : "2";
			p.Add(new BakinParameter("整数", "ゆれの強さ（0：弱、1：中、2：強）", tmp));  //strength 0=weak 1=middle 2=strong
			p.Add(new BakinParameter("整数", "完了するまで待つ", paras[3]));    //wait complete
			AddCommandEnd(p);
		}
		private void AddCommandWait(List<BakinParameter> p, List<string> paras)
		{
			//Wait: 0:Wait time in frames
			AddCommandHeader(p, "WAIT");
			p.Add(new BakinParameter("小数", "時間", Frame2Time(paras[0]))); //time
			p.Add(new BakinParameter("整数", ""));    //?
			AddCommandEnd(p);
		}
		private void AddCommandSppicture(List<BakinParameter> p, List<string> paras)
		{
			//Show Picture: 0:Picture number, 1:image filename, 2:origin(upleft, center), 3:specify type(const, var),
			//	4:x value or id, 5:y value or id, 6:scale X, 7:scale Y, 8:opacity, 9:blend mode(normal, add, mult, screen)
			AddCommandHeader(p, "SPPICTURE");
			p.Add(new BakinParameter("整数", "イメージの管理番号", paras[0]));
			p.Add(new BakinParameter("Guid", "イメージGuid" + "(ファイル名: " + paras[1] + ")"));
			p.Add(new BakinParameter("整数", "X拡大率", paras[6]));
			p.Add(new BakinParameter("整数", "半透明にする（無効：-1、有効：2139062143）", OpacToColor(paras[8])));
			p.Add(new BakinParameter("整数", ""));
			Tuple<string, string> varval = GetBknVarNameOrVal(paras[3], paras[4]);
			p.Add(new BakinParameter(varval.Item1, "X位置（変数可）", varval.Item2));
			varval = GetBknVarNameOrVal(paras[3], paras[5]);
			p.Add(new BakinParameter(varval.Item1, "Y位置（変数可）", varval.Item2));
			p.Add(new BakinParameter("文字列", "モーション"));
			p.Add(new BakinParameter("整数", "じわっと表示"));
			p.Add(new BakinParameter("整数", "Y拡大率", paras[7]));
			p.Add(new BakinParameter("整数", "回転（変数可）"));
			AddCommandEnd(p);
		}
		private void AddCommandSpmove(List<BakinParameter> p, List<string> paras)
		{
			//Move Picture: 0:Picture number, 1:image filename, 2:origin(upleft, center), 3:specify type(const, var),
			//	4:x value or id, 5:y value or id, 6:scale X, 7:scale Y, 8:opacity, 9:blend mode(normal, add, mult, screen),
			//	10:duration, 11:wait flag
			AddCommandHeader(p, "SPMOVE");
			p.Add(new BakinParameter("整数", "イメージの管理番号", paras[0])); //image id
			p.Add(new BakinParameter("整数", "X拡大率", paras[6]));
			p.Add(new BakinParameter("小数", "移動にかける時間", Frame2Time(paras[10])));   //move time in sec
			p.Add(new BakinParameter("整数", ""));
			p.Add(new BakinParameter("整数", ""));
			p.Add(new BakinParameter("整数", ""));
			Tuple<string, string> varval = GetBknVarNameOrVal(paras[3], paras[4]);
			p.Add(new BakinParameter(varval.Item1, "X位置（変数可）", varval.Item2)); //xpos
			varval = GetBknVarNameOrVal(paras[3], paras[5]);
			p.Add(new BakinParameter(varval.Item1, "Y位置（変数可）", varval.Item2)); //ypos
			p.Add(new BakinParameter("整数", "Y拡大率", paras[7]));
			p.Add(new BakinParameter("整数", "完了するまで待つ", paras[11])); //wait complete
			AddCommandEnd(p);
		}
		private void AddCommandSphide(List<BakinParameter> p, List<string> paras)
		{
			//Erase Picture: 0:Picture number
			AddCommandHeader(p, "SPHIDE");
			p.Add(new BakinParameter("整数", "イメージの管理番号", paras[0])); //image id
			p.Add(new BakinParameter("整数", "じわっと消す")); //fade flag
			AddCommandEnd(p);
		}
		private void AddCommandChangerender(int code, List<BakinParameter> p, List<string> paras)
		{
			//Change Weather: 0:Weather type, 1:power, 2:duration, 3:wait flag
			//Change Battle Background: 0:Battleback1   1:Battleback2
			//Change Parallax 284     遠景の変更 CHANGE_RENDER   0:Image filename    1:loop x flag   2:3:loop y flag 3:x shift   4:y shift
			AddCommandHeader(p, "CHANGERENDER");
			string tmp = "";
			if (code == 236)
			{
				tmp = "天候ID:" + paras[0];
			}
			else if (code == 283)
			{
				tmp = "戦闘背景ID:" + paras[0] + "+" + paras[1];
			}
			else
			{
				tmp = "遠景ID:" + paras[0];
			}
			p.Add(new BakinParameter("Guid", "レンダリングGuid" + "(" + tmp + ")"));    //render guid
			p.Add(new BakinParameter("文字列", "レンダリング名"));    //render name
			p.Add(new BakinParameter("文字列", ""));
			AddCommandEnd(p);
		}
		private void AddCommandPlaybgm(int code, List<BakinParameter> p, List<string> paras)
		{
			//Play BGM: 0:{ name: BGM filename, volume: 0 - 100, pitch: 50 - 150, pan: -100 - 100 }
			//Fadeout BGM: 0:Duration in frames
			AddCommandHeader(p, "PLAYBGM");
			if (code == 241)
			{
				MvAudio audio = JsonSerializer.Deserialize<MvAudio>(paras[0]);
				p.Add(new BakinParameter("Guid", "BGMのGuid" + "(ファイル名: " + audio.name + ")"));  //bgm guid
				p.Add(new BakinParameter("整数", "ボリューム", ToStr(audio.volume)));   //vol
				p.Add(new BakinParameter("整数", "テンポ", ToStr(audio.pitch))); //tempo
				p.Add(new BakinParameter("整数", ""));
				p.Add(new BakinParameter("小数", "フェードアウト時間"));  //fadeout time
				p.Add(new BakinParameter("小数", "フェードイン時間"));   //fadein time
			}
			else
			{
				p.Add(new BakinParameter("Guid", "BGMのGuid"));  //bgm guid
				p.Add(new BakinParameter("整数", "ボリューム"));   //vol
				p.Add(new BakinParameter("整数", "テンポ")); //tempo
				p.Add(new BakinParameter("整数", ""));
				p.Add(new BakinParameter("小数", "フェードアウト時間"));  //fadeout time
				p.Add(new BakinParameter("小数", "フェードイン時間", Frame2Time(paras[0])));   //fadein time
			}
			AddCommandEnd(p);
		}
		private void AddCommandPlaybgs(int code, List<BakinParameter> p, List<string> paras)
		{
			//Play BGS: 0:{ name: BGM filename, volume: 0 - 100, pitch: 50 - 150, pan: -100 - 100 }
			//Fadeout BGS: 0:Duration in frames
			AddCommandHeader(p, "PLAYBGS");
			if (code == 245)
			{
				MvAudio audio = JsonSerializer.Deserialize<MvAudio>(paras[0]);
				p.Add(new BakinParameter("Guid", "BGSのGuid" + "(ファイル名: " + audio.name + ")"));  //bgs guid
				p.Add(new BakinParameter("整数", "ボリューム", ToStr(audio.volume)));   //vol
				p.Add(new BakinParameter("整数", "テンポ", ToStr(audio.pitch))); //tempo 50-200
				p.Add(new BakinParameter("整数", ""));
				p.Add(new BakinParameter("小数", "フェードアウト時間"));  //fadeout time
				p.Add(new BakinParameter("小数", "フェードイン時間"));   //fadein time
			}
			else
			{
				p.Add(new BakinParameter("Guid", "BGSのGuid"));  //bgs guid
				p.Add(new BakinParameter("整数", "ボリューム"));   //vol
				p.Add(new BakinParameter("整数", "テンポ")); //tempo 50-200
				p.Add(new BakinParameter("整数", ""));
				p.Add(new BakinParameter("小数", "フェードアウト時間"));  //fadeout time
				p.Add(new BakinParameter("小数", "フェードイン時間", Frame2Time(paras[0])));   //fadein time
			}
			AddCommandEnd(p);
		}
		private void AddCommandPlayjingle(List<BakinParameter> p, List<string> paras)
		{
			//Play ME: 0:{ name: BGM filename, volume: 0 - 100, pitch: 50 - 150, pan: -100 - 100 }
			AddCommandHeader(p, "PLAYJINGLE");
			MvAudio audio = JsonSerializer.Deserialize<MvAudio>(paras[0]);
			p.Add(new BakinParameter("Guid", "MEのGuid" + "(ファイル名: " + audio.name + ")"));   //fanfare guid
			p.Add(new BakinParameter("整数", "ボリューム", ToStr(audio.volume)));   //vol
			p.Add(new BakinParameter("整数", "テンポ", ToStr(audio.pitch))); //tempo
			p.Add(new BakinParameter("整数", "終わるまで待つ", "0")); //wait complete
			AddCommandEnd(p);
		}
		private void AddCommandStopse(List<BakinParameter> p, List<string> paras)
		{
			//Stop SE
			AddCommandHeader(p, "STOPSE");
			p.Add(new BakinParameter("Guid", "停止するSEのGuid、空の場合はすべてのSE"));   //stop audio guid, all if 0
			AddCommandEnd(p);
		}
		private void AddCommandPlaymovie(List<BakinParameter> p, List<string> paras)
		{
			//Play Movie: 0:Video filename
			AddCommandHeader(p, "PLAYMOVIE");
			p.Add(new BakinParameter("Guid", "再生する動画Guid" + "(ファイル名:" + paras[0] + ")"));    //movie guid
			AddCommandEnd(p);
		}
		private void AddCommandGetterrain(List<BakinParameter> p, List<string> paras)
		{
			//Get Location Info, 0:Variable ID, 1:type(terraintag, eventid, tileid(layer1 - 4), regionid),
			//	2:specify type(const, var), 3:x value or id, 4:y value or id
			if (paras[2] == "0" || paras[1] != "2") //unable to specify absolute position. unable to get terraintag, eventid or regionid.
			{
				AddCommandComment(p, paras);
			}
			else
			{
				AddCommandHeader(p, "GETTERRAIN");
				p.Add(new BakinParameter("整数", "取得する座標（0：プレイヤーの現在位置、1：イベントの現在位置、2：変数で指定）", "2")); //0=currentpos 1=eventpos 2=var
				p.Add(new BakinParameter("変数", "X座標用変数の番号", GetBknVarNameOrVal("1", paras[3]).Item2));   //xpos
				p.Add(new BakinParameter("変数", "Y座標用変数の番号", GetBknVarNameOrVal("1", paras[4]).Item2));   //ypos
				p.Add(new BakinParameter("整数", "取得情報（0：地形のリソース名、1：地形の高さ）", "0"));    //0=land res name 1=height
				p.Add(new BakinParameter("変数", "取得先変数の番号", paras[0]));   //to; type":name N=numeric, S=string, A=array
				AddCommandEnd(p);
			}
		}
		private void AddCommandBossbattle(List<BakinParameter> p, List<string> paras)
		{
			//Battle Processing   301     戦闘の処理 BOSSBATTLE  0:specify type(const, var, rand)	1:troop; value or id    2:can escape flag   3:can lose flag
					p.Add(new BakinParameter("整数", "モンスターの数")); //num of monsters
			AddCommandHeader(p, "BOSSBATTLE");
			p.Add(new BakinParameter("Guid", "モンスターnのGuid"));
			p.Add(new BakinParameter("Guid", ""));
			p.Add(new BakinParameter("整数", "負けてもゲームオーバーにしない")); //no gameover flag
			p.Add(new BakinParameter("整数", "逃げられない"));  //no escape flag
			p.Add(new BakinParameter("整数", ""));    //??
			p.Add(new BakinParameter("Guid", "BGMのGuid"));  //bgm guid
			p.Add(new BakinParameter("Guid", "バトルマップのGuid"));   //battle map guid
			p.Add(new BakinParameter("整数", "1000：マップ中心セクション"));    //1000": map center section
			p.Add(new BakinParameter("整数", "バトルマップの中心X座標"));    //battle map centerx
			p.Add(new BakinParameter("整数", "バトルマップの中心Z座標"));    //battle map centerz
			p.Add(new BakinParameter("整数", "1001：モンスター位置セクション（中心からの相対値）"));    //1001": monster pos section, rel to center
			p.Add(new BakinParameter("整数", "モンスターn位置X座標ｘ1000"));    //monster1 posx*1000
			p.Add(new BakinParameter("整数", "モンスターn位置Z座標ｘ1000"));    //monster1 posz*1000
			p.Add(new BakinParameter("整数", "(monster2"));   //monster2 posx*1000
			p.Add(new BakinParameter("整数", "(monster2"));   //monster2 posz*1000
			p.Add(new BakinParameter("整数", "1002：レベルセクション"));  //1002": level section
			p.Add(new BakinParameter("整数", "モンスターnレベル"));   //monster1 level
			p.Add(new BakinParameter("整数", "(monster2"));   //monster2 level
			p.Add(new BakinParameter("整数", "1005：メンバー位置セクション（中心からの相対値）")); //1005": mem pos section, rel to center
			p.Add(new BakinParameter("整数", "メンバーの数"));  //num of members
			p.Add(new BakinParameter("整数", "メンバーn位置X座標ｘ1000")); //mem1 posx*1000
			p.Add(new BakinParameter("整数", "メンバーn位置Z座標ｘ1000")); //mem1 posz*1000
			p.Add(new BakinParameter("整数", "(mem2"));   //mem2 posx*1000
			p.Add(new BakinParameter("整数", "(mem2"));   //mem2 posz*1000
			p.Add(new BakinParameter("整数", "(mem3"));   //mem3 posx*1000
			p.Add(new BakinParameter("整数", "(mem3"));   //mem3 posz*1000
			p.Add(new BakinParameter("整数", "(mem4"));   //mem4 posx*1000
			p.Add(new BakinParameter("整数", "(mem4"));   //mem4 posz*1000
			p.Add(new BakinParameter("整数", "1006：登場メッセージフラグセクション"));   //1006": emerge message flag section
			p.Add(new BakinParameter("整数", "モンスター登場メッセージを出さない"));   //no emerge message flag
			AddCommandEnd(p);
		}
		private void AddCommandShop(List<BakinParameter> p, List<string> paras)
		{
			//Shop Processing: 0:type(item, weapon, armor), 1:id, 2:price type(defprice, specify), 3:specific price,
			//	4:purchase only flag
			AddCommandHeader(p, "SHOP");
			List<string> ids = JsonSerializer.Deserialize<List<string>>(paras[1]);
			List<string> prices = JsonSerializer.Deserialize<List<string>>(paras[3]);
			p.Add(new BakinParameter("整数", "アイテムの数", ToStr(ids.Count))); //num of items
			for (int i = 0; i < ids.Count; i++)
			{
				p.Add(new BakinParameter("Guid", "アイテムのGuid" + "(ID:" + ids[i] + ")")); //item guid
			}
			for (int i = 0; i < prices.Count; i++)
			{
				p.Add(new BakinParameter("整数", "アイテムの価格" + "(" + ToStr(i + 1) + ":" + prices[i] + ")")); //item price
			}
			p.Add(new BakinParameter("整数", "選択肢の位置（2130706432-N　0：左上、1：上、2：右上、3：左、4：中央、5：右、6：左下、7：下、8：右下）", "2130706432")); //pos 2130706432-N 0=upleft 1=up 2=upright 3=left 4=center 5=right 6=botleft 7=bottom 8=botright
			AddCommandEnd(p);
		}
		private void AddCommandChangeheroname(List<BakinParameter> p, List<string> paras)
		{
			//Name Input Processing: 0:Actor ID, 1:max characters
			AddCommandHeader(p, "CHANGEHERONAME");
			p.Add(new BakinParameter("整数", "変更するキャスト（0：指定、1：n番目）", "0")); //0=specify_cast 1=n-th member
			p.Add(new BakinParameter("Guid", "指定キャストGuid" + "(ID:" + paras[0] + ")"));    //cast guid
			p.Add(new BakinParameter("整数", "パーティのn番目（n-1）"));   //member number, N-1
			p.Add(new BakinParameter("整数", "入力可能な最大文字数", paras[1])); //input chars number": max10
			p.Add(new BakinParameter("整数", "ウィンドウ表示位置（0：上、1：中央、2：下）")); //windowpos 0=up 1=center 2=bottom
			p.Add(new BakinParameter("文字列", "入力画面に表示する文字1", "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめもやゆよわをんがぎぐげござじずぜぞだぢづでどばびぶべぼぱぴぷぺぽっ")); //input1
			p.Add(new BakinParameter("文字列", "入力画面に表示する文字2", "アイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミムメモヤユヨワヲンガギグゲゴザジズゼゾダヂヅデドバビブベボパピプペポッ"));   //input2
			p.Add(new BakinParameter("文字列", "入力画面に表示する文字3"));
			p.Add(new BakinParameter("文字列", "入力画面に表示する文字4"));
			AddCommandEnd(p);
		}
		private void AddCommandChghpmp(int code, List<BakinParameter> p, List<string> paras)
		{
			//Change HP: 0:specify type(const, var), 1:actor value or id, 2:Operation(+, -), 3:type(const, var), 4:value or id, 5:allow death flag
			//Change MP: 0:specify type(const, var), 1:actor value or id, 2:Operation(+, -), 3:type(const, var), 4:value or id
			AddCommandHeader(p, "CHGHPMP");
			p.Add(new BakinParameter("整数", "変更するキャスト（0：指定、1：n番目）", "0")); //0=specify_cast 1=n-th member
			Tuple<string, string> varval = GetBknVarNameOrVal(paras[0], paras[1]);
			p.Add(new BakinParameter("Guid", "指定キャストGuid" + "(ID:" + varval.Item2 + ")")); //cast guid
			p.Add(new BakinParameter("整数", "パーティのn番目（n-1）"));   //member number, N-1
			p.Add(new BakinParameter("整数", "効果対象（0：HP、1：MP）", code == 311 ? "0" : "1")); //0=hp 1=mp
			varval = GetBknVarNameOrVal(paras[3], paras[4]);
			p.Add(new BakinParameter(varval.Item1, "数値（変数可）", varval.Item2)); //amount
			p.Add(new BakinParameter("整数", "変化（0：増やす、1：減らす）", paras[2])); //0=increase 1=decrease
			AddCommandEnd(p);
		}
		private void AddCommandChgsttailm(List<BakinParameter> p, List<string> paras)
		{
			//Change State: 0:specify type(const, var), 1:actor value or id, 2:operation(add, remove), 3:state id
			AddCommandHeader(p, "CHGSTTAILM");
			p.Add(new BakinParameter("整数", "変更するキャスト（0：指定、1：n番目）", "0")); //0=specify_cast 1=n-th member
			Tuple<string, string> varval = GetBknVarNameOrVal(paras[0], paras[1]);
			p.Add(new BakinParameter("Guid", "指定キャストGuid" + "(ID:" + varval.Item2 + ")"));    //cast guid, all if 0
			p.Add(new BakinParameter("整数", "パーティのn番目（n-1）"));   //member number, N-1
			p.Add(new BakinParameter("Guid", "状態変化Guid" + "(ID:" + paras[3] + ")"));  //state guid
			p.Add(new BakinParameter("整数", "状態（0：状態変化にする、1：状態変化を治す）", paras[2])); //0=add 1=remove
			AddCommandEnd(p);
		}
		private void AddCommandFullrecov(List<BakinParameter> p, List<string> paras)
		{
			AddCommandHeader(p, "FULLRECOV");
			AddCommandFullrecov(p);
			//Recover All: 0:specify type(const, var), 1:actor value or id (-1:all)
			AddCommandEnd(p);
		}
		private void AddCommandChgexp(List<BakinParameter> p, List<string> paras)
		{
			//Change Exp: 0:specify type(const, var), 1:actor value or id, 2:Operation(+, -), 3:type(const, var),
			//	4:value or id, 5:show level up flag
			AddCommandHeader(p, "CHGEXP");
			p.Add(new BakinParameter("整数", "変更するキャスト（0：指定、1：n番目）", "0")); //0=specify_cast 1=n-th member
			Tuple<string, string> varval = GetBknVarNameOrVal(paras[0], paras[1]);
			p.Add(new BakinParameter("Guid", "変更するキャストGuid" + "(ID:" + varval.Item2 + ")")); //cast guid, all if 0
			p.Add(new BakinParameter("整数", "パーティのn番目（n-1）")); //member number, N-1
			varval = GetBknVarNameOrVal(paras[3], paras[4]);
			p.Add(new BakinParameter(varval.Item1, "経験値（変数可）", varval.Item2)); //amount
			p.Add(new BakinParameter("整数", "変化（0：増やす、1：減らす）", paras[2])); //0=increase 1=decrease
			AddCommandEnd(p);
		}
		private void AddCommandStatus(List<BakinParameter> p, List<string> paras)
		{
			//Change Parameter: 0:specify type(const, var), 1:actor value or id, 2:param ID, 3:Operation(+, -),
			//	4:type(const, var), 5:value or id
			if (paras[2] == "5" || paras[2] == "7")
			{
				//comment "魔法防御" or "運"
				AddCommandComment(p, paras);
			}
			else
			{
				AddCommandHeader(p, "STATUS");
				p.Add(new BakinParameter("整数", "変更するキャスト（0：指定、1：n番目）", "0")); //0=specify_cast 1=n-th member
				Tuple<string, string> varval = GetBknVarNameOrVal(paras[0], paras[1]);
				p.Add(new BakinParameter("Guid", "指定キャストGuid" + "(ID:" + varval.Item2 + ")")); //cast guid
				p.Add(new BakinParameter("整数", "パーティのn番目（n-1）")); //member number, N-1
				string tmp = paras[2] == "6" ? "5" : paras[2];
				p.Add(new BakinParameter("整数", "効果対象（0：最大HP、1：最大MP、2：攻撃力、3：防御力、4：魔力、5：素早さ）", tmp));  //0=maxhp 1=maxmp 2=attack 3=defence 4=magic 5=agility
				varval = GetBknVarNameOrVal(paras[4], paras[5]);
				p.Add(new BakinParameter(varval.Item1, "数値（変数可）", varval.Item2)); //amount
				p.Add(new BakinParameter("整数", "変化（0：上げる、1：下げる）", paras[3])); //0=increase 1=decrease
				AddCommandEnd(p);
			}
		}
		private void AddCommandChgskill(List<BakinParameter> p, List<string> paras)
		{
			//Change Skill: 0:specify type(const, var), 1:actor value or id, 2:operation(add, remove), 3:skill id
			AddCommandHeader(p, "CHGSKILL");
			p.Add(new BakinParameter("整数", "変更するキャスト（0：指定、1：n番目）", "0")); //0=specify_cast 1=n-th member
			Tuple<string, string> varval = GetBknVarNameOrVal(paras[0], paras[1]);
			p.Add(new BakinParameter("Guid", "指定キャストGuid" + "(ID:" + varval.Item2 + ")")); //cast guid
			p.Add(new BakinParameter("整数", "パーティのn番目（n-1）")); //member number, N-1
			p.Add(new BakinParameter("Guid", "変更するスキルGuid" + "(ID:" + paras[3] + ")")); //skill guid
			p.Add(new BakinParameter("整数", "状態（0：習得、1：忘れる）", paras[3])); //0=get 1=forget
			AddCommandEnd(p);
		}
		private void AddCommandEquip(List<BakinParameter> p, List<string> paras)
		{
			//Change Equipment: 0:Actor ID, 1:equip(weapon, shield, head, body, acces), 2:weapon or armor ID
			AddCommandHeader(p, "EQUIP");
			p.Add(new BakinParameter("整数", "変更するキャスト（0：指定、1：n番目）", "0")); //0=specify_cast 1=n-th member
			p.Add(new BakinParameter("Guid", "指定キャストGuid" + "(ID:" + paras[0] + ")")); //cast guid
			p.Add(new BakinParameter("整数", "パーティのn番目（n-1）")); //member number, N-1
			p.Add(new BakinParameter("整数", "装備箇所（0：武器、1：腕防具、2：頭防具、3：体防具、4：装飾品1、5：装飾品2）", paras[1])); //part 0=weapon 1=armor 2=head 3=body 4=acces1 5=acces2
			p.Add(new BakinParameter("Guid", "装備するアイテムGuid" + "(ID:" + paras[2] + ")")); //item guid
			AddCommandEnd(p);
		}
		private void AddCommandStringvariable(List<BakinParameter> p, List<string> paras)
		{
			//Change Name 320: 0:Actor ID, 1:name
			AddCommandHeader(p, "STRINGVARIABLE");
			p.Add(new BakinParameter("整数", "", "-1"));
			p.Add(new BakinParameter("文字列", "新キャスト名", paras[1]));
			p.Add(new BakinParameter("整数", "代入（0：上書き、1：先頭に追加、2：最後尾に追加）", "0")); //0=overwrite 1=addfirst 2=addlast
			p.Add(new BakinParameter("Guid", "指定キャストGuid" + "(ID:" + paras[0] + ")")); //cast guid
			AddCommandEnd(p);
		}
		private void AddCommandChangejob(List<BakinParameter> p, List<string> paras)
		{
			//Change Class: 0:Actor ID, 1:class ID, 2:save EXP flag
			AddCommandHeader(p, "CHANGEJOB");
			p.Add(new BakinParameter("整数", "変更するキャスト（0：指定、1：n番目）", "0")); //0=specify_cast 1=n-th member
			p.Add(new BakinParameter("Guid", "指定キャストGuid" + "(ID:" + paras[0] + ")")); //cast guid
			p.Add(new BakinParameter("整数", "パーティのn番目（n-1）")); //member number, N-1
			p.Add(new BakinParameter("Guid", "職業Guid" + "(ID:" + paras[1] + ")")); //job guid
			p.Add(new BakinParameter("整数", "変更対象（0：職業、1：副業）", "0")); //object 0=job 1=subjob
			p.Add(new BakinParameter("整数", "成長したステータスの引継ぎ", paras[2])); //inherit status flag
			p.Add(new BakinParameter("整数", "副業を職業にする")); //change subjob to job
			AddCommandEnd(p);
		}
		private void AddCommandPlgraphic(List<BakinParameter> p, List<string> paras)
		{
			//Change Actor Images: 0:Actor ID, 1:walking graphic filename, 2:walking graphic index, 3:face filename,
			//	4:face index, 5:battler filename
			AddCommandHeader(p, "PLGRAPHIC");
			p.Add(new BakinParameter("整数", "変更するキャスト（0：指定、1：n番目）", "0")); //0=specify_cast 1=n-th member
			p.Add(new BakinParameter("Guid", "指定キャストGuid" + "(ID:" + paras[0] + ")")); //cast guid
			p.Add(new BakinParameter("整数", "パーティのn番目（n-1）"));   //member number, N-1
			p.Add(new BakinParameter("Guid", "マップ上でのグラフィックGuid" + "(Name:" + paras[1] + ", Index:" + paras[2] + ")"));  //mapchip guid
			p.Add(new BakinParameter("Guid", "レイアウト表示用グラフィックGuid" + "(Name:" + paras[3] + ", Index:" + paras[4] + ")")); //layout_gra guid
			p.Add(new BakinParameter("文字列", "モーション名")); //motion name?
			p.Add(new BakinParameter("整数", "モーションが完了するまでモーション変化させない")); //wait until motion_complete
			AddCommandEnd(p);
		}
		private void AddCommandBtlheal(List<BakinParameter> p, List<string> paras)
		{
			//Change Enemy HP: 0:Enemy ID, 1:Operation(+,-), 2:type(const, var), 3:value or id, 4:allow death flag
			//Change Enemy MP: 0:Enemy ID, 1:Operation(+,-), 2:type(const, var), 3:value or id
			AddCommandHeader(p, "BTLHEAL");
			AddCommandEnd(p);
		}
		private void AddCommandBtlstatus(List<BakinParameter> p, List<string> paras)
		{
			//Change Enemy State: 0:Enemy ID, 1:operation(add, remove), 2:state id
			AddCommandHeader(p, "BTLSTATUS");
			AddCommandEnd(p);
		}
		private void AddCommandBtlappear(List<BakinParameter> p, List<string> paras)
		{
			//Enemy Appear: 0:Enemy index
			AddCommandHeader(p, "BTLAPPEAR");
			AddCommandEnd(p);
		}
		private void AddCommandBtlaction(List<BakinParameter> p, List<string> paras)
		{
			//Force Action: 0:battler(enemy, actor), 1:enemy or actor id, 2:skill id, 3:target index(-2:last - 1:rand 0 -)
			AddCommandHeader(p, "BTLACTION");
			AddCommandEnd(p);
		}
		private void AddCommandBtlstop(List<BakinParameter> p, List<string> paras)
		{
			//Abort Battle
			AddCommandHeader(p, "BTLSTOP");
			AddCommandEnd(p);
		}
		private void AddCommandShowscoreboard(List<BakinParameter> p, List<string> paras)
		{
			//Open Menu Screen
			AddCommandHeader(p, "SHOWSCOREBOARD");
			p.Add(new BakinParameter("整数", "表示フラグ", "1")); //display flag
			p.Add(new BakinParameter("Guid", "使用するレイアウトGuid"));
			AddCommandEnd(p);
		}
		private void AddCommandNoparams(List<BakinParameter> p, string command)
		{
			AddCommandHeader(p, command);
			AddCommandEnd(p);
		}
		private void AddCommandPlwalk(List<BakinParameter> p, List<string> paras)
		{
			AddCommandHeader(p, "PLWALK");
			p.Add(new BakinParameter("整数", "方向（0：上、1：下、2：左、3：右、4：ランダム、5：このイベントの方、6：このイベントの逆、8：向いている方向、10：任意の角度）")); //0=up 1=down 2=left 3=right 4=rand 5=face 6=away 8=toward 10=deg
			varval = GetBknVarNameOrVal(paras[]);
			p.Add(new BakinParameter("[小数", "歩数（変数可）"));    //distance
			p.Add(new BakinParameter("変数", ""));
			p.Add(new BakinParameter("整数", "向きを固定"));   //fix_direction flag
			p.Add(new BakinParameter("整数", "移動できなかった場合は中断"));   //stop if stuck flag
			p.Add(new BakinParameter("整数", "イベントをすり抜ける"));  //through flag
			p.Add(new BakinParameter("整数", "段差を越える"));  //step over flag
			p.Add(new BakinParameter("整数", "モーションを変更しない")); //no motion change flag
			p.Add(new BakinParameter("整数", "角度"));  //degree
			p.Add(new BakinParameter("整数", ""));    //?
			p.Add(new BakinParameter("整数", "向きを滑らかに変化させる"));    //smooth rotate flag
			p.Add(new BakinParameter("整数", "4方向に丸める")); //round-to-4direction flag
			AddCommandEnd(p);
		}
		private void AddCommandWalk(List<BakinParameter> p, List<string> paras)
		{
			AddCommandHeader(p, "WALK");
			p.Add(new BakinParameter("整数", "方向（0：上、1：下、2：左、3：右、4：ランダム、5：このイベントの方、6：このイベントの逆、8：向いている方向、10：任意の角度）")); //0=up 1=down 2=left 3=right 4=rand 5=face 6=away 8=toward 10=deg
			varval = GetBknVarNameOrVal(paras[]);
			p.Add(new BakinParameter("[小数", "歩数（変数可）"));    //distance
			p.Add(new BakinParameter("変数", ""));
			p.Add(new BakinParameter("整数", "向きを固定"));   //fix_direction flag
			p.Add(new BakinParameter("整数", "移動できなかった場合は中断"));   //stop if stuck flag
			p.Add(new BakinParameter("整数", "イベントをすり抜ける"));  //through flag
			p.Add(new BakinParameter("整数", "段差を越える"));  //step over flag
			p.Add(new BakinParameter("整数", "モーションを変更しない")); //no motion change flag
			p.Add(new BakinParameter("整数", "角度"));  //degree
			p.Add(new BakinParameter("整数", ""));    //?
			p.Add(new BakinParameter("整数", "向きを滑らかに変化させる"));    //smooth rotate flag
			p.Add(new BakinParameter("整数", "4方向に丸める")); //round-to-4direction flag
			AddCommandEnd(p);
		}
		private void AddCommandPlwalktgt(List<BakinParameter> p, List<string> paras)
		{
			AddCommandHeader(p, "PLWALKTGT");
			varval = GetBknVarNameOrVal(paras[]);
			p.Add(new BakinParameter("スポット", "マップGuid|スポットID|X位置（変数可）|Y位置|Z位置（変数可）"));  //map_guid|spot_id_from_1001|xpos|ypos|zpos
			p.Add(new BakinParameter("整数", "移動終了時の向き（0：任意の角度、1：進行方向）"));    //0=deg 1=toward
			varval = GetBknVarNameOrVal(paras[]);
			p.Add(new BakinParameter("小数", "角度（変数可）")); //degree
			varval = GetBknVarNameOrVal(paras[]);
			p.Add(new BakinParameter("小数", "移動にかける時間（変数可）"));   //time in sec
			p.Add(new BakinParameter("整数", "補間（0：一定、1：加速、2：減速、3：加速～減速"));   //interp 0=const 1=accel 2=decel 3=accel-decel
			p.Add(new BakinParameter("整数", "完了するまで待つ"));    //wait complete
			p.Add(new BakinParameter("整数", "モーションを変更しない")); //no motion change
			p.Add(new BakinParameter("整数", "曲線補間"));    //curve interp
			AddCommandEnd(p);
		}
		private void AddCommandEvwalktgt(List<BakinParameter> p, List<string> paras)
		{
			AddCommandHeader(p, "EVWALKTGT");
			varval = GetBknVarNameOrVal(paras[]);
			p.Add(new BakinParameter("スポット", "マップGuid|スポットID|X位置（変数可）|Y位置|Z位置（変数可）"));  //map_guid|spot_id_from_1001|xpos|ypos|zpos
			p.Add(new BakinParameter("整数", "移動終了時の向き（0：任意の角度、1：進行方向）"));    //0=deg 1=toward
			varval = GetBknVarNameOrVal(paras[]);
			p.Add(new BakinParameter("小数", "角度（変数可）")); //degree
			varval = GetBknVarNameOrVal(paras[]);
			p.Add(new BakinParameter("小数", "移動にかける時間（変数可）"));   //time in sec
			p.Add(new BakinParameter("整数", "補間（0：一定、1：加速、2：減速、3：加速～減速"));   //interp 0=const 1=accel 2=decel 3=accel-decel
			p.Add(new BakinParameter("整数", "完了するまで待つ"));    //wait complete
			p.Add(new BakinParameter("整数", "モーションを変更しない")); //no motion change
			p.Add(new BakinParameter("整数", "曲線補間"));    //curve interp
			AddCommandEnd(p);
		}
		private void AddCommandAddforcepl(List<BakinParameter> p, List<string> paras)
		{
			AddCommandHeader(p, "ADDFORCEPL");
			varval = GetBknVarNameOrVal(paras[]);
			p.Add(new BakinParameter("整数", "X物理エンジン移動（変数可）"));  //xforce
			varval = GetBknVarNameOrVal(paras[]);
			p.Add(new BakinParameter("整数", "Y物理エンジン移動（変数可）"));  //yforce
			varval = GetBknVarNameOrVal(paras[]);
			p.Add(new BakinParameter("整数", "Z物理エンジン移動（変数可）"));  //zforce
			AddCommandEnd(p);
		}
		private void AddCommandAddforce(List<BakinParameter> p, List<string> paras)
		{
			AddCommandHeader(p, "ADDFORCE");
			varval = GetBknVarNameOrVal(paras[]);
			p.Add(new BakinParameter("整数", "X物理エンジン移動（変数可）"));  //xforce
			varval = GetBknVarNameOrVal(paras[]);
			p.Add(new BakinParameter("整数", "Y物理エンジン移動（変数可）"));  //yforce
			varval = GetBknVarNameOrVal(paras[]);
			p.Add(new BakinParameter("整数", "Z物理エンジン移動（変数可）"));  //zforce
			AddCommandEnd(p);
		}
		private void AddCommandPlrotate(List<BakinParameter> p, List<string> paras)
		{
			AddCommandHeader(p, "PLROTATE");
			p.Add(new BakinParameter("整数", "方向（0：上、1：下、2：左、3：右、4：ランダム、5：このイベントの方、6：このイベントの逆、7：クルッと回転、8：任意の角度）"));   //0=up 1=down 2=left 3=right 4=rand 5=face 6=away 7=rotate 8=deg
			varval = GetBknVarNameOrVal(paras[]);
			p.Add(new BakinParameter("[整数", "角度（変数可）"));    //degree]
			p.Add(new BakinParameter("(no round-to-4direction??)", ""));
			AddCommandEnd(p);
		}
		private void AddCommandRotate(List<BakinParameter> p, List<string> paras)
		{
			AddCommandHeader(p, "ROTATE");
			p.Add(new BakinParameter("整数", "方向（0：上、1：下、2：左、3：右、4：ランダム、5：このイベントの方、6：このイベントの逆、7：クルッと回転、8：任意の角度）"));   //0=up 1=down 2=left 3=right 4=rand 5=face 6=away 7=rotate 8=deg
			varval = GetBknVarNameOrVal(paras[]);
			p.Add(new BakinParameter("[整数", "角度（変数可）"));    //degree]
			p.Add(new BakinParameter("整数", ""));    //round-to-4direction??
			AddCommandEnd(p);
		}
		private void AddCommandPlwalkspeed(List<BakinParameter> p, List<string> paras)
		{
			AddCommandHeader(p, "PLWALKSPEED");
			p.Add(new BakinParameter("整数", "移動スピード（‐3～3）"));    //speed -3 to 3
			AddCommandEnd(p);
		}
		private void AddCommandWalkspeed(List<BakinParameter> p, List<string> paras)
		{
			AddCommandHeader(p, "WALKSPEED");
			p.Add(new BakinParameter("整数", "移動スピード（‐3～3）"));    //speed -3 to 3
			AddCommandEnd(p);
		}
		private void AddCommandGraphic(List<BakinParameter> p, List<string> paras)
		{
			AddCommandHeader(p, "GRAPHIC");
			p.Add(new BakinParameter("Guid", "キャストGuid"));  //mapchip guid
			p.Add(new BakinParameter("文字列", "モーション画像名"));
			AddCommandEnd(p);
		}
		private void AddCommandPlmotion(List<BakinParameter> p, List<string> paras)
		{
			AddCommandHeader(p, "PLMOTION");
			p.Add(new BakinParameter("整数", ""));    //??
			p.Add(new BakinParameter("Guid", ""));  //cast guid?
			p.Add(new BakinParameter("整数", ""));    //??
			p.Add(new BakinParameter("Guid", "マップ上でのグラフィックGuid"));  //mapchip_guid
			p.Add(new BakinParameter("文字列", "モーション画像名"));   //pic name
			p.Add(new BakinParameter("整数", "モーションが完了するまでモーション変化させない")); //wait motion complete
			AddCommandEnd(p);
		}
		private void AddCommandMotion(List<BakinParameter> p, List<string> paras)
		{
			AddCommandHeader(p, "MOTION");
			p.Add(new BakinParameter("Guid", "キャストGuid"));  //mapchip guid
			p.Add(new BakinParameter("文字列", "モーション画像名"));   //pic name
			AddCommandEnd(p);
		}
		private void AddCommandSwpllockrotate(List<BakinParameter> p, List<string> paras)
		{
			AddCommandHeader(p, "SWPLLOCKROTATE");
			p.Add(new BakinParameter("整数", "プレイヤーの向きの変更の禁止"));  //fix direction flag
			AddCommandEnd(p);
		}
		private void AddCommandChangeplayermovable(List<BakinParameter> p, List<string> paras)
		{
			AddCommandHeader(p, "CHANGEPLAYERMOVABLE");
			p.Add(new BakinParameter("整数", "通行可能でない地形への出入り許可"));    //through flag
			AddCommandEnd(p);
		}
		private void AddCommandChangemovable(List<BakinParameter> p, List<string> paras)
		{
			AddCommandHeader(p, "CHANGEMOVABLE");
			p.Add(new BakinParameter("整数", "通行可能でない地形への出入り許可"));    //through flag
			AddCommandEnd(p);
		}
		private void AddCommandPlhide(List<BakinParameter> p, List<string> paras)
		{
			AddCommandHeader(p, "PLHIDE");
			p.Add(new BakinParameter("整数", "透明化フラグ"));  //transparent flag
			AddCommandEnd(p);
		}
		private void AddCommandEvhide(List<BakinParameter> p, List<string> paras)
		{
			AddCommandHeader(p, "EVHIDE");
			p.Add(new BakinParameter("整数", "透明化フラグ"));  //transparent flag
			AddCommandEnd(p);
		}
		private void AddCommandPlayse(List<BakinParameter> p, List<string> paras)
		{
			AddCommandHeader(p, "PLAYSE");
			p.Add(new BakinParameter("Guid", "BGSのGuid"));  //se guid
			p.Add(new BakinParameter("整数", "ボリューム"));   //vol
			p.Add(new BakinParameter("整数", "テンポ")); //tempo
			p.Add(new BakinParameter("整数", "3Dサウンドとして再生")); //3d sound flag
			AddCommandEnd(p);
		}
		private void AddCommandComment(List<BakinParameter> p, List<string> paras)
		{
			AddCommandHeader(p, "COMMENT");
			p.Add(new BakinParameter("文字列", "テキスト", paras.Last()));
			AddCommandEnd(p);
		}

		private void AddCommandHeader(List<BakinParameter> p, string command)
		{
			AddCommandHeader(p, "HEADER");
			p.Add(new BakinParameter("コマンド", command.Replace("\t", "\t(") + ")"));
		}

		private void AddCommandEnd(List<BakinParameter> p)
		{
			AddCommandHeader(p, "END");
			p.Add(new BakinParameter("コマンド終了", ""));
		}


		private string OpacToColor(string opac)
		{
			string hex = int.Parse(opac).ToString("X2");
			return Convert.ToInt32(hex + hex + hex + hex).ToString();
		}

		private string Frame2Time(string val)
		{
			return (float.Parse(val) / 60.0).ToString();
		}

		private int ToInt(string val)
		{
			return int.Parse(val);
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
				return new Tuple<string, string>("ローカル変数", GetBknVarName(val, ""));
			}
			else
			{
				return new Tuple<string, string>("変数", GetBknVarName(val, "N"));
			}
		}

		private string GetBknVarName(string key, string type)
		{
			//type: N for numeric, B for bool.
			type += type == "" ? "" : ":";
			string varname = "";

			if (key == "A" || key == "B" || key == "C" || key == "D")
			{
				return key;
			}
			else
			{
				return (type == "B" ? "S" : "V") + "{000}" + varname; //"variable" or "switch"
			}
		}
		#endregion
	}
}
