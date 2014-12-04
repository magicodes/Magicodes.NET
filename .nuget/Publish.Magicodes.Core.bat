del Magicodes.Core.1*.nupkg /f /q /a 
rem NuGet Update -self
rem NuGet SetApiKey 5d7f1e7f-9c95-4e44-8a80-167f4fb312eb
nuget Pack ../Magicodes.Core/Magicodes.Core.csproj -Build -Properties Configuration=Release
nuget push Magicodes.Core.1*.nupkg 5d7f1e7f-9c95-4e44-8a80-167f4fb312eb
@pause

