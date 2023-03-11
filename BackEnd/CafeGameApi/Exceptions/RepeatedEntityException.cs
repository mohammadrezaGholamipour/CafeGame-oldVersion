using CafeGameApi.Tools;

namespace CafeGameApi.Exceptions;

public class RepeatedEntityException : Exception
{
    public RepeatedEntityException(Type entityType)
            : base($"{entityType.GetDisplayName()} موردنظر تکراری می باشد")
    {
    }

}