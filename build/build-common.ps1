# COMMON PATHS 

$rootFolder = (Get-Item -Path "./" -Verbose).FullName

# List of solutions used only in development mode
[PsObject[]]$serviceArray = @()


$serviceArray += [PsObject]@{ Path = $rootFolder + "/../AuthServer/IdentityServer/AuthServer.Host/"; Service = "authserver" }
$serviceArray += [PsObject]@{ Path = $rootFolder + "/../Gateways/WebAppGateway/WebAppGateway.Host/"; Service = "web-gateway" }
$serviceArray += [PsObject]@{ Path = $rootFolder + "/../BaseService/BaseService.Host/"; Service = "base-service" }
$serviceArray += [PsObject]@{ Path = $rootFolder + "/../MicroServices/FileStorage/FileStorage.Host/"; Service = "file-storage" }
$serviceArray += [PsObject]@{ Path = $rootFolder + "/../MicroServices/Business/Business.Host/"; Service = "Business-service" }
$serviceArray += [PsObject]@{ Path = $rootFolder + "/../MicroServices/AAA/host/AAA.HttpApi.Host/"; Service = "3a-report" }






Write-host ""
Write-host ":::::::::::::: !!! You are in development mode !!! ::::::::::::::" -ForegroundColor red -BackgroundColor  yellow
Write-host "" 