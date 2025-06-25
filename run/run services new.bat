@echo off
setlocal enabledelayedexpansion

:: 定义你要执行 dotnet run 的项目目录列表
set "dirs=../BaseService/BaseService.Host ../AuthServer/IdentityServer/AuthServer.Host ../../../MicroServices/Business/Business.Host"

:: 当前目录
set "root=%cd%"

:: 遍历每个目录
for %%D in (%dirs%) do (
    echo Starting %%D...
    start "" cmd /c "cd /d "%root%\%%D" && dotnet run"
)

echo.
echo All projects completed.

pause


