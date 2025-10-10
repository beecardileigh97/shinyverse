# Run a simple static server for public-dashboard and open the avatar demo in default browser
param(
  [int]$Port = 8000
)

Set-Location -Path "$PSScriptRoot"
if (-not (Test-Path -Path "public-dashboard")) {
  Write-Host "public-dashboard folder not found"
  exit 1
}

# prefer virtualenv python if available
$venvPython = Join-Path -Path $PSScriptRoot -ChildPath ".venv\Scripts\python.exe"
if (Test-Path $venvPython) {
  $python = $venvPython
} else {
  $python = "python"
}

Write-Host "Starting simple HTTP server for public-dashboard on port $Port using $python..."

# ensure logs dir exists
New-Item -ItemType Directory -Path "logs" -Force | Out-Null
$logFile = Join-Path -Path "$PSScriptRoot" -ChildPath "logs\avatar-server.log"

# Start the server in a background process and redirect output to a log
$startCmd = "& `"$python`" -m http.server $Port --directory public-dashboard 2>&1 | Out-File -FilePath `"$logFile`" -Encoding utf8"
Start-Process -FilePath "powershell" -ArgumentList "-NoExit", "-Command", $startCmd -WindowStyle Hidden

Start-Sleep -Seconds 1
Start-Process "http://localhost:$Port/avatar-bottom-box.html"

Write-Host "Server launched; logs are written to $logFile"

function Register-AvatarDemoStartup {
  param(
    [string]$TaskName = "Shinyverse-AvatarDemo",
    [string]$BatPath = "$PSScriptRoot\run-avatar-demo.bat"
  )
  # Register a scheduled task to run at logon
  $exists = schtasks /Query /TN $TaskName 2>$null
  if ($LASTEXITCODE -eq 0) {
    Write-Host "Scheduled task '$TaskName' already exists"
    return
  }
  $cmd = "schtasks /Create /SC ONLOGON /RL HIGHEST /TN `"$TaskName`" /TR `"$BatPath`" /F"
  Write-Host "Creating scheduled task: $cmd"
  iex $cmd
}

Write-Host "To auto-start at login run: Register-AvatarDemoStartup"
