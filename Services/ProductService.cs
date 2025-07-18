using technical_tests_backend_ssr.Models;
using technical_tests_backend_ssr.Repositories;

/// <summary>
/// Provides methods for accessing and managing product data through a specified product repository.
/// </summary>
/// <remarks>This service acts as a layer between the application and the product data repository, offering
/// asynchronous methods to retrieve product information. It requires an implementation of  <see
/// cref="IProductRepository"/> to function, which must be provided during initialization.</remarks>
public class ProductService
{
    private readonly IProductRepository _productoRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductService"/> class with the specified product repository.
    /// </summary>
    /// <param name="productoRepository">The repository used to access product data. Cannot be null.</param>
    public ProductService(IProductRepository productoRepository)
    {
        _productoRepository = productoRepository;
    }

    /// <summary>
    /// Asynchronously retrieves all products from the repository.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of <see
    /// cref="Product"/> objects.</returns>
    public Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return _productoRepository.GetAllAsync();
    }

    /// <summary>
    /// Asynchronously retrieves a product by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the product to retrieve. Must be a positive integer.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the <see cref="Product"/> if found;
    /// otherwise, <see langword="null"/>.</returns>
    public Task<Product?> GetProductByIdAsync(int id)
    {
        return _productoRepository.GetByIdAsync(id);
    }    
}
