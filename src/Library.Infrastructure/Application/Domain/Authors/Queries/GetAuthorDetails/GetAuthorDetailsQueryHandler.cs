using Library.Application.Domain.Authors.Queries.GetAuthorDetails;
using Library.Core.Domain.Authors.Models;
using Library.Core.Exceptions;
using Library.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Application.Domain.Authors.Queries.GetAuthorDetails;

public class GetAuthorDetailsQueryHandler(LibrariesDbContext librariesDbContext)
    : IRequestHandler<GetAuthorDetailsQuery, AuthorDetailsDto>
{
    public async Task<AuthorDetailsDto> Handle(GetAuthorDetailsQuery query, CancellationToken cancellationToken)
    {
        return await librariesDbContext
           .Authors
           .AsNoTracking()
           .Include(a => a.BocksAuthors)
           .ThenInclude(ba => ba.Bock)
           .Select(a => new AuthorDetailsDto
           {
               Id = a.Id,
               FirstName = a.FirstName,
               LastName = a.LastName,
               MiddleName = a.MiddleName,
               Bocks = a.BocksAuthors.Select(ba => new BockDto
               {
                   Id = ba.Bock.Id,
                   Title = ba.Bock.Title,
                   Description = ba.Bock.Description
               }).ToList()
           })
           .SingleOrDefaultAsync(a => a.Id == query.Id, cancellationToken)
       ?? throw new NotFoundException($"{nameof(Author)} with id: '{query.Id}' was not found.");
    }
}