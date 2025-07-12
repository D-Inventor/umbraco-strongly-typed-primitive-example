namespace StronglyTypedPrimitiveExample.Website.Domain;

public interface IStronglyTypedPrimitive<TSelf, TPrimitive>
    where TSelf : IStronglyTypedPrimitive<TSelf, TPrimitive>
{
    static abstract TSelf Wrap(TPrimitive value);
    static abstract TPrimitive Unwrap(TSelf value);
}