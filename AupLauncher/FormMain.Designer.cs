namespace AupLauncher
{
	partial class FormMain
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.labelDesc = new System.Windows.Forms.Label();
            this.labelAviUtlPath = new System.Windows.Forms.Label();
            this.labelAudacityPath = new System.Windows.Forms.Label();
            this.labelCustomPath = new System.Windows.Forms.Label();
            this.labelAviUtlArgs = new System.Windows.Forms.Label();
            this.labelAudacityArgs = new System.Windows.Forms.Label();
            this.labelCustomArgs = new System.Windows.Forms.Label();
            this.tbox_avi_path = new System.Windows.Forms.TextBox();
            this.tbox_avi_args = new System.Windows.Forms.TextBox();
            this.tbox_aud_path = new System.Windows.Forms.TextBox();
            this.tbox_aud_args = new System.Windows.Forms.TextBox();
            this.tbox_cus_path = new System.Windows.Forms.TextBox();
            this.tbox_cus_args = new System.Windows.Forms.TextBox();
            this.cbox_invfile = new System.Windows.Forms.ComboBox();
            this.labelInvfile = new System.Windows.Forms.Label();
            this.btn_avi_path = new System.Windows.Forms.Button();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.btn_aud_path = new System.Windows.Forms.Button();
            this.btn_cus_path = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnUninstall = new System.Windows.Forms.Button();
            this.btnReload = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnComInstall = new System.Windows.Forms.Button();
            this.btnComUnInstall = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelDesc
            // 
            resources.ApplyResources(this.labelDesc, "labelDesc");
            this.labelDesc.Name = "labelDesc";
            // 
            // labelAviUtlPath
            // 
            resources.ApplyResources(this.labelAviUtlPath, "labelAviUtlPath");
            this.labelAviUtlPath.Name = "labelAviUtlPath";
            // 
            // labelAudacityPath
            // 
            resources.ApplyResources(this.labelAudacityPath, "labelAudacityPath");
            this.labelAudacityPath.Name = "labelAudacityPath";
            // 
            // labelCustomPath
            // 
            resources.ApplyResources(this.labelCustomPath, "labelCustomPath");
            this.labelCustomPath.Name = "labelCustomPath";
            // 
            // labelAviUtlArgs
            // 
            resources.ApplyResources(this.labelAviUtlArgs, "labelAviUtlArgs");
            this.labelAviUtlArgs.Name = "labelAviUtlArgs";
            // 
            // labelAudacityArgs
            // 
            resources.ApplyResources(this.labelAudacityArgs, "labelAudacityArgs");
            this.labelAudacityArgs.Name = "labelAudacityArgs";
            // 
            // labelCustomArgs
            // 
            resources.ApplyResources(this.labelCustomArgs, "labelCustomArgs");
            this.labelCustomArgs.Name = "labelCustomArgs";
            // 
            // tbox_avi_path
            // 
            resources.ApplyResources(this.tbox_avi_path, "tbox_avi_path");
            this.tbox_avi_path.Name = "tbox_avi_path";
            this.tbox_avi_path.TextChanged += new System.EventHandler(this.tbox_TextChanged);
            // 
            // tbox_avi_args
            // 
            resources.ApplyResources(this.tbox_avi_args, "tbox_avi_args");
            this.tbox_avi_args.Name = "tbox_avi_args";
            this.tbox_avi_args.TextChanged += new System.EventHandler(this.tbox_TextChanged);
            // 
            // tbox_aud_path
            // 
            resources.ApplyResources(this.tbox_aud_path, "tbox_aud_path");
            this.tbox_aud_path.Name = "tbox_aud_path";
            this.tbox_aud_path.TextChanged += new System.EventHandler(this.tbox_TextChanged);
            // 
            // tbox_aud_args
            // 
            resources.ApplyResources(this.tbox_aud_args, "tbox_aud_args");
            this.tbox_aud_args.Name = "tbox_aud_args";
            this.tbox_aud_args.TextChanged += new System.EventHandler(this.tbox_TextChanged);
            // 
            // tbox_cus_path
            // 
            resources.ApplyResources(this.tbox_cus_path, "tbox_cus_path");
            this.tbox_cus_path.Name = "tbox_cus_path";
            this.tbox_cus_path.TextChanged += new System.EventHandler(this.tbox_TextChanged);
            // 
            // tbox_cus_args
            // 
            resources.ApplyResources(this.tbox_cus_args, "tbox_cus_args");
            this.tbox_cus_args.Name = "tbox_cus_args";
            this.tbox_cus_args.TextChanged += new System.EventHandler(this.tbox_TextChanged);
            // 
            // cbox_invfile
            // 
            this.cbox_invfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbox_invfile.FormattingEnabled = true;
            resources.ApplyResources(this.cbox_invfile, "cbox_invfile");
            this.cbox_invfile.Name = "cbox_invfile";
            this.cbox_invfile.SelectedIndexChanged += new System.EventHandler(this.cbox_invfile_SelectedIndexChanged);
            // 
            // labelInvfile
            // 
            resources.ApplyResources(this.labelInvfile, "labelInvfile");
            this.labelInvfile.Name = "labelInvfile";
            // 
            // btn_avi_path
            // 
            resources.ApplyResources(this.btn_avi_path, "btn_avi_path");
            this.btn_avi_path.Name = "btn_avi_path";
            this.btn_avi_path.UseVisualStyleBackColor = true;
            this.btn_avi_path.Click += new System.EventHandler(this.btn_avi_path_Click);
            // 
            // ofd
            // 
            this.ofd.ReadOnlyChecked = true;
            // 
            // btn_aud_path
            // 
            resources.ApplyResources(this.btn_aud_path, "btn_aud_path");
            this.btn_aud_path.Name = "btn_aud_path";
            this.btn_aud_path.UseVisualStyleBackColor = true;
            this.btn_aud_path.Click += new System.EventHandler(this.btn_aud_path_Click);
            // 
            // btn_cus_path
            // 
            resources.ApplyResources(this.btn_cus_path, "btn_cus_path");
            this.btn_cus_path.Name = "btn_cus_path";
            this.btn_cus_path.UseVisualStyleBackColor = true;
            this.btn_cus_path.Click += new System.EventHandler(this.btn_cus_path_Click);
            // 
            // btnReset
            // 
            resources.ApplyResources(this.btnReset, "btnReset");
            this.btnReset.Name = "btnReset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnUninstall
            // 
            resources.ApplyResources(this.btnUninstall, "btnUninstall");
            this.btnUninstall.Name = "btnUninstall";
            this.btnUninstall.UseVisualStyleBackColor = true;
            this.btnUninstall.Click += new System.EventHandler(this.btnUninstall_Click);
            // 
            // btnReload
            // 
            resources.ApplyResources(this.btnReload, "btnReload");
            this.btnReload.Name = "btnReload";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnComInstall
            // 
            resources.ApplyResources(this.btnComInstall, "btnComInstall");
            this.btnComInstall.Name = "btnComInstall";
            this.btnComInstall.UseVisualStyleBackColor = true;
            this.btnComInstall.Click += new System.EventHandler(this.btnComInstall_Click);
            // 
            // btnComUnInstall
            // 
            resources.ApplyResources(this.btnComUnInstall, "btnComUnInstall");
            this.btnComUnInstall.Name = "btnComUnInstall";
            this.btnComUnInstall.UseVisualStyleBackColor = true;
            this.btnComUnInstall.Click += new System.EventHandler(this.btnComUnInstall_Click);
            // 
            // FormMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnComUnInstall);
            this.Controls.Add(this.btnComInstall);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.btnUninstall);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btn_cus_path);
            this.Controls.Add(this.btn_aud_path);
            this.Controls.Add(this.btn_avi_path);
            this.Controls.Add(this.labelInvfile);
            this.Controls.Add(this.cbox_invfile);
            this.Controls.Add(this.tbox_cus_args);
            this.Controls.Add(this.tbox_cus_path);
            this.Controls.Add(this.tbox_aud_args);
            this.Controls.Add(this.tbox_aud_path);
            this.Controls.Add(this.tbox_avi_args);
            this.Controls.Add(this.tbox_avi_path);
            this.Controls.Add(this.labelCustomArgs);
            this.Controls.Add(this.labelAudacityArgs);
            this.Controls.Add(this.labelAviUtlArgs);
            this.Controls.Add(this.labelCustomPath);
            this.Controls.Add(this.labelAudacityPath);
            this.Controls.Add(this.labelAviUtlPath);
            this.Controls.Add(this.labelDesc);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Icon = global::AupLauncher.Properties.Resources.AupFile;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.FormMain_HelpButtonClicked);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelDesc;
		private System.Windows.Forms.Label labelAviUtlPath;
		private System.Windows.Forms.Label labelAudacityPath;
		private System.Windows.Forms.Label labelCustomPath;
		private System.Windows.Forms.Label labelAviUtlArgs;
		private System.Windows.Forms.Label labelAudacityArgs;
		private System.Windows.Forms.Label labelCustomArgs;
		private System.Windows.Forms.TextBox tbox_avi_path;
		private System.Windows.Forms.TextBox tbox_avi_args;
		private System.Windows.Forms.TextBox tbox_aud_path;
		private System.Windows.Forms.TextBox tbox_aud_args;
		private System.Windows.Forms.TextBox tbox_cus_path;
		private System.Windows.Forms.TextBox tbox_cus_args;
		private System.Windows.Forms.ComboBox cbox_invfile;
		private System.Windows.Forms.Label labelInvfile;
		private System.Windows.Forms.Button btn_avi_path;
		private System.Windows.Forms.OpenFileDialog ofd;
		private System.Windows.Forms.Button btn_aud_path;
		private System.Windows.Forms.Button btn_cus_path;
		private System.Windows.Forms.Button btnReset;
		private System.Windows.Forms.Button btnUninstall;
		private System.Windows.Forms.Button btnReload;
		private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnComInstall;
        private System.Windows.Forms.Button btnComUnInstall;
    }
}

