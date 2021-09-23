# Install packages

cd C:\\Users\\Pat\\source\\repos\\Edyt-Code-Compiler

NuGet.exe install "Edyt-Code-Compiler/packages.config" -o packages/


# Build application

cd C:\\Windows\\Microsoft.NET\\Framework\\v4*

msbuild "C:\Users\Pat\source\repos\Edyt-Code-Compiler\edyt.sln" /t:Rebuild /p:Configuration=Release /p:Platform="Any CPU"