using System;

namespace technical_tests_backend_ssr.Models;

/// <summary>
/// Represents a product with an identifier, name, price, and stock quantity.
/// </summary>
public class Product
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the name associated with the current instance.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the price of the item.
    /// </summary>
    public decimal Price { get; set; }
    /// <summary>
    /// Gets or sets the current stock level of the product.
    /// </summary>
    public int Stock { get; set; }
}
