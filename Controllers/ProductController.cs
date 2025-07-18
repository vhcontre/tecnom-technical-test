using AutoMapper;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// 
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;
    private readonly IMapper _mapper;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="productService"></param>
    /// <param name="mapper"></param>
    public ProductController(ProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }

    /// <summary>
    /// Obtener todos los productos.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAll()
    {
        var products = await _productService.GetAllProductsAsync();
        return Ok(_mapper.Map<IEnumerable<ProductDTO>>(products));
    }
}
