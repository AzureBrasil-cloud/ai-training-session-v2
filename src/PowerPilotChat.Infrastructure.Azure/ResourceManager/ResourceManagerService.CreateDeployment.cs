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
    public async Task<DeploymentProcess> CreateDeploymentAsync(
        Credentials credentials,
        string subscriptionId,
        string resourceGroupName, 
        string templateJson,
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
        
        var deploymentCollection = resourceGroup.Value.GetArmDeployments();

        var deploymentData = new ArmDeploymentContent
        (
            new ArmDeploymentProperties(ArmDeploymentMode.Incremental)
            {
                Template = BinaryData.FromString(templateJson)
            }
        );
        
        var deployment = await deploymentCollection.CreateOrUpdateAsync(
            WaitUntil.Started,
            deploymentName,
            deploymentData);

        return new DeploymentProcess(deployment.Id);
    }
}