using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle(AupLauncher.Program.Caption)]
[assembly: AssemblyProduct(AupLauncher.Program.Caption)]
[assembly: AssemblyDescription(AupLauncher.Program.Description)]
[assembly: AssemblyCompany(AupLauncher.Program.Author)]
[assembly: AssemblyCopyright(AupLauncher.Program.Copyright)]
[assembly: AssemblyVersion(AupLauncher.Program.Version)]
[assembly: AssemblyFileVersion(AupLauncher.Program.Version)]
[assembly: AssemblyInformationalVersion(AupLauncher.Program.CodeName)]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: Guid("c174ef44-fedc-42ce-92f7-41d969651530")]

#if REMOTE
[assembly: AssemblyConfiguration("Any CPU (32-bit preferred); Remote")]
#elif DEBUG
[assembly: AssemblyConfiguration("Any CPU (32-bit preferred); Debug")]
#else
[assembly: AssemblyConfiguration("Any CPU (64-bit preferred); Release")]
#endif
