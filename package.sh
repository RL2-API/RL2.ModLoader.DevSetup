rm -rf package
mkdir package
dotnet publish
cd bin/Release/publish
tar -a -cf ../../../package/RL2.ModLoader.DevSetup.tar.gz *
