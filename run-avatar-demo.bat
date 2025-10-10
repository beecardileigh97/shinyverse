@echo off
cd /d %~dp0
powershell -ExecutionPolicy Bypass -File "%~dp0run-avatar-demo.ps1"
