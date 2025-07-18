using AutoMapper;
using technical_tests_backend_ssr.Models;

/// <summary>
/// Configures mapping profiles for AutoMapper, defining how objects of type <see cref="Product"/> and <see
/// cref="ProductDTO"/> are converted to each other.
/// </summary>
/// <remarks>This profile sets up bidirectional mapping between <see cref="Product"/> and <see
/// cref="ProductDTO"/>. When mapping from <see cref="ProductDTO"/> to <see cref="Product"/>, the <c>Id</c> property is
/// ignored.</remarks>
public class AutoMapperProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AutoMapperProfile"/> class, configuring mappings between domain
    /// models and data transfer objects.
    /// </summary>
    /// <remarks>This profile sets up mappings between the <see cref="Product"/> and <see cref="ProductDTO"/>
    /// types.  The mapping is bidirectional, allowing conversion in both directions.  When mapping from <see
    /// cref="ProductDTO"/> to <see cref="Product"/>, the <c>Id</c> property is ignored.</remarks>
    public AutoMapperProfile()
    {
        CreateMap<Product, ProductDTO>().ReverseMap();
        CreateMap<ProductDTO, Product>().ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}

