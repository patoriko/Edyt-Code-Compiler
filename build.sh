#!/bin/bash

# Install packages

cd C:\\Users\\%USERNAME%\\source\\repos\\Edyt-Code-Compiler

NuGet.exe install "Edyt-Code-Compiler/packages.config" -o packages/


# Build application

cd C:\\Windows\\Microsoft.NET\\Framework\\v4*

msbuild "C:\Users\%USERNAME%\source\repos\Edyt-Code-Compiler\edyt.sln" /t:Rebuild /p:OutDir = C:\\Users\\%USERNAME%\\source\\repos\\Edyt-Code-Compiler; Configuration=Release; Platform = "Any CPU"