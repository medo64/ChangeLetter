#define AppName        GetStringFileInfo('..\Binaries\ChangeLetter.exe', 'ProductName')
#define AppVersion     GetStringFileInfo('..\Binaries\ChangeLetter.exe', 'ProductVersion')
#define AppFileVersion GetStringFileInfo('..\Binaries\ChangeLetter.exe', 'FileVersion')
#define AppCompany     GetStringFileInfo('..\Binaries\ChangeLetter.exe', 'CompanyName')
#define AppCopyright   GetStringFileInfo('..\Binaries\ChangeLetter.exe', 'LegalCopyright')
#define AppBase        LowerCase(StringChange(AppName, ' ', ''))
#define AppSetupFile   AppBase + StringChange(AppVersion, '.', '')

[Setup]
AppName={#AppName}
AppVersion={#AppVersion}
AppVerName={#AppName} {#AppVersion}
AppPublisher={#AppCompany}
AppPublisherURL=http://jmedved.com/{#AppBase}/
AppCopyright={#AppCopyright}
VersionInfoProductVersion={#AppVersion}
VersionInfoProductTextVersion={#AppVersion}
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
MinVersion=0,6.01.7200
PrivilegesRequired=admin
ShowLanguageDialog=no
SolidCompression=yes
ChangesAssociations=yes
DisableWelcomePage=yes


[Messages]
SetupAppTitle=Setup {#AppName} {#AppVersion}
SetupWindowTitle=Setup {#AppName} {#AppVersion}
BeveledLabel=jmedved.com


[Files]
Source: "ChangeLetter.exe";          DestDir: "{app}";  Flags: ignoreversion;
Source: "ChangeLetterExecutor.exe";  DestDir: "{app}";  Flags: ignoreversion;


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
Filename: "{app}\ChangeLetter.exe";  Flags: postinstall nowait skipifsilent runasoriginaluser unchecked;  Description: "Launch application now";
