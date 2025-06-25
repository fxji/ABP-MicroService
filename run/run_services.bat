start "" cmd /c "cd ../BaseService/BaseService.Host && dotnet run"

start "" cmd /c "cd ../AuthServer/IdentityServer/AuthServer.Host && dotnet run"

start "" cmd /c "cd ../MicroServices/Business/Business.Host && dotnet run"

start "" cmd /c "cd ../MicroServices/FileStore/host/FileStore.HttpApi.Host && dotnet run"

start "" cmd /c "cd ../Gateways/WebAppGateway/WebAppGateway.Host && dotnet run"

start "" cmd /c "cd ../MicroServices/AAA/host/AAA.HttpApi.Host && dotnet run"

