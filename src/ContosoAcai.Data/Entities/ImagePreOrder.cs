using ContosoAcai.Common;
using ContosoAcai.Data.Entities.Interfaces;

namespace ContosoAcai.Data.Entities;

public class ImagePreOrder : IEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string UserEmail { get; set; } = null!;
    public string ImageName { get; set; } = null!;
    public string ImageExtension { get; set; } = null!;
    public string[] KeyValuePairs { get; set; } = null!;

    //EF
    public ImagePreOrder()
    {}
    
    public ImagePreOrder(
        string userEmail,
        string[] keyValuePairs,
        string imageName,
        string imageExtension)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        UserEmail = userEmail.NormalizeEmail();
        KeyValuePairs = keyValuePairs;
        ImageName = imageName.Trim();
        ImageExtension = imageExtension.Trim();
    }
}