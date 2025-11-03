@echo off
REM SimpsonsPokeGame - Auto Launcher
REM This script opens the game project in Unity Hub

echo.
echo ============================================
echo   SIMPSONS POKEMON GAME - LAUNCHER
echo ============================================
echo.
echo Opening project in Unity Hub...
echo.

REM Path to the project
set PROJECT_PATH=C:\SimpsonsPokeGameRepo\SimpsonsPokeGame

REM Unity Editor executable path
set UNITY_EXE=C:\Program Files\Unity\Hub\Editor\6000.2.6f2\Editor\Unity.exe

REM Try to launch Unity Editor
if exist "%UNITY_EXE%" (
    echo Found Unity Editor at: %UNITY_EXE%
    REM Open project with Unity Editor
    start "" "%UNITY_EXE%" -projectPath "%PROJECT_PATH%"
    echo Project is opening in Unity...
    echo.
    echo When Unity opens:
    echo  1. Wait for import to complete (2-5 minutes)
    echo  2. AutoSceneInitializer will run automatically
    echo  3. Press Play button to start the game
    echo.
) else (
    echo.
    echo ERROR: Unity Editor not found!
    echo.
    echo Manual launch instructions:
    echo 1. Open Unity Hub
    echo 2. Click "Add" button
    echo 3. Browse to: %PROJECT_PATH%
    echo 4. Wait for import
    echo 5. Press Play
    echo.
    pause
    exit /b 1
)

echo Done! Check Unity window...
timeout /t 3
