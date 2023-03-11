using CafeGameApi.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CafeGameApi.Filters;

public class GlobalExceptionHandler : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;

        switch (exception)
        {
            case EntityNotFoundException entityNotFoundException:
                context.Result = CreateInvalidResult(entityNotFoundException.Message,
                        entityNotFoundException.Data.Values,
                        StatusCodes.Status404NotFound
                );
                context.ExceptionHandled = true;
                break;
            case ServerLimitReachedException serverLimitReachedException:
                context.Result = CreateInvalidResult(serverLimitReachedException.Message,
                        serverLimitReachedException.Data.Values,
                        StatusCodes.Status507InsufficientStorage
                );
                context.ExceptionHandled = true;
                break;
            case InvalidOperationException invalidOperationException:
                context.Result = CreateInvalidResult(invalidOperationException.Message,
                        invalidOperationException.Data.Values,
                        StatusCodes.Status409Conflict);
                context.ExceptionHandled = true;
                break;
            case ValidationException entityValidationException:
                context.Result = CreateInvalidResult(entityValidationException.Message,
                        entityValidationException.Data.Values,
                        StatusCodes.Status422UnprocessableEntity
                );
                context.ExceptionHandled = true;
                break;
            case RepeatedEntityException repeatedEntityException:
                context.Result = CreateInvalidResult(repeatedEntityException.Message,
                        repeatedEntityException.Data.Values,
                        StatusCodes.Status422UnprocessableEntity
                );
                context.ExceptionHandled = true;
                break;
            default:
                context.Result = CreateInvalidResult("خطا در سرور داخلی",
                        exception.Message,
                        StatusCodes.Status500InternalServerError
                );
                context.ExceptionHandled = true;
                break;
        }
    }

    private static JsonResult CreateInvalidResult(string title, object? errors, int statusCode) =>
            new JsonResult(new
            {
                title = title,
                status = statusCode,
                errors = errors
            })
            {
                StatusCode = statusCode
            };
}