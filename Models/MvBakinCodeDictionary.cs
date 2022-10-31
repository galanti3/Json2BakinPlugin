using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json2BakinPlugin.Models
			{
	public class MvBakinCodeDictionary
	{
		private Dictionary<int, string> codePairs = new Dictionary<int, string>() {
			{10100, "DIALOGUE	会話を表示"},
			{10101, "MESSAGE	メッセージを表示"},
			{10200, "CHOICES	選択肢から選んで結果を確認"},
			{10300, "CHANGE_STRING_VARIABLE	文字列入力"},
			{10400, "ITEMMENU	アイテム選択画面を表示"},
			{10500, "TELOP	テロップを表示"},
			{10800, "COMMENT	注釈"},
			{10900, "COMMENT	注釈　スキップ"}, //comment out
			{11100, "IFSWITCH	イベントスイッチの確認"},
			{11101, "IFVARIABLE	変数ボックスの確認"},
			{11102, "IFVARIABLE	変数ボックスの確認"},
			{11103, "COMMENT	注釈　条件分岐：タイマー"}, //comment out
			{11104, "IF_STRING_VARIABLE	文字列変数の確認"},
			{11105, "IFPARTY	パーティに含まれるキャストの確認"},
			{11106, "IFITEM	メンバーが持っているアイテムの確認"},
			{11107, "COMMENT	注釈　条件分岐：アクタークラス、スキル"}, //comment out
			{11108, "IFMONEY	持っているお金の確認"},
			{11109, "BTL_IFMONSTER	バトル中のモンスターのチェック"},
			{11110, "COMMENT	注釈　条件分岐：キャラクターの向き"}, //comment out
			{11111, "IFITEM	パーティが持っているアイテムの確認"},
			{11112, "IFITEM	パーティが持っている武器の確認"},
			{11113, "IFITEM	パーティが持っている防具の確認"},
			{11114, "COMMENT	注釈　条件分岐：押されているボタン"}, //comment out
			{11115, "COMMENT	注釈　条件分岐：スクリプト"}, //comment out
			{11116, "COMMENT	注釈　条件分岐：乗り物"}, //comment out
			{11200, "LOOP	ループ開始"},
			{11300, "BREAK	ループから抜け出す"},
			{11500, "END	イベント終了"},
			{11700, "EXEC	共通イベント呼び出し"},
			{11800, "COMMENT	注釈　ラベル"}, //comment out
			{11900, "COMMENT	注釈　ラベルジャンプ"}, //comment out
			{12100, "SWITCH	イベントスイッチのON/OFF"},
			{12200, "HLVARIABLE	複雑な変数ボックスの操作"},
			{12201, "HLVARIABLE	複雑な変数ボックスの操作:ゲームデータ"},
			{12202, "HLVARIABLE	複雑な変数ボックスの操作：アクター"},
			{12203, "HLVARIABLE	複雑な変数ボックスの操作：モンスター"},
			{12204, "HLVARIABLE	複雑な変数ボックスの操作：キャラクター"},
			{12205, "HLVARIABLE	複雑な変数ボックスの操作：パーティ"},
			{12206, "COMMENT	複雑な変数ボックスの操作：スクリプト"}, //comment out
			{12300, "SWITCH	セルフスイッチのON/OFF"},
			{12400, "EXEC	共通イベント呼び出し：タイマーの操作"},
			{12500, "MONEY	お金を増やす／減らす"},
			{12600, "ITEM	アイテムを増やす／減らす"},
			{12700, "ITEM	武器を増やす／減らす"},
			{12800, "ITEM	防具を増やす／減らす"},
			{12900, "PARTY	パーティにキャストを参加／外す"},
			{12901, "WALK_IN_ROWS_ORDER	隊列にキャストを追加する／順番を入れ替える"},
			{13200, "COMMENT	注釈　戦闘BGMの変更"}, //comment out
			{13300, "COMMENT	注釈　勝利MEの変更"}, //comment out
			{13400, "SW_SAVE	セーブの許可/禁止"},
			{13500, "SW_MENU	メニュー画面の表示の許可/禁止"},
			{13600, "SW_ENCOUNTING	モンスターの出現の許可/禁止"},
			{13700, "COMMENT	注釈　並び替えの許可/禁止"}, //comment out
			{13800, "CHANGE_LAYOUT	表示するレイアウトを変更"},
			{13900, "COMMENT	注釈　敗北MEの変更"}, //comment out
			{14000, "COMMENT	注釈　乗り物BGMの変更"}, //comment out
			{20100, "PLMOVE	プレイヤーを他の場所に移動"},
			{20200, "COMMENT	注釈　乗り物の位置設定"}, //comment out
			{20300, "MOVE	イベントを瞬間移動"},
			{20400, "CAM_ANIMATION	カメラの設定"},
			{20500, "COMMENT	注釈　移動ルートの設定"},
			{20600, "COMMENT	注釈　乗り物の乗降"}, //comment out
			{21100, "EVHIDE	イベントを透明にする／透明から戻す"},
			{21200, "EFFECT	エフェクトの表示"},
			{21300, "EMOTE	感情マークを表示"},
			{21400, "DESTROY_EVENT	イベント削除"},
			{21600, "WALK_IN_ROWS	隊列歩行の許可/禁止"},
			{21700, "COMMENT	注釈　隊列メンバーの集合"}, //comment out
			{22100, "SCREEN_FADE	画面を暗くする"},
			{22200, "SCREEN_FADE	画面を明るくする"},
			{22300, "SCREEN_COLOR	画面の色を変える"},
			{22400, "SCREEN_FLASH	画面をフラッシュ"},
			{22500, "SCREEN_SHAKE	画面を揺らす"},
			{23000, "WAIT	指定した時間待つ"},
			{23100, "SPPICTURE	イメージを表示"},
			{23200, "SPMOVE	イメージを移動"},
			{23300, "COMMENT	注釈　ピクチャの回転"}, //comment out
			{23400, "COMMENT	注釈　ピクチャの色調変更"}, //comment out
			{23500, "SPHIDE	イメージを消す"},
			{23600, "CHANGE_RENDER	レンダリング設定の変更"},
			{24100, "PLAYBGM	ＢＧＭを演奏"},
			{24200, "PLAYBGM	ＢＧＭをフェードアウト"},
			{24300, "COMMENT	注釈　BGMの保存"}, //comment out
			{24400, "COMMENT	注釈　BGMの再開"}, //comment out
			{24500, "PLAYBGS	環境音を設定"},
			{24600, "PLAYBGS	環境音をフェードアウト"},
			{24900, "PLAYJINGLE	ファンファーレを演奏"},
			{25000, "PLAYSE	効果音の再生"},
			{25100, "STOPSE	効果音の停止"},
			{26100, "PLAYMOVIE	ムービーの再生"},
			{28100, "COMMENT	注釈　マップ名表示の変更"}, //comment out
			{28200, "COMMENT	注釈　タイルセットの変更"}, //comment out
			{28300, "CHANGE_RENDER	戦闘背景の変更"},
			{28400, "CHANGE_RENDER	遠景の変更"},
			{28500, "GET_TERRAIN	地形の情報を取得"},
			{30100, "BOSSBATTLE	バトル実行と結果の確認"},
			{30200, "SHOP	お店の表示と結果の確認"},
			{30300, "CHANGE_HERO_NAME	パーティメンバーの名前を変更"},
			{31100, "CHG_HPMP, HP/MPを回復／減らす"},
			{31200, "CHG_HPMP, HP/MPを回復／減らす"},
			{31300, "CHG_STTAILM	状態変化にする／治す"},
			{31400, "FULLRECOV	パーティを全回復"},
			{31500, "CHG_EXP	経験値を増やす／減らす"},
			{31600, "COMMENT	注釈　レベルの増減"}, //comment out
			{31700, "STATUS	キャストの能力値を上げる／下げる"},
			{31800, "CHG_SKILL	スキルを習得/忘れる"},
			{31900, "EQUIP	キャストの装備を変更する"},
			{32000, "STRING_VARIABLE	文字列変数への代入"},
			{32100, "CHANGE_JOB	職業の変更"},
			{32200, "PLGRAPHIC	キャストのグラフィックを変える"},
			{32300, "COMMENT	注釈　乗り物の画像変更"}, //comment out
			{32400, "COMMENT	注釈　二つ名の変更"}, //comment out
			{32500, "COMMENT	注釈　プロフィールの変更"}, //comment out
			{32600, "COMMENT	注釈　TPの増減"}, //comment out
			{33100, "BTL_HEAL	バトルキャストのHP・MPを回復／減らす" },
			{33200, "BTL_HEAL	バトルキャストのHP・MPを回復／減らす" },
			{34200, "COMMENT	注釈　敵キャラのTP増減"}, //comment out
			{33300, "BTL_STATUS	バトルキャストを状態変化にする／治す" },
			{33400, "COMMENT	注釈　敵キャラの全回復"}, //comment out
			{33500, "BTL_APPEAR	モンスターを出現させる" },
			{33600, "COMMENT	注釈　敵キャラの変身" }, //comment out
			{33700, "EFFECT	エフェクトの表示" },
			{33900, "BTL_ACTION	バトルキャストの状態を指定する" },
			{34000, "BTL_STOP	バトルの強制終了" },
			{35200, "SAVE	セーブ画面を表示"},
			{35100, "COMMENT	注釈　メニューの表示"}, //comment out
			{35300, "COMMENT	注釈　ゲームオーバー"}, //comment out
			{35400, "COMMENT	注釈　タイトル画面に戻る"}, //comment out
			{35500, "COMMENT	注釈　スクリプト"}, //comment out
			{35600, "COMMENT	注釈　プラグインコマンド"}, //comment out
			{35700, "COMMENT	注釈　プラグインコマンド"}, //comment out
			{40200, "BRANCH	選択肢"},
			{40300, "BRANCH	選択肢キャンセル時"},
			{41100, "ELSE	条件分岐それ以外"},
			{41300, "ENDLOOP	ループ終了"},
			{60200, "ELSE	バトル結果：逃げた時"},
			{60300, "ELSE	バトル結果：負けた時"},

			//route. currently player or this-event only..
			{0100, "PLWALK	プレイヤー下に移動"}, //MOVE_DOWN
			{0200, "PLWALK	プレイヤー左に移動"}, //MOVE_LEFT
			{0300, "PLWALK	プレイヤー右に移動"}, //MOVE_RIGHT
			{0400, "PLWALK	プレイヤー上に移動"}, //MOVE_UP
			{0500, "PLWALK	プレイヤー左下に移動"}, //MOVE_LOWER_L
			{0600, "PLWALK	プレイヤー右下に移動"}, //MOVE_LOWER_R
			{0700, "PLWALK	プレイヤー左上に移動"}, //MOVE_UPPER_L
			{0800, "PLWALK	プレイヤー右上に移動"}, //MOVE_UPPER_R
			{0900, "PLWALK	プレイヤーランダムに移動"}, //MOVE_RANDOM
			{1000, "PLWALK	プレイヤーに近づく"}, //MOVE_TOWARD
			{1100, "PLWALK	プレイヤーから遠ざかる"}, //MOVE_AWAY
			{1200, "PLWALK	プレイヤー一歩前進"}, //MOVE_FORWARD
			{1300, "PLWALK	プレイヤー一歩後退"}, //MOVE_BACKWARD
			{1400, "ADDFORCEPL	プレイヤージャンプ(プレイヤーを物理エンジンで移動させる)"}, //JUMP
			{1500, "WAIT	ウェイト"}, //WAIT
			{1600, "PLROTATE	プレイヤー下を向く"}, //TURN_DOWN
			{1700, "PLROTATE	プレイヤー左を向く"}, //TURN_LEFT
			{1800, "PLROTATE	プレイヤー右を向く"}, //TURN_RIGHT
			{1900, "PLROTATE	プレイヤー上を向く"}, //TURN_UP
			{2000, "PLROTATE	プレイヤー右に90度回転"}, //TURN_90D_R
			{2100, "PLROTATE	プレイヤー左に90度回転"}, //TURN_90D_L
			{2200, "PLROTATE	プレイヤー右に180度回転"}, //TURN_180D
			{2300, "PLROTATE	プレイヤー左に180度回転"}, //TURN_90D_R_L
			{2400, "PLROTATE	プレイヤーランダムに方向転換"}, //TURN_RANDOM
			{2500, "PLROTATE	プレイヤーの方を向く"}, //TURN_TOWARD
			{2600, "PLROTATE	プレイヤーの逆を向く"}, //TURN_AWAY
			{2700, "SWITCH	スイッチON"}, //SWITCH_ON
			{2800, "SWITCH	スイッチOFF"}, //SWITCH_OFF
			{2900, "PLWALKSPEED	プレイヤー移動速度の変更"}, //CHANGE_SPEED
			{3000, "COMMENT	注釈　プレイヤー移動頻度の変更"},	//CHANGE_FREQ
			{3100, "GRAPHIC	プレイヤー歩行アニメON"}, //WALK_ANIME_ON
			{3200, "GRAPHIC	プレイヤー歩行アニメOFF"}, //WALK_ANIME_OFF
			{3300, "PLMOTION	プレイヤー足踏みアニメON(キャストのモーションを変更)"}, //STEP_ANIME_ON
			{3400, "PLMOTION	プレイヤー足踏みアニメOFF(キャストのモーションを変更)"}, //STEP_ANIME_OFF
			{3500, "SW_PLLOCKROTATE	プレイヤー向き固定ON"}, //DIR_FIX_ON
			{3600, "SW_PLLOCKROTATE	プレイヤー向き固定OFF"}, //DIR_FIX_OFF
			{3700, "CHANGE_PLAYER_MOVABLE	プレイヤーすり抜けON"}, //THROUGH_ON
			{3800, "CHANGE_PLAYER_MOVABLE	プレイヤーすり抜けOFF"}, //THROUGH_OFF
			{3900, "PLHIDE	プレイヤー透明化ON"}, //TRANSPARENT_ON
			{4000, "PLHIDE	プレイヤー透明化OFF"}, //TRANSPARENT_OFF
			{4100, "GRAPHIC	プレイヤー画像の変更"}, //CHANGE_IMAGE
			{4200, "COMMENT	プレイヤー不透明度の変更"}, //CHANGE_OPACITY
			{4300, "COMMENT	プレイヤー合成方法の変更"}, //CHANGE_BLEND_MODE
			{4400, "PLAYSE	SEの演奏"}, //PLAY_SE
			{4500, "COMMENT	スクリプト"}, //SCRIPT
			
			{0101, "WALK	イベント下に移動"}, //MOVE_DOWN
			{0201, "WALK	イベント左に移動"}, //MOVE_LEFT
			{0301, "WALK	イベント右に移動"}, //MOVE_RIGHT
			{0401, "WALK	イベント上に移動"}, //MOVE_UP
			{0501, "WALK	イベント左下に移動"}, //MOVE_LOWER_L
			{0601, "WALK	イベント右下に移動"}, //MOVE_LOWER_R
			{0701, "WALK	イベント左上に移動"}, //MOVE_UPPER_L
			{0801, "WALK	イベント右上に移動"}, //MOVE_UPPER_R
			{0901, "WALK	イベントランダムに移動"}, //MOVE_RANDOM
			{1001, "WALK	プレイヤーに近づく"}, //MOVE_TOWARD
			{1101, "WALK	プレイヤーから遠ざかる"}, //MOVE_AWAY
			{1201, "WALK	イベント一歩前進"}, //MOVE_FORWARD
			{1301, "WALK	イベント一歩後退"}, //MOVE_BACKWARD
			{1401, "ADDFORCE	イベントジャンプ(イベントを物理エンジンで移動させる)"}, //JUMP
			{1601, "ROTATE	イベント下を向く"}, //TURN_DOWN
			{1701, "ROTATE	イベント左を向く"}, //TURN_LEFT
			{1801, "ROTATE	イベント右を向く"}, //TURN_RIGHT
			{1901, "ROTATE	イベント上を向く"}, //TURN_UP
			{2001, "ROTATE	イベント右に90度回転"}, //TURN_90D_R
			{2101, "ROTATE	イベント左に90度回転"}, //TURN_90D_L
			{2201, "ROTATE	イベント右に180度回転"}, //TURN_180D
			{2301, "ROTATE	イベント左に180度回転"}, //TURN_90D_R_L
			{2401, "ROTATE	イベントランダムに方向転換"}, //TURN_RANDOM
			{2501, "ROTATE	プレイヤーの方を向く"}, //TURN_TOWARD
			{2601, "ROTATE	プレイヤーの逆を向く"}, //TURN_AWAY
			{2701, "SWITCH	スイッチON"}, //SWITCH_ON
			{2801, "SWITCH	スイッチOFF"}, //SWITCH_OFF
			{2901, "WALKSPEED	イベント移動速度の変更"}, //CHANGE_SPEED
			{3001, "COMMENT	注釈　イベント移動頻度の変更"},	//CHANGE_FREQ
			{3101, "GRAPHIC	イベント歩行アニメON"}, //WALK_ANIME_ON
			{3201, "GRAPHIC	イベント歩行アニメOFF"}, //WALK_ANIME_OFF
			{3301, "MOTION	イベント足踏みアニメON(イベントのモーションを変更)"}, //STEP_ANIME_ON
			{3401, "MOTION	イベント足踏みアニメOFF(イベントのモーションを変更)"}, //STEP_ANIME_OFF
			{3501, "SW_LOCKROTATE	イベント向き固定ON"}, //DIR_FIX_ON
			{3601, "SW_LOCKROTATE	イベント向き固定OFF"}, //DIR_FIX_OFF
			{3701, "CHANGE_MOVABLE	イベントすり抜けON"}, //THROUGH_ON
			{3801, "CHANGE_MOVABLE	イベントすり抜けOFF"}, //THROUGH_OFF
			{3901, "EVHIDE	イベント透明化ON"}, //TRANSPARENT_ON
			{4001, "EVHIDE	イベント透明化OFF"}, //TRANSPARENT_OFF
			{4101, "GRAPHIC	イベント画像の変更"}, //CHANGE_IMAGE
			{4201, "COMMENT	イベント不透明度の変更"}, //CHANGE_OPACITY
			{4301, "COMMENT	イベント合成方法の変更"}, //CHANGE_BLEND_MODE

			//commands not used in MV
			{1, "SPTEXT	イメージとして画面に文字を表示する"},
			{2, "SHOW_SCORE_BOARD	スコアボードの表示"},
			{3, "CHANGE_GAMEOVER_ACTION	ゲームオーバー時の動作指定"},
			{4, "WEBBROWSER	ウェブブラウザを表示"},
			{5, "SW_CAMLOCK	カメラ操作の許可/禁止"},
			{6, "BTL_SW_CAMERA	バトルカメラ演出の許可/禁止"},
			{8, "CHANGE_PLAYER_SCALE	プレイヤーのスケールを変更"},
			{9, "JOINT_WEAPON	主人公にモデルを取り付ける"},
			{11, "INVINCIBLE	プレイヤーを無敵にする"},
			{12, "PLSNAP	プレイヤーの位置をグリッドに吸着する"},
			{13, "PLWALK_TGT	プレイヤーを座標指定して歩かせる"},
			{14, "ROTATEPL_XYZ	プレイヤーを回転させる"},
			{16, "PLSUBGRP, player subgraphic"},
			{17, "ITEM_THROW_OUT	アイテムを捨てる"},
			{19, "CHANGE_SCALE	イベントのスケールを変更"},
			{20, "EVSNAP	イベントの位置をグリッドに吸着する"},
			{21, "EVWALK_TGT	イベントの座標を変更して歩かせる"},
			{22, "ROTATE_XYZ	イベントを回転させる。"},
			{24, "SUBGRP	イベントのサブグラフィックの表示状態変更"},
			{25, "VARIABLE	変数ボックスへの代入と計算"},
			{26, "HLSTRVARIABLE	複雑な文字列変数の操作"},
			{27, "REPLACE_STRING_VARIABLE	文字列の置き換え"},
			{28, "SW_PLLOCK	プレイヤー操作の許可/禁止"},
			{29, "SW_DASH	プレイヤーのダッシュの許可/禁止"},
			{30, "SW_JUMP	ジャンプの許可/禁止"},
			{31, "INN	宿屋の表示と結果の確認"},
			{32, "SHOT_EVENT	イベントを生成する"},
			{33, "EXIT	ゲーム終了"},
			{34, "FACEEMOTION	マップキャストの表情の変更"},
			{35, "IF_INVENTORY_EMPTY	アイテム袋の空きの確認"},
			{36, "BTL_IFBATTLE	バトル中かどうかを確認"},
			{37, "COL_CONTACT	接触状態の確認"},
			{38, "COL_RAYCAST	周囲の当たり判定の確認"},
			{39, "CHANGE_PLAYER_HEIGHT	プレイヤーのY座標を変更"},
			{40, "FALL_PLAYER	プレイヤーの落下を開始"},
			{41, "CHANGE_HEIGHT	イベントのY座標を変更"},
			{42, "FALL_EVENT	イベントの落下を開始"},
		};

		#region Function
		public string Code(int code)
		{
			return codePairs.Where(d => d.Key == code).FirstOrDefault().Value;
		}
		#endregion
	}
}
