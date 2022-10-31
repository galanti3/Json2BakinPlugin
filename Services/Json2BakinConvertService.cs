using json2bakinPlugin.Models;
using Json2BakinPlugin.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static Yukar.Common.Resource.NSpriteSet;
using System.Windows.Forms;
using Yukar.Common.GameData;
using Yukar.Common.Resource;
using Yukar.Common.Rom;
//using SBControl;

namespace Json2BakinPlugin.Services
{
    public class Json2BakinConvertService
    {
        public BakinCode ConvertToBakinCode(MvCode code)
        {
            BakinCode bakin = new BakinCode();
            bakin.Code = code.BakinCode;
            List<BakinParameter> p = new List<BakinParameter>();
            string command = code.BakinCode != null ? code.BakinCode.Split('\t')[0] : null;
            switch (command)
            {
                case "DIALOGUE":
                    p.Add(new BakinParameter("文字列", "表示するテキスト", code.Params.Last()));   //text
                    p.Add(new BakinParameter("整数", "ウィンドウ表示位置"));  //window pos 0=up 1=middle 2=buttom
                    //p.Add(new BakinParameter("整数", "吹き出し対象（4096：プレイヤー、4097：このイベント）"));  //bubble focus 4096=player 4097=this &
                    //p.Add(new BakinParameter("Guid", "吹き出し対象Guid"));    //bubble focus event]
                    p.Add(new BakinParameter("整数", ""));
                    p.Add(new BakinParameter("Guid", "表示するキャスト1Guid")); //cast1 sprite guid
                    p.Add(new BakinParameter("文字列", "表示するキャスト1表情"));    //cast1 face expression
                    p.Add(new BakinParameter("Guid", "表示するキャスト2Guid")); //cast2 sprite guid
                    p.Add(new BakinParameter("文字列", "表示するキャスト2表情"));    //cast2 face expression
                    p.Add(new BakinParameter("整数", "喋らせるキャスト（0：キャスト1、1：キャスト2）"));   //who's talking [0,1]
                    p.Add(new BakinParameter("整数", "キャスト1左右反転"));   //cast1 flip
                    p.Add(new BakinParameter("整数", "キャスト2左右反転"));   //cast2 flip
                    p.Add(new BakinParameter("整数", "マップの光源を使用する")); //use map light source
                    p.Add(new BakinParameter("整数", "キャスト1ビルボード"));  //cast1 bilboard
                    p.Add(new BakinParameter("整数", "キャスト2ビルボード"));  //cast2 bilboard
                    break;
                case "MESSAGE":
                    p.Add(new BakinParameter("文字列", "表示するテキスト", code.Params.Last()));   //text
                    p.Add(new BakinParameter("整数", "ウィンドウ表示位置"));  //window pos 0=up 1=middle 2=buttom
                    //p.Add(new BakinParameter("整数", "吹き出し対象（4096：プレイヤー、4097：このイベント）"));  //bubble focus 4096=player 4097=this &
                    //p.Add(new BakinParameter("Guid", "吹き出し対象Guid"));    //bubble focus event]
                    p.Add(new BakinParameter("整数", "ウィンドウを表示"));    //show window flag
                    break;
                case "TELOP":
                    p.Add(new BakinParameter("文字列", "表示するテキスト", code.Params.Last()));   //text
                    p.Add(new BakinParameter("整数", "背景（0：黒、1：画像、2：なし）"));   //background 0=black 1=picture 2=none
                    if (p.Last().Value == "1")
                    {
                        p.Add(new BakinParameter("Guid", "画像Guid"));   //if bg=picture, sprite guid
                    }
                    p.Add(new BakinParameter("整数", "テロップをスクロールさせる"));   //scroll
                    break;
                case "EFFECT":
                    p.Add(new BakinParameter("整数", "表示中心位置（0：イベント、1：プレイヤー、2：画面、3：イメージ）"));  //pos 0=this 1=player
                    p.Add(new BakinParameter("整数", "[イメージの管理番号（変数可）]"));
                    p.Add(new BakinParameter("Guid", "エフェクトGuid")); //effect guid
                    p.Add(new BakinParameter("整数", "完了するまで待つ"));    //wait complete
                    break;
                case "EMOTE":
                    p.Add(new BakinParameter("整数", "表示中心位置（0：イベント、1：プレイヤー）"));  //pos 0=this 1=player
                    p.Add(new BakinParameter("Guid", "感情マークGuid")); //emote guid
                    p.Add(new BakinParameter("整数", "完了するまで待つ"));    //wait complete
                    break;
                case "FACEEMOTION":
                    p.Add(new BakinParameter("整数", "変更対象（0：プレイヤー、1：イベント）"));    //target 0=player 1=this
                    p.Add(new BakinParameter("文字列", "簡易設定"));   //template name
                    p.Add(new BakinParameter("整数", "詳細設定：目（変数可）")); //eye id
                    p.Add(new BakinParameter("整数", "詳細設定：眉（変数可）")); //eyebrow id
                    p.Add(new BakinParameter("整数", "詳細設定：口（変数可）")); //lip id
                    break;
                case "SPPICTURE":
                    p.Add(new BakinParameter("整数", "イメージの管理番号（変数可）"));
                    p.Add(new BakinParameter("Guid", "イメージGuid"));
                    p.Add(new BakinParameter("整数", "X拡大率（変数可）"));
                    p.Add(new BakinParameter("整数", "半透明にする（無効：-1、有効：2139062143）"));
                    p.Add(new BakinParameter("整数", ""));
                    p.Add(new BakinParameter("整数", "X位置（変数可）"));
                    p.Add(new BakinParameter("整数", "Y位置（変数可）"));
                    p.Add(new BakinParameter("文字列", "モーション"));
                    p.Add(new BakinParameter("整数", "じわっと表示"));
                    p.Add(new BakinParameter("整数", "Y拡大率（変数可）"));
                    p.Add(new BakinParameter("整数", "回転（変数可）"));
                    break;
                case "SPTEXT":
                    p.Add(new BakinParameter("整数", "イメージの管理番号（変数可）"));  //image id
                    p.Add(new BakinParameter("文字列", "表示する文字列"));    //txt
                    p.Add(new BakinParameter("整数", "拡大率（変数可）"));    //zoom %
                    p.Add(new BakinParameter("整数", "文字色")); //color dec -> ARGB hex
                    p.Add(new BakinParameter("整数", ""));
                    p.Add(new BakinParameter("整数", "X位置（変数可）"));    //x pos
                    p.Add(new BakinParameter("整数", "Y位置（変数可）"));    //y pos
                    p.Add(new BakinParameter("整数", "テキスト揃え（0：左揃え、1：中央揃え、2：右揃え）"));  //text align 0=left 1=center 2=right
                    break;
                case "SPMOVE":
                    p.Add(new BakinParameter("整数", "イメージの管理番号（変数可）"));  //image id
                    p.Add(new BakinParameter("整数", "X拡大率（変数可）"));
                    p.Add(new BakinParameter("小数", "移動にかける時間（変数可）"));   //move time in sec
                    p.Add(new BakinParameter("整数", ""));
                    p.Add(new BakinParameter("整数", ""));
                    p.Add(new BakinParameter("整数", ""));
                    p.Add(new BakinParameter("整数", "X位置（変数可）"));    //xpos
                    p.Add(new BakinParameter("整数", "Y位置（変数可）"));    //ypos
                    p.Add(new BakinParameter("整数", "Y拡大率（変数可）"));
                    p.Add(new BakinParameter("整数", "完了するまで待つ"));    //wait complete
                    break;
                case "SPHIDE":
                    p.Add(new BakinParameter("整数", "イメージの管理番号（変数可）"));  //image id
                    p.Add(new BakinParameter("整数", "じわっと消す"));  //fade flag
                    break;
                case "SCREEN_FADE":
                    p.Add(new BakinParameter("小数", "変更までにかかる時間（変数可）")); //time in sec
                    p.Add(new BakinParameter("整数", "効果（0：明るくする、1：暗くする）"));  //fade 0=in 1=out
                    p.Add(new BakinParameter("整数", "完了するまで待つ"));    //wait complete
                    break;
                case "SCREEN_COLOR":
                    p.Add(new BakinParameter("小数", "変更までにかかる時間（変数可）")); //time in sec
                    p.Add(new BakinParameter("整数", "画面色")); //color dec -> ARGB hex
                    p.Add(new BakinParameter("整数", "完了するまで待つ"));    //wait complete
                    break;
                case "SCREEN_SHAKE":
                    p.Add(new BakinParameter("小数", "ゆらす時間（変数可）"));  //time in sec
                    p.Add(new BakinParameter("整数", "ゆれの強さ（0：弱、1：中、2：強）"));  //strength 0=weak 1=middle 2=strong
                    p.Add(new BakinParameter("整数", "完了するまで待つ"));    //wait complete
                    break;
                case "SCREEN_FLASH":
                    p.Add(new BakinParameter("小数", "フラッシュする時間（変数可）"));  //time in sec
                    break;
                case "SAVE":
                    break;
                case "ITEMMENU":
                    p.Add(new BakinParameter("Guid", "使用するレイアウトGuid")); //layout guid
                    break;
                case "SHOW_SCORE_BOARD":
                    p.Add(new BakinParameter("[]", ""));
                    p.Add(new BakinParameter("整数", "表示フラグ"));   //display flag
                    break;
                case "CHANGE_LAYOUT":
                    p.Add(new BakinParameter("Guid", "使用するレイアウトGuid")); //layout guid
                    break;
                case "CHANGE_GAMEOVER_ACTION":
                    p.Add(new BakinParameter("整数", "動作（0：タイトルへ戻る、1：その場で復活、2：高度な設定）"));  //0=title 1=resurrect 2=detail
                    p.Add(new BakinParameter("Guid", ""));
                    p.Add(new BakinParameter("整数", "X座標")); //xpos
                    p.Add(new BakinParameter("整数", "Z座標")); //zpos
                    p.Add(new BakinParameter("整数", "復活範囲（0：先頭のみ、1：全員）"));   //range 1=all 0=onlytop
                    p.Add(new BakinParameter("整数", "復活時のHP（‐1：1、または指定値％）"));    //hp -1=1 or in %
                    p.Add(new BakinParameter("整数", "復活時のMP（‐1：変更しない、または指定値％）"));    //mp -1=1 or in %
                    p.Add(new BakinParameter("Guid", "移動後に実行する共通イベントGuid"));    //common ev guid
                    break;
                case "PLAYMOVIE":
                    p.Add(new BakinParameter("Guid", "再生する動画Guid"));    //movie guid
                    break;
                case "CHANGE_RENDER":
                    p.Add(new BakinParameter("Guid", "レンダリングGuid"));    //render guid
                    p.Add(new BakinParameter("文字列", "レンダリング名"));    //render name
                    p.Add(new BakinParameter("文字列", "？"));
                    break;
                case "CAM_ANIMATION":
                    p.Add(new BakinParameter("Guid", "カメラGuid"));   //camera_anim guid
                    p.Add(new BakinParameter("整数", "完了するまで待つ"));    //wait complete
                    break;
                case "SW_CAMLOCK":
                    p.Add(new BakinParameter("整数", "カメラ操作の禁止"));    //camera_lock flag
                    p.Add(new BakinParameter("整数", ""));
                    p.Add(new BakinParameter("整数", "横回転操作"));   //horizontal rotate
                    p.Add(new BakinParameter("整数", "縦回転操作"));   //vertical rotate
                    p.Add(new BakinParameter("整数", "ズーム操作"));   //zoom
                    break;
                case "BTL_SW_CAMERA":
                    p.Add(new BakinParameter("整数", "バトル開始時カメラ演出")); //on_battle_start
                    p.Add(new BakinParameter("整数", "攻撃時カメラ演出"));    //on_attack
                    p.Add(new BakinParameter("整数", "スキル使用時カメラ演出")); //on_skill
                    p.Add(new BakinParameter("整数", "アイテム使用時カメラ演出"));    //on_item
                    p.Add(new BakinParameter("整数", "リザルト時カメラ演出"));  //on_result
                    break;
                case "PLMOVE":
                    p.Add(new BakinParameter("[スポット", "マップGuid|スポットID|X位置（変数可）|Y位置|Z位置（変数可）")); //map_guid|spot_id_from_1001|xpos|ypos|zpos
                    p.Add(new BakinParameter("変数", "X位置"));    //xpos type":name N=numeric, S=string, A=array&
                    p.Add(new BakinParameter("変数", "Z位置")); //zpos
                    p.Add(new BakinParameter("ローカル変数", "(x/zpos)]"));   //x/zpos]
                    p.Add(new BakinParameter("整数", "向きを指定（0：変更しない、1：上向き、2：下向き、3：左向き、4：右向き）"));  //direction 0=nochange 1=up 2=down 3=left 4=right
                    break;
                case "PLROTATE":
                    p.Add(new BakinParameter("整数", "方向（0：上、1：下、2：左、3：右、4：ランダム、5：このイベントの方、6：このイベントの逆、7：クルッと回転、8：任意の角度）"));   //0=up 1=down 2=left 3=right 4=rand 5=face 6=away 7=rotate 8=deg
                    p.Add(new BakinParameter("[整数", "角度（変数可）"));    //degree]
                    p.Add(new BakinParameter("(no round-to-4direction??)", ""));
                    break;
                case "PLWALK":
                    p.Add(new BakinParameter("整数", "方向（0：上、1：下、2：左、3：右、4：ランダム、5：このイベントの方、6：このイベントの逆、8：向いている方向、10：任意の角度）")); //0=up 1=down 2=left 3=right 4=rand 5=face 6=away 8=toward 10=deg
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
                    break;
                case "PLWALKSPEED":
                    p.Add(new BakinParameter("整数", "移動スピード（‐3～3）"));    //speed -3 to 3
                    break;
                case "PLHIDE":
                    p.Add(new BakinParameter("整数", "透明化フラグ"));  //transparent flag
                    break;
                case "PLGRAPHIC":
                    p.Add(new BakinParameter("整数", "変更するキャスト（0：指定、1：n番目）"));    //0=specify_cast 1=n-th member
                    p.Add(new BakinParameter("Guid", "指定キャストGuid"));    //cast guid
                    p.Add(new BakinParameter("整数", "パーティのn番目（n-1）"));   //member number, N-1
                    p.Add(new BakinParameter("Guid", "マップ上でのグラフィックGuid"));  //mapchip guid
                    p.Add(new BakinParameter("Guid", "レイアウト表示用グラフィックGuid"));    //layout_gra guid
                    p.Add(new BakinParameter("文字列", ""));
                    p.Add(new BakinParameter("整数", "モーションが完了するまでモーション変化させない")); //wait until motion_complete
                    break;
                case "PLMOTION":
                    p.Add(new BakinParameter("整数", ""));    //??
                    p.Add(new BakinParameter("Guid", ""));  //cast guid?
                    p.Add(new BakinParameter("整数", ""));    //??
                    p.Add(new BakinParameter("Guid", "マップ上でのグラフィックGuid"));  //mapchip_guid
                    p.Add(new BakinParameter("文字列", "モーション画像名"));   //pic name
                    p.Add(new BakinParameter("整数", "モーションが完了するまでモーション変化させない")); //wait motion complete
                    break;
                case "CHANGE_PLAYER_HEIGHT":
                    p.Add(new BakinParameter("整数", "Y座標の指定方法（2：プレイヤーの現在地のY座標から、3：Y座標の絶対値）"));   //2=relative 3=absolute
                    p.Add(new BakinParameter("小数", "変更するY座標（変数可）"));    //ychange
                    p.Add(new BakinParameter("小数", "Y座標の変更にかける時間（変数可）"));   //time
                    p.Add(new BakinParameter("整数", "移動速度（0：等速、1：加速、2：減速）"));    //speed 0=const 1=accel 2=decel
                    p.Add(new BakinParameter("整数", "Y座標変更の完了を待つ")); //wait complete
                    break;
                case "FALL_PLAYER":
                    p.Add(new BakinParameter("小数", "重力加速度（変数可）"));  //accel amount
                    p.Add(new BakinParameter("整数", "落下の完了を待つ"));    //wait complete
                    break;
                case "CHANGE_PLAYER_MOVABLE":
                    p.Add(new BakinParameter("整数", "通行可能でない地形への出入り許可"));    //through flag
                    break;
                case "CHANGE_PLAYER_SCALE":
                    p.Add(new BakinParameter("小数", "変更するスケール（変数可）"));   //scale size
                    p.Add(new BakinParameter("小数", "スケールの変更にかける時間（変数可）"));  //time in sec
                    p.Add(new BakinParameter("整数", "スケール変更の完了を待つ"));    //wait complete
                    break;
                case "JOINT_WEAPON":
                    p.Add(new BakinParameter("整数", "変更するキャスト（0：指定、1：n番目）"));    //0=specify_cast 1=n-th member
                    p.Add(new BakinParameter("Guid", "指定キャストGuid"));    //cast guid
                    p.Add(new BakinParameter("整数", "パーティのn番目（n-1）"));   //member number, N-1
                    p.Add(new BakinParameter("整数", "ジョイント（0：すべて外す、1：装備しているアイテムに応じたモデルをジョイント、2：任意のモデルをジョイント）")); //joint type 0=removeall 1=attachequipped 2=specify
                    p.Add(new BakinParameter("[整数", "任意モデル用パラメータ？"));   //for specify
                    p.Add(new BakinParameter("整数", "右手モデルジョイント（0：変更、2：変更しない）"));    //for specify right, 0=change 2=nochange
                    p.Add(new BakinParameter("[Guid", "右手モデルGuid"));    //right model guid]
                    p.Add(new BakinParameter("整数", "左手モデルジョイント（0：変更、2：変更しない）"));    //for specify left, 0=change 2=nochange
                    p.Add(new BakinParameter("[Guid", "左手モデルGuid"));    //left model guid]]
                    break;
                case "SW_PLLOCKROTATE":
                    p.Add(new BakinParameter("整数", "プレイヤーの向きの変更の禁止"));  //fix direction flag
                    break;
                case "INVINCIBLE":
                    p.Add(new BakinParameter("小数", "無敵時間（変数可）"));   //time in sec
                    p.Add(new BakinParameter("整数", "無敵中はグラフィックを点滅させる"));    //flash gra flag
                    p.Add(new BakinParameter("整数", "無敵中は体当たりで敵にダメージを与えない"));    //no damage to enemy flag
                    break;
                case "PLSNAP":
                    break;
                case "PLWALK_TGT":
                    p.Add(new BakinParameter("スポット", "マップGuid|スポットID|X位置（変数可）|Y位置|Z位置（変数可）"));  //map_guid|spot_id_from_1001|xpos|ypos|zpos
                    p.Add(new BakinParameter("整数", "移動終了時の向き（0：任意の角度、1：進行方向）"));    //0=deg 1=toward
                    p.Add(new BakinParameter("小数", "角度（変数可）")); //degree
                    p.Add(new BakinParameter("小数", "移動にかける時間（変数可）"));   //time in sec
                    p.Add(new BakinParameter("整数", "補間（0：一定、1：加速、2：減速、3：加速～減速"));   //interp 0=const 1=accel 2=decel 3=accel-decel
                    p.Add(new BakinParameter("整数", "完了するまで待つ"));    //wait complete
                    p.Add(new BakinParameter("整数", "モーションを変更しない")); //no motion change
                    p.Add(new BakinParameter("整数", "曲線補間"));    //curve interp
                    break;
                case "EQUIP":
                    p.Add(new BakinParameter("整数", "変更するキャスト（0：指定、1：n番目）"));    //0=specify_cast 1=n-th member
                    p.Add(new BakinParameter("Guid", "指定キャストGuid"));    //cast guid
                    p.Add(new BakinParameter("整数", "パーティのn番目（n-1）"));   //member number, N-1
                    p.Add(new BakinParameter("整数", "装備箇所（0：武器、1：腕防具、2：頭防具、3：体防具、4：装飾品1、5：装飾品2）"));  //part 0=weapon 1=armor 2=head 3=body 4=acces1 5=acces2
                    p.Add(new BakinParameter("Guid", "装備するアイテムGuid"));  //item guid
                    break;
                case "CHANGE_JOB":
                    p.Add(new BakinParameter("整数", "変更するキャスト（0：指定、1：n番目）"));    //0=specify_cast 1=n-th member
                    p.Add(new BakinParameter("Guid", "指定キャストGuid"));    //cast guid
                    p.Add(new BakinParameter("整数", "パーティのn番目（n-1）"));   //member number, N-1
                    p.Add(new BakinParameter("Guid", "職業Guid"));    //job guid
                    p.Add(new BakinParameter("整数", "変更対象（0：職業、1：副業）")); //object 0=job 1=subjob
                    p.Add(new BakinParameter("整数", "成長したステータスの引継ぎ"));   //inherit status flag
                    p.Add(new BakinParameter("整数", "副業を職業にする"));    //change subjob to job
                    break;
                case "ROTATEPL_XYZ":
                    p.Add(new BakinParameter("整数", "回転（0：絶対値、1：現在の向きからの相対値）")); //0=absolute 1=relative
                    p.Add(new BakinParameter("整数", "X回転（変数可）"));    //xrotate +-360
                    p.Add(new BakinParameter("整数", "Y回転（変数可）"));    //yrotate +-360
                    p.Add(new BakinParameter("整数", "Z回転（変数可）"));    //zrotate +-360
                    break;
                case "ADDFORCEPL":
                    p.Add(new BakinParameter("整数", "X物理エンジン移動（変数可）"));  //xforce
                    p.Add(new BakinParameter("整数", "Y物理エンジン移動（変数可）"));  //yforce
                    p.Add(new BakinParameter("整数", "Z物理エンジン移動（変数可）"));  //zforce
                    break;
                case "PLSUBGRP":
                    p.Add(new BakinParameter("整数", ""));    //??
                    p.Add(new BakinParameter("Guid", ""));  //??
                    p.Add(new BakinParameter("整数", ""));
                    p.Add(new BakinParameter("整数", "変更するサブグラフィックの番号（変数可）"));    //subgraphic number
                    p.Add(new BakinParameter("整数", "表示フラグ"));   //display flag
                    p.Add(new BakinParameter("小数", "変更にかける時間（変数可）"));   //time in sec
                    break;
                case "PARTY":
                    p.Add(new BakinParameter("整数", "変更するキャスト（0：指定、1：n番目）"));    //0=specify_cast 1=n-th member
                    p.Add(new BakinParameter("Guid", "指定キャストGuid"));    //cast guid
                    p.Add(new BakinParameter("整数", "パーティのn番目（n-1）"));   //member number, N-1
                    p.Add(new BakinParameter("整数", "参加状態（0：参加、1：外す）")); //0=join 1=remove
                    p.Add(new BakinParameter("整数", "前回参加時のステータスを引き継ぐ"));    //inherit previous status
                    break;
                case "CHANGE_HERO_NAME":
                    p.Add(new BakinParameter("整数", "変更するキャスト（0：指定、1：n番目）"));    //0=specify_cast 1=n-th member
                    p.Add(new BakinParameter("Guid", "指定キャストGuid"));    //cast guid
                    p.Add(new BakinParameter("整数", "パーティのn番目（n-1）"));   //member number, N-1
                    p.Add(new BakinParameter("整数", "入力可能な最大文字数（変数可）"));    //input chars number": max10
                    p.Add(new BakinParameter("整数", "ウィンドウ表示位置（0：上、1：中央、2：下）")); //windowpos 0=up 1=center 2=bottom
                    p.Add(new BakinParameter("文字列", "入力画面に表示する文字1"));   //input1
                    p.Add(new BakinParameter("文字列", "入力画面に表示する文字2"));   //input2
                    p.Add(new BakinParameter("文字列", "入力画面に表示する文字3"));
                    p.Add(new BakinParameter("文字列", "入力画面に表示する文字4"));
                    break;
                case "WALK_IN_ROWS_ORDER":
                    p.Add(new BakinParameter("整数", "隊列の人数"));   //number of casts
                    p.Add(new BakinParameter("整数", "並び替え後1番目のタイプ（0：メンバー、1：グラフィック、2：キャスト）"));    //1st 0=member 1=graphic 2=cast
                    p.Add(new BakinParameter("整数", "並び替え後1番目の現在の順番（n-1）")); //1st current order, N-1
                    p.Add(new BakinParameter("整数", "並び替え後2番目のタイプ（0：メンバー、1：グラフィック、2：キャスト）"));    //2nd 0=member 1=graphic 2=cast
                    p.Add(new BakinParameter("整数", "並び替え後2番目の現在の順番（n-1）")); //2nd current order, N-1
                    p.Add(new BakinParameter("整数", "並び替え後3番目のタイプ（0：メンバー、1：グラフィック、2：キャスト）"));    //3rd 0=member 1=graphic 2=cast
                    p.Add(new BakinParameter("整数", "並び替え後3番目の現在の順番（n-1）")); //3rd current order, N-1
                    p.Add(new BakinParameter("整数", "並び替え後4番目のタイプ（0：メンバー、1：グラフィック、2：キャスト）"));    //4th 0=member 1=graphic 2=cast
                    p.Add(new BakinParameter("整数", "並び替え後4番目の現在の順番（n-1）")); //4th current order, N-1
                    p.Add(new BakinParameter("[整数", "並び替え後5番目のタイプ（0：メンバー、1：グラフィック、2：キャスト）"));   //5th 0=member 1=graphic 2=cast
                    p.Add(new BakinParameter("Guid", "並び替え後5番目のGuid")); //added cast guid]
                    break;
                case "ITEM":
                    p.Add(new BakinParameter("Guid", "アイテムGuid"));  //item guid
                    p.Add(new BakinParameter("整数", ""));    //??
                    p.Add(new BakinParameter("整数", "個数（変数可）")); //amount
                    p.Add(new BakinParameter("整数", "変化（1：増やす、2：減らす）")); //1=increase 2=decrease
                    break;
                case "ITEM_THROW_OUT":
                    break;
                case "MONEY":
                    p.Add(new BakinParameter("整数", ""));    //??
                    p.Add(new BakinParameter("整数", "金額（変数可）")); //amount
                    p.Add(new BakinParameter("整数", "変化（1：増やす、2：減らす）")); //1=increase 2=decrease
                    break;
                case "CHG_HPMP":
                    p.Add(new BakinParameter("整数", "変更するキャスト（0：指定、1：n番目）"));    //0=specify_cast 1=n-th member
                    p.Add(new BakinParameter("Guid", "指定キャストGuid"));    //cast guid
                    p.Add(new BakinParameter("整数", "パーティのn番目（n-1）"));   //member number, N-1
                    p.Add(new BakinParameter("整数", "効果対象（0：HP、1：MP）")); //0=hp 1=mp
                    p.Add(new BakinParameter("整数", "数値（変数可）")); //amount
                    p.Add(new BakinParameter("整数", "変化（0：増やす、1：減らす）")); //0=increase 1=decrease
                    break;
                case "FULLRECOV":
                    break;
                case "CHG_EXP":
                    p.Add(new BakinParameter("整数", ""));    //fix??
                    p.Add(new BakinParameter("Guid", "変更するキャストGuid、空の場合パーティ全体"));   //cast guid, all if 0
                    p.Add(new BakinParameter("整数", "パーティのn番目（n-1）"));   //member number, N-1
                    p.Add(new BakinParameter("整数", "経験値（変数可）"));    //amount
                    p.Add(new BakinParameter("整数", "変化（0：増やす、1：減らす）")); //0=increase 1=decrease
                    break;
                case "CHG_SKILL":
                    p.Add(new BakinParameter("整数", "変更するキャスト（0：指定、1：n番目）"));    //0=specify_cast 1=n-th member
                    p.Add(new BakinParameter("Guid", "指定キャストGuid"));    //cast guid
                    p.Add(new BakinParameter("整数", "パーティのn番目（n-1）"));   //member number, N-1
                    p.Add(new BakinParameter("Guid", "変更するスキルGuid"));   //skill guid
                    p.Add(new BakinParameter("整数", "状態（0：習得、1：忘れる）"));  //0=get 1=forget
                    break;
                case "STATUS":
                    p.Add(new BakinParameter("整数", "変更するキャスト（0：指定、1：n番目）"));    //0=specify_cast 1=n-th member
                    p.Add(new BakinParameter("Guid", "指定キャストGuid"));    //cast guid
                    p.Add(new BakinParameter("整数", "パーティのn番目（n-1）"));   //member number, N-1
                    p.Add(new BakinParameter("整数", "効果対象（0：最大HP、1：最大MP、2：攻撃力、3：防御力、4：魔力、5：素早さ）"));  //0=maxhp 1=maxmp 2=attack 3=defence 4=magic 5=speed
                    p.Add(new BakinParameter("整数", "数値（変数可）")); //amount
                    p.Add(new BakinParameter("整数", "変化（0：上げる、1：下げる）")); //0=increase 1=decrease
                    break;
                case "CHG_STTAILM":
                    p.Add(new BakinParameter("整数", "変更するキャスト（0：指定、1：n番目）"));    //0=specify_cast 1=n-th member
                    p.Add(new BakinParameter("Guid", "指定キャストGuid"));    //cast guid, all if 0
                    p.Add(new BakinParameter("整数", "パーティのn番目（n-1）"));   //member number, N-1
                    p.Add(new BakinParameter("Guid", "状態変化Guid"));  //state guid
                    p.Add(new BakinParameter("整数", "状態（0：状態変化にする、1：状態変化を治す）")); //0=add 1=remove
                    break;
                case "MOVE":
                    p.Add(new BakinParameter("[スポット", "マップGuid|スポットID|X位置（変数可）|Y位置|Z位置（変数可）")); //map_guid|spot_id_from_1001|xpos|ypos|zpos
                    p.Add(new BakinParameter("変数", "X位置"));    //xpos type":name N=numeric, S=string, A=array&
                    p.Add(new BakinParameter("変数", "Z位置")); //zpos
                    p.Add(new BakinParameter("ローカル変数", "(x/zpos)]"));   //x/zpos]
                    p.Add(new BakinParameter("Guid", "イベントGuid"));  //event guid
                    p.Add(new BakinParameter("整数", "向きを指定（0：変更しない、1：上向き、2：下向き、3：左向き、4：右向き）"));  //direction 0=nochange 1=up 2=down 3=left 4=right
                    break;
                case "ROTATE":
                    p.Add(new BakinParameter("整数", "方向（0：上、1：下、2：左、3：右、4：ランダム、5：このイベントの方、6：このイベントの逆、7：クルッと回転、8：任意の角度）"));   //0=up 1=down 2=left 3=right 4=rand 5=face 6=away 7=rotate 8=deg
                    p.Add(new BakinParameter("[整数", "角度（変数可）"));    //degree]
                    p.Add(new BakinParameter("整数", ""));    //round-to-4direction??
                    break;
                case "WALK":
                    p.Add(new BakinParameter("整数", "方向（0：上、1：下、2：左、3：右、4：ランダム、5：このイベントの方、6：このイベントの逆、8：向いている方向、10：任意の角度）")); //0=up 1=down 2=left 3=right 4=rand 5=face 6=away 8=toward 10=deg
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
                    break;
                case "WALKSPEED":
                    p.Add(new BakinParameter("整数", "移動スピード（‐3～3）"));    //speed -3 to 3
                    break;
                case "EVHIDE":
                    p.Add(new BakinParameter("整数", "透明化フラグ"));  //transparent flag
                    break;
                case "GRAPHIC":
                    p.Add(new BakinParameter("Guid", "キャストGuid"));  //mapchip guid
                    p.Add(new BakinParameter("文字列", "モーション画像名"));
                    break;
                case "MOTION":
                    p.Add(new BakinParameter("Guid", "キャストGuid"));  //mapchip guid
                    p.Add(new BakinParameter("文字列", "モーション画像名"));   //pic name
                    break;
                case "CHANGE_HEIGHT":
                    p.Add(new BakinParameter("整数", "Y座標の指定方法（2：プレイヤーの現在地のY座標から、3：Y座標の絶対値）"));   //2=relative 3=absolute
                    p.Add(new BakinParameter("小数", "変更するY座標（変数可）"));    //ychange
                    p.Add(new BakinParameter("小数", "Y座標の変更にかける時間（変数可）"));   //time
                    p.Add(new BakinParameter("整数", "移動速度（0：等速、1：加速、2：減速）"));    //speed 0=const 1=accel 2=decel
                    p.Add(new BakinParameter("整数", "Y座標変更の完了を待つ")); //wait complete
                    break;
                case "FALL_EVENT":
                    p.Add(new BakinParameter("小数", "重力加速度（変数可）"));  //accel amount
                    p.Add(new BakinParameter("整数", "落下の完了を待つ"));    //wait complete
                    break;
                case "CHANGE_MOVABLE":
                    p.Add(new BakinParameter("整数", "通行可能でない地形への出入り許可"));    //through flag
                    break;
                case "CHANGE_SCALE":
                    p.Add(new BakinParameter("小数", "変更するスケール（変数可）"));   //scale size
                    p.Add(new BakinParameter("小数", "スケールの変更にかける時間（変数可）"));  //time in sec
                    p.Add(new BakinParameter("整数", "スケール変更の完了を待つ"));    //wait complete
                    break;
                case "EVSNAP":
                    break;
                case "EVWALK_TGT":
                    p.Add(new BakinParameter("スポット", "マップGuid|スポットID|X位置（変数可）|Y位置|Z位置（変数可）"));  //map_guid|spot_id_from_1001|xpos|ypos|zpos
                    p.Add(new BakinParameter("整数", "移動終了時の向き（0：任意の角度、1：進行方向）"));    //0=deg 1=toward
                    p.Add(new BakinParameter("小数", "角度（変数可）")); //degree
                    p.Add(new BakinParameter("小数", "移動にかける時間（変数可）"));   //time in sec
                    p.Add(new BakinParameter("整数", "補間（0：一定、1：加速、2：減速、3：加速～減速"));   //interp 0=const 1=accel 2=decel 3=accel-decel
                    p.Add(new BakinParameter("整数", "完了するまで待つ"));    //wait complete
                    p.Add(new BakinParameter("整数", "モーションを変更しない")); //no motion change
                    p.Add(new BakinParameter("整数", "曲線補間"));    //curve interp
                    break;
                case "ROTATE_XYZ":
                    p.Add(new BakinParameter("整数", "回転（0：絶対値、1：現在の向きからの相対値）")); //0=absolute 1=relative
                    p.Add(new BakinParameter("整数", "X回転（変数可）"));    //xrotate +-360
                    p.Add(new BakinParameter("整数", "Y回転（変数可）"));    //yrotate +-360
                    p.Add(new BakinParameter("整数", "Z回転（変数可）"));    //zrotate +-360
                    break;
                case "ADDFORCE":
                    p.Add(new BakinParameter("整数", "X物理エンジン移動（変数可）"));  //xforce
                    p.Add(new BakinParameter("整数", "Y物理エンジン移動（変数可）"));  //yforce
                    p.Add(new BakinParameter("整数", "Z物理エンジン移動（変数可）"));  //zforce
                    break;
                case "SUBGRP":
                    p.Add(new BakinParameter("整数", "変更するサブグラフィックの番号（変数可）"));    //subgraphic number
                    p.Add(new BakinParameter("整数", "表示フラグ"));   //display flag
                    p.Add(new BakinParameter("小数", "変更にかける時間（変数可）"));   //time in sec
                    break;
                case "PLAYBGM":
                    p.Add(new BakinParameter("Guid", "BGMのGuid"));  //bgm guid
                    p.Add(new BakinParameter("整数", "ボリューム"));   //vol
                    p.Add(new BakinParameter("整数", "テンポ")); //tempo
                    p.Add(new BakinParameter("整数", ""));
                    p.Add(new BakinParameter("小数", "フェードアウト時間（変数可）"));  //fadeout time
                    p.Add(new BakinParameter("小数", "フェードイン時間（変数可）"));   //fadein time
                    break;
                case "PLAYBGS":
                    p.Add(new BakinParameter("Guid", "BGSのGuid"));  //bgs guid
                    p.Add(new BakinParameter("整数", "ボリューム"));   //vol
                    p.Add(new BakinParameter("整数", "テンポ")); //tempo 50-200
                    p.Add(new BakinParameter("整数", ""));
                    p.Add(new BakinParameter("小数", "フェードアウト時間（変数可）"));  //fadeout time
                    p.Add(new BakinParameter("小数", "フェードイン時間（変数可）"));   //fadein time
                    break;
                case "PLAYSE":
                    p.Add(new BakinParameter("Guid", "BGSのGuid"));  //se guid
                    p.Add(new BakinParameter("整数", "ボリューム"));   //vol
                    p.Add(new BakinParameter("整数", "テンポ")); //tempo
                    p.Add(new BakinParameter("整数", "3Dサウンドとして再生")); //3d sound flag
                    break;
                case "STOPSE":
                    p.Add(new BakinParameter("Guid", "停止するSEのGuid、空の場合はすべてのSE"));   //stop audio guid, all if 0
                    break;
                case "PLAYJINGLE":
                    p.Add(new BakinParameter("Guid", "MEのGuid"));   //fanfare guid
                    p.Add(new BakinParameter("整数", "ボリューム"));   //vol
                    p.Add(new BakinParameter("整数", "テンポ")); //tempo
                    p.Add(new BakinParameter("整数", "終わるまで待つ")); //wait complete
                    break;
                case "VARIABLE":
                    p.Add(new BakinParameter("整数", ""));    //?
                    p.Add(new BakinParameter("変数", "変数ボックスの番号"));  //type":name N=numeric, S=string, A=array
                    p.Add(new BakinParameter("整数", ""));    //?
                    p.Add(new BakinParameter("整数", "数値（変数可）"));
                    p.Add(new BakinParameter("整数", "計算（0：代入、1：足す、2：引く、3：かける、4：割る、5：乱数を足す（0～数値））")); //0=overwrite 1=add 2=sub 3=mult 4=div 5=addrand
                    break;
                case "HLVARIABLE":
                    p.Add(new BakinParameter("整数", ""));
                    p.Add(new BakinParameter("変数", "変数ボックスの番号"));  //type":name N=numeric, S=string, A=array
                    p.Add(new BakinParameter("整数", "データタイプ"));
                    p.Add(new BakinParameter("整数", "数値"));  //value
                    p.Add(new BakinParameter("整数", "計算（0：代入、1：足す、2：引く、3：かける、4：割る、6：割った余りを代入、7：小数点以下を切り捨てて代入）"));    //0=overwrite 1=add 2=sub 3=mult 4=div 6=mod 7=floor
                    break;
                case "STRING_VARIABLE":
                    p.Add(new BakinParameter("変数", "文字列変数の番号"));   //type":name N=numeric, S=string, A=array
                    p.Add(new BakinParameter("文字列 (value)", "文字列変数の番号"));
                    p.Add(new BakinParameter("整数", "代入（0：上書き、1：先頭に追加、2：最後尾に追加）"));  //0=overwrite 1=addfirst 2=addlast
                    break;
                case "CHANGE_STRING_VARIABLE":
                    p.Add(new BakinParameter("変数", "文字列変数の番号"));   //type":name N=numeric, S=string, A=array
                    p.Add(new BakinParameter("整数", "入力可能な最大文字数（変数可）")); //maxchar
                    p.Add(new BakinParameter("整数", "ウィンドウ表示位置（0：上、1：中央、2：下）")); //pos 0=top 1=center 2=bottom
                    p.Add(new BakinParameter("文字列", "入力画面に表示する文字1"));   //input1
                    p.Add(new BakinParameter("文字列", "入力画面に表示する文字2"));   //input2
                    p.Add(new BakinParameter("文字列", "入力画面に表示する文字3"));
                    p.Add(new BakinParameter("文字列", "入力画面に表示する文字4"));
                    break;
                case "HLSTRVARIABLE":
                    p.Add(new BakinParameter("変数", "文字列変数の番号"));   //to; type":name N=numeric, S=string, A=array
                    p.Add(new BakinParameter("整数", "タイプ（0：文字列変数の内容、1：現在のマップ名、2：キャストのステータス）"));  //0=string var 1=current map name 2=cast status
                    p.Add(new BakinParameter("[Guid", "キャストGuid")); //cast guid
                    p.Add(new BakinParameter("(none for current map name)", ""));
                    p.Add(new BakinParameter("変数", "代入元の文字列変数の番号"));   //from; type":name N=numeric, S=string, A=array]
                    p.Add(new BakinParameter("[変数", "キャストのステータス（0：名前、1：職業、2：副業、3：武器、4：腕防具、5：頭防具、6：体防具、7：装飾品1、8：装飾品2）"));    //cast status 0=name 1=job 2=subjob 3=weapon 4=armor 5=head 6=body 7=acces1 8=acces2]
                    p.Add(new BakinParameter("整数", "代入（0：上書き、1：先頭に追加、2：最後尾に追加）"));  //0=overwrite 1=addfirst 2=addlast
                    break;
                case "REPLACE_STRING_VARIABLE":
                    p.Add(new BakinParameter("変数", "文字列変数の番号"));   //to; type":name N=numeric, S=string, A=array
                    p.Add(new BakinParameter("文字列", "置き換え前の文字列")); //from
                    p.Add(new BakinParameter("文字列", "置き換え後の文字列")); //to
                    break;
                case "GET_TERRAIN":
                    p.Add(new BakinParameter("整数", "取得する座標（0：プレイヤーの現在位置、1：イベントの現在位置、2：変数で指定）"));    //0=currentpos 1=eventpos 2=var
                    p.Add(new BakinParameter("[(none for currentpos)", ""));
                    p.Add(new BakinParameter("Guid", "イベントGuid"));  //event guid
                    p.Add(new BakinParameter("変数", "X座標用変数の番号"));   //xpos &
                    p.Add(new BakinParameter("変数", "Y座標用変数の番号"));   //ypos]
                    p.Add(new BakinParameter("整数", "取得情報（0：地形のリソース名、1：地形の高さ）"));    //0=land res name 1=height
                    p.Add(new BakinParameter("変数", "取得先変数の番号"));   //to; type":name N=numeric, S=string, A=array
                    break;
                case "SWITCH":
                    p.Add(new BakinParameter("変数", "イベントスイッチ名"));  //to; type":name N=numeric, S=string, A=array
                    p.Add(new BakinParameter("整数", "状態(0：オン、1：オフ）"));   //0=on 1=off
                    break;
                case "SW_PLLOCK":
                    p.Add(new BakinParameter("整数", "プレイヤー捜査の禁止"));  //control disable flag
                    break;
                case "SW_DASH":
                    p.Add(new BakinParameter("整数", "プレイヤーのダッシュの禁止"));   //dash disable flag
                    break;
                case "WALK_IN_ROWS":
                    p.Add(new BakinParameter("整数", "隊列歩行の許可")); //walk row flag
                    break;
                case "SW_ENCOUNTING":
                    p.Add(new BakinParameter("整数", "モンスターの出現の禁止")); //monster disable flag
                    break;
                case "SW_MENU":
                    p.Add(new BakinParameter("整数", "メニュー画面の表示の禁止"));    //menu disable flag
                    break;
                case "SW_SAVE":
                    p.Add(new BakinParameter("整数", "セーブの禁止"));  //save disable flag
                    break;
                case "SW_JUMP":
                    p.Add(new BakinParameter("整数", "ジャンプの禁止")); //jump disable flag
                    break;
                case "CHOICES":
                    p.Add(new BakinParameter("整数", "選択肢の数"));   //num of choices
                    p.Add(new BakinParameter("文字列", "選択肢nのラベル"));   //choice1
                    p.Add(new BakinParameter("文字列", "(choice2)"));  //choice2
                    p.Add(new BakinParameter("[文字列", "(choice3...)]")); //choice3...]
                    p.Add(new BakinParameter("整数", "選択肢の位置（0：左上、1：上、2：右上、3：左、4：中央、5：右、6：左下、7：下、8：右下）"));    //pos 0=upleft 1=up 2=upright 3=left 4=center 5=right 6=botleft 7=bottom 8=botright
                    break;
                case "BRANCH":
                    p.Add(new BakinParameter("整数", "選択肢n番号（n-1）")); //choice N-1
                    break;
                case "BOSSBATTLE":
                    p.Add(new BakinParameter("整数", "モンスターの数")); //num of monsters
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
                    break;
                case "SHOP":
                    p.Add(new BakinParameter("整数", "アイテムの数"));  //num of items
                    p.Add(new BakinParameter("Guid", "アイテムnのGuid"));    //item1 guid
                    p.Add(new BakinParameter("[(item2 guid..)]", ""));
                    p.Add(new BakinParameter("整数", "アイテムnの価格"));    //item1 price
                    p.Add(new BakinParameter("[(item2 price..)]", ""));
                    p.Add(new BakinParameter("整数", "選択肢の位置（2130706432-N　0：左上、1：上、2：右上、3：左、4：中央、5：右、6：左下、7：下、8：右下）"));   //pos 2130706432-N 0=upleft 1=up 2=upright 3=left 4=center 5=right 6=botleft 7=bottom 8=botright
                    break;
                case "INN":
                    p.Add(new BakinParameter("[変数", "宿泊料（変数可）"));   //price in var
                    p.Add(new BakinParameter("整数", "(absolute"));   //absolute price]
                    p.Add(new BakinParameter("整数", "状態変化を回復")); //recover state flag
                    p.Add(new BakinParameter("整数", "先頭不能を回復")); //recover dead flag
                    p.Add(new BakinParameter("整数", "選択肢の位置（0：左上、1：上、2：右上、3：左、4：中央、5：右、6：左下、7：下、8：右下）"));    //pos 0=upleft 1=up 2=upright 3=left 4=center 5=right 6=botleft 7=bottom 8=botright
                    break;
                case "IFSWITCH":
                    p.Add(new BakinParameter("変数", "イベントスイッチ名"));  //type":name N=numeric, S=string, A=array
                    p.Add(new BakinParameter("整数", "条件（0：オン、1：オフ）"));   //0=on 1=off
                    break;
                case "IFVARIABLE":
                    p.Add(new BakinParameter("変数", "変数名"));    //type":name N=numeric, S=string, A=array
                    p.Add(new BakinParameter("[変数", "数値（変数可）"));   //type":name N=numeric, S=string, A=array
                    p.Add(new BakinParameter("整数", ""));
                    p.Add(new BakinParameter("整数", "比較条件（0：=、1：>=、2：<=、3：!=、4：>、5：<）"));    //0== 1=>= 2=<= 3=!= 4=> 5=<
                    break;
                case "IF_STRING_VARIABLE":
                    p.Add(new BakinParameter("変数", "文字列変数名")); //type":name N=numeric, S=string, A=array
                    p.Add(new BakinParameter("文字列", "比較文字列"));  //string
                    p.Add(new BakinParameter("整数", "比較条件（0：同じ、1：先頭が同じ、2：最後尾が同じ、3：含む）"));    //0=equal 1=startwith 2=endwith 3=include
                    break;
                case "IFPARTY":
                    p.Add(new BakinParameter("Guid", "チェックするキャストGuid"));    //member guid
                    p.Add(new BakinParameter("整数", "条件（0：パーティにいる、1：パーティにいない）"));    //0=with 1=without
                    break;
                case "IFITEM":
                    p.Add(new BakinParameter("Guid", "チェックするアイテムGuid"));    //item guid
                    p.Add(new BakinParameter("[変数", "個数（変数可）"));   //type":name N=numeric, S=string, A=array
                    p.Add(new BakinParameter("整数", ""));
                    p.Add(new BakinParameter("整数", "条件（0：持っている、1：持っていない）"));    //0=having 1=not having
                    p.Add(new BakinParameter("整数", "装備中のアイテムを含めない"));   //exclude equipped flag
                    break;
                case "IFMONEY":
                    p.Add(new BakinParameter("整数", "チェックする金額（変数可）"));   //amount
                    p.Add(new BakinParameter("整数", "条件（0：持っている、1：持っていない）"));    //0=having 1=not having
                    break;
                case "IF_INVENTORY_EMPTY":
                    p.Add(new BakinParameter("[変数", "アイテム袋空き数（変数可）")); //type":name N=numeric, S=string, A=array
                    p.Add(new BakinParameter("整数", ""));
                    break;
                case "COL_CONTACT":
                    p.Add(new BakinParameter("整数", "チェック元（0：プレイヤー、1：このイベント）")); //0=player 1=this
                    p.Add(new BakinParameter("整数", "チェック先（0：地形（着地状態かどうか）、1：物体、2：プレイヤー、3：イベント"));    //0=land 1=obj 2=player 3=event
                    p.Add(new BakinParameter("整数", "接触したチェック先の名称を取得")); //get contact name flag
                    p.Add(new BakinParameter("[変数", "何個目を取得するか（変数可）"));    //type":name N=numeric, S=string, A=array
                    p.Add(new BakinParameter("整数", "(N-th"));   //N-th obj]
                    p.Add(new BakinParameter("変数", "代入先変数名")); //name to; type":name N=numeric, S=string, A=array
                    break;
                case "COL_RAYCAST":
                    p.Add(new BakinParameter("整数", "チェック元（0：プレイヤー、1：このイベント）")); //0=player 1=this
                    p.Add(new BakinParameter("整数", "チェック先（0：地形、1：物体、2：プレイヤー、3：イベント"));  //0=land 1=obj 2=player 3=event
                    p.Add(new BakinParameter("整数", "向き（0：正面z+、1：左x-、2：右x+、3：上y+、4：下y-、5：後方z-、6：任意の角度）"));   //0=z+ 1=x- 2=x+ 3=y+ 4=y- 5=z- 6=deg
                    p.Add(new BakinParameter("小数", "何マス先までチェックするか（変数可）"));  //distance
                    p.Add(new BakinParameter("[変数", "角度X（変数可）"));  //type":name N=numeric, S=string, A=array
                    p.Add(new BakinParameter("小数", "(xdegree)]"));  //xdegree]
                    p.Add(new BakinParameter("[変数", "角度Y（変数可）"));  //type":name N=numeric, S=string, A=array
                    p.Add(new BakinParameter("小数", "(ydegree)]"));  //ydegree]
                    p.Add(new BakinParameter("整数", "基準（0：ローカル、1：ワールド）"));   //0=local 1=world
                    break;
                case "ELSE":
                    break;
                case "ENDIF":
                    break;
                case "WAIT":
                    p.Add(new BakinParameter("[変数", "待ち時間（変数可）")); //type":name N=numeric, S=string, A=array
                    p.Add(new BakinParameter("小数", "(time)]")); //time]
                    p.Add(new BakinParameter("整数", ""));    //?
                    break;
                case "LOOP":
                    break;
                case "ENDLOOP":
                    break;
                case "BREAK":
                    break;
                case "END":
                    break;
                case "EXEC":
                    p.Add(new BakinParameter("Guid", "共通イベントGuid"));    //common event guid
                    p.Add(new BakinParameter("整数", "実行先のイベントが完了するまで待つ"));   //wait complete
                    break;
                case "SHOT_EVENT":
                    p.Add(new BakinParameter("Guid", "生成されるイベントGuid")); //generated event guid
                    p.Add(new BakinParameter("整数", "発射元（0：プレイヤー、1：このイベント）"));   //from 0=player 1=this
                    p.Add(new BakinParameter("[整数", "0度の基準（0：下方向、1：向いている方向、2：生成元から見たプレイヤー、3：生成元から見たこのイベント、4＋：各イベント）")); //0deg base 0=down 1=face 2=toplayer 3=tothis
                    p.Add(new BakinParameter("Guid", "発射先イベントGuid"));   //toevent guid]
                    p.Add(new BakinParameter("[変数", "角度（変数可）"));   //type":name N=numeric, S=string, A=array
                    p.Add(new BakinParameter("整数", "(degree)]"));   //degree]
                    p.Add(new BakinParameter("整数", "発射数（変数可）"));    //num of shots
                    p.Add(new BakinParameter("整数", "生成ごとにばらす角度（変数可）")); //each shift deg +-180
                    p.Add(new BakinParameter("整数", "角度のランダム幅（変数可）"));   //deg rand 0-360
                    p.Add(new BakinParameter("小数", "生成ごとの待ち時間（変数可）"));  //interval in sec
                    p.Add(new BakinParameter("整数", "発射完了を待つ")); //wait complete shot
                    break;
                case "DESTROY_EVENT":
                    break;
                case "EXIT":
                    p.Add(new BakinParameter("整数", "終了方法（0：タイトル画面に戻る、1：ゲームオーバー）")); //0=totitle 1=gameover
                    break;
                case "BTL_HEAL":
                    break;
                case "BTL_STATUS":
                    break;
                case "BTL_APPEAR":
                    break;
                case "BTL_ACTION":
                    break;
                case "BTL_STOP":
                    break;
                case "COMMENT":
                    p.Add(new BakinParameter("文字列", "テキスト", code.Params.Last()));
                    break;
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

    }
}
