namespace ErrorOr.Extensions;

public static class ErrorOrExtensions
{
    public static ErrorOr<TResult> Select<T, TResult>(this ErrorOr<T> errorOr, Func<T, TResult> mapping)
    {
        if (mapping == null) throw new ArgumentNullException(nameof(mapping));

        return errorOr.Match(
            value => ErrorOrFactory.From(mapping(value)),
            ErrorOr<TResult>.From
        );
    }
}
