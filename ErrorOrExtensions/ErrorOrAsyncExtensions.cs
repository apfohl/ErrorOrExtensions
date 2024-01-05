namespace ErrorOr.Extensions;

public static class ErrorOrAsyncExtensions
{
    public static Task<ErrorOr<TResult>> Select<T, TResult>(
        this ErrorOr<T> errorOr,
        Func<T, Task<TResult>> mapping
    ) => ErrorOrExtensions.Select(errorOr, mapping)
        .Match(
            async task => ErrorOrFactory.From(await task.ConfigureAwait(false)),
            list => Task.FromResult(ErrorOr<TResult>.From(list))
        );

    public static async Task<ErrorOr<TResult>> Select<T, TResult>(
        this Task<ErrorOr<T>> errorOrTask,
        Func<T, TResult> mapping
    ) => (await errorOrTask.ConfigureAwait(false)).Select(mapping);

    public static async Task<ErrorOr<TResult>> Select<T, TResult>(
        this Task<ErrorOr<T>> errorOrTask,
        Func<T, Task<TResult>> mapping
    ) => await (await errorOrTask.ConfigureAwait(false)).Select(mapping);
}
