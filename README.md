# AupLauncher
Copyright (C) 2020 Takym.

[���{��](#ja)
[English](#en)

<a id="ja"></a>
## �T�v
AviUtl �� Audacity �̃v���W�F�N�g�t�@�C����F�����K�؂ȃA�v���P�[�V�������N�����܂��B
Windows 10 (1909) �œ���m�F���Ă��܂��B

## ����
���̃A�v���P�[�V�����̓��W�X�g����ύX���܂��B
���s�O�Ƀo�b�N�A�b�v����鎖���������߂��܂��B

## ���p�K��
���̃\�t�g�E�F�A��MIT���C�Z���X�̉��Ŕz�z����܂��B
�ڂ�����[LICENSE.txt](LICENSE.txt)���������������B

## �C���X�g�[�����@
0. AviUtl �� Audacity ���C���X�g�[������Ă��鎖��O��ɓ��삵�܂��B�܂��ŏ��ɃC���X�g�[�����Ă����Ă��������B
	* [AviUtl](http://spring-fragrance.mints.ne.jp/aviutl/)
	* [Audacity](https://www.audacityteam.org/)
1. �K���ȏꏊ�ɉ��L�̃t�H���_�ƃt�@�C�����R�s�[���܂��B
	* en
		* AupLauncher.resources.dll
	* ja
		* AupLauncher.resources.dll
	* AupLauncher.exe
	* AupLauncher.exe.config
2. AupLauncher.exe ���E�N���b�N���Ǘ��҂Ƃ��Ď��s���܂��B����ƁA�g���q��񂪎����I�Ɋ֘A�t�����܂��B
3. �ݒ荀�ڂ����ɍ��킹�ďC�����܂��B
4. �ύX��ۑ����܂��B
5. �C���X�g�[�������ł��B

## �A���C���X�g�[�����@
1. �ݒ��ʂ��J���܂��B
2. [�A���C���X�g�[��] �{�^���������܂��B
3. ���b�Z�[�W���\�����ꂽ�� [�͂�] ��I�����܂��B
4. �֘A�t�H���_�ƃt�@�C�����蓮�ō폜���܂��B

## �g����
* ��L�̎w���ɏ]���Đ������C���X�g�[�������ꍇ�A�u.aup�v�t�@�C�����J���Ǝ����I�� AviUtl �܂��� Audacity ���N�����܂��B
* �܂��A�t�@�C�����̐擪�Ɂuaupfile:�v�ƕt���ău���E�U�E�G�N�X�v���[�����̃A�h���X�o�[�ɓ��͂��鎖�ł��N���ł��܂��B
* AupLauncher.exe �ɉ���������^�����ɋN�������ꍇ�A�ݒ��ʂ��J����܂��B
* �ݒ��ʂ̉E���[?]�{�^���������ƃo�[�W������񂪕\������܂��B

## �o�[�W��������
|�o�[�W����|�J���R�[�h��|�X�V��    |���e                                                                                           |
|:--------:|:----------:|:---------|:----------------------------------------------------------------------------------------------|
|v0.0.0.5  |aupl00a5    |2020/04/03|���������𕪂���Ղ��C���B                                                                     |
|v0.0.0.4  |aupl00a4    |2020/03/31|�t�@�C�����̋󔒂𐳂�����������l�ɂ����B                                                     |
|v0.0.0.3  |aupl00a3    |2020/03/29|[?]�{�^�����\������Ȃ����ۂ��C�������B�����ȃt�@�C�����J�������A�ݒ��ʂ�\���ł���l�ɂ����B|
|v0.0.0.2  |aupl00a2    |2020/03/29|�o�[�W���������A�v�����Ŋm�F�ł���l�ɂ����B                                                 |
|v0.0.0.1  |aupl00a1    |2020/03/26|�O��̃����[�X�̕s���啝�ɏC�������B                                                       |
|v0.0.0.0  |aupl00a0    |2020/03/24|�ŏ��̃����[�X�B                                                                               |

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
|v0.0.0.5  |aupl00a5    |2020/04/03|Fixed this manual friendly only.                                                         |
|v0.0.0.4  |aupl00a4    |2020/03/31|Fixed to process for blanks in a file name.                                              |
|v0.0.0.3  |aupl00a3    |2020/03/29|Fixed to show [?] button. Added a handle to show the settings window for an invalid file.|
|v0.0.0.2  |aupl00a2    |2020/03/29|Added a version information dialog.                                                      |
|v0.0.0.1  |aupl00a1    |2020/03/26|Fixed many bugs.                                                                         |
|v0.0.0.0  |aupl00a0    |2020/03/24|The first release.                                                                       |
