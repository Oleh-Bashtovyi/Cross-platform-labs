Vagrant.configure("2") do |config|

  # Ubuntu VM
  config.vm.define "ubuntu" do |ubuntu|
    ubuntu.vm.box = "ubuntu/jammy64"
    ubuntu.vm.hostname = "ubuntu-vm"
    ubuntu.vm.network "public_network"
    ubuntu.vm.provider "virtualbox" do |vb|
      vb.memory = "4096"
      vb.cpus = 4
    end

    # Synced folder for Linux VM
    ubuntu.vm.synced_folder ".", "/home/vagrant/project"

    # Provisioning .NET Core 8.0 installation on Ubuntu
    ubuntu.vm.provision "shell", run: "always", inline: <<-SHELL
      # Update the system
      sudo apt-get update
      sudo apt-get install -y wget apt-transport-https

      # Install the Microsoft GPG key
      wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb
      sudo dpkg -i packages-microsoft-prod.deb

      # Install .NET SDK
      sudo apt-get update
      sudo apt-get install -y dotnet-sdk-8.0

      # Check the installation
      echo "Verify that .NET Core 8 is installed..."
      dotnet --version
      dotnet --list-sdks
      dotnet --list-runtimes

      if command -v dotnet &> /dev/null; then
        # cd /home/vagrant/project
        # echo "Starting project..."
        # echo "Lab 1:"
        # echo "========================================"
        # dotnet run --project Lab4 run lab1 -I Lab4/Lab1/INPUT.TXT -o Lab4/Lab1/OUTPUT.TXT
        # echo "Lab 2:"
        # echo "========================================"
        # dotnet run --project Lab4 run lab2 -I Lab4/Lab2/INPUT.TXT -o Lab4/Lab2/OUTPUT.TXT
        # echo "Lab 3:"
        # echo "========================================"
        # dotnet run --project Lab4 run lab3 -I Lab4/Lab3/INPUT.TXT -o Lab4/Lab3/OUTPUT.TXT

        # Configure NuGet source for private BaGet repository
        dotnet nuget remove source "BaGet"
        dotnet nuget add source http://192.168.56.1:5000/v3/index.json --name "BaGet"
        
        # Install OBashtovyi tool
        Write-Host "Installing tool"
        dotnet tool install --global OBashtovyi --version 1.0.0
        Write-Host "After Installation"
        
        # Check the installation
        OBashtovyi version
      else
        echo "Cannot start project, .NET Core 8 is not installed!"
      fi
    SHELL
  end


  # Windows VM
  config.vm.define "windows" do |windows|
    windows.vm.box = "gusztavvargadr/windows-10"
    windows.vm.network "public_network"
    windows.vm.provider "virtualbox" do |vb|
      vb.memory = "6096"
      vb.cpus = 4
    end

    # Synced folder for Windows VM
    windows.vm.synced_folder ".", "C:/project"
    
    # Provisioning .NET Core 8 installation using Chocolatey
    windows.vm.provision "shell", run: "always", inline: <<-SHELL
      # Set execution policy to allow the installation script to run
      Set-ExecutionPolicy Bypass -Scope Process -Force

      # Install Chocolatey if it's not already installed
      if (-not (Get-Command "choco" -ErrorAction SilentlyContinue)) {
        Write-Host "Installing Chocolatey..."
        [System.Net.WebClient]::new().DownloadString('https://chocolatey.org/install.ps1') | Invoke-Expression
      }
      else{
        Write-Host "Chocolatey already installed"
      }

      # Install .NET SDK 8.0 using Chocolatey
      Write-Host "Installing dotnet-8.0-sdk..."
      choco install dotnet-8.0-sdk -y

      # Verify that .NET Core 8 is successfully installed
      Write-Host "Verify that .NET Core 8 is installed..."
      dotnet --version
      dotnet --list-sdks
      dotnet --list-runtimes

      # Recheck .NET Core installation and run
      if (Get-Command "dotnet" -ErrorAction SilentlyContinue) {
        # cd C:\\project
        # Write-Host "Starting project..."
        # Write-Host "Lab 1:"
        # Write-Host "========================================"
        # dotnet run --project Lab4 run lab1 -I Lab4\\Lab1\\INPUT.TXT -o Lab4\\Lab1\\OUTPUT.TXT
        # Write-Host "Lab 2:"
        # Write-Host "========================================"
        # dotnet run --project Lab4 run lab2 -I Lab4\\Lab2\\INPUT.TXT -o Lab4\\Lab2\\OUTPUT.TXT
        # Write-Host "Lab 3:"
        # Write-Host "========================================"
        # dotnet run --project Lab4 run lab3 -I Lab4\\Lab3\\INPUT.TXT -o Lab4\\Lab3\\OUTPUT.TXT

        # Configure NuGet source for private BaGet repository
        dotnet nuget remove source "BaGet"
        dotnet nuget add source http://192.168.56.1:5000/v3/index.json --name "BaGet"
        
        # Install OBashtovyi tool
        Write-Host "Installing tool"
        dotnet tool install --global OBashtovyi --version 1.0.0
        Write-Host "After Installation"
        
        # Check the installation
        OBashtovyi version

      } else {
        Write-Host "can not start project, .NET Core 8 is not installed!"
      }
    SHELL
  end
end

