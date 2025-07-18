using FluentValidation;

/// <summary>
/// Provides validation rules for <see cref="ProductDTO"/> instances.
/// </summary>
/// <remarks>This validator ensures that a <see cref="ProductDTO"/> has a non-empty name with a length between 3
/// and 100 characters, a price greater than 0, and a non-negative stock value.</remarks>
public class ProductDTOValidator : AbstractValidator<ProductDTO>
{
    /// <summary>
    /// Provides validation rules for <c>ProductDTO</c> objects.
    /// </summary>
    /// <remarks>This validator ensures that the product name is not empty and has a length between 3 and 100
    /// characters. It also checks that the product price is greater than 0 and that the stock is not
    /// negative.</remarks>
    public ProductDTOValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .Length(3, 100).WithMessage("El nombre debe tener entre 3 y 100 caracteres.");

        RuleFor(p => p.Price)
            .GreaterThan(0).WithMessage("El precio debe ser mayor a 0.");

        RuleFor(p => p.Stock)
            .GreaterThanOrEqualTo(0).WithMessage("El stock no puede ser negativo.");
    }
}
