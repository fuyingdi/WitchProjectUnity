@echo off
rem 设置 Unity 安装路径
set UNITY_PATH="C:\Program Files\Unity\Hub\Editor\2022.3.57f1\Editor\Unity.exe"

rem 设置项目路径
set PROJECT_PATH="C:\Users\fuyin\Desktop\WitchProjectUnity"

rem 输出本次操作的时间戳
echo Running Unity solution regeneration at %date% %time%

rem 检查 Unity 是否存在
if exist %UNITY_PATH% (
    echo Unity executable found at %UNITY_PATH%
) else (
    echo Error: Unity executable not found at %UNITY_PATH%
    exit /b 1
)

rem 检查项目路径是否存在
if exist %PROJECT_PATH% (
    echo Unity project path found at %PROJECT_PATH%
) else (
    echo Error: Unity project path not found at %PROJECT_PATH%
    exit /b 1
)

rem 运行 Unity 命令以同步解决方案并记录其输出
%UNITY_PATH% -batchmode -quit -projectPath %PROJECT_PATH% -executeMethod SyncVS.SyncSolution > UnitySync.log 2>&1

rem 检查日志文件
type UnitySync.log

echo Finished running Unity solution regeneration.

rem 暂停以便查看输出
@pause
