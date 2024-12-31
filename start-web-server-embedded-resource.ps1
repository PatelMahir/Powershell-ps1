$src = (Get-Item -Path "..\..\..\..\" -Verbose).FullName;
$name = "name-of-folder-where-web-server-api-resides";
$cdProjectDir = [string]::Format("cd /d {0}\{1}",$src, $name);
$params=@("/C"; $cdProjectDir; " && dotnet run"; )
Start-Process -Verb runas "cmd.exe" $params;
