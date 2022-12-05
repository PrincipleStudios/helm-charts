param (
    [String]
    $subscription = '233e4023-8ea5-418c-b407-2ea999d54189',
    [String]
    $acrName = 'PrincipleStudios',

    [String]
    $appName = 'PrincipleHelmCharts-GitHub-Actions'
)

az account set --subscription $subscription

$details = az ad sp create-for-rbac --name "$appName" `
    --skip-assignment `
    --sdk-auth --only-show-errors | ConvertFrom-Json

$objectId = az ad sp show --id $($details.clientSecret) --query objectId -o tsv --only-show-errors

$dump = az role assignment create `
    --role AcrPush `
    --assignee-principal-type ServicePrincipal `
    --assignee-object-id $objectId `
    --scope $(az acr show --name $acrName --query id -o tsv) `
    --only-show-errors

@{
    REGISTRY_PASSWORD = $($details.clientSecret)
    REGISTRY_USERNAME = $($details.clientId)
} | ConvertTo-Json
