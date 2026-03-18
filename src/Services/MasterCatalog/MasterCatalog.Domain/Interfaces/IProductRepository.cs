using MasterCatalog.Domain.Entities;

namespace MasterCatalog.Domain.Interfaces;

                                                                // Domain ne dépend de RIEN
                                                                //             ↑
                                                                // Application dépend de Domain
                                                                //             ↑
                                                                // Infrastructure dépend de Application

public interface IProductRepository
{
    //Recuperation
    Task<MasterProduct?> GetByIdAsync(string id, CancellationToken cancellationToken = default); //un cancellation token par ce que il permet dannuler l'operation si luser ferme la connexion 
    Task<MasterProduct?> GetBySkuAsync(string id, CancellationToken cancellationToken = default);
    Task<IEnumerable<MasterProduct>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<MasterProduct>> GetByStatusAsync(ProductStatus status , CancellationToken cancellationToken = default);


    //Persistance
    Task<MasterProduct> CreateAsync(MasterProduct product , CancellationToken cancellationToken = default);
    Task<MasterProduct> UpdateAsync(MasterProduct product, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id , CancellationToken cancellationToken = default);

    //Vérification
    Task<bool> ExistsBySkuAsync(string sku , CancellationToken cancellationToken = default);
}