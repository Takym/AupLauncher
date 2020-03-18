using AupLauncher.Properties;
using Microsoft.Win32;

namespace AupLauncher
{
	public class SettingProfile
	{
		private static readonly SettingProfile _default_value = new SettingProfile();

		public string DisplayName  { get; set; }
		public string AviUtlPath   { get; set; }
		public string AudacityPath { get; set; }
		public bool   IsDeleted    { get; set; }

		public SettingProfile()
		{
			this.DisplayName  = Resources.NewProfileDisplayName;
			this.AviUtlPath   = "C:\\AviUtl\\aviutl.exe";
			this.AudacityPath = "‪%ProgramFiles(x86)%\\Audacity\\audacity.exe";
		}

		public virtual void LoadFromRegistry(RegistryKey reg)
		{
			this.DisplayName  = reg.GetValue(nameof(this.DisplayName),  _default_value.DisplayName) .ToString();
			this.AviUtlPath   = reg.GetValue(nameof(this.AviUtlPath),   _default_value.AviUtlPath)  .ToString();
			this.AudacityPath = reg.GetValue(nameof(this.AudacityPath), _default_value.AudacityPath).ToString();
		}

		public virtual void SaveToRegistry(RegistryKey reg)
		{
			reg.SetValue(nameof(this.DisplayName),  this.DisplayName,  RegistryValueKind.String);
			reg.SetValue(nameof(this.AviUtlPath),   this.AviUtlPath,   RegistryValueKind.ExpandString);
			reg.SetValue(nameof(this.AudacityPath), this.AudacityPath, RegistryValueKind.ExpandString);
		}
	}
}
