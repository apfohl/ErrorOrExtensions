namespace ErrorOr.Extensions;

public static class ErrorOrLinqExtensions
{
    public static ErrorOr<TResult> SelectMany<T, TCollection, TResult>(
        this ErrorOr<T> errorOr,
        Func<T, ErrorOr<TCollection>> collection,
        Func<T, TCollection, TResult> selector
    )
    {
        if (collection == null) throw new ArgumentNullException(nameof(collection));
        if (selector == null) throw new ArgumentNullException(nameof(selector));

        return errorOr.Then(e => collection(e).Select(result => selector(e, result)));
    }
}
