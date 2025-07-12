using StronglyTypedPrimitiveExample.Website.Domain;

public record CreateMessageRequest(string Title, PersonID AuthorId);

public record CreateMessageResponse(MessageID Id, Uri Url);