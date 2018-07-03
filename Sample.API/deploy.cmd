REM CLEAN release folder:
rd /s /q "bin/Release"
dotnet build -c Release
dotnet publish -c Release
cf push sample-api  