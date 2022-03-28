namespace luxclusif.order.domain.Conts;
public static class ErrorsMessages
{
    public static readonly string NotNull = "The field {0} are required.";
    public static readonly string NotDefaultDateTime = "The field {0} are required.";
    public static readonly string BetweenLength = "The field {0} has to have length between {1} and {2}.";

    /// <summary>
    /// ErrorsMessages.BetweenLength.GetMessage(nameof(Name),3,100)}
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="par"></param>
    /// <returns></returns>
    public static string GetMessage(this string msg, params object[] par)
    {
        return string.Format(msg, par);
    }
}
