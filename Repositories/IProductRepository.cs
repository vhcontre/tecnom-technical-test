using technical_tests_backend_ssr.Models;

namespace technical_tests_backend_ssr.Repositories;

/// <summary>
/// Defines a repository interface for managing <see cref="Product"/> entities.
/// </summary>
/// <remarks>This interface provides asynchronous methods for retrieving product data. Implementations should
/// handle data access and storage concerns.</remarks>
public interface IProductRepository
{
    /// <summary>
    /// Asynchronously retrieves all products.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains an  IEnumerable{T} of Product
    /// objects representing all available products.</returns>
    Task<IEnumerable<Product>> GetAllAsync();
    /// <summary>
    /// Asynchronously retrieves a product by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the product to retrieve. Must be a positive integer.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the product with the specified
    /// identifier, or <see langword="null"/> if no product is found.</returns>
    Task<Product?> GetByIdAsync(int id);    
}
