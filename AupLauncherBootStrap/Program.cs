using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using AupLauncher;
using AupLauncher.Properties;
using SVersion = System.Version;

namespace AupLauncherBootStrap
{
	public static class Program
	{
		private static bool IsAdministrator()
		{
			var identity = System.Security.Principal.WindowsIdentity.GetCurrent();
			var principal = new System.Security.Principal.WindowsPrincipal(identity);
			return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
		}
		public const string Caption = nameof(AupLauncher);
		public const string Description = "Launcher for AviUtl & Audacity Project Files";
		public const string Author = "Takym and kokkiemouse";
		public const string Copyright = "Copyright (C) 2020 Takym and kokkiemouse.";
		public const string Version = "0.0.0.7";
		public const string CodeName = "aupl00b7";

		public static Settings Settings { get; private set; }

		[STAThread()]
		internal static int Main(string[] args)
		{
#if !DEBUG
			try {
#else
			{
#endif
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
								Application.Run(new FormMain(Settings));
								return 0;
							}
							else
							{
								RunElevated(System.Reflection.Assembly.GetEntryAssembly().Location, "/i", null, true);
								return 0;
							}
						}
						if (args[0] == "/icom")
						{
							if (IsAdministrator())
							{
								Settings.ShellEx_COM_Register();
								return 0;
							}
							else
							{
								RunElevated(System.Reflection.Assembly.GetEntryAssembly().Location, "/icom", null, true);
								return 0;
							}
						}
						if (args[0] == "/uncom")
						{
							if (IsAdministrator())
							{
								Settings.ShellEx_COM_UnRegister();
								return 0;
							}
							else
							{
								RunElevated(System.Reflection.Assembly.GetEntryAssembly().Location, "/uncom", null, true);
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
								RunElevated(System.Reflection.Assembly.GetEntryAssembly().Location, "/r", null, true);
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
					Application.Run(new FormMain(Settings));
					return 0;
					
				}
#if !DEBUG
			} catch (Exception e) {
				return SaveErrorReport(e);
#endif
			}
		}

		public static SVersion GetVersion()
		{
			return SVersion.Parse(Version);
		}

		public static (ExecutionKind, string) DetermineFileKind(string fname)
		{
			if (fname.StartsWith("aupfile:"))
			{
				fname = fname.Substring(8);
			}
			if (string.IsNullOrEmpty(fname))
			{
				return (ExecutionKind.ShowSettings, Application.ExecutablePath);
			}
			if (!File.Exists(fname))
			{
				MessageBox.Show(
					string.Format(AupLauncher.Program.Get_Resources_Message_FileNotFound(), fname),
					Caption,
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning);
				return (ExecutionKind.Nothing, fname);
			}
			using (var fs = new FileStream(fname, FileMode.Open, FileAccess.Read, FileShare.Read))
			using (var br = new BinaryReader(fs, Encoding.UTF8))
			using (var sr = new StreamReader(fs, Encoding.UTF8))
			{
				if (fs.Length == 0)
				{
					return (Settings.Default.HandleForInvalidFile, fname);
				}

				byte[] sig = br.ReadBytes(_aviutl_signature.Length);
				if (sig.Length == _aviutl_signature.Length)
				{
					for (int i = 0; i < _aviutl_signature.Length; ++i)
					{
						if (sig[i] != _aviutl_signature[i])
						{
							goto audacity;
						}
					}
					return (ExecutionKind.AviUtl, fname);
				}
			audacity:
				fs.Seek(0, SeekOrigin.Begin);
				string sig2 = sr.ReadLine().TrimStart();
				if (sig2.StartsWith("<?xml"))
				{
					return (ExecutionKind.Audacity, fname);
				}
				else
				{
					return (Settings.Default.HandleForInvalidFile, fname);
				}
			}
		}

		public static int StartupProgram((ExecutionKind kind, string fname) arg)
		{
			var psi = new ProcessStartInfo();
			switch (arg.kind)
			{
				case ExecutionKind.AviUtl:
					psi.FileName = Environment.ExpandEnvironmentVariables(Settings.Default.AviUtlPath);
					psi.Arguments = Environment.ExpandEnvironmentVariables(Settings.Default.AviUtlArgs);
					break;
				case ExecutionKind.Audacity:
					psi.FileName = Environment.ExpandEnvironmentVariables(Settings.Default.AudacityPath);
					psi.Arguments = Environment.ExpandEnvironmentVariables(Settings.Default.AudacityArgs);
					break;
				case ExecutionKind.RunCustomProgram:
					psi.FileName = Environment.ExpandEnvironmentVariables(Settings.Default.CustomProgramPath);
					psi.Arguments = Environment.ExpandEnvironmentVariables(Settings.Default.CustomProgramArgs);
					break;
				case ExecutionKind.ShowSettings:
					psi.FileName = Application.ExecutablePath;
					psi.Arguments = string.Empty;
					break;
				case ExecutionKind.ShowError:
					MessageBox.Show(
						string.Format(AupLauncher.Program.Get_Resources_Message_InvalidType(), arg.fname),
						Caption,
						MessageBoxButtons.OK,
						MessageBoxIcon.Warning);
					return 0;
				case ExecutionKind.Nothing:
					return 0;
				default: //case ExecutionKind.InvalidValue:
					MessageBox.Show(
						AupLauncher.Program.Get_Resources_Message_InvalidExecutionKind(),
						Caption,
						MessageBoxButtons.OK,
						MessageBoxIcon.Warning);
					return -1;
			}
			psi.UseShellExecute = true;
			psi.WorkingDirectory = Path.GetDirectoryName(arg.fname);
			psi.Arguments = CreateArgs(psi.Arguments, $"\"{arg.fname}\"");
			psi.Verb = "open";
			Process.Start(psi);
			return 0;
		}

		public static string CreateArgs(string format, string args)
		{
			string bat = Path.ChangeExtension(Path.GetTempFileName(), "bat");
			File.WriteAllText(bat, $"@echo off\r\necho {format}", new UTF8Encoding(false));
			var psi = new ProcessStartInfo();
			psi.CreateNoWindow = true;
			psi.UseShellExecute = false;
			psi.WorkingDirectory = Environment.CurrentDirectory;
			psi.FileName = Environment.GetEnvironmentVariable("COMSPEC");
			psi.Arguments = $"/C call \"{bat}\" {args}";
			psi.Verb = "open";
			psi.RedirectStandardOutput = true;
			using (var proc = Process.Start(psi))
			using (var sr = proc.StandardOutput)
			{
				proc.WaitForExit();
				return sr.ReadToEnd().Trim();
			}
		}

		public static int SaveErrorReport(Exception e)
		{
			var dt = DateTime.Now;
			int pid = Process.GetCurrentProcess().Id;
			int ret = e.HResult;
			string msg = e.Message;
			string fname = Path.Combine(Application.StartupPath, "Logs", $"ErrorReports.{dt:yyyyMMddHHmmssfff}.[{pid}].log");
			Directory.CreateDirectory(Path.GetDirectoryName(fname));
			using (var fs = new FileStream(fname, FileMode.Append, FileAccess.Write, FileShare.None))
			using (var sw = new StreamWriter(fs, Encoding.UTF8))
			{
				sw.WriteLine(AupLauncher.Program.Get_Resources_ER_LINE_1(), Caption);
				sw.WriteLine(AupLauncher.Program.Get_Resources_ER_LINE_2());
				sw.WriteLine(AupLauncher.Program.Get_Resources_ER_LINE_3());
				sw.WriteLine(AupLauncher.Program.Get_Resources_ER_LINE_4(), Version, CodeName);
				sw.WriteLine(AupLauncher.Program.Get_Resources_ER_LINE_5(), dt);
				sw.WriteLine(AupLauncher.Program.Get_Resources_ER_LINE_6(), pid);
				sw.WriteLine(AupLauncher.Program.Get_Resources_ER_LINE_7(), fname);
				sw.WriteLine();
				int n = 0;
				do
				{
					sw.WriteLine(AupLauncher.Program.Get_Resources_ER_LINE_8_Title(), n);
					sw.WriteLine(AupLauncher.Program.Get_Resources_ER_LINE_8_Message(), e.Message);
					sw.WriteLine(AupLauncher.Program.Get_Resources_ER_LINE_8_HResult(), e.HResult);
					sw.WriteLine(AupLauncher.Program.Get_Resources_ER_LINE_8_HelpLink(), e.HelpLink);
					sw.WriteLine(AupLauncher.Program.Get_Resources_ER_LINE_8_Source(), e.Source);
					sw.WriteLine(AupLauncher.Program.Get_Resources_ER_LINE_8_SourceFunc(), e.TargetSite?.Name, e.TargetSite?.ReflectedType.FullName);
					sw.WriteLine(AupLauncher.Program.Get_Resources_ER_LINE_8_StackTrace());
					sw.WriteLine("    * {0}", e.StackTrace?.Replace(sw.NewLine, $"{sw.NewLine}    * "));
					sw.WriteLine(AupLauncher.Program.Get_Resources_ER_LINE_8_Data());
					foreach (var key in e.Data.Keys)
					{
						sw.WriteLine("    * [{0}]={1}", key.ToString().PadRight(48), e.Data[key]);
					}
					sw.WriteLine("----------------");
					sw.WriteLine(e.ToString());
					sw.WriteLine("================");
					sw.WriteLine();
					e = e.InnerException; ++n;
				} while (e != null);
			}
			MessageBox.Show(
				string.Format(AupLauncher.Program.Get_Resources_Message_ErrorReport(), msg, fname),
				Caption,
				MessageBoxButtons.OK,
				MessageBoxIcon.Error);
			return ret;
		}

		private static byte[] _aviutl_signature = new byte[] {
			0x41, 0x76, 0x69, 0x55, 0x74, 0x6C, 0x20, 0x50, 0x72, 0x6F, 0x6A, 0x65, 0x63, 0x74, 0x46, 0x69,
			0x6C, 0x65, 0x20, 0x76, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x20, 0x30, 0x2E, 0x31, 0x38, 0x00
		};
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
	}
}
