using System;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using AupLauncher.Properties;

namespace AupLauncher
{
	public partial class FormAbout : Form
	{
		public FormAbout()
		{
			this.InitializeComponent();
		}

		private void FormAbout_Load(object sender, EventArgs e)
		{
			var sb     = new StringBuilder();
			var config = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyConfigurationAttribute>();
			sb.AppendFormat(Resources.V_Line1, Program.Caption)      .AppendLine();
			sb.AppendFormat(Resources.V_Line2, Program.Author)       .AppendLine();
			sb.AppendFormat(Resources.V_Line3, Program.Copyright)    .AppendLine();
			sb.AppendFormat(Resources.V_Line4, Program.Version)      .AppendLine();
			sb.AppendFormat(Resources.V_Line5, Program.CodeName)     .AppendLine();
			sb.AppendFormat(Resources.V_Line6, config.Configuration) .AppendLine();
			sb.AppendFormat(Resources.V_Line7, Program.Description)  .AppendLine();
			sb.AppendFormat(Resources.V_Line8, Resources.Description).AppendLine();
			tbox.Text = sb.ToString();
		}
	}
}
