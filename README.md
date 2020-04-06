# AupLauncher
Copyright (C) 2020 Takym and kokkiemouse.

[日本語](#ja)
[English](#en)

<a id="ja"></a>
## 概要
AviUtl と Audacity のプロジェクトファイルを認識し適切なアプリケーションを起動します。
Windows 10 (2004) で動作確認しています。

## 注意
このアプリケーションはレジストリを変更します。
実行前にバックアップを取る事をおすすめします。

## 利用規約
このソフトウェアはMITライセンスの下で配布されます。
詳しくは[LICENSE.txt](LICENSE.txt)をご覧ください。

## インストール方法
0. AviUtl と Audacity がインストールされている事を前提に動作します。まず最初にインストールしておいてください。
	* [AviUtl](http://spring-fragrance.mints.ne.jp/aviutl/)
	* [Audacity](https://www.audacityteam.org/)
1. 適当な場所に下記のフォルダとファイルをコピーします。
	* en
		* AupLauncher.resources.dll
	* ja
		* AupLauncher.resources.dll
	* AupLauncher.exe
	* AupLauncher.exe.config
2. AupLauncher.exe を右クリックし管理者として実行します。すると、拡張子情報が自動的に関連付けられます。
3. 設定項目を環境に合わせて修正します。
4. 変更を保存します。
5. インストール完了です。

## アンインストール方法
1. 設定画面を開きます。
2. [アンインストール] ボタンを押します。
3. メッセージが表示されたら [はい] を選択します。
4. 関連フォルダとファイルを手動で削除します。

## 使い方
* 上記の指示に従って正しくインストールした場合、「.aup」ファイルを開くと自動的に AviUtl または Audacity が起動します。
* また、ファイル名の先頭に「aupfile:」と付けてブラウザ・エクスプローラ等のアドレスバーに入力する事でも起動できます。
* AupLauncher.exe に何も引数を与えずに起動した場合、設定画面が開かれます。
* 設定画面の右上の[?]ボタンを押すとバージョン情報が表示されます。

## バージョン履歴
|バージョン|開発コード名|更新日    |内容                                                                                                                           |
|:--------:|:----------:|:---------|:------------------------------------------------------------------------------------------------------------------------------|
|v0.0.0.7  |aupl00b7    |2020/04/06|動的アイコンのインストール、アンインストールを個別にできるようにした。さらに、特定の操作だけ管理者権限を要求するよう改善した。 |
|v0.0.0.6  |Derived From aupl00a6|2020/04/05|無効なファイルの処理の「無効な値」を非表示にした。                                                                    |
|v0.0.0.5  |Derived From aupl00a5|2020/04/04|サムネで区別できる機能を追加。                                                                                        |
|v0.0.0.5  |aupl00a5    |2020/04/03|説明書きを分かり易く修正。                                                                                                     |
|v0.0.0.4  |aupl00a4    |2020/03/31|ファイル名の空白を正しく処理する様にした。                                                                                     |
|v0.0.0.3  |aupl00a3    |2020/03/29|[?]ボタンが表示されない現象を修正した。無効なファイルを開いた時、設定画面を表示できる様にした。                                |
|v0.0.0.2  |aupl00a2    |2020/03/29|バージョン情報をアプリ内で確認できる様にした。                                                                                 |
|v0.0.0.1  |aupl00a1    |2020/03/26|前回のリリースの不具合を大幅に修正した。                                                                                       |
|v0.0.0.0  |aupl00a0    |2020/03/24|最初のリリース。                                                                                                               |

<a id="en"></a>
## Summary
This tool recognizes a project file of AviUtl and Audacity to start a proper application for editing.
Operation has been confirmed on Windows 10 (1909).

## Attension
This application will be change the Windows Registry.
I should recommend you to backup the registry before a run.

## Terms of Use
This software is distributed and licensed under the MIT License.
Please refer [LICENSE.txt](LICENSE.txt) for more information.

## How to Install
0. Please make sure that both AviUtl and Audacity is installed already. Download links below:
	* [AviUtl](http://spring-fragrance.mints.ne.jp/aviutl/)
	* [Audacity](https://www.audacityteam.org/)
1. Copy below the application folders and files to some location:
	* en
		* AupLauncher.resources.dll
	* ja
		* AupLauncher.resources.dll
	* AupLauncher.exe
	* AupLauncher.exe.config
2. Right-click AupLauncher.exe and run as administrator. Then, the extension information will be registered automatically.
3. Fix settings for your environment.
4. Save changes.
5. Finish!

## How to Uninstall
1. Open the settings window.
2. Click the [Uninstall] button.
3. Click the [Yes] button after the message box shown.
4. Remove related folders and files manually.

## How to Use
* When you followed above direction and installed successfully, the launcher will open ".aup" file with AviUtl or Audacity.
* The launcher will open if you typed "aupfile:" + the file name in the adress bar.
* When you executed AupLauncher.exe without any arguments, the launcher will open the settings window.
* If you want to see a version information, please push a [?] button at right-top on the settings window.

## Change Log
|Version   |Code Name   |Update    |Changes                                                                                  |
|:--------:|:----------:|:---------|:----------------------------------------------------------------------------------------|
|v0.0.0.7  |aupl00b7    |2020/04/06|Added the option to install dynamic icons.In addition, it has been improved so that only certain operations require administrator privileges.                                                         |
|v0.0.0.6  |Derived From aupl00a6|2020/04/05|Invisibled the "invalid value" item of handles for an invalid file.                                                          |
|v0.0.0.5  |Derived From aupl00a5|2020/04/04|Thumbnail function add.                                                         |
|v0.0.0.5  |aupl00a5    |2020/04/03|Fixed this manual friendly only.                                                         |
|v0.0.0.4  |aupl00a4    |2020/03/31|Fixed to process for blanks in a file name.                                              |
|v0.0.0.3  |aupl00a3    |2020/03/29|Fixed to show [?] button. Added a handle to show the settings window for an invalid file.|
|v0.0.0.2  |aupl00a2    |2020/03/29|Added a version information dialog.                                                      |
|v0.0.0.1  |aupl00a1    |2020/03/26|Fixed many bugs.                                                                         |
|v0.0.0.0  |aupl00a0    |2020/03/24|The first release.                                                                       |
