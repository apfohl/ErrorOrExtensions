# ErrorOr Extensions

An extension library for [ErrorOr](https://github.com/amantinband/error-or) by
[Amichai Mantinband](https://github.com/amantinband).

## Features

This library extends the [ErrorOr](https://github.com/amantinband/error-or) library with useful features to allow for
some specific workflows, for creating meaningful error handling in your applications.

### Select and SelectMany

An instance of an `ErrorOr<T>` can be seen as a list containing a single element or none. With that in mind it would be
useful you have the ability to map an `ErrorOr<T>` to a new type/value. This library provides two extensions to allow
for this: `Select` and `SelectMany`.

The method signatures look basically the same as for `IEnumarable<T>`:

```csharp
ErrorOr<TResult> Select<T, TResult>(this ErrorOr<T> errorOr, Func<T, TResult> mapping)

ErrorOr<TResult> SelectMany<T, TResult>(this ErrorOr<T> errorOr, Func<T, ErrorOr<TResult>> mapping)
```

These methods allow for changing the value inside an `ErrorOr<T>` if it is successful, but keep the errors intact if it
is erroneous.

Example:

```csharp
ErrorOr<string> errorOr = ErrorOrFactory.From(42).Select(value => value.ToString());

ErrorOr<string> errorOr = ErrorOrFactory.From(42).SelectMany(value => Mapping(value));
ErrorOr<string> Mapping(int value) => ErrorOrFactory.From(value.ToString());
```

### Async

Many APIs work asynchronous with `Task<T>` as return type. When using such APIs that return `Task<ErrorOr<T>>` it is
useful to be able to use `Select` and `SelectMany` on them as well without having to strip the `Task<T>` beforehand.

This library has you covered. Both, `Select` and `SelectMany` are provided in three variants:

```csharp
Task<ErrorOr<TResult>> Select<T, TResult>(this ErrorOr<T> errorOr, Func<T, Task<TResult>> mapping)
Task<ErrorOr<TResult>> Select<T, TResult>(this Task<ErrorOr<T>> errorOrTask, Func<T, TResult> mapping)
Task<ErrorOr<TResult>> Select<T, TResult>(this Task<ErrorOr<T>> errorOrTask, Func<T, Task<TResult>> mapping)
Task<ErrorOr<TResult>> SelectMany<T, TResult>(this ErrorOr<T> errorOr, Func<T, Task<ErrorOr<TResult>>> mapping)
Task<ErrorOr<TResult>> SelectMany<T, TResult>(this Task<ErrorOr<T>> errorOrTask, Func<T, ErrorOr<TResult>> mapping)
Task<ErrorOr<TResult>> SelectMany<T, TResult>(this Task<ErrorOr<T>> errorOrTask, Func<T, Task<ErrorOr<TResult>>> mapping)
```

Every use case should be covered.

### LINQ and LINQ async

This library provides LINQ extensions for `ErrorOr`. This enables a very clean approach of binding multiple `ErrorOr`
generating function together. You can write them with the usual `from x in y select x` syntax you know and love from
LINQ. If an error occurs along the way, processing the chain is interrupted and the collected errors are passed to the
resulting instance of `ErrorOr<T>`.

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
