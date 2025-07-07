#!/usr/bin/env pwsh

param(
    [Parameter(Position=0)]
    [string]$Command = "install"
)

if ($Command -eq "install") {
    Write-Host "Installing Playwright browsers..."
    pwsh bin/Debug/net9.0/playwright.ps1 install
}
elseif ($Command -eq "codegen") {
    Write-Host "Starting Playwright codegen..."
    pwsh bin/Debug/net9.0/playwright.ps1 codegen https://localhost:7001
}
else {
    Write-Host "Usage: ./playwright.ps1 [install|codegen]"
    Write-Host "  install  - Install Playwright browsers"
    Write-Host "  codegen  - Start Playwright code generator"
}