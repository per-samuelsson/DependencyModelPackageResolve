@echo off

dotnet build .\src\Target
dotnet msbuild .\src\SampleApp\SampleApp.csproj /t:Publish
dotnet .\src\SampleApp\bin\Debug\netcoreapp2.0\publish\SampleApp.dll ..\..\..\..\..\Target\bin\Debug\NetStandard2.0\Target.dll