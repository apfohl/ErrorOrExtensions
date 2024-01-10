# ErrorOr.Extensions

[![.NET](https://github.com/apfohl/ErrorOrExtensions/actions/workflows/dotnet.yml/badge.svg)](https://github.com/apfohl/ErrorOrExtensions/actions/workflows/dotnet.yml) [![Release](https://github.com/apfohl/ErrorOrExtensions/actions/workflows/release.yml/badge.svg)](https://github.com/apfohl/ErrorOrExtensions/actions/workflows/release.yml) [![codecov](https://codecov.io/gh/apfohl/ErrorOrExtensions/graph/badge.svg?token=GFOVG506X9)](https://codecov.io/gh/apfohl/ErrorOrExtensions) [![Nuget](https://img.shields.io/nuget/v/ErrorOr.Extensions)](https://www.nuget.org/packages/ErrorOr.Extensions/)

An extension library for [ErrorOr](https://github.com/amantinband/error-or) by [Amichai Mantinband](https://github.com/amantinband).

## Features

This library extends the [ErrorOr](https://github.com/amantinband/error-or) library with useful features to allow for
some specific workflows, for creating meaningful error handling in your applications.

### LINQ and LINQ async

This library provides LINQ extensions for `ErrorOr`. This enables a very clean approach of binding multiple `ErrorOr`
generating function together. You can write them with the usual `from x in y select x` syntax you know and love from
LINQ. If an error occurs along the way, processing of the chain is interrupted and the collected errors are passed to
the resulting instance of `ErrorOr<T>`.

Example:

```csharp
ErrorOr<string> errorOr =
    from userId in ParseInput("10")            // returns ErrorOr<int>
    from user in userRepository.Lookup(userId) // returns ErrorOr<User>
    select user.Name                           // maps to string
```

This also works if the methods that are chained are asynchronous:

```csharp
Task<ErrorOr<string>> errorOr =
    from userId in ParseInput("10")            // returns Task<ErrorOr<int>>
    from user in userRepository.Lookup(userId) // returns Task<ErrorOr<User>>
    select user.Name                           // maps to string
```

## Closing

The author of this extension library is not affiliated with [Amichai Mantinband](https://github.com/amantinband) nor
does he claim anything from the [ErrorOr](https://github.com/amantinband/error-or) library.
