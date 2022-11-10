<h1 align="center">
<!--  <br>
  <a href="http://www.amitmerchant.com/electron-markdownify"><img src="https://raw.githubusercontent.com/amitmerchant1990/electron-markdownify/master/app/img/markdownify.png" alt="Markdownify" width="200"></a>
  <br> -->
  Json2BakinPlugin
  <br>
</h1>

<h4 align="center">An <a href="https://rpgbakin.com/en" target="_blank">RPG Developer Bakin</a> plugin to convert RPG Maker MV (MZ) events to Bakin events.</h4>
<h4 align="center">RPGツクールMV(MZ)イベントをBakinイベントに変換する<a href="https://rpgbakin.com/en" target="_blank">RPG Developer Bakin</a>用プラグイン</h4>

[![MIT license](https://img.shields.io/badge/License-MIT-blue.svg)](https://lbesson.mit-license.org/)
[![Twitter Followers](https://badgen.net/twitter/follow/galanti3)](https://twitter.com/galanti3)
[![Buymeacoffee](https://badgen.net/badge/icon/buymeacoffee?icon=buymeacoffee&label)](https://www.buymeacoffee.com/galanti3)
  
<h4 align="center">English</h4>
<p align="center">
  <a href="#key-features">Key Features</a> •
  <a href="#download">Download</a> •
  <a href="#how-to-use">How To Use</a> •
  <a href="#variables-and-switches">Variables And Switches</a> •
  <a href="#timer">Timer</a> •
  <a href="#limitations">Limitations</a> •
  <a href="#command-conversion-list">Command Conversion List</a> •
  <a href="#credits">Credits</a> •
  <a href="#license">License</a>
</p>
<h4 align="center">日本語</h4>
<p align="center">
  <a href="#key-features">おもな機能</a> •
  <a href="#download">ダウンロード</a> •
  <a href="#how-to-use">使い方</a> •
  <a href="#variables-and-switches">変数とスイッチ</a> •
  <a href="#timer">タイマー</a> •
  <a href="#limitations">制約</a> •
  <a href="#command-conversion-list">コマンド変換リスト</a> •
  <a href="#credits">クレジット</a> •
  <a href="#license">ライセンス</a>
</p>

<!-- ![screenshot](https://raw.githubusercontent.com/amitmerchant1990/electron-markdownify/master/app/img/markdownify.gif) -->

## Key Features

* Exports Bakin-readable command files converted from RPG Maker MV event data
* Converts common events
* Converts RPG Maker MZ events (preliminary)
* Exports unsupported RPG Maker commands as comments
* Event-wise conversion: all pages in an MV event can be imported to a Bakin event at once.
* Adds an alert comment prior to a command which requires modification
* Human-friendly: Appends a human-readable description to each Bakin command and parameter (currently Japanese only)

## Download

You can download Json2BakinPlugin dll file and dependent dlls from [here](https://github.com/galanti3/Json2BakinPlugin/releases/tag/v1.2.0).

## How To Use

1. Extract downloaded zip file. Place Json2BakinPlugin folder under the Plugin folder of RPG Developer Bakin installation. 
  > **Note**
  > If you installed RPG Develper Bakin via Steam, the Plugin folder can be usually found at C:\Program FIles\Steam\..

2. Start RPG Maker Bakin and open any game project.

3. From the menu bar of the map editor, navigate to Functions > Extentions > "Json - Bakin Conversion Plugin" and open the plugin.

4. Select RPG Maker MV (MZ) data folder containing Map, CommonEvent and related data
  > **Note**
  > The name of the selected folder should be "data", which contains all your game project data in RPG Maker architecture.

5. Select arbitorary folder for storing exported Bakin readable event files.

6. If you don't want to add modification alert comments to Bakin commands and/or don't want to export non-converted commands as Bakin comments, unselect corresponding checkbox(es).
  
7. Press "Convert" button. Converted Bakin-readable command files (.txt) will be exported to the folder you specified.
  
8. Close the plugin. On the map editor, select an event and open the event sheet editor.
  
9. Press "Import" button on the upper right corner of the editor. Select a Bakin command file you want to import.

10. The command edit window turns empty. Confirm that new sheet(s) appear on the sheet list window.
  > **Note**
  > A single Bakin command file contains all pages of an RPG Maker event.

11. Select a sheet you want to edit. Modify each command with reference to (if you appended) alert/non-converted comments.

13. Do the same for common events. On the map editor, extract common event pane (on the left), add a new sheet for each of exported common events, then import corresponding Bakin command file.

12. (Optional) If you use timer related commands, don't forget to import Timer common events! For detail, see Timer section.

## Variables and Switches

* RPG Maker MV has three types of variables: variables, switches and self-switches. These variables are converted to numerical variables, 0/1 numerical variables and 0/1 local variables in RPG Developer Bakin, respectively.

* Variable names in RPG Developer Bakin will be : [V(RPG Maker variable number)]. For example, RPG Maker MV's variable No.1 will be [V001] in RPG Developer Bakin.

* Switch names in RPG Developer Bakin will be : [S(RPG Maker switch number)]. For example, RPG Maker MV's switch No.1 will be [S001] in RPG Developer Bakin.

* Self-switch names don't change i.e. local variable names will be "A", "B", "C" or "D" in RPG Developer Bakin, as used in RPG Maker.

* Timer used by converted commands is triggered by "TimerTrigger" variable and the timer information will be stored in "TimerInfo" variable (see also Timer section).

## Timer

RPG Developer Bakin operates timer functions via a common event. If RPG Maker events contain any timer commands, Json2BakinPlugin automatically generates a new Timer common event. To make timer related commands functional, generated Timer common event file (named "CommonEvent_Timer.txt") must be imported to RPG Developer Bakin's common event. The Timer event is triggered by "TimeTrigger" variable and its real time info (in seconds) is stored in "TimerInfo" variable.

## Limitations

* Conversion of Some RPG Maker commands to BAKIN commands will be imperfect or even impossible due to both engines' conceptual differences of event implementation. For detail, see <a href="#convert-table">Command Conversion List</a> below.

* RPG Maker MV (MZ) manages event resources (characters, items, sounds, images etc.) by their name or database ID whereas RPG Developer Bakin manages them by Guid (unique ID) which is assigned when the resources are registered to the engine. For this reason, all resource specification settings in RPG Maker commands will be cleared through conversion process and have to be set manually with corresponding Bakin resources.

* RPG Developer Bakin can't set move route of other events (i.e. the events not running commands) with simple move commands. If RPG Maker MV (MZ) events have commands to move other events, the target of such movements will be changed to the command-running event when run on RPG Developer Bakin.

* RPG Developer Bakin can't operate simultaneous move of multiple events by using simple move commands, unlike RPG Maker MV (MZ) where move-route command can deal with multiple event control. Therefore each move command is sequentially executed on RPG Developer Bakin; parallel movement (i.e. no wait for completion of movement) isn't possible.

* RPG Maker MZ conversion not fully tested. It would work, but not guaranteed.

## Command Conversion List
<h3>Basic Commands</h3>

|MV(MZ) Codes|Bakin Codes|Description(JP)|Description(EN), in progress|
|---------------:|-----------|-------------------|--------|
|000|`CLOSE`|クローズタグ||
|101|`DIALOGUE`|会話を表示||
|101-1|`MESSAGE`|メッセージを表示|Not used|
|102|`CHOICES`|選択肢から選んで結果を確認||
|103|`CHANGE_STRING_VARIABLE`|文字列入力||
|104|`ITEMMENU`|アイテム選択画面を表示||
|105|`TELOP`|テロップを表示||
|108|`COMMENT`|注釈||
|109|N/A|スキップ||
|111-0|`IFSWITCH`|イベントスイッチの確認||
|111-1|`IFVARIABLE`|変数ボックスの確認：数値||
|111-2|`IFSWITCH`|変数ボックスの確認：スイッチ||
|111-3|`IFVARIABLE`|条件分岐：タイマー||
|111-4|`IFPARTY`|パーティに含まれるキャストの確認||
|111-5|`IF_STRING_VARIABLE`|文字列変数の確認||
|111-6|N/A|条件分岐：アクタークラス、スキル、状態||
|111-7|`IFITEM`|メンバーが持っているアイテムの確認||
|111-8|`BTL_IFMONSTER`|バトル中のモンスターのチェック||
|111-9|N/A|条件分岐：キャラクターの向き||
|111-10|`IFMONEY`|持っているお金の確認||
|111-11|`IFITEM`|パーティが持っているアイテムの確認||
|111-12|`IFITEM`|パーティが持っている武器の確認||
|111-13|`IFITEM`|パーティが持っている防具の確認||
|111-14|N/A|条件分岐：押されているボタン||
|111-15|N/A|条件分岐：スクリプト||
|111-16|N/A|条件分岐：乗り物||
|112|`LOOP`|ループ開始||
|113|`BREAK`|ループから抜け出す||
|115|`END`|イベント終了||
|117|`EXEC`|共通イベント呼び出し||
|118|N/A|ラベル||
|119|N/A|ラベルジャンプ||
|121|`SWITCH`|イベントスイッチのON/OFF||
|122-0|`HLVARIABLE`|複雑な変数ボックスの操作||
|122-1|`HLVARIABLE`|複雑な変数ボックスの操作：乱数||
|122-2|`HLVARIABLE`|複雑な変数ボックスの操作：アイテム||
|122-3|`HLVARIABLE`|複雑な変数ボックスの操作：アクター||
|122-4|`HLVARIABLE`|複雑な変数ボックスの操作：モンスター||
|122-5|`HLVARIABLE`|複雑な変数ボックスの操作：キャラクター||
|122-6|N/A|変数：パーティアクターID||
|122-7|`HLVARIABLE`|複雑な変数ボックスの操作：ゲームデータ||
|122-8|N/A|複雑な変数ボックスの操作：スクリプト||
|123|`SWITCH`|セルフスイッチのON/OFF||
|124|`VARIABLE`|変数ボックスの操作<br>(コモンイベント「カウントダウンタイマー」が必要)|Common Event (Timer) needed|
|125|`MONEY`|お金を増やす／減らす||
|126|`ITEM`|アイテムを増やす／減らす||
|127|`ITEM`|武器を増やす／減らす||
|128|`ITEM`|防具を増やす／減らす||
|129|`PARTY`|パーティにキャストを参加／外す||
|132|N/A|戦闘BGMの変更||
|133|N/A|勝利MEの変更||
|134|`SW_SAVE`|セーブの許可/禁止||
|135|`SW_MENU`|メニュー画面の表示の許可/禁止||
|136|`SW_ENCOUNTING`|モンスターの出現の許可/禁止||
|137|N/A|並び替えの許可/禁止||
|138|`CHANGE_LAYOUT`|表示するレイアウト（ウィンドウカラー）を変更||
|139|N/A|敗北MEの変更||
|140|N/A|乗り物BGMの変更||
|201|`PLMOVE`|プレイヤーを他の場所に移動||
|202|N/A|乗り物の位置設定||
|203|`MOVE`|イベントを瞬間移動||
|204|`CAM_ANIMATION`|カメラの設定||
|205|`MOVEROUTE`|移動ルートの設定||
|206|N/A|乗り物の乗降||
|211|`EVHIDE`|イベントを透明にする／透明から戻す||
|212|`EFFECT`|エフェクトの表示||
|213|`EMOTE`|感情マークを表示||
|214|`DESTROY_EVENT`|イベント削除||
|216|`WALK_IN_ROWS`|隊列歩行の許可/禁止||
|217|N/A|隊列メンバーの集合||
|221|`SCREEN_FADE`|画面を暗くする||
|222|`SCREEN_FADE`|画面を明るくする||
|223|`SCREEN_COLOR`|画面の色を変える||
|224|`SCREEN_FLASH`|画面をフラッシュ||
|225|`SCREEN_SHAKE`|画面を揺らす||
|230|`WAIT`|指定した時間待つ||
|231|`SPPICTURE`|イメージを表示||
|232|`SPMOVE`|イメージを移動||
|233|N/A|ピクチャの回転||
|234|N/A|ピクチャの色調変更||
|235|`SPHIDE`|イメージを消す||
|236|`CHANGE_RENDER`|レンダリング設定の変更||
|241|`PLAYBGM`|BGMを演奏||
|242|`PLAYBGM`|BGMをフェードアウト||
|243|N/A|BGMの保存||
|244|N/A|BGMの再開||
|245|`PLAYBGS`|環境音を設定||
|246|`PLAYBGS`|環境音をフェードアウト||
|249|`PLAYJINGLE`|ファンファーレを演奏||
|250|`PLAYSE`|効果音の再生||
|251|`STOPSE`|効果音の停止||
|261|`PLAYMOVIE`|ムービーの再生||
|281|N/A|マップ名表示の変更||
|282|N/A|タイルセットの変更||
|283|`CHANGE_RENDER`|戦闘背景の変更||
|284|`CHANGE_RENDER`|遠景の変更||
|285|`GET_TERRAIN`|地形の情報を取得||
|301|`BOSSBATTLE`|バトル実行と結果の確認||
|302|`SHOP`|お店の表示と結果の確認||
|303|`CHANGE_HERO_NAME`|パーティメンバーの名前を変更||
|311|`CHG_HPMP`|HP/MPを回復／減らす||
|312|`CHG_HPMP`|HP/MPを回復／減らす||
|313|`CHG_STTAILM`|状態変化にする／治す||
|314|`FULLRECOV`|パーティを全回復||
|315|`CHG_EXP`|経験値を増やす／減らす||
|316|N/A|レベルの増減||
|317|`STATUS`|キャストの能力値を上げる／下げる||
|318|`CHG_SKILL`|スキルを習得/忘れる||
|319|`EQUIP`|キャストの装備を変更する||
|320|`STRING_VARIABLE`|文字列変数への代入||
|321|`CHANGE_JOB`|職業の変更||
|322|`PLGRAPHIC`|キャストのグラフィックを変える||
|323|N/A|乗り物の画像変更||
|324|N/A|二つ名の変更||
|325|N/A|プロフィールの変更||
|326|N/A|TPの増減||
|331|`BTL_HEAL`|バトルキャストのHP・MPを回復／減らす||
|332|`BTL_HEAL`|バトルキャストのHP・MPを回復／減らす||
|342|N/A|敵キャラのTP増減|||
|333|`BTL_STATUS`|バトルキャストを状態変化にする／治す||
|334|N/A|敵キャラの全回復||
|335|`BTL_APPEAR`|モンスターを出現させる||
|336|N/A|敵キャラの変身||
|337|`EFFECT`|エフェクトの表示||
|339|`BTL_ACTION`|バトルキャストの状態を指定する||
|340|`BTL_STOP`|バトルの強制終了||
|351|`SHOW_SCORE_BOARD`|メニューの表示（イベント用フリーレイアウトを表示）||
|352|`SAVE`|セーブ画面を表示||
|353|N/A|ゲームオーバー||
|354|N/A|タイトル画面に戻る||
|355|`COMMENT`|スクリプト||
|356|`COMMENT`|プラグインコマンド||
|357|`COMMENT`|プラグインコマンド||
|402|`BRANCH`|選択肢||
|403|`BRANCH`|選択肢キャンセル時||
|404|`CLOSE`|選択肢終了||
|411|`ELSE`|条件分岐それ以外||
|413|`ENDLOOP`|ループ終了||
|602|`ELSE`|バトル結果：逃げた時||
|603|`ELSE`|バトル結果：負けた時||

<h3>Move Route Commands (Player or command running event only)</h3>

|MV(MZ) Codes|Bakin Codes|Description(JP)|Description(EN)|
|---------------:|-----------|-------------------|--------|
|01|`PLWALK`|下に移動|Move down|
|02|`PLWALK`|左に移動|Move left|
|03|`PLWALK`|右に移動|Move right|
|04|`PLWALK`|上に移動|Move up|
|05|`PLWALK`|左下に移動|Move lower L|
|06|`PLWALK`|右下に移動|Move lower R|
|07|`PLWALK`|左上に移動|Move upper L|
|08|`PLWALK`|右上に移動|Move upper R|
|09|`PLWALK`|ランダムに移動|Move random|
|10|`PLWALK`|プレイヤーに近づく|Move toward|
|11|`PLWALK`|プレイヤーから遠ざかる|Move away|
|12|`PLWALK`|一歩前進|Move forward|
|13|`PLWALK`|一歩後退|Move backward|
|14|`ADDFORCEPL`|ジャンプ|Jump|
|15|`WAIT`|ウェイト|Wait|
|16|`PLROTATE`|下を向く|Turn down|
|17|`PLROTATE`|左を向く|Turn left|
|18|`PLROTATE`|右を向く|Turn right|
|19|`PLROTATE`|上を向く|Turn up|
|20|`PLROTATE`|右に90度回転|Turn 90d R|
|21|`PLROTATE`|左に90度回転|Turn 90d L|
|22|`PLROTATE`|右に180度回転|Turn 180d|
|23|`PLROTATE`|左右どちらかに90度回転|Turn 90d RL|
|24|`PLROTATE`|ランダムに方向転換|Turn random|
|25|`PLROTATE`|プレイヤーの方を向く|Turn toward|
|26|`PLROTATE`|プレイヤーの逆を向く|Turn away|
|27|`SWITCH`|スイッチON|Switch on|
|28|`SWITCH`|スイッチOFF|Switch off|
|29|`PLWALKSPEED`|移動速度の変更|Change speed|
|30|N/A|移動頻度の変更|Change freq|
|31|`GRAPHIC`|歩行アニメON|Walk anime on|
|32|`GRAPHIC`|歩行アニメOFF|Walk anime off|
|33|`PLMOTION`|足踏みアニメON|Step anime on|
|34|`PLMOTION`|足踏みアニメOFF|Step anime off|
|35|`SW PLLOCKROTATE`|向き固定ON|Dir fix on|
|36|`SW PLLOCKROTATE`|向き固定OFF|Dir fix off|
|37|`CHANGE PLAYER MOVABLE`|すり抜けON|Through on|
|38|`CHANGE PLAYER MOVABLE`|すり抜けOFF|Through off|
|39|`PLHIDE`|透明化ON|Transparent on|
|40|`PLHIDE`|透明化OFF|Transparent off|
|41|`GRAPHIC`|画像の変更|Change image|
|42|N/A|不透明度の変更|Change opacity|
|43|N/A|合成方法の変更|Change blend mode|
|44|`PLAYSE`|SEの演奏|Play SE|
|45|N/A|スクリプト|Script|

## Credits

This plugin refers to following game engine architectures:

- [RPG Developer Bakin](http://electron.atom.io/)
- [RPG Maker MV (MZ)](https://nodejs.org/)

## Support

<a href="https://www.buymeacoffee.com/galanti3" target="_blank"><img src="https://www.buymeacoffee.com/assets/img/custom_images/purple_img.png" alt="Buy Me A Coffee" style="height: 41px !important;width: 174px !important;box-shadow: 0px 3px 2px 0px rgba(190, 190, 190, 0.5) !important;-webkit-box-shadow: 0px 3px 2px 0px rgba(190, 190, 190, 0.5) !important;" ></a>

## License

MIT

---

> GitHub [@galanti3](https://github.com/galanti3) &nbsp;&middot;&nbsp;
> Twitter [@galanti3](https://twitter.com/galanti3)

