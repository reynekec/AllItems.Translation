$ErrorActionPreference = 'Stop'

$repoRoot = Split-Path -Parent $MyInvocation.MyCommand.Path
$projectPath = Join-Path $repoRoot 'src/AllItems.Translation.App/AllItems.Translation.App.csproj'
$publishDir = Join-Path $repoRoot 'artifacts/publish'
$deployDir = 'C:\tools\Translation'
$exeName = 'AllItems.Translation'
$exePath = Join-Path $deployDir ($exeName + '.exe')

Write-Host ''
Write-Host '=== AllItems.Translation Deploy ===' -ForegroundColor Cyan
Write-Host "Project: $projectPath"
Write-Host "Deploy folder: $deployDir"
Write-Host ''

try {
    $running = Get-Process -Name $exeName -ErrorAction SilentlyContinue
    if ($running) {
        Write-Host "Stopping running process '$exeName'..." -ForegroundColor Yellow
        $running | Stop-Process -Force

        $timeoutSec = 10
        $sw = [System.Diagnostics.Stopwatch]::StartNew()
        do {
            Start-Sleep -Milliseconds 200
            $stillRunning = Get-Process -Name $exeName -ErrorAction SilentlyContinue
        } while ($stillRunning -and $sw.Elapsed.TotalSeconds -lt $timeoutSec)

        if ($stillRunning) {
            throw "Could not stop process '$exeName' within $timeoutSec seconds."
        }
    }
    else {
        Write-Host "Process '$exeName' is not running."
    }

    if (Test-Path $publishDir) {
        Remove-Item -Path $publishDir -Recurse -Force
    }

    Write-Host 'Publishing application...' -ForegroundColor Yellow
    & dotnet publish $projectPath -c Release -o $publishDir

    if ($LASTEXITCODE -ne 0) {
        throw "dotnet publish failed with exit code $LASTEXITCODE."
    }

    Write-Host 'Copying published files to deployment folder...' -ForegroundColor Yellow
    New-Item -ItemType Directory -Path $deployDir -Force | Out-Null
    Copy-Item -Path (Join-Path $publishDir '*') -Destination $deployDir -Recurse -Force

    if (-not (Test-Path $exePath)) {
        throw "Deployment succeeded but executable was not found at: $exePath"
    }

    Write-Host 'Starting application...' -ForegroundColor Yellow
    Start-Process -FilePath $exePath -WorkingDirectory $deployDir | Out-Null

    Write-Host ''
    Write-Host 'Deployment successful.' -ForegroundColor Green
    exit 0
}
catch {
    Write-Host ''
    Write-Host 'Deployment failed.' -ForegroundColor Red
    Write-Host $_.Exception.Message -ForegroundColor Red
    Write-Host ''
    Write-Host 'Press Enter to close this window...' -ForegroundColor DarkYellow
    [void](Read-Host)
    exit 1
}
