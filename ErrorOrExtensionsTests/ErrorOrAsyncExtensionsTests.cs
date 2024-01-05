using ErrorOr;
using ErrorOr.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace ErrorOrExtensionsTests;

public static class ErrorOrAsyncExtensionsTests
{
    [Test]
    public static async Task Select_erroror_with_value_to_async_mapping_returns_erroror_task_with_new_value()
    {
        const int input = 42;
        (await ErrorOrFactory.From("Test").Select(_ => Task.FromResult(input)))
            .Should().Be(ErrorOrFactory.From(input));
    }

    [Test]
    public static async Task Select_erroneous_erroror_to_async_mapping_returns_erroneous_erroror_task()
    {
        var errors = new List<Error> { Error.Failure() };
        (await ErrorOr<string>.From(errors).Select(_ => Task.FromResult(42)))
            .Should()
            .Be(ErrorOr<int>.From(errors));
    }

    [Test]
    public static async Task Select_erroror_task_with_value_to_mapping_returns_error_task_with_new_value()
    {
        const int input = 42;
        (await Task.FromResult(ErrorOrFactory.From("Test")).Select(_ => input))
            .Should().Be(ErrorOrFactory.From(input));
    }

    [Test]
    public static async Task Select_erroror_task_with_value_to_async_mapping_returns_erroror_task_with_new_value()
    {
        const int input = 42;
        (await Task.FromResult(ErrorOrFactory.From("Test")).Select(_ => Task.FromResult(input)))
            .Should().Be(ErrorOrFactory.From(input));
    }
}
