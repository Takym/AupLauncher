using System;
using System.Windows.Forms;

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
		}
	}
}
