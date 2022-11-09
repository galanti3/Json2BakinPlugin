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
  <a href="#convert-table">Command Conversion List</a> •
  <a href="#credits">Credits</a> •
  <a href="#related">Related</a> •
  <a href="#license">License</a>
</p>
<h4 align="center">日本語</h4>
<p align="center">
  <a href="#key-features">おもな機能</a> •
  <a href="#download">ダウンロード</a> •
  <a href="#how-to-use">使い方</a> •
  <a href="#convert-table">コマンド変換リスト</a> •
  <a href="#credits">クレジット</a> •
  <a href="#related">関連リンク</a> •
  <a href="#license">ライセンス</a>
</p>

![screenshot](https://raw.githubusercontent.com/amitmerchant1990/electron-markdownify/master/app/img/markdownify.gif)

## Key Features

* Exports Bakin-readable command files converted from RPG Maker MV event data
* Supports common event conversion
* Suppots RPG Maker MZ conversion (preliminary)
* Exports unsupported RPG Maker commands as comments
* Event-wise conversion: all pages of an event can be imported to a Bakin event.
* Adds an alert comment prior to a command which needs modification
* Human-friendly: Appends a description to each Bakin command and parameter (currently Japanese only)

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
  
7. Press "Convert" button. Converted Bakin-readable command files will be exported to the folder you specified.
  
8. Close the plugin. On the map editor, select an event and open the event sheet editor.
  
9. Press "Import" button on the upper right corner of the editor. Select the exported Bakin-readable command file you want to import.

10. The command edit window turns empty. Confirm that new sheet(s) appear on the sheet list window.
  > **Note**
  > A single Bakin command file contains all pages of an RPG Maker event.

11. Select a sheet you want to edit. Modify each command with referring to alert/non-converted comments (if available).

## Limitations

* Some RPG Maker commands aren't converted properly to BAKIN commands or even impossible to convert due to their conceptual differences of event implementation. For detail, see <a href="#convert-table">Command Conversion List</a> below.

* RPG Maker MV (MZ) manages event resources (characters, items, sounds, images etc.) by their names or database IDs whereas RPG Developer Bakin manages them by Guids (unique ID) which are assigned when they are registered to the engine. For this reason, all resources specified in RPG Maker commands must be reassigned to corresponding Bakin resources.


## Credits

This software uses the following open source packages:

- [Electron](http://electron.atom.io/)
- [Node.js](https://nodejs.org/)
- [Marked - a markdown parser](https://github.com/chjj/marked)
- [showdown](http://showdownjs.github.io/showdown/)
- [CodeMirror](http://codemirror.net/)
- Emojis are taken from [here](https://github.com/arvida/emoji-cheat-sheet.com)
- [highlight.js](https://highlightjs.org/)

## Related

[markdownify-web](https://github.com/amitmerchant1990/markdownify-web) - Web version of Markdownify

## Support

<a href="https://www.buymeacoffee.com/galanti3" target="_blank"><img src="https://www.buymeacoffee.com/assets/img/custom_images/purple_img.png" alt="Buy Me A Coffee" style="height: 41px !important;width: 174px !important;box-shadow: 0px 3px 2px 0px rgba(190, 190, 190, 0.5) !important;-webkit-box-shadow: 0px 3px 2px 0px rgba(190, 190, 190, 0.5) !important;" ></a>

## You may also like...

- [Pomolectron](https://github.com/amitmerchant1990/pomolectron) - A pomodoro app
- [Correo](https://github.com/amitmerchant1990/correo) - A menubar/taskbar Gmail App for Windows and macOS

## License

MIT

---

> GitHub [@galanti3](https://github.com/galanti3) &nbsp;&middot;&nbsp;
> Twitter [@galanti3](https://twitter.com/galanti3)

