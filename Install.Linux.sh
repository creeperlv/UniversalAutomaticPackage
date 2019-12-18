#!/bin/bash
mkdir UAP
cd UAP
git clone https://github.com/creeperlv/UniversalAutomaticPackage.git
cd UniversalAutomaticPackage
dotnet publish
mv -rf UAPCLI/bin/Debug/netcoreapp3.0/publish /usr/share/UAP/
sudo ln -s /usr/share/UAP/UAPCLI /usr/bin/UAPCLI