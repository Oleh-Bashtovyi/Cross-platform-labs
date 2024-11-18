# Install Chocolatey
if (-not (Get-Command "choco" -ErrorAction SilentlyContinue)) {
    Set-ExecutionPolicy Bypass -Scope Process -Force
    [System.Net.WebClient]::new().DownloadString('https://chocolatey.org/install.ps1') | Invoke-Expression
}

# Install .NET SDK 8.0 and runtime
choco install dotnet-8.0-sdk -y
choco install dotnet-8.0-runtime -y

# Refresh environment variables
refreshenv

if (Get-Command "dotnet" -ErrorAction SilentlyContinue) {
    Write-Host ".NET Core 8 successfully installed."
    dotnet --list-sdks
    dotnet --list-runtimes

    # Installing tool
    dotnet nuget add source http://192.168.56.1:5000/v3/index.json --name "BaGet"
    dotnet tool install --global OBashtovyi --version 1.0.0
    
    OBashtovyi version

    cd C:\\project    

    OBashtovyi run lab1 -I Lab4/Lab1/INPUT.TXT -o Lab4/Lab1/OUTPUT.TXT    

} else {
    Write-Host ".NET Core 8 installation failed. Manual intervention required."
}