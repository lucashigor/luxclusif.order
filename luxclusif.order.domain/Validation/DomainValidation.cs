using luxclusif.order.domain.Conts;
using luxclusif.order.domain.Exceptions;
using System.Runtime.CompilerServices;

namespace luxclusif.order.domain.Validation;
public static class DomainValidation
{
    public static void NotNull(this object target,
                              [System.Runtime.CompilerServices.CallerArgumentExpression("target")] string fieldName = "")
    {
        if (target is null)
        {
            var message = ErrorsMessages.NotNull.GetMessage(fieldName);
            throw new EntityGenericException(message);
        }
    }

    public static void NotNullOrEmptyOrWhiteSpace(this string target,
                              [CallerArgumentExpression("target")] string fieldName = "")
    {
        if (string.IsNullOrWhiteSpace(target) || string.IsNullOrEmpty(target))
        {
            var message = ErrorsMessages.NotNull.GetMessage(fieldName);
            throw new EntityGenericException(message);
        }
    }

    public static void NotDefaultDateTime(this DateTime target,
                              [CallerArgumentExpression("target")] string fieldName = "")
    {
        DateTime? nullableTarget = target;

        NotDefaultDateTime(nullableTarget, fieldName);
    }

    public static void NotDefaultDateTime(this DateTime? target,
                          [CallerArgumentExpression("target")] string fieldName = "")
    {
        if (target.HasValue && (target.Value == (default) || target.Value == DateTime.MinValue))
        {
            var message = ErrorsMessages.NotDefaultDateTime.GetMessage(fieldName);
            throw new EntityGenericException(message);
        }
    }

    public static string BetweenLength(this string target, int minLength, int maxLength,
                              [CallerArgumentExpression("target")] string fieldName = "")
    {
        if (!string.IsNullOrEmpty(target) && (target.Length < minLength || target.Length > maxLength))
        {
            var message = ErrorsMessages.BetweenLength.GetMessage(fieldName, minLength, maxLength);
            throw new EntityGenericException(message);
        }

        return target;
    }
}