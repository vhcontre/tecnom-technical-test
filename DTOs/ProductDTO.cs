/// <summary>
/// ProductoDTO es un objeto de transferencia de datos que se utiliza para la entrada y salida de datos de la API.
/// </summary>
public class ProductDTO
{
    public int Id { get; set; }

    /// <summary>
    /// Nombre del producto.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Precio del producto.    
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Stock del producto.
    /// </summary>
    public int Stock { get; set; }
}
