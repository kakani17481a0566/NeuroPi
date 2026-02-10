# Deploy NeuropiForms to Azure

$resourceGroupName = "NeuropiFormsRG"
$location = "eastus"
$appServicePlanName = "NeuropiFormsPlan"
$webAppName = "neuropiforms-app-" + (Get-Random -Minimum 1000 -Maximum 9999)
$sku = "F1" # Free tier

# 1. Create Resource Group
Write-Host "Creating Resource Group '$resourceGroupName'..."
az group create --name $resourceGroupName --location $location

# 2. Create App Service Plan
Write-Host "Creating App Service Plan '$appServicePlanName'..."
az appservice plan create --name $appServicePlanName --resource-group $resourceGroupName --sku $sku --is-linux

# 3. Create Web App
Write-Host "Creating Web App '$webAppName'..."
az webapp create --name $webAppName --resource-group $resourceGroupName --plan $appServicePlanName --runtime "dotnet:8"

# 4. Configure Connection String
Write-Host "Configuring Connection String..."
$connectionString = "Host=testdb-server-india.postgres.database.azure.com;Port=5432;Database=testdb;Username=pgadmin;Password=P@ssw0rd123!;SSL Mode=Require;Trust Server Certificate=true"
az webapp config connection-string set --name $webAppName --resource-group $resourceGroupName --settings DefaultConnection=$connectionString --connection-string-type PostgreSQL

# 5. Deploy Application
Write-Host "Deploying Application..."
dotnet publish -c Release -o ./publish
cd ./publish
Compress-Archive -Path * -DestinationPath ../publish.zip
cd ..
az webapp deployment source config-zip --resource-group $resourceGroupName --name $webAppName --src publish.zip

# 6. Output Endpoint
$endpoint = az webapp show --name $webAppName --resource-group $resourceGroupName --query defaultHostName --output tsv
Write-Host "Deployment Completed!"
Write-Host "App URL: https://$endpoint"
