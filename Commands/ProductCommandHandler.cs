public sealed class ProductCommandHandler
{
    private readonly IAppDbContext _context;
    private readonly IDomainMapper _mapper;

    public ProductCommandHandler(IAppDbContext context, IDomainMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ProductGetResponse> Handle(
        ProductGetRequest request,
        CancellationToken cancellationToken
    )
    {
        var product = await _context.Products.FindAsync(request.Id.Guid, cancellationToken);
        return _mapper.MapProductToProductGetResponse(product);
    }
}
