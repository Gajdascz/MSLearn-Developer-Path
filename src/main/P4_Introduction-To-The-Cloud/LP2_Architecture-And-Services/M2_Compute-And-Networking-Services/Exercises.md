# Compute and Networking Services Exercises

## Create a Virtual Machine

### Task 1: Create A Resource Group
```bash
nolan [ ~ ]$ az group create --name IntroAzureRG --location eastus
{
  "id": "/resourceGroups/IntroAzureRG",
  "location": "eastus",
  "managedBy": null,
  "name": "IntroAzureRG",
  "properties": {
    "provisioningState": "Succeeded"
  },
  "tags": null,
  "type": "Microsoft.Resources/resourceGroups"
}
```
### Task 2: Create A Linux Virtual Machine
```bash
nolan [ ~ ]$ az vm create   --resource-group "IntroAzureRG"   --name my-vm   --size Standard_D2s_v5   --location canadaeast   --public-ip-sku Standard 
  --image Ubuntu2204   --admin-username azureuser   --generate-ssh-keys
The default value of '--size' will be changed to 'Standard_D2s_v5' from 'Standard_DS1_v2' in a future release.
{
  "fqdns": "",
  "id": "/resourceGroups/IntroAzureRG/providers/Microsoft.Compute/virtualMachines/my-vm",
  "location": "canadaeast",
  "macAddress": "60-45-BD-F9-DF-CE",
  "powerState": "VM running",
  "privateIpAddress": "10.0.0.4",
  "publicIpAddress": "20.175.125.102",
  "resourceGroup": "IntroAzureRG"
}
```

### Task 3: Install Nginx
```bash
nolan [ ~ ]$ az vm extension set --resource-group "IntroAzureRG" --vm-name my-vm --name customScript --publisher Microsoft.Azure.Extensions --version 2.1 --settings '{"fileUris":["https://raw.githubusercontent.com/MicrosoftDocs/mslearn-welcome-to-azure/master/configure-nginx.sh"]}' --protected-settings '{"commandToExecute": "./configure-nginx.sh"}'
{
  "autoUpgradeMinorVersion": true,
  "id": "/resourceGroups/IntroAzureRG/providers/Microsoft.Compute/virtualMachines/my-vm/extensions/customScript",
  "location": "canadaeast",
  "name": "customScript",
  "provisioningState": "Succeeded",
  "publisher": "Microsoft.Azure.Extensions",
  "resourceGroup": "IntroAzureRG",
  "settings": {
    "fileUris": [
      "https://raw.githubusercontent.com/MicrosoftDocs/mslearn-welcome-to-azure/master/configure-nginx.sh"
    ]
  },
  "type": "Microsoft.Compute/virtualMachines/extensions",
  "typeHandlerVersion": "2.1",
  "typePropertiesType": "customScript"
}
```


## Configure Network Access

### Task 1: Access Web Server
- Query the public IP address of the VM created in the previous exercise and store it in a Bash variable.
```bash
nolan [ ~ ]$ IPADDRESS="$(az vm list-ip-addresses --resource-group "IntroAzureRG" --name my-vm --query "[].virtualMachine.network.publicIpAddresses[*].ipAddress" --output tsv)"
```
- Connect to the web server using curl and the IPADDRESS variable.
```bash
nolan [ ~ ]$ curl --connect-timeout 5 http://$IPADDRESS
curl: (28) Connection timed out after 5002 milliseconds
```
- Echo the IP address and open it in the browser.
```bash
nolan [ ~ ]$ echo $IPADDRESS
20.175.125.102
```

### Task 2: List The Current Network Security Group Rules
- Find out why the webserver is not accessible by examining current NSG rules.
```bash
nolan [ ~ ]$ az network nsg list --resource-group "IntroAzureRG" --query '[].name' --output tsv
my-vmNSG
nolan [ ~ ]$ az network nsg rule list --resource-group "IntroAzureRG" --nsg-name my-vmNSG
[
  {
    "access": "Allow",
    "destinationAddressPrefix": "*",
    "destinationAddressPrefixes": [],
    "destinationPortRange": "22",
    "destinationPortRanges": [],
    "direction": "Inbound",
    "etag": "W/\"6eb2c0b7-ab25-4d57-977b-3d80ada5cbee\"",
    "id": "/resourceGroups/IntroAzureRG/providers/Microsoft.Network/networkSecurityGroups/my-vmNSG/securityRules/default-allow-ssh",
    "name": "default-allow-ssh",
    "priority": 1000,
    "protocol": "Tcp",
    "provisioningState": "Succeeded",
    "resourceGroup": "IntroAzureRG",
    "sourceAddressPrefix": "*",
    "sourceAddressPrefixes": [],
    "sourcePortRange": "*",
    "sourcePortRanges": [],
    "type": "Microsoft.Network/networkSecurityGroups/securityRules"
  }
]
nolan [ ~ ]$ az network nsg rule list --resource-group "IntroAzureRG" --nsg-name my-vmNSG --query '[].{Name:name, Priority:priority, Port:destinationPortRange, Access:access}' --output table
Name               Priority    Port    Access
-----------------  ----------  ------  --------
default-allow-ssh  1000        22      Allow
```

### Task 3: Create The Network Security Rule
- Create a new NSG rule to allow HTTP traffic on port 80.
```bash
nolan [ ~ ]$ az network nsg rule create --resource-group "IntroAzureRG" --nsg-name my-vmNSG --name allow-http --protocol tcp --priority 100 --destination-port-range 80 --access Allow
{
  "access": "Allow",
  "destinationAddressPrefix": "*",
  "destinationAddressPrefixes": [],
  "destinationPortRange": "80",
  "destinationPortRanges": [],
  "direction": "Inbound",
  "etag": "W/\"c4f544dc-5fdd-4fdf-a1eb-b26c533a66d2\"",
  "id": "/resourceGroups/IntroAzureRG/providers/Microsoft.Network/networkSecurityGroups/my-vmNSG/securityRules/allow-http",
  "name": "allow-http",
  "priority": 100,
  "protocol": "Tcp",
  "provisioningState": "Succeeded",
  "resourceGroup": "IntroAzureRG",
  "sourceAddressPrefix": "*",
  "sourceAddressPrefixes": [],
  "sourcePortRange": "*",
  "sourcePortRanges": [],
  "type": "Microsoft.Network/networkSecurityGroups/securityRules"
}
```
- Verify that the new rule has been created.
```bash
nolan [ ~ ]$ az network nsg rule list --resource-group "IntroAzureRG" --nsg-name my-vmNSG --query '[].{Name:name, Priority:priority, Port:destinationPortRange, Access:access}' --output table
Name               Priority    Port    Access
-----------------  ----------  ------  --------
default-allow-ssh  1000        22      Allow
allow-http         100         80      Allow
```

### Task 4: Access The Web Server Again
````bash
nolan [ ~ ]$ curl --connect-timeout 5 http://$IPADDRESS
<html><body><h2>Welcome to Azure! My name is my-vm.</h2></body></html>
````