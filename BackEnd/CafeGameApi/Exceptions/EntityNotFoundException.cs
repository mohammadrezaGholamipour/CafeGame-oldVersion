using CafeGameApi.Tools;

namespace CafeGameApi.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string entityName)
            : base($"{entityName} موردنظر یافت نشد")
    {
    }
    public EntityNotFoundException(Type entityType)
            : base($"{entityType.GetDisplayName()} موردنظر یافت نشد")
    {
    }
}