namespace MasterCatalog.Application.DTOs;

public record ProductDto(
    string Id,
    string Sku,
    string Title,
    string Description,
    decimal Price,
    string Currency,
    int Quantity,
    string Status,
    string Category,
    string Condition,
    List<string> ImageUrls,
    Dictionary<string, string> Attributes,
    DateTime CreatedAt,
    DateTime UpdatedAt
);