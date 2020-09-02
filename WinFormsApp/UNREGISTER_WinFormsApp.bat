@echo off
echo NOTICE: Script MUST be run as Administrator.
:: Errors from "reg" tool are muted to avoid flooding the build log with errors from already deleted registry entries.

:: Fix issue with "Run as Administrator" current dir
setlocal enableextensions
cd /d "%~dp0"


:: Remove all traces of WinFormsApp from registry
for %%R in (HKEY_LOCAL_MACHINE HKEY_CURRENT_USER) do (
  :: TypeLib
  reg delete "%%R\SOFTWARE\Classes\TypeLib\{65A4951D-E0FF-4B47-8FAD-30EBFB6BABC4}" /f 2> NUL
  
  for %%P in (32 64) do (
    :: COM class
    reg delete "%%R\SOFTWARE\Classes\CLSID\{1DF2592C-39F6-4679-AA20-660440E28CD0}"     /f /reg:%%P 2> NUL
  )
)

::pause
