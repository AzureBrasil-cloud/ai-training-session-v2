using ContosoAcai.Data;
using ContosoAcai.Infrastructure.Azure.AiInference;
using ContosoAcai.Infrastructure.Azure.DocumentIntelligence;
using ContosoAcai.Infrastructure.Azure.Speech;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ContosoAcai.Application.Orders;

public partial class OrderService(
    AppDbContext context,
    IConfiguration configuration,
    DocumentIntelligenceService documentIntelligenceService,
    ILogger<OrderService> logger,
    AiInferenceService aiInferenceService,
    SpeechService speechService);
