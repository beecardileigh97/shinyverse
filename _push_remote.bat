@echo off
cd /d %~dp0
REM remove existing origin if present
git remote remove origin 2>nul || echo no existing origin
REM add the requested remote
git remote add origin https://github.com/beecardileigh97/shinyverse.git
REM ensure branch is named main
git branch -M main
REM push to remote (may prompt for credentials or use credential manager)
git push -u origin main
pause
