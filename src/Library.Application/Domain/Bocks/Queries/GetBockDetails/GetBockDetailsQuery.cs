using MediatR;

namespace Library.Application.Domain.Bocks.Queries.GetBockDetails;

public record GetBockDetailsQuery(Guid Id) : IRequest<BockDetailsDto>;