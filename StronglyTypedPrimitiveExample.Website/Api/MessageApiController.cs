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
        var message = MessageEntity.Create(request.Title, request.AuthorId);
        _messageRepository.Save(message);

        var resourceUrl = new Uri(Url.ActionLink(nameof(Get), values: new { messageId = message.Id })!);
        return Created(resourceUrl, new CreateMessageResponse(message.Id, resourceUrl));
    }

    [HttpGet("{messageId:guid}")]
    [Produces<GetMessageResponse>]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Get([FromRoute] MessageID messageId)
    {
        var message = _messageRepository.Find(messageId);

        return message is not null ? Ok(new GetMessageResponse(message.Id, message.Title, message.AuthorId))
            : NotFound();
    }
}