public sealed class ClaimCommandHandler
{
    private readonly IAppDbContext _context;
    private readonly IDomainMapper _mapper;

    public ClaimCommandHandler(IAppDbContext context, IDomainMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ClaimGetResponse?> Handle(
        ClaimGetRequest request,
        CancellationToken cancellationToken
    )
    {
        var product = await _context.Claims.FindAsync(request.Id.Value, cancellationToken);
        if (product == null)
            return default;
        return _mapper.MapClaimToClaimGetResponse(product);
    }
}
