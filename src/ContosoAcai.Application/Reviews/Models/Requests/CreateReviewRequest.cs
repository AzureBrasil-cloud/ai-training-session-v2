namespace ContosoAcai.Application.Reviews.Models.Requests;

public record CreateReviewRequest(string UserEmail, string Content);