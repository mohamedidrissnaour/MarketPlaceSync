using MasterCatalog.Domain.Entities;
using MasterCatalog.Domain.Interfaces;
using MasterCatalog.Infrastructure.Persistence;
using MongoDB.Driver;

namespace MasterCatalog.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IMongoCollection<MasterProduct> _collection;

    public ProductRepository(MongoDbContext context)
    {
        _collection = context.Products;
    }



    public async Task<MasterProduct?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(p => p.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<MasterProduct?> GetBySkuAsync(string sku, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(p => p.Sku == sku.ToUpper()).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<MasterProduct>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _collection.Find(_ => true).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<MasterProduct>> GetByStatusAsync(ProductStatus status, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(p => p.Status == status).ToListAsync(cancellationToken);
    }

    public async Task<MasterProduct> CreateAsync(MasterProduct product, CancellationToken cancellationToken = default)
    {
        await _collection.InsertOneAsync(product, cancellationToken: cancellationToken);
        return product;
    }

    public async Task<MasterProduct> UpdateAsync(MasterProduct product, CancellationToken cancellationToken = default)
    {
        await _collection.ReplaceOneasync(p => p.Id == product.Id, product, cancellationToken: cancellationToken);
        return product;
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        await _collection.DeleteOneAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsBySkuAsync(string sku, CancellationToken cancellatioNToken = default)
    {
        var count = await _collection.CountDocumentsAsync(p => p.Sku == sku.ToUpper(), cancellationToken: cancellationToken);
        return count > 0;
    }
}