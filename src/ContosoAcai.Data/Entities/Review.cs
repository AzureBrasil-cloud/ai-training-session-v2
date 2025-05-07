using ContosoAcai.Data.Entities.Interfaces;

namespace ContosoAcai.Data.Entities;

public class Review : IEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string UserEmail { get; set; } = null!;
    public string Content { get; set; } = null!; 
    public ReviewClassification Classification { get; set; }
    
    // Ef
    private Review() { }
    
    public Review(string userEmail, string content, ReviewClassification classification)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        UserEmail = userEmail;
        Content = content;
        Classification = classification;
    }
}

public enum ReviewClassification
{
    VeryBad = 1,
    Bad,
    Neutral,
    Good,
    VeryGood,
    Unknown
}