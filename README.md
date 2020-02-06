# FarmBeats Partner Integration Accelerator

This solution is intended to provide a reference implementaion of a FarmBeats partner integration. It consist of the folowing components:


### Managment Service

The Managment service  is responsible for registering a device/sensor model conifuration and device instance registration on a target Azure FarmBeats deployment.

### Ingestion Implementation

The Business Kit Ingestion service is responsible for ingesting telemetry into a target Azure FarmBeats deployment from a deployed an IoT Central instance with the Business Kit device template.

#### Configuration Settings

'''json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "EventHubInputConnectionString": "",
    "EventHubOutputConnectionString": "",
    "FarmBeatsApiUrl": "https://fbdatahubwebsite.azurewebsites.net/",
    "PartnerSpAuthority": "https://login.microsoftonline.com/microsoft.onmicrosoft.com",
    "PartnerSpClientId": "",
    "PartnerSpClientSecret": "",
    "PartnerSpResource": ""
  }
}

'''
