```
Install dotnet on Linux
```
	https://github.com/dotnet/core/blob/master/Documentation/linux-setup.md

```
Install modules on Windows IIS
```
https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/aspnet-core-module?view=aspnetcore-3.1


```
Commands worker for me
```
	sudo apt-get install -y gpg
	wget -qO- https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.asc.gpg
	sudo mv microsoft.asc.gpg /etc/apt/trusted.gpg.d/
	wget -q https://packages.microsoft.com/config/ubuntu/18.04/prod.list
	sudo mv prod.list /etc/apt/sources.list.d/microsoft-prod.list
	sudo chown root:root /etc/apt/trusted.gpg.d/microsoft.asc.gpg
	sudo chown root:root /etc/apt/sources.list.d/microsoft-prod.list
	sudo apt-get install -y apt-transport-https
	sudo apt-get update
	sudo apt-get install dotnet-sdk-3.0

```
DB Migration
```
PM> Add-Migration InitialCreate
PM> Update-Database


