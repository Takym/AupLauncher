using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using AupLauncher.Properties;
using SVersion = System.Version;

namespace AupLauncher
{
	public static class Program
	{
		public const string Caption        = nameof(AupLauncher);
		public const string Description    = "Launcher for AviUtl & Audacity Project Files";
		public const string Author         = "Takym";
		public const string Authors        = "Takym, kokkiemouse";
		public const string Copyright      = "Copyright (C) 2020 Takym.";
<<<<<<< HEAD
		public const string Version        = "0.0.0.6";
		public const string CodeName       = "Derived From aupl00a6";
=======
		public const string Version        = "0.0.0.7";
		public const string CodeName       = "aupl00a7";
>>>>>>> 96aedbbc523088ed8decbae426d332bd35c7ae67

		public static Settings Settings { get; private set; }

		[STAThread()]
		internal static int Main(string[] args)
		{
#if !DEBUG
			try {
#else
			{
#endif

				if (args.Length == 1)
				{
					if (args[0] == "/icom")
					{
						if (IsAdministrator())
						{

							//Processオブジェクトを作成する
							System.Diagnostics.Process p = new System.Diagnostics.Process();
							//起動する実行ファイルのパスを設定する
							p.StartInfo.FileName = "rundll32.exe";
							System.Management.ManagementObject mo =
																new System.Management.ManagementObject("Win32_Processor.DeviceID='CPU0'");
							ushort addWidth = (ushort)mo["AddressWidth"];
							if (addWidth == 32)
							{
								p.StartInfo.Arguments = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\COM_reg_x86.dll Register \"" + Application.ExecutablePath + "\"";
							}
							else if (addWidth == 64)
							{
								p.StartInfo.Arguments = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\COM_reg_x64.dll Register \"" + Application.ExecutablePath + "\"";

							}
							mo.Dispose();

							//起動する。プロセスが起動した時はTrueを返す。
							bool result = p.Start();
							return 0;
						}
						else
						{
							RunElevated(System.Reflection.Assembly.GetEntryAssembly().Location, "/icom", null, false);
							return 0;
						}
					}

					if (args[0] == "/uncom")
					{
						if (IsAdministrator())
						{
							//Processオブジェクトを作成する
							System.Diagnostics.Process p = new System.Diagnostics.Process();
							//起動する実行ファイルのパスを設定する
							p.StartInfo.FileName = "rundll32.exe";
							System.Management.ManagementObject mo =
																new System.Management.ManagementObject("Win32_Processor.DeviceID='CPU0'");
							ushort addWidth = (ushort)mo["AddressWidth"];
							if (addWidth == 32)
							{
								p.StartInfo.Arguments = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\COM_reg_x86.dll UnRegister \"" + Application.ExecutablePath + "\"";
							}
							else if (addWidth == 64)
							{
								p.StartInfo.Arguments = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\COM_reg_x64.dll UnRegister \"" + Application.ExecutablePath + "\"";

							}
							mo.Dispose();

							//起動する。プロセスが起動した時はTrueを返す。
							bool result = p.Start();
							return 0;
						}
						else
						{
							RunElevated(System.Reflection.Assembly.GetEntryAssembly().Location, "/uncom", null, false);
							return 0;
						}
					}
				}
				using (Settings = new Settings())
				{
					if (args.Length == 1)
					{
						if (args[0] == "/i")
						{
							if (IsAdministrator())
							{
								Application.EnableVisualStyles();
								Application.SetCompatibleTextRenderingDefault(false);
								Application.Run(new FormMain());
								return 0;
							}
							else
							{
								RunElevated(System.Reflection.Assembly.GetEntryAssembly().Location, "/i", null, true);
								return 0;
							}
						}
						if (args[0] == "/rr")
						{
							if (IsAdministrator())
							{
								//Processオブジェクトを作成する
								System.Diagnostics.Process p = new System.Diagnostics.Process();
								//起動する実行ファイルのパスを設定する
								p.StartInfo.FileName = "rundll32.exe";
								System.Management.ManagementObject mo =
																	new System.Management.ManagementObject("Win32_Processor.DeviceID='CPU0'");
								ushort addWidth = (ushort)mo["AddressWidth"];
								if (addWidth == 32)
								{
									p.StartInfo.Arguments = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\COM_reg_x86.dll UnRegister_UN \"" + Application.ExecutablePath + "\"";
								}
								else if (addWidth == 64)
								{
									p.StartInfo.Arguments = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\COM_reg_x64.dll UnRegister_UN \"" + Application.ExecutablePath + "\"";

								}
								mo.Dispose();

								//起動する。プロセスが起動した時はTrueを返す。
								bool result = p.Start();
								return 0;
							}
							else
							{
								RunElevated(System.Reflection.Assembly.GetEntryAssembly().Location, "/rr", null, false);
								return 0;
							}
						}
						if (args[0] == "/r")
						{
							if (IsAdministrator())
							{
								Settings.Uninstall();
								return 0;
							}
							else
							{
								RunElevated(System.Reflection.Assembly.GetEntryAssembly().Location, "/r", null, false);
								return 0;
							}
						}
						if (Settings.IsInstalled)
						{
							return StartupProgram(DetermineFileKind(args[0]));
						}

					}
					Application.EnableVisualStyles();
					Application.SetCompatibleTextRenderingDefault(false);
					Application.Run(new FormMain());
					return 0;

				}
#if !DEBUG
			} catch (NotSupportedException nse) {
				MessageBox.Show(
					Resources.Message_AnotherVersionInstalled,
					Caption,
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
				return SaveErrorReport(nse, true);
			} catch (Exception e) {
				return SaveErrorReport(e, true);
#endif
			}
		}

		public static SVersion GetVersion()
		{
			return SVersion.Parse(Version);
		}

		public static (ExecutionKind, string) DetermineFileKind(string fname)
		{
			if (fname.StartsWith("aupfile:")) {
				fname = fname.Substring(8);
			}
			if (string.IsNullOrEmpty(fname)) {
				return (ExecutionKind.ShowSettings, Application.ExecutablePath);
			}
			if (!File.Exists(fname)) {
				MessageBox.Show(
					string.Format(Resources.Message_FileNotFound, fname),
					Caption,
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning);
				return (ExecutionKind.Nothing, fname);
			}
			using (var fs = new FileStream(fname, FileMode.Open, FileAccess.Read, FileShare.Read))
			using (var br = new BinaryReader(fs, Encoding.UTF8))
			using (var sr = new StreamReader(fs, Encoding.UTF8)) {
				if (fs.Length == 0) {
					return (Settings.Default.HandleForInvalidFile, fname);
				}

				byte[] sig = br.ReadBytes(_aviutl_signature.Length);
				if (sig.Length == _aviutl_signature.Length) {
					for (int i = 0; i < _aviutl_signature.Length; ++i) {
						if (sig[i] != _aviutl_signature[i]) {
							goto audacity;
						}
					}
					return (ExecutionKind.AviUtl, fname);
				}
audacity:
				fs.Seek(0, SeekOrigin.Begin);
				string sig2 = sr.ReadLine().TrimStart();
				if (sig2.StartsWith("<?xml")) {
					return (ExecutionKind.Audacity, fname);
				} else {
					return (Settings.Default.HandleForInvalidFile, fname);
				}
			}
		}

		public static int StartupProgram((ExecutionKind kind, string fname) arg)
		{
			var psi = new ProcessStartInfo();
			switch (arg.kind) {
			case ExecutionKind.AviUtl:
				psi.FileName  = Environment.ExpandEnvironmentVariables(Settings.Default.AviUtlPath);
				psi.Arguments = Environment.ExpandEnvironmentVariables(Settings.Default.AviUtlArgs);
				break;
			case ExecutionKind.Audacity:
				psi.FileName  = Environment.ExpandEnvironmentVariables(Settings.Default.AudacityPath);
				psi.Arguments = Environment.ExpandEnvironmentVariables(Settings.Default.AudacityArgs);
				break;
			case ExecutionKind.RunCustomProgram:
				psi.FileName  = Environment.ExpandEnvironmentVariables(Settings.Default.CustomProgramPath);
				psi.Arguments = Environment.ExpandEnvironmentVariables(Settings.Default.CustomProgramArgs);
				break;
			case ExecutionKind.ShowSettings:
				psi.FileName  = Application.ExecutablePath;
				psi.Arguments = string.Empty;
				break;
			case ExecutionKind.ShowError:
				MessageBox.Show(
					string.Format(Resources.Message_InvalidType, arg.fname),
					Caption,
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning);
				return 0;
			case ExecutionKind.Nothing:
				return 0;
			default: //case ExecutionKind.InvalidValue:
				MessageBox.Show(
					Resources.Message_InvalidExecutionKind,
					Caption,
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning);
				return -1;
			}
			psi.UseShellExecute  = true;
			psi.WorkingDirectory = Path.GetDirectoryName(arg.fname);
			psi.Arguments        = CreateArgs(psi.Arguments, $"\"{arg.fname}\"");
			psi.Verb             = "open";
			Process.Start(psi);
			return 0;
		}

		public static string CreateArgs(string format, string args)
		{
			string bat = Path.ChangeExtension(Path.GetTempFileName(), "bat");
			File.WriteAllText(bat, $"@echo off\r\necho {format}", new UTF8Encoding(false));
			var psi = new ProcessStartInfo();
			psi.CreateNoWindow         = true;
			psi.UseShellExecute        = false;
			psi.WorkingDirectory       = Environment.CurrentDirectory;
			psi.FileName               = Environment.GetEnvironmentVariable("COMSPEC");
			psi.Arguments              = $"/C call \"{bat}\" {args}";
			psi.Verb                   = "open";
			psi.RedirectStandardOutput = true;
			using (var proc = Process.Start(psi))
			using (var sr   = proc.StandardOutput) {
				proc.WaitForExit();
				return sr.ReadToEnd().Trim();
			}
		}

		public static int SaveErrorReport(Exception e, bool showMsg)
		{
			var    dt    = DateTime.Now;
			int    pid   = Process.GetCurrentProcess().Id;
			int    ret   = e.HResult;
			string msg   = e.Message;
			string fname = Path.Combine(Application.StartupPath, "Logs", $"ErrorReports.{dt:yyyyMMddHHmmssfff}.[{pid}].log");
			Directory.CreateDirectory(Path.GetDirectoryName(fname));
			using (var fs = new FileStream(fname, FileMode.Append, FileAccess.Write, FileShare.None))
			using (var sw = new StreamWriter(fs, Encoding.UTF8)) {
				sw.WriteLine(Resources.ER_Line1, Caption);
				sw.WriteLine(Resources.ER_Line2);
				sw.WriteLine(Resources.ER_Line3);
				sw.WriteLine(Resources.ER_Line4, Version, CodeName);
				sw.WriteLine(Resources.ER_Line5, dt);
				sw.WriteLine(Resources.ER_Line6, pid);
				sw.WriteLine(Resources.ER_Line7, fname);
				sw.WriteLine();
				int n = 0;
				do {
					sw.WriteLine(Resources.ER_Line8_Title,      n);
					sw.WriteLine(Resources.ER_Line8_Message,    e.Message);
					sw.WriteLine(Resources.ER_Line8_HResult,    e.HResult);
					sw.WriteLine(Resources.ER_Line8_HelpLink,   e.HelpLink);
					sw.WriteLine(Resources.ER_Line8_Source,     e.Source);
					sw.WriteLine(Resources.ER_Line8_SourceFunc, e.TargetSite?.Name, e.TargetSite?.ReflectedType.FullName);
					sw.WriteLine(Resources.ER_Line8_StackTrace);
					sw.WriteLine("    * {0}", e.StackTrace?.Replace(sw.NewLine, $"{sw.NewLine}    * "));
					sw.WriteLine(Resources.ER_Line8_Data);
					foreach (var key in e.Data.Keys) {
						sw.WriteLine("    * [{0}]={1}", key.ToString().PadRight(48), e.Data[key]);
					}
					sw.WriteLine("----------------");
					sw.WriteLine(e.ToString());
					sw.WriteLine("================");
					sw.WriteLine();
					e = e.InnerException; ++n;
				} while (e != null);
			}
			if (showMsg) {
				MessageBox.Show(
					string.Format(Resources.Message_ErrorReport, msg, fname),
					Caption,
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}
			return ret;
		}

		private static byte[] _aviutl_signature = new byte[] {
			0x41, 0x76, 0x69, 0x55, 0x74, 0x6C, 0x20, 0x50, 0x72, 0x6F, 0x6A, 0x65, 0x63, 0x74, 0x46, 0x69,
			0x6C, 0x65, 0x20, 0x76, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x20, 0x30, 0x2E, 0x31, 0x38, 0x00
		};
		/// <summary>
		/// 権限を昇格してプロセスを実行します。
		/// </summary>
		/// <param name="fileName">実行するファイル名</param>
		/// <param name="arguments">コマンドライン引数</param>
		/// <param name="parentForm">UAC表示用の親ウィンドウ</param>
		/// <param name="waitExit">trueで終了まで待機、falseで待機しない。</param>
		/// <returns>正常に実行できればtrue、実行できなければfalse</returns>
		public static bool RunElevated(string fileName, string arguments,
	Form parentForm, bool waitExit)
		{
			//プログラムがあるか調べる
			if (!System.IO.File.Exists(fileName))
			{
				throw new System.IO.FileNotFoundException();
			}

			System.Diagnostics.ProcessStartInfo psi =
				new System.Diagnostics.ProcessStartInfo();
			//ShellExecuteを使う。デフォルトtrueなので、必要はない。
			psi.UseShellExecute = true;
			//昇格して実行するプログラムのパスを設定する
			psi.FileName = fileName;
			//動詞に「runas」をつける
			psi.Verb = "runas";
			//子プログラムに渡すコマンドライン引数を設定する
			psi.Arguments = arguments;

			if (parentForm != null)
			{
				//UACダイアログが親プログラムに対して表示されるようにする
				psi.ErrorDialog = true;
				psi.ErrorDialogParentHandle = parentForm.Handle;
			}

			try
			{
				//起動する
				System.Diagnostics.Process p = System.Diagnostics.Process.Start(psi);
				if (waitExit)
				{
					//終了するまで待機する
					p.WaitForExit();
				}
			}
			catch (System.ComponentModel.Win32Exception)
			{
				//「ユーザーアカウント制御」ダイアログでキャンセルされたなどによって
				//起動できなかった時
				return false;
			}

			return true;
		}
		/// <summary>
		/// 管理者権限か否か判定します。
		/// </summary>
		/// <returns>管理者権限の場合はTrue、そうでない場合はFalseが返されます。</returns>
		private static bool IsAdministrator()
		{
			var identity = System.Security.Principal.WindowsIdentity.GetCurrent();
			var principal = new System.Security.Principal.WindowsPrincipal(identity);
			return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
		}
	}
}
