namespace ProductCatalog.Domain.Entities;
public abstract class Entity
{
    public int Id { get; protected set; }
    public string Name { get; protected set; } = string.Empty;
}
