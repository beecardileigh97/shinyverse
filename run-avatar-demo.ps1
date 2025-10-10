# Run a simple static server for public-dashboard and open the avatar demo in default browser
param(
  [int]$Port = 8000
)

Set-Location -Path "$PSScriptRoot"
if (-not (Test-Path -Path "public-dashboard")) {
  Write-Host "public-dashboard folder not found"
  exit 1
}

Write-Host "Starting simple HTTP server for public-dashboard on port $Port..."
Start-Process -NoNewWindow -FilePath "powershell" -ArgumentList "-NoExit", "-Command", "python -m http.server $Port --directory public-dashboard"
Start-Sleep -Seconds 1
Start-Process "http://localhost:$Port/avatar-bottom-box.html"
