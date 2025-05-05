using ContosoAcai.Common;
using ContosoAcai.Data.Entities.Interfaces;

namespace ContosoAcai.Data.Entities;

public class AudioPreOrder : IEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string UserEmail { get; set; } = null!;
    public string AudioName { get; set; } = null!;
    public string AudioExtension { get; set; } = null!;
    public string Content { get; set; } = null!;

    //EF
    public AudioPreOrder()
    {}
    
    public AudioPreOrder(
        string userEmail,
        string audioName,
        string audioExtension,
        string content)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        UserEmail = userEmail.NormalizeEmail();
        AudioName = audioName.Trim();
        AudioExtension = audioExtension.Trim();
        Content = content;
    }
}