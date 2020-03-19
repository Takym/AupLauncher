using AupLauncher.Properties;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AupLauncher
{
	public sealed class Settings : IDisposable
	{
		private readonly RegistryKey                                   _reg;
		private          string                                        _defkey;
		public           Dictionary<string, SettingProfile> Profiles   { get; private set; }
		public           bool                               IsDisposed { get; private set; }

		public SettingProfile Default
		{
			get {
				if (this.IsDisposed) {
					throw new ObjectDisposedException(nameof(Settings));
				}
				if (!this.Profiles.ContainsKey(_defkey)) {
					var pf = new SettingProfile();
					pf.DisplayName = Resources.DefaultProfileDisplayName;
					this.Profiles.Add(_defkey, pf);
				}
				return this.Profiles[_defkey];
			}
		}

		public bool IsInstalled
		{
			get {
				if (this.IsDisposed) {
					throw new ObjectDisposedException(nameof(Settings));
				}
				return _reg.GetValue("Installed", "FALSE").ToString() == "TRUE";
			}
		}

		public Settings()
		{
			_reg = Registry.CurrentUser.CreateSubKey($"Software\\{nameof(AupLauncher)}");
			this.Profiles   = new Dictionary<string, SettingProfile>();
			this.IsDisposed = false;
			this.LoadFromRegistry();
		}

		~Settings()
		{
			this.Dispose(false);
		}

		public void LoadFromRegistry()
		{
			if (this.IsDisposed) {
				throw new ObjectDisposedException(nameof(Settings));
			}
			var sver = Version.Parse(_reg.GetValue("SavedVersion", "255.255.255.255").ToString());
			if (sver <= Program.GetVersion()) {
				_defkey = _reg.GetValue("DefaultProfile", "_default").ToString();
				string[] profiles = _reg.GetSubKeyNames();
				for (int i = 0; i < profiles.Length; ++i) {
					if (this.Profiles.ContainsKey(profiles[i])) {
						using (var pfreg = _reg.OpenSubKey(profiles[i])) {
							this.Profiles[profiles[i]].LoadFromRegistry(pfreg);
						}
					} else {
						var pf = new SettingProfile();
						using (var pfreg = _reg.OpenSubKey(profiles[i])) {
							pf.LoadFromRegistry(pfreg);
						}
						this.Profiles.Add(profiles[i], pf);
					}
				}
			} else {
				throw new NotSupportedException();
			}
		}

		public void SaveToRegistry()
		{
			if (this.IsDisposed) {
				throw new ObjectDisposedException(nameof(Settings));
			}
			_reg.SetValue("SavedVersion",   Program.Version, RegistryValueKind.String);
			_reg.SetValue("DefaultProfile", _defkey,         RegistryValueKind.String);
			var delList = new List<string>();
			foreach (var item in this.Profiles) {
				if (item.Value.IsDeleted && item.Key != _defkey) {
					_reg.DeleteSubKeyTree(item.Key, false);
					delList.Add(item.Key);
				} else {
					using (var pfreg = _reg.CreateSubKey(item.Key)) {
						item.Value.SaveToRegistry(pfreg);
					}
				}
			}
			for (int i = 0; i < delList.Count; ++i) {
				this.Profiles.Remove(delList[i]);
			}
		}

		public void Install()
		{
			_ = this.Default;
			this.SaveToRegistry();

			{
				// 元々の拡張子情報をバックアップする。
				var cls = Registry.ClassesRoot;
				using (var aupsrc = cls.OpenSubKey(".aup"))
				using (var backup = _reg.CreateSubKey($"{_defkey}\\Backup"))
				using (var aupdst = backup.CreateSubKey(".aup")) {
					this.CopyRegistry(aupsrc, aupdst, true);
					string shell = aupsrc.GetValue("").ToString();
					using (var shellsrc = cls.OpenSubKey(shell))
					using (var shelldst = backup.CreateSubKey(shell)) {
						this.CopyRegistry(shellsrc, shelldst, true);
					}
					using (var mime = cls.OpenSubKey("MIME\\Database\\Content Type")) {
						if (cls.GetValueNames().Contains("video+audio/x-au-project")) {
							using (var mimesrc = cls.OpenSubKey("video+audio/x-au-project"))
							using (var mimedst = backup.CreateSubKey("mime")) {
								this.CopyRegistry(mimesrc, mimedst, true);
							}
						} else {
							using (var mimedst = backup.CreateSubKey("mime")) {
								mimedst.SetValue("", "(Deleted)");
							}
						}
					}
				}
			}

			using (var cls = Registry.CurrentUser.CreateSubKey($"Software\\Classes")) {
				// 拡張子情報を作成する。
				using (var extinfo = _reg.CreateSubKey($"{_defkey}\\ExtInfo")) {
					// 基本情報
					using (var aup    = extinfo.CreateSubKey(".aup"))
					using (var aupcls = cls.CreateSubKey(".aup")) {
						aup.SetValue("",                   "aupfile");
						aup.SetValue("AupLauncherManaged", "RestoreBackup");
						aup.SetValue("Content Type",       "video+audio/x-au-project");
						aup.SetValue("PerceivedType",      "video+audio");
						using (var progids = aup.CreateSubKey("OpenWithProgIds")) {
							progids.SetValue("aupfile", "");
						}
						this.CopyRegistry(aup, aupcls, true);
					}
					// シェル拡張情報
					using (var shell    = extinfo.CreateSubKey("aupfile"))
					using (var shellcls = cls.CreateSubKey("aupfile")) {
						shell.SetValue("",                   Resources.ShellDescription);
						shell.SetValue("AupLauncherManaged", "RestoreBackup");
						shell.SetValue("Content Type",       "video+audio/x-au-project");
						shell.SetValue("PerceivedType",      "video+audio");
						shell.SetValue("URL Protocol",       "");
						shell.SetValue("AlwaysShowExt",      "1");
						using (var icon = shell.CreateSubKey("DefaultIcon")) {
							icon.SetValue("", Application.ExecutablePath);
						}
						using (var verb = shell.CreateSubKey("shell\\open")) {
							verb.SetValue("", Resources.ShellMenu_Open);
							using (var cmd = verb.CreateSubKey("command")) {
								cmd.SetValue("", Application.ExecutablePath);
							}
						}
						this.CopyRegistry(shell, shellcls, true);
					}
					// MIMEタイプ
					using (var mime    = extinfo.CreateSubKey("mime"))
					using (var mimecls = cls.CreateSubKey("MIME\\Database\\Content Type\\video+audio/x-au-project")) {
						mime.SetValue("AupLauncherManaged", "RestoreBackup");
						mime.SetValue("Extension",          ".aup");
						this.CopyRegistry(mime, mimecls, true);
					}
				}
			}

			_reg.SetValue("Installed", "TRUE");
		}

		public void Uninstall()
		{
			using (var cls = Registry.CurrentUser.CreateSubKey($"Software\\Classes")) {
				// バックアップ情報で上書き。
				using (var backup = _reg.CreateSubKey($"{_defkey}\\Backup")) {
					string progid;
					// 基本情報
					using (var aupcls = cls.CreateSubKey(".aup"))
					using (var aupbak = backup.OpenSubKey(".aup")) {
						progid = aupbak.GetValue("").ToString();
						if (aupcls.GetValue("AupLauncherManaged", "").ToString() == "RestoreBackup") {
							this.CopyRegistry(aupbak, aupcls, true);
						}
					}
					// シェル拡張情報
					using (var shellcls = cls.CreateSubKey("aupfile"))
					using (var shellbak = backup.OpenSubKey(progid)) {
						if (shellcls.GetValue("AupLauncherManaged", "").ToString() == "RestoreBackup") {
							this.CopyRegistry(shellbak, shellcls, true);
						}
					}
					// MIMEタイプ
					bool deleteMime = false;
					using (var mimecls = cls.CreateSubKey("MIME\\Database\\Content Type\\video+audio/x-au-project"))
					using (var mimebak = backup.OpenSubKey("mime")) {
						if (mimecls.GetValue("AupLauncherManaged", "").ToString() == "RestoreBackup") {
							if (mimebak.GetValue("").ToString() == "(Deleted)") {
								deleteMime = true;
							} else {
								this.CopyRegistry(mimebak, mimecls, true);
							}
						}
					}
					if (deleteMime) {
						using (var mime = cls.CreateSubKey("MIME\\Database\\Content Type")) {
							mime.DeleteSubKeyTree("video+audio/x-au-project", false);
						}
					}
				}
			}

			_reg.SetValue("Installed", "FALSE");
		}

		private void CopyRegistry(RegistryKey src, RegistryKey dst, bool cleanup)
		{
			if (cleanup) {
				this.DeleteRegistry(dst);
			}
			string[] vals = src.GetValueNames();
			for (int i = 0; i < vals.Length; ++i) {
				var    t = src.GetValueKind(vals[i]);
				object v = src.GetValue(vals[i], null, RegistryValueOptions.DoNotExpandEnvironmentNames);
				dst.SetValue(vals[i], v, t);
			}
			string[] keys = src.GetSubKeyNames();
			for (int i = 0; i < keys.Length; ++i) {
				using (var srcreg = src.OpenSubKey(keys[i]))
				using (var dstreg = dst.CreateSubKey(keys[i])) {
					this.CopyRegistry(srcreg, dstreg, false);
				}
			}
		}

		private void DeleteRegistry(RegistryKey reg)
		{
			string[] vals = reg.GetValueNames();
			for (int i = 0; i < vals.Length; ++i) {
				reg.DeleteValue(vals[i], false);
			}
			string[] keys = reg.GetSubKeyNames();
			for (int i = 0; i < keys.Length; ++i) {
				reg.DeleteSubKeyTree(keys[i], false);
			}
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (!this.IsDisposed) {
				this.SaveToRegistry();
				if (disposing) {
					_reg.Close();
				}
				this.Profiles.Clear();
				this.Profiles   = null;
				this.IsDisposed = true;
			}
		}
	}
}
