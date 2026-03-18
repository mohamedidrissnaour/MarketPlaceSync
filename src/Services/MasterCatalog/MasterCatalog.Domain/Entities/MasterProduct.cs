namespace MasterCatalog.Domain.Entities;

public class MasterProduct
{
    public string Id { get; private set; } = default!;
    public string Sku { get; private set; } = default!;
    public string Title { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public decimal Price { get; private set; }
    public string Currency { get; private set; } = "EUR";
    public int Quantity { get; private set; }
    public List<string> ImageUrls { get; private set; } = new();
    public string Category { get; private set; } = default!;
    public string Condition { get; private set; } = "New";
    public ProductStatus Status { get; private set; } = ProductStatus.Active;
    public Dictionary<string, string> Attributes { get; private set; } = new();
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private MasterProduct() { }


    // methodes de protection du domaine (garantir qun objet ne peut pas exister un etat invalide)
    public static MasterProduct Create(string sku,
     string title,
      string description,
       decimal price,
        int quantity,
         string category,
          List<string> imageUrls,
           string currency = "EUR",
            string condition = "New")
    {
        if (string.IsNullOrWhiteSpace(sku))
            throw new ArgumentException("SKU ne peut pas etre vide", nameof(sku));

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Le titre ne peut pas etre vide", nameof(title));

        if (price < 0)
            throw new ArgumentException("Le prix ne peut pas être négatif.", nameof(price));

        if (quantity < 0)
            throw new ArgumentException("La quantité ne peut pas être négative.", nameof(quantity));

        //else
        return new MasterProduct
        {
            Id = Guid.NewGuid().ToString(),
            Sku = sku.Trim().ToUpper(),
            Title = title.Trim(),
            Description = description.Trim(),
            Price = price,
            Currency = currency,
            Quantity = quantity,
            Category = category.Trim(),
            ImageUrls = imageUrls,
            Condition = condition,
            Status = ProductStatus.Active,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }


    public void UpdateStock(int newQuantity)
    {
        if (newQuantity < 0)
            throw new ArgumentException("La quantité ne peut pas etre negative");

        Quantity = newQuantity;
        UpdatedAt = DateTime.UtcNow;

        if (Quantity == 0)
            Status = ProductStatus.OutOfStock;
        else if (Status == ProductStatus.OutOfStock)
            Status = ProductStatus.Active;
    }

    public void UpdatePrice(decimal newPrice, string currency)
    {
        if (newPrice < 0)
            throw new ArgumentException("Le prix ne peut pas etre négatif");

        Price = newPrice;
        Currency = currency;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateDetails(string title, string description, string category)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Le titre ne peut pas etre vide");

        Title = title.Trim();
        Description = description.Trim();
        Category = category.Trim();
        UpdatedAt = DateTime.UtcNow;

    }

    public void AddAttribute(string key, string value)
    {
        Attributes[key] = value;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Discontinue()
    {
        Status = ProductStatus.Discontinued;
        UpdatedAt = DateTime.UtcNow;

    }
}

