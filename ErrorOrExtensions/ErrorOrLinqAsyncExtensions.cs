namespace ErrorOr.Extensions;

public static class ErrorOrLinqAsyncExtensions
{
    public static Task<ErrorOr<TResult>> SelectMany<T, TCollection, TResult>(
        this Task<ErrorOr<T>> errorOrTask,
        Func<T, Task<ErrorOr<TCollection>>> collectionTask,
        Func<T, TCollection, TResult> selector)
    {
        if (collectionTask == null) throw new ArgumentNullException(nameof(collectionTask));
        if (selector == null) throw new ArgumentNullException(nameof(selector));

        return errorOrTask.SelectMany(e => collectionTask(e).Select(result => selector(e, result)));
    }
}
