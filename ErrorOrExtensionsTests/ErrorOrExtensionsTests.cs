using ErrorOr;
using ErrorOr.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace ErrorOrExtensionsTests;

public static class ErrorOrExtensionsTests
{
    [Test]
    public static void Select_with_null_mapping_throws_exception() =>
        Assert.Throws<ArgumentNullException>(
            () => ErrorOrFactory.From("Test").Select(((Func<string, string>)null)!)
        );

    [Test]
    public static void Select_with_value_returns_erroror_with_new_type_and_value()
    {
        const int mappedValue = 42;
        ErrorOrFactory
            .From("Test")
            .Select(_ => mappedValue)
            .Should()
            .Be(ErrorOrFactory.From(mappedValue));
    }

    [Test]
    public static void Select_erroneous_erroror_returns_erroneous_erroror()
    {
        var errors = new List<Error> { Error.Failure() };
        ErrorOr<string>
            .From(errors)
            .Select(_ => 42)
            .Should()
            .Be(ErrorOr<int>.From(errors));
    }

    [Test]
    public static void SelectMany_to_null_method_throws_exception() =>
        Assert.Throws<ArgumentNullException>(
            () => ErrorOrFactory.From("Test").SelectMany(((Func<string, ErrorOr<string>>)null)!)
        );

    [Test]
    public static void SelectMany_erroror_with_value_to_method_returns_erroror()
    {
        const int mappedValue = 42;
        ErrorOrFactory
            .From("Test")
            .SelectMany(_ => ErrorOrFactory.From(mappedValue))
            .Should()
            .Be(ErrorOrFactory.From(mappedValue));
    }

    [Test]
    public static void SelectMany_erroneous_erroror_to_method_returns_erroneous_erroror()
    {
        var errors = new List<Error> { Error.Failure() };
        ErrorOr<string>
            .From(errors)
            .SelectMany(_ => ErrorOrFactory.From(42))
            .Should()
            .Be(ErrorOr<int>.From(errors));
    }
}
