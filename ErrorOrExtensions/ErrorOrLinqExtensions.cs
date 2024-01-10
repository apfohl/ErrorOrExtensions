namespace ErrorOr.Extensions;

public static class ErrorOrLinqExtensions
{
    public static ErrorOr<TResult> Select<T, TResult>(this ErrorOr<T> errorOr, Func<T, TResult> mapping)
    {
        if (mapping == null) throw new ArgumentNullException(nameof(mapping));

        return errorOr.Then(mapping);
    }

    public static ErrorOr<TResult> SelectMany<T, TCollection, TResult>(
        this ErrorOr<T> errorOr,
        Func<T, ErrorOr<TCollection>> collection,
        Func<T, TCollection, TResult> selector
    )
    {
        if (collection == null) throw new ArgumentNullException(nameof(collection));
        if (selector == null) throw new ArgumentNullException(nameof(selector));

        return errorOr.Then(e => collection(e).Then(result => selector(e, result)));
    }
}
