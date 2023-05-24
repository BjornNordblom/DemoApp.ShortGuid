using Mediator;

public record ClaimCreateCommand(string ReferenceNumber, string Currency)
    : IRequest<ClaimGetResponse>;
