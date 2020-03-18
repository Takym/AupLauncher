using System;
using System.Windows.Forms;

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
			using (Settings = new Settings()) {
				if (args.Length == 1 && Settings.IsInstalled) {
					// TODO: ここでAviUtlのプロジェクトファイルかAudacityのプロジェクトファイルか判別する。
					return 0;
				} else {
					Application.EnableVisualStyles();
					Application.SetCompatibleTextRenderingDefault(false);
					Application.Run(new FormMain());
					return 1;
				}
			}
		}
	}
}
