@echo off
cd /d %~dp0
REM ensure repo exists
if not exist .git (
  git init
)
REM replace local git config
git config --replace-all user.name "shiny-bot"
git config --replace-all user.email "shiny-bot@example.com"

git add .
git commit -m "chore: initial commit - scaffold shinyverse" || echo "commit may have failed or there is nothing to commit"

git status --porcelain=1 -b
pause
