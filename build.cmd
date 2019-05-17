@echo off
cls

::Only bootstrap when no paket.exe
if not exist .paket\paket.exe (
	.paket\paket.bootstrapper.exe
	if errorlevel 1 (
	  exit /b %errorlevel%
	)
)

dotnet restore build.proj

fake build --target %*