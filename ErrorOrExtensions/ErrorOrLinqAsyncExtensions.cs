namespace ErrorOr.Extensions;

public static class ErrorOrLinqAsyncExtensions
{
    public static async Task<ErrorOr<TResult>> Select<T, TResult>(
        this Task<ErrorOr<T>> errorOrTask,
        Func<T, TResult> mapping
    )
    {
        if (mapping == null) throw new ArgumentNullException(nameof(mapping));

        return (await errorOrTask.ConfigureAwait(false)).Then(mapping);
    }

    public static Task<ErrorOr<TResult>> SelectMany<T, TCollection, TResult>(
        this Task<ErrorOr<T>> errorOrTask,
        Func<T, Task<ErrorOr<TCollection>>> collectionTask,
        Func<T, TCollection, TResult> selector)
    {
        if (collectionTask == null) throw new ArgumentNullException(nameof(collectionTask));
        if (selector == null) throw new ArgumentNullException(nameof(selector));

        return errorOrTask.ThenAsync(e => collectionTask(e).Then(result => selector(e, result)));
    }
}
