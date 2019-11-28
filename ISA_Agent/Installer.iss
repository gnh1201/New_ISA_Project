[Setup]
AppName=ISA Agent Demo
AppVersion=1.0
WizardStyle=modern
DefaultDirName={autopf}\ISA
DefaultGroupName=ISA
UninstallDisplayIcon={app}\ISA_Agent.exe
Compression=lzma2
SolidCompression=yes
OutputDir=userdocs:InstallerBuilds

[Files]
Source: "bin/Release/*"; DestDir: "{app}"
Source: "html/*"; DestDir: "{app}/html"; Flags: ignoreversion recursesubdirs

[Icons]
Name: "{group}\ISA Agent Demo"; Filename: "{app}\ISA_Agent.exe"

[Run]
Filename: "{app}\ISA_Agent.exe"; Description: "Start {cm:AppName}";

[CustomMessages]
AppName=ISA Agent Demo
