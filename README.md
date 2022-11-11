 　　　　　　　　　　　　　　　　　　　　　　 ![smile](https://user-images.githubusercontent.com/15904672/201221341-3e3bc596-642d-439b-a38e-9b47d6538bc0.png)
<h1 align="center">
  Json2BakinPlugin
  <br>
</h1>

<h4 align="center">An <a href="https://rpgbakin.com/en" target="_blank">RPG Developer Bakin</a> plugin to convert RPG Maker MV (MZ) event data to Bakin event data.</h4>
<h4 align="center">RPGツクールMV(MZ)イベントデータをBakinイベントデータに変換する<a href="https://rpgbakin.com/en" target="_blank">RPG Developer Bakin</a>用プラグイン</h4>

　　　　　　　　　　　　　　　　　　 [![MIT license](https://img.shields.io/badge/License-MIT-blue.svg)](https://lbesson.mit-license.org/)
[![Twitter Followers](https://badgen.net/twitter/follow/galanti3)](https://twitter.com/galanti3)
[![Buymeacoffee](https://badgen.net/badge/icon/buymeacoffee?icon=buymeacoffee&label)](https://www.buymeacoffee.com/galanti3)

**Still in Beta. Might generates improperly formatted export files, causing a crush of Bakin application or destruction of your game project.<br>Use this plugin at your own risk. Make a backup before using this plugin.<br>
ベータ版です。不適切なフォーマットの出力ファイルによりBakinアプリの暴走やゲームプロジェクトデータの破壊を引き起こす可能性があります。
使用は自己責任でお願いします。使用前にゲームプロジェクトのバックアップを取っておくことをおすすめします。**

<h4 align="center">English</h4>
<p align="center">
  <a href="#terminology">Terminology</a> •
  <a href="#key-features">Key Features</a> •
  <a href="#download">Download</a> •
  <a href="#how-to-use">How To Use</a> •
  <a href="#variables-and-switches">Variables And Switches</a> •
  <a href="#timer">Timer</a> •
  <a href="#movement-route-setting">Movement Route Setting</a> •<br>
  <a href="#limitations">Limitations</a> •
  <a href="#command-conversion-list">Command Conversion List</a> •
  <a href="#credits">Credits</a> •
  <a href="#license">License</a>
</p>
<h4 align="center">日本語(このページの下の方にあります)</h4>
<p align="center">
  <a href="#用語">用語</a> •
  <a href="#おもな機能">おもな機能</a> •
  <a href="#ダウンロード">ダウンロード</a> •
  <a href="#使い方">使い方</a> •
  <a href="#変数とスイッチ">変数とスイッチ</a> •
  <a href="#移動ルート設定">移動ルート設定</a> •<br>
  <a href="#タイマー">タイマー</a> •
  <a href="#制約">制約</a> •
  <a href="#command-conversion-list">コマンド変換リスト</a> •
  <a href="#credits">クレジット</a> •
  <a href="#license">ライセンス</a>
</p>

<!-- ![screenshot](https://raw.githubusercontent.com/amitmerchant1990/electron-markdownify/master/app/img/markdownify.gif) -->

## Terminology
* "Bakin" : RPG Developer Bakin application
* "MV" : RPG Maker MV application
* "MZ" : RPG Maker MZ application
* "Json file" : A Json file generated by RPG Maker MV (MZ), containing MV (MZ) event and command data
* "Bakin file" : A text file generated by this plugin, containing Bakin commands which are converted from Json file
* "MV (MZ) event" : A page container used in MV (MZ). A single event can contain multiple pages
* "MV (MZ) page" : A set of MV (MZ) commands
* "Bakin event" : A sheet container used in Bakin, corresponding to MV (MZ) event
* "Bakin Sheet" : A set of Bakin commands, corresponding to MV (MZ) page
* "MV (MZ) common event" : A special type of MV (MZ) event. This contains only one page
* "Bakin common event" : A special type of Bakin event

## Key Features

* Exports Bakin-readable command files converted from RPG Maker MV event data
* Converts common events
* Converts RPG Maker MZ events (preliminary)
* Exports unsupported RPG Maker commands as comments
* Event-wise conversion: all pages contained in an MV event can be imported to a Bakin event at once.
* Adds an alert comment prior to a command which requires modification
* Human-friendly: Appends a human-readable description to each Bakin command and parameter (currently Japanese only)

## Download

You can download Json2BakinPlugin dll file and dependent dlls from [here](https://github.com/galanti3/Json2BakinPlugin/releases/tag/v1.2.0).

## How To Use

1. Extract downloaded zip file. Place Json2BakinPlugin folder under the Plugin folder of Bakin installation. 
  > **Note**
  > If you installed Bakin via Steam, the Plugin folder can be usually found at C:\Program Files (x86)\Steam\steamapps\common\Bakin

2. Start Bakin and open any game project.

3. From the menu bar of the map editor, navigate to Functions > Expanded Features > "Json - Bakin Conversion Plugin" and open the plugin.

4. Select MV (MZ) data folder containing Map, CommonEvent and related Json files
  > **Note**
  > The name of the selected folder should be "data", which contains all your game project data in RPG Maker architecture.

5. Select arbitorary folder for storing exported Bakin files.

6. If you don't want to add modification alert comments to Bakin command lists and/or don't want to export non-converted MV (MZ) commands as Bakin comments, unselect corresponding checkbox(es).
  
7. Press "Convert" button. MV (MZ) events will be converted to Bakin events and exported to Bakin files (.txt) in the specified export folder. A conversion log file (log.txt) is also generated in the same export folder.
  
8. Close the plugin. On the map editor, select an event and open the event sheet editor.
  
9. Press "Import" button on the upper right corner of the editor. Select a Bakin file you want to import.
![Json2Bakin03](https://user-images.githubusercontent.com/15904672/201215213-fb7942e1-ffbb-43ad-9062-18e5a70011fc.png)

  > **Note**
  > When importing, you may be asked if you want to replace variables.
  >**Always choose "No"!**
  >
  >![Json2Bakin04](https://user-images.githubusercontent.com/15904672/201217910-b198978f-9588-46da-ae7b-01521945a9b6.png)

10. The command edit window turns empty. Confirm that new sheet(s) appear on the sheet list window.
  > **Note**
  > A single Bakin file contains all pages of an MV (MZ) event.

11. Select a sheet you want to edit. Modify each command with reference to (if you activated) alert/non-converted comments.

13. Do the same for common events. On the map editor, extract common event pane on the left, add a new sheet for each of common event Bakin files, then import them.

12. (Optional) If you use timer related commands, don't forget to import "Countdown Timer" common event! For detail, see <a href="#timer">Timer</a> section.

## Variables and Switches

* MV (MZ) utilizes three types of variables: "variables", "switches" and "self-switches". These variables are converted to "numerical variables", "0/1 numerical variables" and "0/1 local variables" for Bakin, respectively.

* Converted variable names in Bakin will be "[V(MV (MZ) variable number)]Var_Name". For example, MV's variable No.1, Named "TalkNumber", is converted to "[V001]TalkNumber" for Bakin.

* Converted switch names in Bakin will be "[S(MV (MZ) switch number)]Switch_Name". For example, MV's switch No.1, Named "TalkedToKing", is converted to "[S001]TalkedToKing" for Bakin.

* Self-switch names don't change i.e. local variable names will be "A", "B", "C" or "D" in Bakin, as used in MV (MZ).

* The timer function used by converted commands is triggered by "TimerTrigger" variable and the timer information is stored in "TimerInfo" variable (see also Timer section).

## Timer

Bakin offers the timer functionality via a common event. If MV (MZ) events contain any timer commands, a new "Countdown Timer" common event is automatically generated duging the conversion process. To make the timer related commands functional, generated Timer common event Bakin file (named "CommonEvent_Timer.txt") must be imported to Bakin common event. The Timer event is triggered by "TimeTrigger" variable and its real time info (in seconds) is stored in "TimerInfo" variable.

## Movement Route setting

~~MV (MZ)'s consecutive movement commands (walk up, down, right, left, upper-right, upper-left, lower-right or lower-left) will be merged into a single Bakin's "walk to specified coodinates" command. The destination point is automatically calculated. If any other command (e.g. wait or turn) exists in between those move commands, no merge happens.~~<br>
MV (MZ)'s consecutive same-direction movement commands (walk up, down, right, left, upper-right, upper-left, lower-right or lower-left) will be merged into a single Bakin's movement command. The step size is automatically calculated.

## Limitations

* Conversion of Some MV (MZ) commands to BAKIN commands will be imperfect or even impossible due to both engines' conceptual differences of event implementation. For detail, see <a href="#command-conversion-list">Command Conversion List</a> below.

* MV (MZ) manages event resources (characters, items, sounds, images etc.) by their name or database ID whereas Bakin manages them by Guid (unique ID) which is assigned when the resources are registered to your Bakin project. For this reason, all resource specification settings in MV (MZ) commands will be cleared through conversion process and have to be set manually on the Bakin editor with corresponding Bakin resources.

* Bakin commands can't set moving route of other events (i.e. the events that aren't running commands). If MV (MZ) events contain commands to move other events, the target of those movement commands will be changed to the command-running event when executed on Bakin.

* Bakin commands can't operate simultaneous move of multiple events, unlike MV (MZ) where "set route" command deals with multiple event controls. Therefore each movement command is sequentially executed on Bakin; parallel movement (i.e. no wait for completion of movement) isn't possible.

* RPG Maker MZ conversion not fully tested. It would work, but not guaranteed.

## Command Conversion List
<h3>Basic Commands</h3>

|MV(MZ) Codes|Bakin Codes|Description(JP)|Description(EN), to be filled|
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

<h3>Move Route Commands: Player (upper) or command running event (lower) only</h3>

|MV(MZ) Codes|Bakin Codes|Description(JP)|Description(EN)|
|---------------:|-----------|-------------------|--------|
|01|`PLWALK`<br>`WALK`|下に移動|Move down|
|02|`PLWALK`<br>`WALK`|左に移動|Move left|
|03|`PLWALK`<br>`WALK`|右に移動|Move right|
|04|`PLWALK`<br>`WALK`|上に移動|Move up|
|05|`PLWALK`<br>`WALK`|左下に移動|Move lower L|
|06|`PLWALK`<br>`WALK`|右下に移動|Move lower R|
|07|`PLWALK`<br>`WALK`|左上に移動|Move upper L|
|08|`PLWALK`<br>`WALK`|右上に移動|Move upper R|
|09|`PLWALK`<br>`WALK`|ランダムに移動|Move random|
|10|`PLWALK`<br>`WALK`|プレイヤーに近づく|Move toward|
|11|`PLWALK`<br>`WALK`|プレイヤーから遠ざかる|Move away|
|12|`PLWALK`<br>`WALK`|一歩前進|Move forward|
|13|`PLWALK`<br>`WALK`|一歩後退|Move backward|
|14|`ADDFORCEPL`<br>`ADDFORCE`|ジャンプ|Jump|
|15|`WAIT`|ウェイト|Wait|
|16|`PLROTATE`<br>`ROTATE`|下を向く|Turn down|
|17|`PLROTATE`<br>`ROTATE`|左を向く|Turn left|
|18|`PLROTATE`<br>`ROTATE`|右を向く|Turn right|
|19|`PLROTATE`<br>`ROTATE`|上を向く|Turn up|
|20|`PLROTATE`<br>`ROTATE`|右に90度回転|Turn 90d R|
|21|`PLROTATE`<br>`ROTATE`|左に90度回転|Turn 90d L|
|22|`PLROTATE`<br>`ROTATE`|右に180度回転|Turn 180d|
|23|`PLROTATE`<br>`ROTATE`|左右どちらかに90度回転|Turn 90d RL|
|24|`PLROTATE`<br>`ROTATE`|ランダムに方向転換|Turn random|
|25|`PLROTATE`<br>`ROTATE`|プレイヤーの方を向く|Turn toward|
|26|`PLROTATE`<br>`ROTATE`|プレイヤーの逆を向く|Turn away|
|27|`SWITCH`|スイッチON|Switch on|
|28|`SWITCH`|スイッチOFF|Switch off|
|29|`PLWALKSPEED`<br>`WALKSPEED`|移動速度の変更|Change speed|
|30|N/A|移動頻度の変更|Change freq|
|31|`GRAPHIC`|歩行アニメON|Walk anime on|
|32|`GRAPHIC`|歩行アニメOFF|Walk anime off|
|33|`PLMOTION`<br>`MOTION`|足踏みアニメON|Step anime on|
|34|`PLMOTION`<br>`MOTION`|足踏みアニメOFF|Step anime off|
|35|`SW PLLOCKROTATE`<br>N/A|向き固定ON|Dir fix on|
|36|`SW PLLOCKROTATE`<br>N/A|向き固定OFF|Dir fix off|
|37|`CHANGE_PLAYER_MOVABLE`<br>`CHANGE_MOVABLE`|すり抜けON|Through on|
|38|`CHANGE_PLAYER_MOVABLE`<br>`CHANGE_MOVABLE`|すり抜けOFF|Through off|
|39|`PLHIDE`<br>`EVHIDE`|透明化ON|Transparent on|
|40|`PLHIDE`<br>`EVHIDE`|透明化OFF|Transparent off|
|41|`GRAPHIC`|画像の変更|Change image|
|42|N/A|不透明度の変更|Change opacity|
|43|N/A|合成方法の変更|Change blend mode|
|44|`PLAYSE`|SEの演奏|Play SE|
|45|N/A|スクリプト|Script|

<h3 align="center">日本語</h3>

## 用語
* "Bakin" : RPG Developer Bakin アプリケーション
* "ツクール" : RPGツクール MV(MZ) アプリケーション
* "Jsonファイル" : ツクールのイベントデータやコマンドデータを含んだJsonファイル
* "Bakinファイル" : Jsonファイルから変換されたBakinコマンドを含んだテキストファイル
* "ツクールイベント" : ツクールで使われる、１つ以上のページを含むデータのまとまり
* "ツクールページ" : ツクールコマンドデータ本体
* "Bakinイベント" : Bakinで使われる、１つ以上のシートを含むデータのまとまり(ツクールイベントに相当)
* "Bakinシート" : Bakinコマンドデータ本体(ツクールページに相当)
* "ツクールコモンイベント" : 特殊なタイプのツクールイベント、１ページのみ
* "Bakinコモンイベント" : 特殊なタイプのBakinイベント

## おもな機能

* RPGツクール MVのイベントデータをBakinにインポートできる形式に変換
* コモンイベント変換に対応
* RPGツクール MZのイベントの変換に対応(ベータ版)
* Bakinに未対応のツクールコマンドはコメントとして出力
* イベント単位の変換：１つのツクールイベントに含まれるすべてのページをBakinイベントへ一挙にインポート可能
* インポート後に編集が必要なコマンドに警告コメントを付加
* 人にやさしいBakinファイル：Bakinファイル内のコマンドやパラメータに分かりやすい説明文を付加

## ダウンロード

Json2Bakinプラグインのdllと関連するdllsは[ここ](https://github.com/galanti3/Json2BakinPlugin/releases/tag/v1.2.0)からダウンロードできます。

## 使い方

1. ダウンロードしたzipファイルを解凍して、できた「Json2BakinPlugin」フォルダを丸ごとBakinアプリケーションフォルダ内の「Plugin」フォルダの中に入れてください。 
  > **Note**
  > Steamを通してBakinをインストールした場合、「Plugin」フォルダは通常「C:\Program Files (x86)\Steam\steamapps\common\Bakin」内にあるはずです。

2. Bakinを開始し、ゲームプロジェクトを開いてください。

3. マップエディタのメニューバーから「機能」>「拡張機能」>「JsonーBakin変換プラグイン」を選択し、プラグインを開きます。

4. ツクールのマップデータやコモンイベントデータなどを含んだ「data」フォルダを選択します。
  > **Note**
  > フォルダ名は必ず「data」のはずです。これはツクールプロジェクトが自動で作成するフォルダです。

5. 変換されたBakinファイルの出力先フォルダを選択します。どこでも構いません。

6. もし編集が必要なBakinコマンドのための警告コメントが不要、または変換不可コマンドをコメントとして出力したくないなら、それぞれ対応するチェックマークを外してください。
  
7. 「変換」ボタンを押せばツクールイベントがBakinイベントに変換され、指定フォルダ内にBakinファイル(.txt)として出力されます。変換ログファイル(log.txt)も同じフォルダ内に出力されます。
  
8. プラグインを閉じます。マップエディタで適当なイベントを選択し、イベントシートエディタを開きます。
  
9. エディタの右上にある「インポート」ボタンを押し、インポートしたいBakinファイルを選択してください。
![Json2Bakin03](https://user-images.githubusercontent.com/15904672/201215213-fb7942e1-ffbb-43ad-9062-18e5a70011fc.png)

  > **Note**
  > Bakinファイルをインポートすると「新しい変数名に置き換えますか」と聞かれることがあります。
  >**必ず「いいえ」を選択してください！**
  >
  >![Json2Bakin02](https://user-images.githubusercontent.com/15904672/201216612-496a3328-6050-44d1-8443-5a57a08ed060.png)

10. インポートが行われるとコマンド編集画面が空白になります。左側にあるシート一覧ウィンドウにインポートした分のシートが増えていることを確認してください。
  > **Note**
  > 1つのBakinファイルは1つのツクールイベントに対応しています。Bakinファイルをインポートすると、対応するイベント内のページがすべて読み込まれます。

11. 編集したいシートを選択します。警告コメントや変換不可コメント(もしあれば)を参考にしながら、コマンドを修正してください。

13. コモンイベントも同様にインポートします。マップエディタから、左側にある「コモンイベント」を押してコモンイベントパネルを表示させます。「共通イベント」の下に新たなシートを変換したコモンイベントの数だけ作成し、それぞれに対してBakinファイルをインポートしてください。

12. (オプション)もしツクールイベント内でタイマー関連コマンドを使っている場合は、「カウントダウンタイマー」コモンイベントを必ずインポートしてください！詳しくは<a href="#タイマー">タイマー</a>セクションを見てください。

## 変数とスイッチ

* ツクールは「変数」「スイッチ」「セルフスイッチ」の3種類の変数を使用します。これらはそれぞれBakinの「変数ボックス」「2値変数ボックス」「2値ローカル変数ボックス」に変換されます。

* 変換後の変数名は「[V(ツクール変数番号)]変数名」になります。例えば、ツクール上で「会話数」という名前の1番目の変数はBakinでは「[V001]会話数」という名前になります。

* 変換後のスイッチ名は「[S(ツクールスイッチ番号)]スイッチ名」になります。例えば、ツクール上で「王様と話した」という名前の1番目のスイッチはBakinでは「[S001]王様と話した」という名前になります。

* セルフスイッチ名は変換後もそのままです。ツクールのセルフスイッチ名と同じく、Bakinでもローカル変数名は「A」、「B」、「C」、「D」になります。

* ツクールイベントで使用しているタイマーコマンドは、Bakin上で「TimerTrigger」という変数でオンオフするよう変換されます。タイマーの現在秒数は「TimerInfo」という変数に格納されます(<a href="#タイマー">タイマー</a>セクションも参照してください)。

## タイマー

Bakinではタイマー機能はコモンイベントを通して提供されます。もしツクールイベントがタイマー関連コマンドを使用している場合、変換時に「カウントダウンタイマー」コモンイベントが自動的に生成され、Bakinファイルに出力されます。変換されたタイマー関連コマンドをBakinで動作させるには、生成されたタイマーBakinファイル(CommonEvent_Timer.txt)を必ずコモンイベントにインポートしてください。タイマーイベントは「TimeTrigger」という変数によってオンオフ操作され、リアルタイムの秒数は「TimerInfo」という変数に格納されます。

## 移動ルート設定

~~ツクールイベント内の連続する移動コマンド(上、下、左、右、右上、左上、右下、左下) は1つのBakinコマンド「目的地に向かって歩く」に集約されます。目的ポイントは自動的に計算されます。もし移動コマンド間に別のコマンド(ウエイト、向きを変えるなど)が存在する場合は、集約は行われません。~~<br>
ツクールイベント内の連続する同じ方向への移動コマンド(上、下、左、右、右上、左上、右下、左下) は1つのBakinコマンド「歩かせる」に集約されます。歩数は自動的に計算されます。

## 制約

* ツクールとBakinのイベント実装に関する設計思想が異なるため、Bakinコマンドへの変換が不完全、または変換不可能なツクールコマンドがあります。詳細については<a href="#command-conversion-list">コマンド変換リスト</a>を参照してください。

* ツクールはイベントで使用するリソース(キャラ、アイテム、サウンド、画像など)を名前またはデータベースIDで管理しています。一方BakinではそれらをGUID（ユニークなID)で管理します。GUIDはリソースがプロジェクトに登録される際にそれぞれ付与されます。このため、ツクールコマンドで指定されたあらゆるリソースの名前やIDは変換時にすべてクリアされてしまい、Bakinにインポートした後に手動でそれらのリソースを再設定する必要があります。

* Bakinではコマンドによって他のイベント(コマンドを実行していないイベント)の動きを制御することができません。もしツクールイベント内で他のイベントを動かすコマンドを使っている場合、変換時に動かす対象が「このイベント」に変更されます。つまり、Bakinでそのコマンドを実行した場合、動かす対象のイベントでなく自分が動きます。

* Bakinでは歩行コマンドによって複数のイベントを同時に動かすことはできません(ツクールでは「移動ルートの設定」コマンドを使えば可能です)。そのため、Bakinでは移動コマンドは上から順番に1つずつ実行され、並列実行(移動完了を待たずに次のコマンドを実行)はできません。

* RPGツクール MZのイベントの変換は動作未確認です。たぶん動くと思いますが、保証はしません。


## Credits

This plugin refers to following game engine architectures:<br>
このプラグインは以下のゲームエンジンのアーキテクチャを参考に作成されました：<br>

- [RPG Developer Bakin](https://rpgbakin.com/en)
- [RPG Maker MV (MZ)](https://www.rpgmakerweb.com/)

## Support

投げ銭<br>
<a href="https://www.buymeacoffee.com/galanti3" target="_blank"><img src="https://www.buymeacoffee.com/assets/img/custom_images/purple_img.png" alt="Buy Me A Coffee" style="height: 41px !important;width: 174px !important;box-shadow: 0px 3px 2px 0px rgba(190, 190, 190, 0.5) !important;-webkit-box-shadow: 0px 3px 2px 0px rgba(190, 190, 190, 0.5) !important;" ></a>

## License

MIT

---

> GitHub [@galanti3](https://github.com/galanti3) &nbsp;&middot;&nbsp;
> Twitter [@galanti3](https://twitter.com/galanti3)

