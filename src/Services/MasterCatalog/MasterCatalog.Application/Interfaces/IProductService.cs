using MasterCatalog.Application.DTOs;

namespace MasterCatalog.Application.Interfaces;

    public interface IProductService
    {
        Task<ProductDto?> GetByIdAsync(string id , CancellationToken cancellationToken = default);
        Task<IEnumerable<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ProductDto> CreateAsync(ProductDto dto, CancellationToken cancellationToken = default);
        Task<ProductDto> UpdateAsync(string id, ProductDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    }
