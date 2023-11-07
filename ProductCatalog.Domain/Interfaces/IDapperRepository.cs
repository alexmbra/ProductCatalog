namespace ProductCatalog.Domain.Interfaces;
public interface IDapperRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int? id);
}
