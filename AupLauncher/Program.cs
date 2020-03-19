using AupLauncher.Properties;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using SVersion = System.Version;

namespace AupLauncher
{
	public static class Program
	{
		public const string Author    = "Takym";
		public const string Copyright = "Copyright (C) 2020 Takym.";
		public const string Version   = "0.0.0.0";
		public const string CodeName  = "aupl00a0";

		public static Settings Settings { get; private set; }

		[STAThread()]
		internal static int Main(string[] args)
		{
			try {
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
			} catch (Exception e) {
				return e.HResult;
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
			if (!File.Exists(fname)) {
				MessageBox.Show(
					string.Format(Resources.Message_FileNotFound, fname),
					nameof(AupLauncher),
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning);
				return (ExecutionKind.Nothing, fname);
			}
			using (var fs = new FileStream(fname, FileMode.Open, FileAccess.Read, FileShare.Read))
			using (var br = new BinaryReader(fs, Encoding.UTF8))
			using (var sr = new StreamReader(fs, Encoding.UTF8)) {
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
				psi.FileName  = Settings.Default.AviUtlPath;
				psi.Arguments = Settings.Default.AviUtlArgs;
				break;
			case ExecutionKind.Audacity:
				psi.FileName  = Settings.Default.AudacityPath;
				psi.Arguments = Settings.Default.AudacityArgs;
				break;
			case ExecutionKind.RunCustomProgram:
				psi.FileName  = Settings.Default.CustomProgramPath;
				psi.Arguments = Settings.Default.CustomProgramArgs;
				break;
			case ExecutionKind.ShowError:
				MessageBox.Show(
					string.Format(Resources.Message_InvalidType, arg.fname),
					nameof(AupLauncher),
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning);
				return 0;
			case ExecutionKind.Nothing:
				return 0;
			default: //case ExecutionKind.InvalidValue:
				MessageBox.Show(
					string.Format(Resources.Message_InvalidExecutionKind),
					nameof(AupLauncher),
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning);
				return -1;
			}
			psi.UseShellExecute   = true;
			psi.WorkingDirectory  = Path.GetDirectoryName(arg.fname);
			psi.Arguments        += $" \"{arg.fname}\"";
			psi.Verb              = "open";
			Process.Start(psi);
			return 0;
		}

		private static byte[] _aviutl_signature = new byte[] {
			0x41, 0x76, 0x69, 0x55, 0x74, 0x6C, 0x20, 0x50, 0x72, 0x6F, 0x6A, 0x65, 0x63, 0x74, 0x46, 0x69,
			0x6C, 0x65, 0x20, 0x76, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x20, 0x30, 0x2E, 0x31, 0x38, 0x00
		};
	}
}
