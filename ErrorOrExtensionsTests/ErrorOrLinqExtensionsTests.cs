using ErrorOr;
using ErrorOr.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace ErrorOrExtensionsTests;

public static class ErrorOrLinqExtensionsTests
{
    [Test]
    public static void Select_from_erroror_with_value_returns_erroror_with_value()
    {
        const string input = "Test";
        (from s in ErrorOrFactory.From(input) select s)
            .Should()
            .Be(ErrorOrFactory.From(input));
    }

    [Test]
    public static void SelectMany_with_null_collection_throws_exception() =>
        Assert.Throws<ArgumentNullException>(
            () => ErrorOrFactory
                .From("Test")
                .SelectMany(
                    ((Func<string, ErrorOr<string>>)null)!,
                    (i, c) => $"{i}{c}"
                )
        );

    [Test]
    public static void SelectMany_with_null_selector_throws_exception() =>
        Assert.Throws<ArgumentNullException>(
            () => ErrorOrFactory
                .From("Testy")
                .SelectMany(
                    ErrorOrFactory.From,
                    ((Func<string, string, string>)null)!)
        );

    [Test]
    public static void SelectMany_from_erroror_with_value_returns_error_with_value()
    {
        const int input = 42;
        (
            from s in ErrorOrFactory.From("Test")
            from i in ErrorOrFactory.From(input)
            select i
        ).Should().Be(ErrorOrFactory.From(input));
    }
}
