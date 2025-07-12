using StronglyTypedPrimitiveExample.Website.Domain;

namespace StronglyTypedPrimitiveExample.Website.Api;

public record GetMessageResponse(MessageID Id, string Title, PersonID AuthorId);