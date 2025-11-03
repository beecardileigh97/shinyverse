# Create a clean Unity project and set it up with our game scripts
$unityPath = "C:\Program Files\Unity\Hub\Editor\6000.2.6f2\Editor\Unity.exe"
$projectPath = "C:\SimpsonsPokeGameRepo\SimpsonsPokeGame"
$scriptsPath = "$projectPath\Assets\Scripts"

Write-Host "Creating clean Unity project..." -ForegroundColor Green
Write-Host "Project path: $projectPath" -ForegroundColor Cyan

# Verify scripts exist
if (-Not (Test-Path $scriptsPath)) {
    Write-Host "ERROR: Scripts path not found: $scriptsPath" -ForegroundColor Red
    exit 1
}

$scriptCount = @(Get-ChildItem $scriptsPath -Filter "*.cs" -Recurse).Count
Write-Host "Found $scriptCount C# scripts" -ForegroundColor Green

# Launch Unity to initialize project
Write-Host "Launching Unity to initialize project..." -ForegroundColor Yellow
& $unityPath -projectPath $projectPath -quit -batchmode -logFile $null

Start-Sleep -Seconds 10

# Now launch normally
Write-Host "Opening project in Unity..." -ForegroundColor Green
Start-Process -FilePath $unityPath -ArgumentList "-projectPath `"$projectPath`"" -WindowStyle Normal

Write-Host "Done! Unity is opening..." -ForegroundColor Green
