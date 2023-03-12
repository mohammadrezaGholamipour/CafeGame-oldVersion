namespace CafeGameApi.Exceptions;

public class ServerLimitReachedException : Exception
{
    public ServerLimitReachedException()
            : base("سرور مشغول است")
    {
    }
}