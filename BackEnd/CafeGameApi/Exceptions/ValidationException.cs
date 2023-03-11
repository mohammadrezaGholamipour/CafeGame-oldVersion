namespace CafeGameApi.Exceptions;

public class ValidationException : Exception
{
    public ValidationException(string message,
            List<KeyValuePair<string, string>> errors)
            : base(message)
    {
        for (var i = 0; i < errors.Count; i++)
        {
            Data.Add((i+1)+@"_"+errors[i].Key, errors[i].Value);
        }
    }

    public ValidationException(List<KeyValuePair<string, string>> errors)
            : base("خطا در اعتبار سنجی داده های ارسالی")
    {
        for (var i = 0; i < errors.Count; i++)
        {
            Data.Add((i+1)+@"_"+errors[i].Key, errors[i].Value);
        }
    }

    public ValidationException(string message,
            KeyValuePair<string, string> error)
            : base(message)
    {
        Data.Add(error.Key, error.Value);
    }

    public ValidationException(string errorKey,
            string errorValue)
            : base("خطا در اعتبار سنجی داده های ارسالی")
    {
        Data.Add(errorKey, errorValue);
    }
}