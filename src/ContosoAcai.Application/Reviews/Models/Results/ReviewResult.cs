using ContosoAcai.Data.Entities;

namespace ContosoAcai.Application.Reviews.Models.Results;

public record ReviewResult(
    Guid Id,
    string UserEmail,
    string Content,
    ReviewClassification Classification,
    DateTime CreatedAt)
{
    public string ClassificationName => Classification.ToString();
    
    public static implicit operator ReviewResult(Review review)
    {
        return new ReviewResult(
            review.Id,
            review.UserEmail,
            review.Content,
            review.Classification,
            review.CreatedAt);
    }
}