using MediatR;
using PagesResponses;

namespace Library.Application.Domain.Bocks.Queries.GetBocks;

public record GetBocksQuery(int Page, int PageSize) : IRequest<PageResponse<BockDto[]>>;