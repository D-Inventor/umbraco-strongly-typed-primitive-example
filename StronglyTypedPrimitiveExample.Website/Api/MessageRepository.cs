using StronglyTypedPrimitiveExample.Website.Domain;

namespace StronglyTypedPrimitiveExample.Website.Api;

public class MessageRepository
{
    private readonly Dictionary<MessageID, MessageEntity> _collection = [];

    public void Save(MessageEntity message)
    {
        if (message.Id == default)
        {
            message.Id = new MessageID(Guid.NewGuid());
        }

        _collection[message.Id] = message;
    }

    public MessageEntity? Find(MessageID id)
        => _collection.TryGetValue(id, out var result) ? result
        : null;

    public IEnumerable<MessageEntity> Find(PersonID authorId)
        => _collection.Values.Where(message => message.AuthorId == authorId);
}