using ContosoAcai.Application.Reviews.Models.Results;
using Microsoft.EntityFrameworkCore;

namespace ContosoAcai.Application.Reviews;

public partial class ReviewService
{
    public async Task<IList<ReviewResult>> ListAsync()
    {
        var imagePreOrders = await context.Reviews
            .ToListAsync();

        return imagePreOrders.Select(x => (ReviewResult)x).ToList();
    }
}