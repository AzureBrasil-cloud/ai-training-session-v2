using ContosoAcai.Application.Reviews;
using ContosoAcai.Application.Reviews.Models.Requests;
using ContosoAcai.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ContosoAcai.Web.Controllers.Reviews;

public partial class ReviewsController
{
    [HttpPost("/api/reviews")]
    public async Task<IActionResult> Create(
        [FromBody] CreateReviewRequest request,
        [FromServices] ReviewService service)
    {
        return (await service.CreateAsync(request)).ToApiResponse();
    }
}