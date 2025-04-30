using ContosoAcai.Common;
using PowerPilotChat.Data.Entities.Interfaces;

namespace ContosoAcai.Data.Entities;

public class Order : IEntity
{
    private const double ToppingPrice = 2f;

    private double SizeValue => Size switch
    {
        Size.Small => 5.00f,
        Size.Medium => 7.50f,
        Size.Large => 10.00f,
        _ => 0.00
    };
    
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string UserEmail { get; set; } = null!;
    public Size Size { get; set; }
    public string[] Extras { get; set; } = null!; 
    public double TotalValue { get; set; }

    //EF
    public Order()
    {}
    
    public Order(
        string userEmail,
        Size size,
        string[] extras)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        UserEmail = userEmail.NormalizeEmail();
        Size = size;
        Extras = extras
            .Select(x => x.Trim().ToLower())
            .Distinct()
            .ToArray();

        // Calculate total value
        TotalValue = SizeValue + (extras.Length * ToppingPrice);
    }
}

public enum Size
{
    Small = 1,
    Medium,
    Large
}