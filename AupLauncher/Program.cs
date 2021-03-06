﻿using System;
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
		public const string Copyright      = "Copyright (C) 2020 Takym.";
		public const string Version        = "0.0.0.7";
		public const string CodeName       = "aupl00a7";

		public static Settings Settings { get; private set; }

		[STAThread()]
		internal static int Main(string[] args)
		{
#if !DEBUG
			try {
#else
			{
#endif
				using (Settings = new Settings()) {
					if (args.Length == 1 && Settings.IsInstalled) {
						return StartupProgram(DetermineFileKind(args[0]));
					} else {
						Application.EnableVisualStyles();
						Application.SetCompatibleTextRenderingDefault(false);
						Application.Run(new FormMain());
						return 0;
					}
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
	}
}
