using ContosoAcai.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using AiInferenceService = ContosoAcai.Infrastructure.AiInference.AiInferenceService;

namespace ContosoAcai.Application.Reviews;

public partial class ReviewService(
    AppDbContext context,
    IConfiguration configuration,
    ILogger<ReviewService> logger,
    AiInferenceService aiInferenceService);