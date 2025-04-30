using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.Resources;
using PowerPilotChat.Infrastructure.Azure.ResourceManager.Models;
using PowerPilotChat.Infrastructure.Azure.Shared;

namespace PowerPilotChat.Infrastructure.Azure.ResourceManager;

public partial class ResourceManagerService
{
    public async Task<Deployment?> GetDeploymentById(
        Credentials credentials,
        string subscriptionId,
        string resourceGroupName,
        string deploymentName)
    {
        // ref -> https://github.com/Azure-Samples/azure-samples-net-management/blob/master/samples/resources/deploy-using-arm-template/Program.cs
        var credential = new ClientSecretCredential(
            credentials.TenantId, 
            credentials.ClientId, 
            credentials.ClientSecret);
        
        var armClient = new ArmClient(credential);
        
        SubscriptionResource subscription = await armClient.GetSubscriptions().GetAsync(subscriptionId);
        var resourceGroup = await subscription.GetResourceGroups().GetAsync(resourceGroupName);
        
        var deployment = await resourceGroup.Value
            .GetArmDeployments()
            .GetIfExistsAsync(deploymentName);

        if (!deployment.HasValue)
        {
            return null;
        }
        
        return new Deployment(
            deployment.Value!.Data.Id!, 
            deployment.Value.Data.Name!,
            deployment.Value.Data.Properties.ProvisioningState.ToString(),
            deployment.Value.Data.Properties.Error?.ToString());
    }
}