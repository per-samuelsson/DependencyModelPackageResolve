@echo off

dotnet build .\src\Target
dotnet run -p .\src\SampleApp\SampleApp.csproj ..\..\..\..\Target\bin\Debug\NetStandard2.0\Target.dll