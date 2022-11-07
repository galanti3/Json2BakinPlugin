using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json2BakinPlugin.Models
{
	public static class MvBakinCodeDictionary
	{
		#region Method
		public static List<string> Code(int code)
		{
			Dictionary<int, List<string>> codePairs = new Dictionary<int, List<string>>() {
			{00000, new List<string>{"CLOSE", "クローズタグ"}},
			{10100, new List<string>{"DIALOGUE", "会話を表示"}},
			{10101, new List<string>{"MESSAGE", "メッセージを表示"}},
			{10200, new List<string>{"CHOICES", "選択肢から選んで結果を確認"}},
			{10300, new List<string>{"CHANGE_STRING_VARIABLE", "文字列入力"}},
			{10400, new List<string>{"ITEMMENU", "アイテム選択画面を表示"}},
			{10500, new List<string>{"TELOP", "テロップを表示"}},
			{10800, new List<string>{"COMMENT", "注釈"}},
			{10900, new List<string>{"ERROR", "スキップ"}}, //comment out
			{11100, new List<string>{"IFSWITCH", "イベントスイッチの確認"}},
			{11101, new List<string>{"IFVARIABLE", "変数ボックスの確認"}},
			{11102, new List<string>{"IFSWITCH", "変数ボックスの確認"}},
			{11103, new List<string>{"ERROR_IF", "条件分岐：タイマー"}}, //comment out
			{11104, new List<string>{"IFPARTY", "パーティに含まれるキャストの確認"}},
			{11105, new List<string>{"IF_STRING_VARIABLE", "文字列変数の確認"}},
			{11106, new List<string>{"ERROR_IF", "条件分岐：アクタークラス、スキル、状態"}}, //comment out
			{11107, new List<string>{"IFITEM", "メンバーが持っているアイテムの確認"}},
			{11108, new List<string>{"BTL_IFMONSTER", "バトル中のモンスターのチェック"}},
			{11109, new List<string>{"ERROR_IF", "条件分岐：キャラクターの向き"}}, //comment out
			{11110, new List<string>{"IFMONEY", "持っているお金の確認"}},
			{11111, new List<string>{"IFITEM", "パーティが持っているアイテムの確認"}},
			{11112, new List<string>{"IFITEM", "パーティが持っている武器の確認"}},
			{11113, new List<string>{"IFITEM", "パーティが持っている防具の確認"}},
			{11114, new List<string>{"ERROR_IF", "条件分岐：押されているボタン"}}, //comment out
			{11115, new List<string>{"ERROR_IF", "条件分岐：スクリプト"}}, //comment out
			{11116, new List<string>{"ERROR_IF", "条件分岐：乗り物"}}, //comment out
			{11200, new List<string>{"LOOP", "ループ開始"}},
			{11300, new List<string>{"BREAK", "ループから抜け出す"}},
			{11500, new List<string>{"END", "イベント終了"}},
			{11700, new List<string>{"EXEC", "共通イベント呼び出し"}},
			{11800, new List<string>{"ERROR", "ラベル"}}, //comment out
			{11900, new List<string>{"ERROR", "ラベルジャンプ"}}, //comment out
			{12100, new List<string>{"SWITCH", "イベントスイッチのON/OFF"}},
			{12200, new List<string>{"HLVARIABLE", "複雑な変数ボックスの操作"}},
			{12201, new List<string>{"HLVARIABLE", "変数ボックスへの代入と計算：乱数"}},
			{12202, new List<string>{"HLVARIABLE", "複雑な変数ボックスの操作：アイテム"}},
			{12203, new List<string>{"HLVARIABLE", "複雑な変数ボックスの操作：アクター"}},
			{12204, new List<string>{"HLVARIABLE", "複雑な変数ボックスの操作：モンスター"}},
			{12205, new List<string>{"HLVARIABLE", "複雑な変数ボックスの操作：キャラクター"}},
			{12206, new List<string>{"ERROR", "変数：パーティアクターID"}},
			{12207, new List<string>{"HLVARIABLE", "複雑な変数ボックスの操作：ゲームデータ"}},
			{12208, new List<string>{"ERROR", "複雑な変数ボックスの操作：スクリプト"}}, //comment out
			{12300, new List<string>{"SWITCH", "セルフスイッチのON/OFF"}},
			{12400, new List<string>{"EXEC", "共通イベント呼び出し：タイマーの操作"}},
			{12500, new List<string>{"MONEY", "お金を増やす／減らす"}},
			{12600, new List<string>{"ITEM", "アイテムを増やす／減らす"}},
			{12700, new List<string>{"ITEM", "武器を増やす／減らす"}},
			{12800, new List<string>{"ITEM", "防具を増やす／減らす"}},
			{12900, new List<string>{"PARTY", "パーティにキャストを参加／外す"}},
			{13200, new List<string>{"ERROR", "戦闘BGMの変更"}}, //comment out
			{13300, new List<string>{"ERROR", "勝利MEの変更"}}, //comment out
			{13400, new List<string>{"SW_SAVE", "セーブの許可/禁止"}},
			{13500, new List<string>{"SW_MENU", "メニュー画面の表示の許可/禁止"}},
			{13600, new List<string>{"SW_ENCOUNTING", "モンスターの出現の許可/禁止"}},
			{13700, new List<string>{"ERROR", "並び替えの許可/禁止"}}, //comment out
			{13800, new List<string>{"CHANGE_LAYOUT", "表示するレイアウト（ウィンドウカラー）を変更"}},
			{13900, new List<string>{"ERROR", "敗北MEの変更"}}, //comment out
			{14000, new List<string>{"ERROR", "乗り物BGMの変更"}}, //comment out
			{20100, new List<string>{"PLMOVE", "プレイヤーを他の場所に移動"}},
			{20200, new List<string>{"ERROR", "乗り物の位置設定"}}, //comment out
			{20300, new List<string>{"MOVE", "イベントを瞬間移動"}},
			{20400, new List<string>{"CAM_ANIMATION", "カメラの設定"}},
			{20500, new List<string>{"MOVEROUTE", "移動ルートの設定"}},
			{20600, new List<string>{"ERROR", "乗り物の乗降"}}, //comment out
			{21100, new List<string>{"EVHIDE", "イベントを透明にする／透明から戻す"}},
			{21200, new List<string>{"EFFECT", "エフェクトの表示"}},
			{21300, new List<string>{"EMOTE", "感情マークを表示"}},
			{21400, new List<string>{"DESTROY_EVENT", "イベント削除"}},
			{21600, new List<string>{"WALK_IN_ROWS", "隊列歩行の許可/禁止"}},
			{21700, new List<string>{"ERROR", "隊列メンバーの集合"}}, //comment out
			{22100, new List<string>{"SCREEN_FADE", "画面を暗くする"}},
			{22200, new List<string>{"SCREEN_FADE", "画面を明るくする"}},
			{22300, new List<string>{"SCREEN_COLOR", "画面の色を変える"}},
			{22400, new List<string>{"SCREEN_FLASH", "画面をフラッシュ"}},
			{22500, new List<string>{"SCREEN_SHAKE", "画面を揺らす"}},
			{23000, new List<string>{"WAIT", "指定した時間待つ"}},
			{23100, new List<string>{"SPPICTURE", "イメージを表示"}},
			{23200, new List<string>{"SPMOVE", "イメージを移動"}},
			{23300, new List<string>{"ERROR", "ピクチャの回転"}}, //comment out
			{23400, new List<string>{"ERROR", "ピクチャの色調変更"}}, //comment out
			{23500, new List<string>{"SPHIDE", "イメージを消す"}},
			{23600, new List<string>{"CHANGE_RENDER", "レンダリング設定の変更"}},
			{24100, new List<string>{"PLAYBGM", "BGMを演奏"}},
			{24200, new List<string>{"PLAYBGM", "BGMをフェードアウト"}},
			{24300, new List<string>{"ERROR", "BGMの保存"}}, //comment out
			{24400, new List<string>{"ERROR", "BGMの再開"}}, //comment out
			{24500, new List<string>{"PLAYBGS", "環境音を設定"}},
			{24600, new List<string>{"PLAYBGS", "環境音をフェードアウト"}},
			{24900, new List<string>{"PLAYJINGLE", "ファンファーレを演奏"}},
			{25000, new List<string>{"PLAYSE", "効果音の再生"}},
			{25100, new List<string>{"STOPSE", "効果音の停止"}},
			{26100, new List<string>{"PLAYMOVIE", "ムービーの再生"}},
			{28100, new List<string>{"ERROR", "マップ名表示の変更"}}, //comment out
			{28200, new List<string>{"ERROR", "タイルセットの変更"}}, //comment out
			{28300, new List<string>{"CHANGE_RENDER", "戦闘背景の変更"}},
			{28400, new List<string>{"CHANGE_RENDER", "遠景の変更"}},
			{28500, new List<string>{"GET_TERRAIN", "地形の情報を取得"}},
			{30100, new List<string>{"BOSSBATTLE", "バトル実行と結果の確認"}},
			{30200, new List<string>{"SHOP", "お店の表示と結果の確認"}},
			{30300, new List<string>{"CHANGE_HERO_NAME", "パーティメンバーの名前を変更"}},
			{31100, new List<string>{"CHG_HPMP, HP/MPを回復／減らす"}},
			{31200, new List<string>{"CHG_HPMP, HP/MPを回復／減らす"}},
			{31300, new List<string>{"CHG_STTAILM", "状態変化にする／治す"}},
			{31400, new List<string>{"FULLRECOV", "パーティを全回復"}},
			{31500, new List<string>{"CHG_EXP", "経験値を増やす／減らす"}},
			{31600, new List<string>{"ERROR", "レベルの増減"}}, //comment out
			{31700, new List<string>{"STATUS", "キャストの能力値を上げる／下げる"}},
			{31800, new List<string>{"CHG_SKILL", "スキルを習得/忘れる"}},
			{31900, new List<string>{"EQUIP", "キャストの装備を変更する"}},
			{32000, new List<string>{"STRING_VARIABLE", "文字列変数への代入"}},
			{32100, new List<string>{"CHANGE_JOB", "職業の変更"}},
			{32200, new List<string>{"PLGRAPHIC", "キャストのグラフィックを変える"}},
			{32300, new List<string>{"ERROR", "乗り物の画像変更"}}, //comment out
			{32400, new List<string>{"ERROR", "二つ名の変更"}}, //comment out
			{32500, new List<string>{"ERROR", "プロフィールの変更"}}, //comment out
			{32600, new List<string>{"ERROR", "TPの増減"}}, //comment out
			{33100, new List<string>{"BTL_HEAL", "バトルキャストのHP・MPを回復／減らす"} },
			{33200, new List<string>{"BTL_HEAL", "バトルキャストのHP・MPを回復／減らす"} },
			{34200, new List<string>{"ERROR", "敵キャラのTP増減"}}, //comment out
			{33300, new List<string>{"BTL_STATUS", "バトルキャストを状態変化にする／治す"} },
			{33400, new List<string>{"ERROR", "敵キャラの全回復"}}, //comment out
			{33500, new List<string>{"BTL_APPEAR", "モンスターを出現させる"} },
			{33600, new List<string>{"ERROR", "敵キャラの変身"} }, //comment out
			{33700, new List<string>{"EFFECT", "エフェクトの表示"} },
			{33900, new List<string>{"BTL_ACTION", "バトルキャストの状態を指定する"} },
			{34000, new List<string>{"BTL_STOP", "バトルの強制終了"} },
			{35100, new List<string>{"SHOW_SCORE_BOARD", "メニューの表示（イベント用フリーレイアウトを表示）"}},
			{35200, new List<string>{"SAVE", "セーブ画面を表示"}},
			{35300, new List<string>{"ERROR", "ゲームオーバー"}}, //comment out
			{35400, new List<string>{"ERROR", "タイトル画面に戻る"}}, //comment out
			{35500, new List<string>{"ERROR", "スクリプト"}}, //comment out
			{35600, new List<string>{"ERROR", "プラグインコマンド"}}, //comment out
			{35700, new List<string>{"ERROR", "プラグインコマンド"}}, //comment out
			{40200, new List<string>{"BRANCH", "選択肢"}},
			{40300, new List<string>{"BRANCH", "選択肢キャンセル時"}},
			{41100, new List<string>{"ELSE", "条件分岐それ以外"}},
			{41300, new List<string>{"ENDLOOP", "ループ終了"}},
			{60200, new List<string>{"ELSE", "バトル結果：逃げた時"}},
			{60300, new List<string>{"ELSE", "バトル結果：負けた時"}},

			//route. currently player or this-event only..
			{0100, new List<string>{"PLWALK", "プレイヤー下に移動"}}, //MOVE_DOWN
			{0200, new List<string>{"PLWALK", "プレイヤー左に移動"}}, //MOVE_LEFT
			{0300, new List<string>{"PLWALK", "プレイヤー右に移動"}}, //MOVE_RIGHT
			{0400, new List<string>{"PLWALK", "プレイヤー上に移動"}}, //MOVE_UP
			{0500, new List<string>{"PLWALK", "プレイヤー左下に移動"}}, //MOVE_LOWER_L
			{0600, new List<string>{"PLWALK", "プレイヤー右下に移動"}}, //MOVE_LOWER_R
			{0700, new List<string>{"PLWALK", "プレイヤー左上に移動"}}, //MOVE_UPPER_L
			{0800, new List<string>{"PLWALK", "プレイヤー右上に移動"}}, //MOVE_UPPER_R
			{0900, new List<string>{"PLWALK", "プレイヤーランダムに移動"}}, //MOVE_RANDOM
			{1000, new List<string>{"PLWALK", "プレイヤーに近づく"}}, //MOVE_TOWARD
			{1100, new List<string>{"PLWALK", "プレイヤーから遠ざかる"}}, //MOVE_AWAY
			{1200, new List<string>{"PLWALK", "プレイヤー一歩前進"}}, //MOVE_FORWARD
			{1300, new List<string>{"PLWALK", "プレイヤー一歩後退"}}, //MOVE_BACKWARD
			{1400, new List<string>{"ADDFORCEPL", "プレイヤージャンプ(プレイヤーを物理エンジンで移動させる)"}}, //JUMP
			{1500, new List<string>{"WAIT", "ウェイト"}}, //WAIT
			{1600, new List<string>{"PLROTATE", "プレイヤー下を向く"}}, //TURN_DOWN
			{1700, new List<string>{"PLROTATE", "プレイヤー左を向く"}}, //TURN_LEFT
			{1800, new List<string>{"PLROTATE", "プレイヤー右を向く"}}, //TURN_RIGHT
			{1900, new List<string>{"PLROTATE", "プレイヤー上を向く"}}, //TURN_UP
			{2000, new List<string>{"PLROTATE", "プレイヤー右に90度回転"}}, //TURN_90D_R
			{2100, new List<string>{"PLROTATE", "プレイヤー左に90度回転"}}, //TURN_90D_L
			{2200, new List<string>{"PLROTATE", "プレイヤー右に180度回転"}}, //TURN_180D
			{2300, new List<string>{"PLROTATE", "プレイヤー左に180度回転"}}, //TURN_90D_R_L
			{2400, new List<string>{"PLROTATE", "プレイヤーランダムに方向転換"}}, //TURN_RANDOM
			{2500, new List<string>{"PLROTATE", "プレイヤーの方を向く"}}, //TURN_TOWARD
			{2600, new List<string>{"PLROTATE", "プレイヤーの逆を向く"}}, //TURN_AWAY
			{2700, new List<string>{"SWITCH", "スイッチON"}}, //SWITCH_ON
			{2800, new List<string>{"SWITCH", "スイッチOFF"}}, //SWITCH_OFF
			{2900, new List<string>{"PLWALKSPEED", "プレイヤー移動速度の変更"}}, //CHANGE_SPEED
			{3000, new List<string>{"ERROR", "プレイヤー移動頻度の変更"}},	//CHANGE_FREQ
			{3100, new List<string>{"GRAPHIC", "プレイヤー歩行アニメON"}}, //WALK_ANIME_ON
			{3200, new List<string>{"GRAPHIC", "プレイヤー歩行アニメOFF"}}, //WALK_ANIME_OFF
			{3300, new List<string>{"PLMOTION", "プレイヤー足踏みアニメON(キャストのモーションを変更)"}}, //STEP_ANIME_ON
			{3400, new List<string>{"PLMOTION", "プレイヤー足踏みアニメOFF(キャストのモーションを変更)"}}, //STEP_ANIME_OFF
			{3500, new List<string>{"SW_PLLOCKROTATE", "プレイヤー向き固定ON"}}, //DIR_FIX_ON
			{3600, new List<string>{"SW_PLLOCKROTATE", "プレイヤー向き固定OFF"}}, //DIR_FIX_OFF
			{3700, new List<string>{"CHANGE_PLAYER_MOVABLE", "プレイヤーすり抜けON"}}, //THROUGH_ON
			{3800, new List<string>{"CHANGE_PLAYER_MOVABLE", "プレイヤーすり抜けOFF"}}, //THROUGH_OFF
			{3900, new List<string>{"PLHIDE", "プレイヤー透明化ON"}}, //TRANSPARENT_ON
			{4000, new List<string>{"PLHIDE", "プレイヤー透明化OFF"}}, //TRANSPARENT_OFF
			{4100, new List<string>{"GRAPHIC", "プレイヤー画像の変更"}}, //CHANGE_IMAGE
			{4200, new List<string>{"ERROR", "プレイヤー不透明度の変更"}}, //CHANGE_OPACITY
			{4300, new List<string>{"ERROR", "プレイヤー合成方法の変更"}}, //CHANGE_BLEND_MODE
			{4400, new List<string>{"PLAYSE", "SEの演奏"}}, //PLAY_SE
			{4500, new List<string>{"ERROR", "スクリプト"}}, //SCRIPT
			
			{0101, new List<string>{"WALK", "イベント下に移動"}}, //MOVE_DOWN
			{0201, new List<string>{"WALK", "イベント左に移動"}}, //MOVE_LEFT
			{0301, new List<string>{"WALK", "イベント右に移動"}}, //MOVE_RIGHT
			{0401, new List<string>{"WALK", "イベント上に移動"}}, //MOVE_UP
			{0501, new List<string>{"WALK", "イベント左下に移動"}}, //MOVE_LOWER_L
			{0601, new List<string>{"WALK", "イベント右下に移動"}}, //MOVE_LOWER_R
			{0701, new List<string>{"WALK", "イベント左上に移動"}}, //MOVE_UPPER_L
			{0801, new List<string>{"WALK", "イベント右上に移動"}}, //MOVE_UPPER_R
			{0901, new List<string>{"WALK", "イベントランダムに移動"}}, //MOVE_RANDOM
			{1001, new List<string>{"WALK", "プレイヤーに近づく"}}, //MOVE_TOWARD
			{1101, new List<string>{"WALK", "プレイヤーから遠ざかる"}}, //MOVE_AWAY
			{1201, new List<string>{"WALK", "イベント一歩前進"}}, //MOVE_FORWARD
			{1301, new List<string>{"WALK", "イベント一歩後退"}}, //MOVE_BACKWARD
			{1401, new List<string>{"ADDFORCE", "イベントジャンプ(イベントを物理エンジンで移動させる)"}}, //JUMP
			{1601, new List<string>{"ROTATE", "イベント下を向く"}}, //TURN_DOWN
			{1701, new List<string>{"ROTATE", "イベント左を向く"}}, //TURN_LEFT
			{1801, new List<string>{"ROTATE", "イベント右を向く"}}, //TURN_RIGHT
			{1901, new List<string>{"ROTATE", "イベント上を向く"}}, //TURN_UP
			{2001, new List<string>{"ROTATE", "イベント右に90度回転"}}, //TURN_90D_R
			{2101, new List<string>{"ROTATE", "イベント左に90度回転"}}, //TURN_90D_L
			{2201, new List<string>{"ROTATE", "イベント右に180度回転"}}, //TURN_180D
			{2301, new List<string>{"ROTATE", "イベント左に180度回転"}}, //TURN_90D_R_L
			{2401, new List<string>{"ROTATE", "イベントランダムに方向転換"}}, //TURN_RANDOM
			{2501, new List<string>{"ROTATE", "プレイヤーの方を向く"}}, //TURN_TOWARD
			{2601, new List<string>{"ROTATE", "プレイヤーの逆を向く"}}, //TURN_AWAY
			{2701, new List<string>{"SWITCH", "スイッチON"}}, //SWITCH_ON
			{2801, new List<string>{"SWITCH", "スイッチOFF"}}, //SWITCH_OFF
			{2901, new List<string>{"WALKSPEED", "イベント移動速度の変更"}}, //CHANGE_SPEED
			{3001, new List<string>{"ERROR", "イベント移動頻度の変更"}},	//CHANGE_FREQ
			{3101, new List<string>{"GRAPHIC", "イベント歩行アニメON"}}, //WALK_ANIME_ON
			{3201, new List<string>{"GRAPHIC", "イベント歩行アニメOFF"}}, //WALK_ANIME_OFF
			{3301, new List<string>{"MOTION", "イベント足踏みアニメON(イベントのモーションを変更)"}}, //STEP_ANIME_ON
			{3401, new List<string>{"MOTION", "イベント足踏みアニメOFF(イベントのモーションを変更)"}}, //STEP_ANIME_OFF
			{3501, new List<string>{"SW_LOCKROTATE", "イベント向き固定ON"}}, //DIR_FIX_ON
			{3601, new List<string>{"SW_LOCKROTATE", "イベント向き固定OFF"}}, //DIR_FIX_OFF
			{3701, new List<string>{"CHANGE_MOVABLE", "イベントすり抜けON"}}, //THROUGH_ON
			{3801, new List<string>{"CHANGE_MOVABLE", "イベントすり抜けOFF"}}, //THROUGH_OFF
			{3901, new List<string>{"EVHIDE", "イベント透明化ON"}}, //TRANSPARENT_ON
			{4001, new List<string>{"EVHIDE", "イベント透明化OFF"}}, //TRANSPARENT_OFF
			{4101, new List<string>{"GRAPHIC", "イベント画像の変更"}}, //CHANGE_IMAGE
			{4201, new List<string>{"ERROR", "イベント不透明度の変更"}}, //CHANGE_OPACITY
			{4301, new List<string>{"ERROR", "イベント合成方法の変更"}}, //CHANGE_BLEND_MODE

			//commands not used in MV
			{1, new List<string>{"SPTEXT", "イメージとして画面に文字を表示する"}},
			{3, new List<string>{"CHANGE_GAMEOVER_ACTION", "ゲームオーバー時の動作指定"}},
			{4, new List<string>{"WEBBROWSER", "ウェブブラウザを表示"}},
			{5, new List<string>{"SW_CAMLOCK", "カメラ操作の許可/禁止"}},
			{6, new List<string>{"BTL_SW_CAMERA", "バトルカメラ演出の許可/禁止"}},
			{8, new List<string>{"CHANGE_PLAYER_SCALE", "プレイヤーのスケールを変更"}},
			{9, new List<string>{"JOINT_WEAPON", "主人公にモデルを取り付ける"}},
			{11, new List<string>{"INVINCIBLE", "プレイヤーを無敵にする"}},
			{12, new List<string>{"PLSNAP", "プレイヤーの位置をグリッドに吸着する"}},
			{13, new List<string>{"PLWALK_TGT", "プレイヤーを座標指定して歩かせる"}},
			{14, new List<string>{"ROTATEPL_XYZ", "プレイヤーを回転させる"}},
			{15, new List<string>{"WALK_IN_ROWS_ORDER", "隊列にキャストを追加する／順番を入れ替える"}},
			{16, new List<string>{"PLSUBGRP, player subgraphic"}},
			{17, new List<string>{"ITEM_THROW_OUT", "アイテムを捨てる"}},
			{19, new List<string>{"CHANGE_SCALE", "イベントのスケールを変更"}},
			{20, new List<string>{"EVSNAP", "イベントの位置をグリッドに吸着する"}},
			{21, new List<string>{"EVWALK_TGT", "イベントの座標を変更して歩かせる"}},
			{22, new List<string>{"ROTATE_XYZ", "イベントを回転させる。"}},
			{24, new List<string>{"SUBGRP", "イベントのサブグラフィックの表示状態変更"}},
			{26, new List<string>{"HLSTRVARIABLE", "複雑な文字列変数の操作"}},
			{27, new List<string>{"REPLACE_STRING_VARIABLE", "文字列の置き換え"}},
			{28, new List<string>{"SW_PLLOCK", "プレイヤー操作の許可/禁止"}},
			{29, new List<string>{"SW_DASH", "プレイヤーのダッシュの許可/禁止"}},
			{30, new List<string>{"SW_JUMP", "ジャンプの許可/禁止"}},
			{31, new List<string>{"INN", "宿屋の表示と結果の確認"}},
			{32, new List<string>{"SHOT_EVENT", "イベントを生成する"}},
			{33, new List<string>{"EXIT", "ゲーム終了"}},
			{34, new List<string>{"FACEEMOTION", "マップキャストの表情の変更"}},
			{35, new List<string>{"IF_INVENTORY_EMPTY", "アイテム袋の空きの確認"}},
			{36, new List<string>{"BTL_IFBATTLE", "バトル中かどうかを確認"}},
			{37, new List<string>{"COL_CONTACT", "接触状態の確認"}},
			{38, new List<string>{"COL_RAYCAST", "周囲の当たり判定の確認"}},
			{39, new List<string>{"CHANGE_PLAYER_HEIGHT", "プレイヤーのY座標を変更"}},
			{40, new List<string>{"FALL_PLAYER", "プレイヤーの落下を開始"}},
			{41, new List<string>{"CHANGE_HEIGHT", "イベントのY座標を変更"}},
			{42, new List<string>{"FALL_EVENT", "イベントの落下を開始"}},
			};

			return codePairs.Where(d => d.Key == code).FirstOrDefault().Value;
		}
		#endregion
	}
}
