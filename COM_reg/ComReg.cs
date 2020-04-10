using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace COM_reg
{
    public class ComReg
    {
        [DllExport(CallingConvention.StdCall)]
        public static void Register(IntPtr hwnd,IntPtr hinst, System.Text.StringBuilder lpszCmdLine, System.Int32 nCmdShow)
        {
            System.Threading.Thread.Sleep(15000);

			string path = System.IO.Path.Combine(RuntimeEnvironment.GetRuntimeDirectory(), "RegAsm.exe");
			// パスをコンソールに出力
			//Console.WriteLine("[" + path + "]");

			System.Diagnostics.Process p = new System.Diagnostics.Process();
			p.StartInfo.FileName = path;
			// 渡されたコマンドライン引数をそのまま渡す
			p.StartInfo.Arguments = "/codebase \"" + lpszCmdLine.ToString().Replace("\r","").Replace("\n","") + "\"";
			// 出力を取得できるようにする
			p.StartInfo.UseShellExecute = false;
			p.StartInfo.RedirectStandardOutput = true;
			p.StartInfo.RedirectStandardInput = false;
			// ウィンドウを表示しない
			p.StartInfo.CreateNoWindow = true;

			// 起動
			p.Start();

			// 出力を取得
			string results = p.StandardOutput.ReadToEnd();
			// プロセス終了まで待機する
			p.WaitForExit();
			p.Close();
			System.Diagnostics.Process p2 = new System.Diagnostics.Process();
			//起動する実行ファイルのパスを設定する
			p2.StartInfo.FileName = lpszCmdLine.ToString();

			//起動する。プロセスが起動した時はTrueを返す。
			bool resultaaa = p2.Start();

		}
		[DllExport(CallingConvention.StdCall)]
		public static void UnRegister(IntPtr hwnd, IntPtr hinst, System.Text.StringBuilder lpszCmdLine, System.Int32 nCmdShow)
		{
			System.Threading.Thread.Sleep(15000);

			string path = System.IO.Path.Combine(RuntimeEnvironment.GetRuntimeDirectory(), "RegAsm.exe");
			// パスをコンソールに出力
			//Console.WriteLine("[" + path + "]");

			System.Diagnostics.Process p = new System.Diagnostics.Process();
			p.StartInfo.FileName = path;
			// 渡されたコマンドライン引数をそのまま渡す
			p.StartInfo.Arguments = "/unregister \"" + lpszCmdLine.ToString() + "\"";
			// 出力を取得できるようにする
			p.StartInfo.UseShellExecute = false;
			p.StartInfo.RedirectStandardOutput = true;
			p.StartInfo.RedirectStandardInput = false;
			// ウィンドウを表示しない
			p.StartInfo.CreateNoWindow = true;

			// 起動
			p.Start();

			// 出力を取得
			string results = p.StandardOutput.ReadToEnd();
			// プロセス終了まで待機する
			p.WaitForExit();
			p.Close();
			System.Diagnostics.Process p2 = new System.Diagnostics.Process();
			//起動する実行ファイルのパスを設定する
			p2.StartInfo.FileName = lpszCmdLine.ToString();
			p2.StartInfo.Arguments = "";
			//起動する。プロセスが起動した時はTrueを返す。
			bool resultaaa = p2.Start();

		}
		[DllExport(CallingConvention.StdCall)]
		public static void UnRegister_UN(IntPtr hwnd, IntPtr hinst, System.Text.StringBuilder lpszCmdLine, System.Int32 nCmdShow)
		{
			System.Threading.Thread.Sleep(15000);

			string path = System.IO.Path.Combine(RuntimeEnvironment.GetRuntimeDirectory(), "RegAsm.exe");
			// パスをコンソールに出力
			//Console.WriteLine("[" + path + "]");

			System.Diagnostics.Process p = new System.Diagnostics.Process();
			p.StartInfo.FileName = path;
			// 渡されたコマンドライン引数をそのまま渡す
			p.StartInfo.Arguments = "/unregister \"" + lpszCmdLine.ToString() + "\"";
			// 出力を取得できるようにする
			p.StartInfo.UseShellExecute = false;
			p.StartInfo.RedirectStandardOutput = true;
			p.StartInfo.RedirectStandardInput = false;
			// ウィンドウを表示しない
			p.StartInfo.CreateNoWindow = true;

			// 起動
			p.Start();

			// 出力を取得
			string results = p.StandardOutput.ReadToEnd();
			// プロセス終了まで待機する
			p.WaitForExit();
			p.Close();
			System.Diagnostics.Process p2 = new System.Diagnostics.Process();
			//起動する実行ファイルのパスを設定する
			p2.StartInfo.FileName = lpszCmdLine.ToString();
			p2.StartInfo.Arguments = "/r";
			//起動する。プロセスが起動した時はTrueを返す。
			bool resultaaa = p2.Start();

		}
	}
}
