using Library.Api.Common;
using Library.Api.Constants;
using Library.Api.Domain.Authors.Requests;
using Library.Application.Domain.Authors.Commands.CreateAuthor;
using Library.Application.Domain.Authors.Commands.DeleteAuthor;
using Library.Application.Domain.Authors.Commands.UpdateAuthor;
using Library.Application.Domain.Authors.Queries.GetAuthorDetails;
using Library.Application.Domain.Authors.Queries.GetAuthors;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using PagesResponses;

namespace Library.Api.Domain.Authors;

[Route(Routes.Authors)]
public class AuthorsController(IMediator mediator) : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(PageResponse<AuthorDto[]>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAuthorsAsync(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var query = new GetAuthorsQuery(page, pageSize);
        var authors = await mediator.Send(query, cancellationToken);
        return Ok(authors);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AuthorDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAuthorDetailsAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var query = new GetAuthorDetailsQuery(id);
        var author = await mediator.Send(query, cancellationToken);
        return Ok(author);
    }

    [HttpPost]
    [ProducesResponseType(typeof(AuthorDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAuthorAsync(
        [FromBody][Required] CreateAuthorRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateAuthorCommand(request.FirstName, request.LastName, request.MiddleName);
        var authorId = await mediator.Send(command, cancellationToken);
        return Created(authorId);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAuthorAsync(
        [FromRoute][Required] Guid id,
        [FromBody][Required] UpdateAuthorRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new UpdateAuthorCommand(id, request.FirstName, request.LastName, request.MiddleName);
        await mediator.Send(command, cancellationToken);
        return Ok();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAuthorAsync(
        [FromQuery][Required] IReadOnlyCollection<Guid> ids,
        CancellationToken cancellationToken = default)
    {
        var command = new DeleteAuthorCommand(ids);
        await mediator.Send(command, cancellationToken);
        return Ok();
    }
}