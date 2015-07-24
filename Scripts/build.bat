
set version=%1
if "%version%" == "" (
   set version=0.0.0.0
)
set config=%2
if "%config%" == "" (
   set config=Release
)

%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild Build.config /p:Configuration="%config%" /p:build_number="%version%"