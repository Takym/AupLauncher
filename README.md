# AupLauncher
Copyright (C) 2020 Takym.

[English](#en)

<a id="ja"></a>
## 概要
AviUtl と Audacity のプロジェクトファイルを認識し適切なアプリケーションを起動します。

## 利用規約
このソフトウェアはMITライセンスの下で配布されます。
詳しくは[LICENSE.txt](LICENSE.txt)をご覧ください。

## インストール方法
0. まず最初に AviUtl と Audacity をインストールしてください。
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

## バージョン履歴
|バージョン|開発コード名|更新日    |内容                                                                    |
|:--------:|:----------:|:---------|:-----------------------------------------------------------------------|
|v0.0.0.1  |aupl00a1    |2020/03/26|前回のリリースの不具合を大幅に修正しました。27日の真夜中に完成しました。|
|v0.0.0.0  |aupl00a0    |2020/03/24|最初のリリースです。                                                    |

<a id="en"></a>
## Summary
This tool recognizes a project file of AviUtl and Audacity to start a proper application for editing.

## Terms of Use
This software is distributed and licensed under the MIT License.
Please refer [LICENSE.txt](LICENSE.txt) for more information.

## How to Install
0. First, please install both AviUtl and Audacity.
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
* When you executed AupLauncher.exe without any arguments, the launcher will open the settings window.

## Change Log
|Version   |Code Name   |Update    |Changes                                              |
|:--------:|:----------:|:---------|:----------------------------------------------------|
|v0.0.0.1  |aupl00a1    |2020/03/26|Fixed many bugs. This release is made midnight of 27.|
|v0.0.0.0  |aupl00a0    |2020/03/24|The first release.                                   |
