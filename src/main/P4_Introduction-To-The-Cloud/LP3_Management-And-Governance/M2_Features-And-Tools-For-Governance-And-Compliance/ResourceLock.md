# Configure A Resource Lock

## Task 1: Create A Resource Group
```bash
nolan [ ~ ]$ az group create --location eastus --resource-group AzureIntroRG
{
  "id": "/resourceGroups/AzureIntroRG",
  "location": "eastus",
  "managedBy": null,
  "name": "AzureIntroRG",
  "properties": {
    "provisioningState": "Succeeded"
  },
  "tags": null,
  "type": "Microsoft.Resources/resourceGroups"
}
```

## Task 2: Create A Storage Account
```bash
nolan [ ~ ]$ az storage account create --resource-group AzureIntroRG --name gajdascz --sku Standard_LRS
{
  "accessTier": "Hot",
  "accountMigrationInProgress": null,
  "allowBlobPublicAccess": false,
  "allowCrossTenantReplication": false,
  "allowSharedKeyAccess": null,
  "allowedCopyScope": null,
  "azureFilesIdentityBasedAuthentication": null,
  "blobRestoreStatus": null,
  "creationTime": "2025-11-20T20:58:29.096203+00:00",
  "customDomain": null,
  "defaultToOAuthAuthentication": null,
  "dnsEndpointType": null,
  "dualStackEndpointPreference": null,
  "enableExtendedGroups": null,
  "enableHttpsTrafficOnly": true,
  "enableNfsV3": null,
  "encryption": {
    "encryptionIdentity": null,
    "keySource": "Microsoft.Storage",
    "keyVaultProperties": null,
    "requireInfrastructureEncryption": null,
    "services": {
      "blob": {
        "enabled": true,
        "keyType": "Account",
        "lastEnabledTime": "2025-11-20T20:58:29.424323+00:00"
      },
      "file": {
        "enabled": true,
        "keyType": "Account",
        "lastEnabledTime": "2025-11-20T20:58:29.424323+00:00"
      },
      "queue": null,
      "table": null
    }
  },
  "extendedLocation": null,
  "failoverInProgress": null,
  "geoPriorityReplicationStatus": null,
  "geoReplicationStats": null,
  "id": "/resourceGroups/AzureIntroRG/providers/Microsoft.Storage/storageAccounts/gajdascz",
  "identity": null,
  "immutableStorageWithVersioning": null,
  "isHnsEnabled": null,
  "isLocalUserEnabled": null,
  "isSftpEnabled": null,
  "isSkuConversionBlocked": null,
  "keyCreationTime": {
    "key1": "2025-11-20T20:58:29.424323+00:00",
    "key2": "2025-11-20T20:58:29.424323+00:00"
  },
  "keyPolicy": null,
  "kind": "StorageV2",
  "largeFileSharesState": null,
  "lastGeoFailoverTime": null,
  "location": "eastus",
  "minimumTlsVersion": "TLS1_0",
  "networkRuleSet": {
    "bypass": "AzureServices",
    "defaultAction": "Allow",
    "ipRules": [],
    "ipv6Rules": [],
    "resourceAccessRules": null,
    "virtualNetworkRules": []
  },
  "placement": null,
  "primaryLocation": "eastus",
  "privateEndpointConnections": [],
  "provisioningState": "Succeeded",
  "publicNetworkAccess": null,
  "resourceGroup": "AzureIntroRG",
  "routingPreference": null,
  "sasPolicy": null,
  "secondaryEndpoints": null,
  "secondaryLocation": null,
  "sku": {
    "name": "Standard_LRS",
    "tier": "Standard"
  },
  "statusOfPrimary": "available",
  "statusOfSecondary": null,
  "storageAccountSkuConversionStatus": null,
  "tags": {},
  "type": "Microsoft.Storage/storageAccounts",
  "zones": null
}
```

## Task 3: Create A Resource Lock
```bash
nolan [ ~ ]$ az lock create --name StorageLock --lock-type ReadOnly --resource-type Microsoft.Storage/storageAccounts --resource-group AzureIntroRG --resource <resource-name>
{
  "id": "/resourcegroups/AzureIntroRG/providers/Microsoft.Storage/storageAccounts/gajdascz/providers/Microsoft.Authorization/locks/StorageLock",
  "level": "ReadOnly",
  "name": "StorageLock",
  "notes": null,
  "owners": null,
  "resourceGroup": "AzureIntroRG",
  "type": "Microsoft.Authorization/locks"
}
```
### Task 4: Attempt To Create Container In Locked Storage Account
```bash
nolan [ ~ ]$ az storage container create --name mycontainer --account-name <storage-account-name>
Code: ScopeLocked
Message: The scope '/resourceGroups/AzureIntroRG/providers/Microsoft.Storage/storageAccounts/' cannot perform write operation because following scope(s) are locked: '/resourcegroups/AzureIntroRG/providers/Microsoft.Storage/storageAccounts/'. Please remove the lock and try again.
Server failed to authenticate the request. Please refer to the information in the www-authenticate header.
RequestId:192fbe12-c01e-0063-6564-5a988f000000
Time:2025-11-20T21:28:43.4954227Z
ErrorCode:NoAuthenticationInformation
```

## Task 5: Update Resource Lock
```bash
nolan [ ~ ]$ az lock update --name StorageLock --lock-type CanNotDelete --resource-type Microsoft.Storage/storageAccounts --resource-group AzureIntroRG --resource <resource-name>
{
  "id": "/resourcegroups/AzureIntroRG/providers/Microsoft.Storage/storageAccounts/<account-name>/providers/Microsoft.Authorization/locks/StorageLock",
  "level": "CanNotDelete",
  "name": "StorageLock",
  "notes": null,
  "owners": null,
  "resourceGroup": "AzureIntroRG",
  "type": "Microsoft.Authorization/locks"
}
```

## Task 6: Create Container In Updated Locked Storage Account
```bash
nolan [ ~ ]$ az storage container create --name mycontainer --account-name <storage-account-name>
{
  "created": true
}
```