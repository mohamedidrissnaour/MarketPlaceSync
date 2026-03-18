namespace MasterCatalog.Infrastructure.Configuration;

public class MongoDbSettings {
    public string ConnectionString {get; set;} = default!;
    public string DatabaseName {get; set;} = default!;
    public string ProductsCollectionName {get;set;} = "products";
}