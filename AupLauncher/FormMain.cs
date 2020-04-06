using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using AupLauncher.Properties;

namespace AupLauncher
{
	public partial class FormMain : Form
	{
		[DllImport("user32.dll")]
		private static extern IntPtr SendMessage(HandleRef hWnd,
	uint Msg, IntPtr wParam, IntPtr lParam);
		private const int BCM_FIRST = 0x1600;
		private const int BCM_SETSHIELD = BCM_FIRST + 0x000C;

		/// <summary>
		/// UACの盾アイコンをボタンコントロールに表示（あるいは、非表示）する
		/// </summary>
		/// <param name="targetButton">盾アイコンを表示するボタンコントロール</param>
		/// <param name="showShield">盾アイコンを表示する時はtrue。
		/// 非表示にする時はfalse1。</param>
		public static void SetShieldIcon(Button targetButton, bool showShield)
		{
			if (targetButton == null)
			{
				throw new ArgumentNullException("targetButton");
			}

			//Windows Vista以上か確認する
			if (Environment.OSVersion.Platform != PlatformID.Win32NT ||
				Environment.OSVersion.Version.Major < 6)
			{
				return;
			}

			//FlatStyleをSystemにする
			targetButton.FlatStyle = FlatStyle.System;

			//盾アイコンを表示（または非表示）にする
			SendMessage(new HandleRef(targetButton, targetButton.Handle),
				BCM_SETSHIELD,
				IntPtr.Zero,
				showShield ? new IntPtr(1) : IntPtr.Zero);
		}

		/// <summary>
		/// UACの盾アイコンをボタンコントロールに表示する
		/// </summary>
		/// <param name="targetButton">盾アイコンを表示するボタンコントロール</param>
		public static void SetShieldIcon(Button targetButton)
		{
			SetShieldIcon(targetButton, true);
		}
		private readonly LocalizedExecutionKind _ek_invalid_value = ExecutionKind.InvalidValue.Localized();
		private bool IsAdministrator()
		{
			var identity = System.Security.Principal.WindowsIdentity.GetCurrent();
			var principal = new System.Security.Principal.WindowsPrincipal(identity);
			return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
		}
		public FormMain(Settings settingsa)
		{
			this.InitializeComponent();
			this.Text = $"{Program.Caption} [v{Program.Version}, cn:{Program.CodeName}]";
			Program.Settings = settingsa;
		}

		private void FormMain_Load(object sender, EventArgs e)
		{
			if (!Program.Settings.IsInstalled)
			{
				if (IsAdministrator())
				{
					Program.Settings.Install();
					MessageBox.Show(null,Resources.Message_Installed, nameof(AupLauncher).ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else
				{
					RunElevated(System.Reflection.Assembly.GetEntryAssembly().Location, "/i", null, false);
					Application.Exit();
				}

			}
			cbox_invfile.Items.Add(ExecutionKind.ShowError.Localized());
			cbox_invfile.Items.Add(ExecutionKind.Nothing.Localized());
			cbox_invfile.Items.Add(ExecutionKind.AviUtl.Localized());
			cbox_invfile.Items.Add(ExecutionKind.Audacity.Localized());
			cbox_invfile.Items.Add(ExecutionKind.RunCustomProgram.Localized());
			cbox_invfile.Items.Add(ExecutionKind.ShowSettings.Localized());
			this.btnReload_Click(sender, e);
			if (!IsAdministrator())
			{
				SetShieldIcon(this.btnComInstall);
				SetShieldIcon(this.btnComUnInstall);
				SetShieldIcon(this.btnUninstall);
			}
		}

		private void FormMain_HelpButtonClicked(object sender, CancelEventArgs e)
		{
			e.Cancel = true;
			new FormAbout().ShowDialog(this);
		}

		private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing && btnSave.Enabled)
			{
				var dr = MessageBox.Show(
					Resources.Message_ConfirmClosing,
					Program.Caption,
					MessageBoxButtons.YesNoCancel,
					MessageBoxIcon.Question);
				if (dr == DialogResult.Yes)
				{
					this.btnSave_Click(sender, e);
				}
				else if (dr == DialogResult.Cancel)
				{
					e.Cancel = true;
				}
			}
		}

		private void btn_avi_path_Click(object sender, EventArgs e)
		{
			ofd.Reset();
			ofd.ReadOnlyChecked = true;
			ofd.Filter = Resources.UI_avi_path_filter + "|aviutl.exe";
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				tbox_avi_path.Text = ofd.FileName;
			}

			btnReload.Enabled = true;
			btnSave.Enabled = true;
		}

		private void btn_aud_path_Click(object sender, EventArgs e)
		{
			ofd.Reset();
			ofd.ReadOnlyChecked = true;
			ofd.Filter = Resources.UI_aud_path_filter + "|audacity.exe";
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				tbox_aud_path.Text = ofd.FileName;
			}

			btnReload.Enabled = true;
			btnSave.Enabled = true;
		}

		private void btn_cus_path_Click(object sender, EventArgs e)
		{
			ofd.Reset();
			ofd.ReadOnlyChecked = true;
			ofd.Filter = Resources.UI_cus_path_filter + "|*.exe;*.com;*.bat;*.cmd";
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				tbox_cus_path.Text = ofd.FileName;
			}

			btnReload.Enabled = true;
			btnSave.Enabled = true;
		}

		private void tbox_TextChanged(object sender, EventArgs e)
		{
			btnReload.Enabled = true;
			btnSave.Enabled = true;
		}

		private void cbox_invfile_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.SetCustomProgramEnabled();

			btnReload.Enabled = true;
			btnSave.Enabled = true;
		}

		private void btnReset_Click(object sender, EventArgs e)
		{
			Program.Settings.Default.Reset();
			this.RefreshControls();

			btnReload.Enabled = true;
			btnSave.Enabled = true;
		}

		private void btnReload_Click(object sender, EventArgs e)
		{
			Program.Settings.LoadFromRegistry();
			this.RefreshControls();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{

			if (!_ek_invalid_value.Equals(cbox_invfile.SelectedItem))
			{
				cbox_invfile.Items.Remove(_ek_invalid_value);
			}
			Program.Settings.Default.AviUtlPath = tbox_avi_path.Text;
			Program.Settings.Default.AviUtlArgs = tbox_avi_args.Text;
			Program.Settings.Default.AudacityPath = tbox_aud_path.Text;
			Program.Settings.Default.AudacityArgs = tbox_aud_args.Text;
			Program.Settings.Default.CustomProgramPath = tbox_cus_path.Text;
			Program.Settings.Default.CustomProgramArgs = tbox_cus_args.Text;
			Program.Settings.Default.HandleForInvalidFile = ((LocalizedExecutionKind)(cbox_invfile.SelectedItem)).Value;
			Program.Settings.SaveToRegistry();

			btnReload.Enabled = false;
			btnSave.Enabled = false;
		}

		private void btnUninstall_Click(object sender, EventArgs e)
		{
			var dr = MessageBox.Show(
			Resources.Message_ConfirmUninstalling,
			Program.Caption,
			MessageBoxButtons.YesNo,
			MessageBoxIcon.Question);
			if (dr == DialogResult.Yes)
			{
				if (IsAdministrator())
				{
					Program.Settings.Uninstall();
				}
				else
				{
					RunElevated(System.Reflection.Assembly.GetEntryAssembly().Location, "/r", this, true);
					

				}
				MessageBox.Show(this,Resources.Message_UnInstalled, nameof(AupLauncher).ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
				this.Close();

			}
		}

		private void RefreshControls()
		{
			var hfif = Program.Settings.Default.HandleForInvalidFile;
			if (cbox_invfile.Items.Contains(_ek_invalid_value))
			{
				cbox_invfile.Items.Remove(_ek_invalid_value);
			}
			if (hfif == ExecutionKind.InvalidValue)
			{
				cbox_invfile.Items.Add(_ek_invalid_value);
			}
			tbox_avi_path.Text = Program.Settings.Default.AviUtlPath;
			tbox_avi_args.Text = Program.Settings.Default.AviUtlArgs;
			tbox_aud_path.Text = Program.Settings.Default.AudacityPath;
			tbox_aud_args.Text = Program.Settings.Default.AudacityArgs;
			tbox_cus_path.Text = Program.Settings.Default.CustomProgramPath;
			tbox_cus_args.Text = Program.Settings.Default.CustomProgramArgs;
			cbox_invfile.SelectedItem = hfif.Localized();

			this.SetCustomProgramEnabled();

			btnReload.Enabled = false;
			btnSave.Enabled = false;
		}

		private void SetCustomProgramEnabled()
		{
			if (((LocalizedExecutionKind)(cbox_invfile.SelectedItem)).Value == ExecutionKind.RunCustomProgram)
			{
				labelCustomPath.Enabled = true;
				labelCustomArgs.Enabled = true;
				tbox_cus_path.Enabled = true;
				tbox_cus_args.Enabled = true;
				btn_cus_path.Enabled = true;
			}
			else
			{
				labelCustomPath.Enabled = false;
				labelCustomArgs.Enabled = false;
				tbox_cus_path.Enabled = false;
				tbox_cus_args.Enabled = false;
				btn_cus_path.Enabled = false;
			}
		}

		private void btnComInstall_Click(object sender, EventArgs e)
		{
			if (IsAdministrator())
			{
				Program.Settings.ShellEx_COM_Register();


			}
			else
			{
				RunElevated(System.Reflection.Assembly.GetEntryAssembly().Location, "/icom", this, true);
				
			}

			MessageBox.Show(this, Resources.Message_COMInstalled, nameof(AupLauncher).ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
		/// <summary>
		/// 管理者権限が必要なプログラムを起動する
		/// </summary>
		/// <param name="fileName">プログラムのフルパス。</param>
		/// <param name="arguments">プログラムに渡すコマンドライン引数。</param>
		/// <param name="parentForm">親プログラムのウィンドウ。</param>
		/// <param name="waitExit">起動したプログラムが終了するまで待機する。</param>
		/// <returns>起動に成功した時はtrue。
		/// 「ユーザーアカウント制御」ダイアログでキャンセルされた時はfalse。</returns>
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

		private void btnComUnInstall_Click(object sender, EventArgs e)
		{
			if (IsAdministrator())
			{
				Program.Settings.ShellEx_COM_UnRegister();

			}
			else
			{
				RunElevated(System.Reflection.Assembly.GetEntryAssembly().Location, "/uncom", this, true);

			}
			MessageBox.Show(this, Resources.Message_COMUnInstalled, nameof(AupLauncher).ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);

		}
	}
}
