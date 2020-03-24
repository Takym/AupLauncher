using System;
using System.Windows.Forms;
using AupLauncher.Properties;

namespace AupLauncher
{
	public partial class FormMain : Form
	{
		public FormMain()
		{
			this.InitializeComponent();
			this.Text = $"{Program.Caption} [v{Program.Version}, cn:{Program.CodeName}]";
		}

		private void FormMain_Load(object sender, EventArgs e)
		{
			if (!Program.Settings.IsInstalled) {
				Program.Settings.Install();
			}
			cbox_invfile.Items.Add(ExecutionKind.ShowError       .Localized());
			cbox_invfile.Items.Add(ExecutionKind.Nothing         .Localized());
			cbox_invfile.Items.Add(ExecutionKind.AviUtl          .Localized());
			cbox_invfile.Items.Add(ExecutionKind.Audacity        .Localized());
			cbox_invfile.Items.Add(ExecutionKind.RunCustomProgram.Localized());
			this.btnReload_Click(sender, e);
		}

		private void btn_avi_path_Click(object sender, EventArgs e)
		{
			ofd.Reset();
			ofd.ReadOnlyChecked = true;
			ofd.Filter          = Resources.UI_avi_path_filter;
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
			ofd.Filter          = Resources.UI_aud_path_filter;
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
			ofd.Filter          = Resources.UI_cus_path_filter;
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

			btnReload.Enabled = true;
			btnSave  .Enabled = true;
		}

		private void btnReload_Click(object sender, EventArgs e)
		{
			Program.Settings.LoadFromRegistry();
			tbox_avi_path.Text        = Program.Settings.Default.AviUtlPath;
			tbox_avi_args.Text        = Program.Settings.Default.AviUtlArgs;
			tbox_aud_path.Text        = Program.Settings.Default.AudacityPath;
			tbox_aud_args.Text        = Program.Settings.Default.AudacityArgs;
			tbox_cus_path.Text        = Program.Settings.Default.CustomProgramPath;
			tbox_cus_args.Text        = Program.Settings.Default.CustomProgramArgs;
			cbox_invfile.SelectedItem = Program.Settings.Default.HandleForInvalidFile.Localized();

			this.SetCustomProgramEnabled();

			btnReload.Enabled = false;
			btnSave  .Enabled = false;
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
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
			var dr = MessageBox.Show(
				Resources.Message_ConfirmUninstalling,
				Program.Caption,
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);
			if (dr == DialogResult.Yes) {
				Program.Settings.Uninstall();
				this.Close();
			}
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
