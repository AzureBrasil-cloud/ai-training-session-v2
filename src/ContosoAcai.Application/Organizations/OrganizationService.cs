using Microsoft.Extensions.Logging;
using PowerPilotChat.Data;

namespace PowerPilotChat.Application.Organizations;

public partial class OrganizationService(AppDbContext context, ILogger<OrganizationService> logger);