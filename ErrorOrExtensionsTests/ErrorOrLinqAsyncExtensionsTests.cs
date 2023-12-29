using ErrorOr;
using ErrorOr.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace ErrorOrExtensionsTests;

public static class ErrorOrLinqAsyncExtensionsTests
{
    [Test]
    public static async Task Select_from_errorro_task_with_value_returns_erroror_task_with_value()
    {
        const string input = "Test";
        (await (from s in Task.FromResult(ErrorOrFactory.From(input)) select s))
            .Should().Be(ErrorOrFactory.From(input));
    }

    [Test]
    public static void SelectMany_with_null_collection_throws_exception() =>
        Assert.ThrowsAsync<ArgumentNullException>(
            () => Task.FromResult(ErrorOrFactory.From("Test")).SelectMany(
                ((Func<string, Task<ErrorOr<string>>>)null)!,
                (i, c) => $"{i}{c}")
        );

    [Test]
    public static void SelectMany_with_null_selector_throws_exception() =>
        Assert.ThrowsAsync<ArgumentNullException>(() =>
            Task.FromResult(ErrorOrFactory.From("Test"))
                .SelectMany(s => Task.FromResult(ErrorOrFactory.From(s)), ((Func<string, string, string>)null)!));

    [Test]
    public static async Task SelectMany_from_erroror_task_with_value_returns_erroror_task_with_value()
    {
        const int input = 42;
        (await (
            from s in Task.FromResult(ErrorOrFactory.From("Test"))
            from i in Task.FromResult(ErrorOrFactory.From(input))
            select i
        )).Should().Be(ErrorOrFactory.From(input));
    }
}
