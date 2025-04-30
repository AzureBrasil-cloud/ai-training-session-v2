using Azure;
using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;
using PowerPilotChat.Infrastructure.Azure.ResourceManager.Models;
using PowerPilotChat.Infrastructure.Azure.Shared;

namespace PowerPilotChat.Infrastructure.Azure.ResourceManager;

public partial class ResourceManagerService
{
    public async Task<ResourceGroup> CreateResourceGroupsAsync(
        Credentials credentials, 
        string subscriptionId, 
        AzureLocation location, 
        string name)
    {
        var credential = new ClientSecretCredential(
            credentials.TenantId, 
            credentials.ClientId, 
            credentials.ClientSecret);

        var armClient = new ArmClient(credential);
        
        SubscriptionResource subscription = await armClient.GetSubscriptions().GetAsync(subscriptionId);
        var resourceGroups = subscription.GetResourceGroups();
        var resourceGroupData = new ResourceGroupData(location);
        ArmOperation<ResourceGroupResource> operation = await resourceGroups.CreateOrUpdateAsync(WaitUntil.Completed, name, resourceGroupData);
        var resourceGroup = operation.Value;
        
        return new ResourceGroup(resourceGroup.Data.Id!, resourceGroup.Data.Name);
    }
}