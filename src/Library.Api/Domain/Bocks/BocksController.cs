using System.ComponentModel.DataAnnotations;
using Library.Api.Common;
using Library.Api.Constants;
using Library.Api.Domain.Bocks.Requests;
using Library.Application.Domain.Bocks.Commands.AddBockAuthor;
using Library.Application.Domain.Bocks.Commands.CreateBock;
using Library.Application.Domain.Bocks.Commands.DeleteBock;
using Library.Application.Domain.Bocks.Commands.RemoveBockAuthor;
using Library.Application.Domain.Bocks.Commands.UpdateBock;
using Library.Application.Domain.Bocks.Queries.GetBockDetails;
using Library.Application.Domain.Bocks.Queries.GetBocks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PagesResponses;

namespace Library.Api.Domain.Bocks;

[Route(Routes.Bocks)]
public class BocksController(IMediator mediator) : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(PageResponse<BockDto[]>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBocksAsync(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var query = new GetBocksQuery(page, pageSize);
        var bocks = await mediator.Send(query, cancellationToken);
        return Ok(bocks);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BockDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBockDetailsAsync(
        [FromRoute][Required] Guid id,
        CancellationToken cancellationToken = default)
    {
        var query = new GetBockDetailsQuery(id);
        var bock = await mediator.Send(query, cancellationToken);
        return Ok(bock);
    }

    [HttpPost]
    [ProducesResponseType(typeof(BockDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBockAsync(
        [FromBody][Required] CreateBockRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateBockCommand(request.Title, request.Description);
        var bockId = await mediator.Send(command, cancellationToken);
        return Created(bockId);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateBockAsync(
        [FromRoute][Required] Guid id,
        [FromBody][Required] UpdateBockRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new UpdateBockCommand(id, request.Title, request.Description);
        await mediator.Send(command, cancellationToken);
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBockAsync(
        [FromRoute][Required] Guid id,
        CancellationToken cancellationToken = default)
    {
        var command = new DeleteBockCommand(id);
        await mediator.Send(command, cancellationToken);
        return Ok();
    }

    [HttpPost("{id}/authors")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddBockAuthorAsync(
        [FromRoute][Required] Guid id,
        [FromBody][Required] AddBockAuthorRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new AddBockAuthorCommand(id, request.AuthorId);
        await mediator.Send(command, cancellationToken);
        return Ok();
    }

    [HttpDelete("{id}/authors/{authorId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveBockAuthorAsync(
        [FromRoute][Required] Guid id,
        [FromRoute][Required] Guid authorId,
        CancellationToken cancellationToken = default)
    {
        var command = new RemoveBockAuthorCommand(id, authorId);
        await mediator.Send(command, cancellationToken);
        return Ok();
    }
}
