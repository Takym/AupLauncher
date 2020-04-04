using System;
using System.ComponentModel;
using System.Windows.Forms;
using AupLauncher.Properties;

namespace AupLauncher
{
	public partial class FormMain : Form
	{
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
				if (IsAdministrator()) { 
				Program.Settings.Install();
				}
				else
				{
					MessageBox.Show(this, "管理者権限で実行してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
					Application.Exit();
				}

			}
			cbox_invfile.Items.Add(ExecutionKind.ShowError       .Localized());
			cbox_invfile.Items.Add(ExecutionKind.Nothing         .Localized());
			cbox_invfile.Items.Add(ExecutionKind.AviUtl          .Localized());
			cbox_invfile.Items.Add(ExecutionKind.Audacity        .Localized());
			cbox_invfile.Items.Add(ExecutionKind.RunCustomProgram.Localized());
			cbox_invfile.Items.Add(ExecutionKind.ShowSettings    .Localized());
			this.btnReload_Click(sender, e);
		}

		private void FormMain_HelpButtonClicked(object sender, CancelEventArgs e)
		{
			e.Cancel = true;
			new FormAbout().ShowDialog(this);
		}

		private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing && btnSave.Enabled) {
				var dr = MessageBox.Show(
					Resources.Message_ConfirmClosing,
					Program.Caption,
					MessageBoxButtons.YesNoCancel,
					MessageBoxIcon.Question);
				if (dr == DialogResult.Yes) {
					this.btnSave_Click(sender, e);
				} else if (dr == DialogResult.Cancel) {
					e.Cancel = true;
				}
			}
		}

		private void btn_avi_path_Click(object sender, EventArgs e)
		{
			ofd.Reset();
			ofd.ReadOnlyChecked = true;
			ofd.Filter          = Resources.UI_avi_path_filter + "|aviutl.exe";
			if (ofd.ShowDialog() == DialogResult.OK) {
				tbox_avi_path.Text = ofd.FileName;
			}

			btnReload.Enabled = true;
			btnSave  .Enabled = true;
		}

		private void btn_aud_path_Click(object sender, EventArgs e)
		{
			ofd.Reset();
			ofd.ReadOnlyChecked = true;
			ofd.Filter          = Resources.UI_aud_path_filter + "|audacity.exe";
			if (ofd.ShowDialog() == DialogResult.OK) {
				tbox_aud_path.Text = ofd.FileName;
			}

			btnReload.Enabled = true;
			btnSave  .Enabled = true;
		}

		private void btn_cus_path_Click(object sender, EventArgs e)
		{
			ofd.Reset();
			ofd.ReadOnlyChecked = true;
			ofd.Filter          = Resources.UI_cus_path_filter + "|*.exe;*.com;*.bat;*.cmd";
			if (ofd.ShowDialog() == DialogResult.OK) {
				tbox_cus_path.Text = ofd.FileName;
			}

			btnReload.Enabled = true;
			btnSave  .Enabled = true;
		}

		private void tbox_TextChanged(object sender, EventArgs e)
		{
			btnReload.Enabled = true;
			btnSave  .Enabled = true;
		}

		private void cbox_invfile_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.SetCustomProgramEnabled();

			btnReload.Enabled = true;
			btnSave  .Enabled = true;
		}

		private void btnReset_Click(object sender, EventArgs e)
		{
			Program.Settings.Default.Reset();
			this.RefreshControls();

			btnReload.Enabled = true;
			btnSave  .Enabled = true;
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
			Program.Settings.Default.AviUtlPath           = tbox_avi_path.Text;
			Program.Settings.Default.AviUtlArgs           = tbox_avi_args.Text;
			Program.Settings.Default.AudacityPath         = tbox_aud_path.Text;
			Program.Settings.Default.AudacityArgs         = tbox_aud_args.Text;
			Program.Settings.Default.CustomProgramPath    = tbox_cus_path.Text;
			Program.Settings.Default.CustomProgramArgs    = tbox_cus_args.Text;
			Program.Settings.Default.HandleForInvalidFile = ((LocalizedExecutionKind)(cbox_invfile.SelectedItem)).Value;
			Program.Settings.SaveToRegistry();

			btnReload.Enabled = false;
			btnSave  .Enabled = false;
		}

		private void btnUninstall_Click(object sender, EventArgs e)
		{
			if (IsAdministrator())
			{
				var dr = MessageBox.Show(
				Resources.Message_ConfirmUninstalling,
				Program.Caption,
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);
				if (dr == DialogResult.Yes)
				{
					Program.Settings.Uninstall();
					this.Close();
				}
			}
			else
			{
				MessageBox.Show(this, "管理者権限で実行してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
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
			tbox_avi_path.Text        = Program.Settings.Default.AviUtlPath;
			tbox_avi_args.Text        = Program.Settings.Default.AviUtlArgs;
			tbox_aud_path.Text        = Program.Settings.Default.AudacityPath;
			tbox_aud_args.Text        = Program.Settings.Default.AudacityArgs;
			tbox_cus_path.Text        = Program.Settings.Default.CustomProgramPath;
			tbox_cus_args.Text        = Program.Settings.Default.CustomProgramArgs;
			cbox_invfile.SelectedItem = hfif.Localized();

			this.SetCustomProgramEnabled();

			btnReload.Enabled = false;
			btnSave.Enabled   = false;
		}

		private void SetCustomProgramEnabled()
		{
			if (((LocalizedExecutionKind)(cbox_invfile.SelectedItem)).Value == ExecutionKind.RunCustomProgram) {
				labelCustomPath.Enabled = true;
				labelCustomArgs.Enabled = true;
				tbox_cus_path  .Enabled = true;
				tbox_cus_args  .Enabled = true;
				btn_cus_path   .Enabled = true;
			} else {
				labelCustomPath.Enabled = false;
				labelCustomArgs.Enabled = false;
				tbox_cus_path  .Enabled = false;
				tbox_cus_args  .Enabled = false;
				btn_cus_path   .Enabled = false;
			}
		}
	}
}
