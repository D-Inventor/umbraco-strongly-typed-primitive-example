using StronglyTypedPrimitiveExample.Website.Domain;

public record CreateMessageRequest(string Title, Slug Slug, PersonID AuthorId);

public record CreateMessageResponse(MessageID Id, Uri Url);