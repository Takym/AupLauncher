using AupLauncher.Properties;
using Microsoft.Win32;

namespace AupLauncher
{
	public class SettingProfile
	{
		private static readonly SettingProfile _default_value = new SettingProfile();

		public string        DisplayName          { get; set; }
		public string        AviUtlPath           { get; set; }
		public string        AviUtlArgs           { get; set; }
		public string        AudacityPath         { get; set; }
		public string        AudacityArgs         { get; set; }
		public string        CustomProgramPath    { get; set; }
		public string        CustomProgramArgs    { get; set; }
		public ExecutionKind HandleForInvalidFile { get; set; }
		public bool          IsDeleted            { get; set; }

		public SettingProfile()
		{
			this.Reset();
		}

		public virtual void LoadFromRegistry(RegistryKey reg)
		{
			this.DisplayName          = reg.GetValue(nameof(this.DisplayName),          _default_value.DisplayName)         .ToString();
			this.AviUtlPath           = reg.GetValue(nameof(this.AviUtlPath),           _default_value.AviUtlPath)          .ToString();
			this.AviUtlArgs           = reg.GetValue(nameof(this.AviUtlArgs),           _default_value.AviUtlArgs)          .ToString();
			this.AudacityPath         = reg.GetValue(nameof(this.AudacityPath),         _default_value.AudacityPath)        .ToString();
			this.AudacityArgs         = reg.GetValue(nameof(this.AudacityArgs),         _default_value.AudacityArgs)        .ToString();
			this.CustomProgramPath    = reg.GetValue(nameof(this.CustomProgramPath),    _default_value.CustomProgramPath)   .ToString();
			this.CustomProgramArgs    = reg.GetValue(nameof(this.CustomProgramArgs),    _default_value.CustomProgramArgs)   .ToString();
			this.HandleForInvalidFile = reg.GetValue(nameof(this.HandleForInvalidFile), _default_value.HandleForInvalidFile).ToExecutionKind();
		}

		public virtual void SaveToRegistry(RegistryKey reg)
		{
			reg.SetValue(nameof(this.DisplayName),          this.DisplayName,          RegistryValueKind.String);
			reg.SetValue(nameof(this.AviUtlPath),           this.AviUtlPath,           RegistryValueKind.String);
			reg.SetValue(nameof(this.AviUtlArgs),           this.AviUtlArgs,           RegistryValueKind.String);
			reg.SetValue(nameof(this.AudacityPath),         this.AudacityPath,         RegistryValueKind.String);
			reg.SetValue(nameof(this.AudacityArgs),         this.AudacityArgs,         RegistryValueKind.String);
			reg.SetValue(nameof(this.CustomProgramPath),    this.CustomProgramPath,    RegistryValueKind.String);
			reg.SetValue(nameof(this.CustomProgramArgs),    this.CustomProgramArgs,    RegistryValueKind.String);
			reg.SetValue(nameof(this.HandleForInvalidFile), this.HandleForInvalidFile);
		}

		public virtual void Reset()
		{
			this.DisplayName          = Resources.NewProfileDisplayName;
			this.AviUtlPath           = "C:\\AviUtl\\aviutl.exe";
			this.AviUtlArgs           = "%1";
			this.AudacityPath         = "C:\\Program Files (x86)\\Audacity\\audacity.exe";
			this.AudacityArgs         = "%1";
			this.CustomProgramPath    = "C:\\Windows\\notepad.exe";
			this.CustomProgramArgs    = "%1";
			this.HandleForInvalidFile = ExecutionKind.ShowError;
		}
	}
}
