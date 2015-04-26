#define AppName        GetStringFileInfo('..\Binaries\ChangeLetter.exe', 'ProductName')
#define AppVersion     GetStringFileInfo('..\Binaries\ChangeLetter.exe', 'ProductVersion')
#define AppFileVersion GetStringFileInfo('..\Binaries\ChangeLetter.exe', 'FileVersion')
#define AppCompany     GetStringFileInfo('..\Binaries\ChangeLetter.exe', 'CompanyName')
#define AppCopyright   GetStringFileInfo('..\Binaries\ChangeLetter.exe', 'LegalCopyright')
#define AppBase        LowerCase(StringChange(AppName, ' ', ''))
#define AppSetupFile   AppBase + StringChange(AppVersion, '.', '')

#define AppVersionEx   StringChange(AppVersion, '0.00', '')
#if "" != VersionHash
#  define AppVersionEx AppVersionEx + " (" + VersionHash + ")"
#endif


[Setup]
AppName={#AppName}
AppVersion={#AppVersion}
AppVerName={#AppName} {#AppVersion}
AppPublisher={#AppCompany}
AppPublisherURL=http://jmedved.com/{#AppBase}/
AppCopyright={#AppCopyright}
VersionInfoProductVersion={#AppVersion}
VersionInfoProductTextVersion={#AppVersionEx}
VersionInfoVersion={#AppFileVersion}
DefaultDirName={pf}\{#AppCompany}\{#AppName}
OutputBaseFilename={#AppSetupFile}
OutputDir=..\Releases
SourceDir=..\Binaries
AppId=JosipMedved_ChangeLetter
CloseApplications="yes"
RestartApplications="no"
UninstallDisplayIcon={app}\ChangeLetter.exe
AlwaysShowComponentsList=no
ArchitecturesInstallIn64BitMode=x64
DisableProgramGroupPage=yes
MergeDuplicateFiles=yes
MinVersion=0,5.1
PrivilegesRequired=admin
ShowLanguageDialog=no
SolidCompression=yes
ChangesAssociations=yes
DisableWelcomePage=yes
LicenseFile=..\Setup\License.rtf


[Messages]
SetupAppTitle=Setup {#AppName} {#AppVersionEx}
SetupWindowTitle=Setup {#AppName} {#AppVersionEx}
BeveledLabel=jmedved.com


[Files]
Source: "ChangeLetter.exe";          DestDir: "{app}";  Flags: ignoreversion;
Source: "ChangeLetterExecutor.exe";  DestDir: "{app}";  Flags: ignoreversion;
Source: "ReadMe.txt";  DestDir: "{app}";  Attribs: readonly;  Flags: overwritereadonly uninsremovereadonly;


[Tasks]
Name: context_drive;  Description: "Drive context menu";


[Icons]
Name: "{userstartmenu}\Change Letter";  Filename: "{app}\ChangeLetter.exe"


[Registry]
Root: HKCU;  Subkey: "Software\Josip Medved";                ValueType: none;    Flags: uninsdeletekeyifempty;
Root: HKLM;  Subkey: "Software\Josip Medved\Change Letter";  ValueType: none;    Flags: uninsdeletekeyifempty;

Root: HKCR;  Subkey: "Drive\shell\ChangeLetter";             ValueType: none;    Flags: deletekey uninsdeletekey;
Root: HKCR;  Subkey: "Drive\shell\ChangeLetter";             ValueType: string;  ValueName: "";                  ValueData: "Change letter";                  Tasks: context_drive;
Root: HKCR;  Subkey: "Drive\shell\ChangeLetter";             ValueType: string;  ValueName: "Icon";              ValueData: """{app}\ChangeLetter.exe""";     Tasks: context_drive;
Root: HKCR;  Subkey: "Drive\shell\ChangeLetter";             ValueType: string;  ValueName: "MultiSelectModel";  ValueData: "Single";                         Tasks: context_drive;
Root: HKCR;  Subkey: "Drive\shell\ChangeLetter\command";     ValueType: string;  ValueName: "";                  ValueData: """{app}\ChangeLetter.exe"" %1";  Tasks: context_drive


[Run]
Filename: "{app}\ChangeLetter.exe";  Flags: postinstall nowait skipifsilent runasoriginaluser unchecked;            Description: "Launch application now";
Filename: "{app}\ReadMe.txt";        Flags: postinstall nowait skipifsilent runasoriginaluser unchecked shellexec;  Description: "View ReadMe.txt";


[Code]

procedure InitializeWizard;
begin
  WizardForm.LicenseAcceptedRadio.Checked := True;
end;
