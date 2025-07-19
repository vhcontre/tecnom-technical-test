using technical_tests_backend_ssr.Data;
using technical_tests_backend_ssr.Models;
using Microsoft.EntityFrameworkCore;

namespace technical_tests_backend_ssr.Repositories;

/// <summary>
/// Provides methods for accessing and managing product data in the database.
/// </summary>
/// <remarks>This repository is responsible for retrieving product information from the underlying data store. It
/// implements the <see cref="IProductRepository"/> interface, providing asynchronous methods to fetch all products or a
/// specific product by its identifier.</remarks>
public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductRepository"/> class with the specified database context.
    /// </summary>
    /// <param name="context">The database context used to access the product data. Cannot be null.</param>
    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Asynchronously retrieves all products from the data source.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see
    /// cref="IEnumerable{Product}"/> of all products available in the data source. The collection will be empty if no
    /// products are found.</returns>
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    /// <summary>
    /// Asynchronously retrieves a product by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the product to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the product with the specified
    /// identifier,  or <see langword="null"/> if no product with the specified identifier is found.</returns>
    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }
}