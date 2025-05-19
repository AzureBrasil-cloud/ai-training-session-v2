using ContosoAcai.Data;
using ContosoAcai.Infrastructure.Azure.DocumentIntelligence;
using ContosoAcai.Infrastructure.Azure.Speech;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using AiInferenceService = ContosoAcai.Infrastructure.AiInference.AiInferenceService;
using SpeechService = ContosoAcai.Infrastructure.Speech.SpeechService;

namespace ContosoAcai.Application.Orders;

public partial class OrderService(
    AppDbContext context,
    IConfiguration configuration,
    DocumentIntelligenceService documentIntelligenceService,
    ILogger<OrderService> logger,
    AiInferenceService aiInferenceService,
    SpeechService speechService);
