using Microsoft.AspNetCore.Mvc;
using StronglyTypedPrimitiveExample.Website.Domain;

namespace StronglyTypedPrimitiveExample.Website.Api;

[Route("/api/[controller]")]
[ApiController]
public class MessageApiController(MessageRepository messageRepository) : ControllerBase
{
    private readonly MessageRepository _messageRepository = messageRepository;

    [HttpPost]
    [ProducesResponseType<CreateMessageResponse>(StatusCodes.Status201Created)]
    public IActionResult Create([FromBody] CreateMessageRequest request)
    {
        var message = MessageEntity.Create(request.Title, request.Slug, request.AuthorId);
        _messageRepository.Save(message);

        var apiUrl = new Uri(Url.ActionLink(nameof(GetById), values: new { messageId = message.Id })!);
        var friendlyUrl = new Uri(Url.ActionLink(nameof(GetBySlug), values: new { slug = message.Slug })!);
        return Created(apiUrl, new CreateMessageResponse(message.Id, friendlyUrl));
    }

    [HttpGet("{messageId:guid}")]
    [Produces<GetMessageResponse>]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById([FromRoute] MessageID messageId)
    {
        var message = _messageRepository.Find(messageId);

        return message is not null ? Ok(new GetMessageResponse(message.Id, message.Title, message.AuthorId))
            : NotFound();
    }

    [HttpGet("{slug}")]
    [Produces<GetMessageResponse>]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetBySlug([FromRoute] Slug slug)
    {
        var message = _messageRepository.Find(slug);

        return message is not null ? Ok(new GetMessageResponse(message.Id, message.Title, message.AuthorId))
            : NotFound();
    }
}