using System.Data;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Domain.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ProductCatalog.Infra.Data.Repositories;
public class DapperProductRepository : IDapperRepository<Product>
{
    private readonly string _connectionString;

    public DapperProductRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        using IDbConnection dbConnection = new SqlConnection(_connectionString);
        dbConnection.Open();
        const string query = "SELECT * FROM Products";
        var products = await dbConnection.QueryAsync<Product>(query);
        return products;
    }

    //public async Task<Product?> GetByIdAsync(int? id)
    //{
    //    using IDbConnection dbConnection = new SqlConnection(_connectionString);
    //    dbConnection.Open();
    //    const string query = "SELECT * FROM Products WHERE Id = @Id";
    //    var product = await dbConnection.QueryFirstOrDefaultAsync<Product>(query, new { Id = id });
    //    if (product == null)
    //    {
    //        throw new Exception("Product not found.");
    //    }
    //    return product;
    //}

    public async Task<Product?> GetByIdAsync(int? id)
    {
        using IDbConnection dbConnection = new SqlConnection(_connectionString);
        dbConnection.Open();
        const string query = @"
        SELECT p.*, c.*
        FROM Products p
        LEFT JOIN Categories c ON p.CategoryId = c.Id
        WHERE p.Id = @Id";

        var productDictionary = new Dictionary<int, Product>();
        var product = await dbConnection.QueryAsync<Product, Category, Product>(
            query,
            (prod, cat) =>
            {
                if (!productDictionary.TryGetValue(prod.Id, out var productEntry))
                {
                    productEntry = prod;
                    productEntry.Category = cat;
                    productDictionary.Add(prod.Id, productEntry);
                }
                else
                {
                    productEntry.Category = cat;
                }
                return productEntry;
            },
            new { Id = id },
            splitOn: "Id" // This indicates the column where the split occurs
        );

        return product.FirstOrDefault();
    }

}
