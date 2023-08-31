import os
os.system("cd VPet-API && mkdir library && cd library && nuget.exe install ../packages.config")
os.system("cd VPet-API && dotnet build")