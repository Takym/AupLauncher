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

		[STAThread()]
		internal static int Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new FormMain());
			return 0;
		}
	}
}
