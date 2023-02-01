# ExpressionTreeComparer
A tool that compares two expression trees to check if they are equivalent.

# Usage

```Csharp
Expression<Func<Person, bool>> expression1 = p => p.Name == "string";
Expression<Func<Person, bool>> expression2 = person => person.Name == "string";

var result = expression1.IsEquivalentTo(expression2);
```

result shows `True`.

# How does it work ?

ExpressionTreeComparer will visit all expression nodes inside the two expressions and do the comparison at the end. It will compare values of literals, types of parameters, names of methods...etc.

The types of nodes that are supported:
- constructors (including parameters)
- Methods (including parameters)
- Properties
- Binary operators
- Unary operators
- Literals

ExpressionTreeComparer supports `Func`s and `Action`s with any number of parameters.

# Examples

## Example 1

```Csharp
Expression<Func<Person, bool>> expression1 = p => p.Name == "string";
Expression<Func<Person, bool>> expression2 = person => person.Name == "another string";

var result = expression1.IsEquivalentTo(expression2);
```

result shows `False`.

## Example 2

```Csharp
Expression<Func<Person, bool>> expression1 = p => p.Name == "string";
Expression<Func<Person, bool>> expression2 = person => person.IsDisactivated;

var result = expression1.IsEquivalentTo(expression2);
```

result shows `False`.

## Example 3

```Csharp
Expression<Action<Person>> expression1 = p => p.SetName(fullname: "James Taylor");
Expression<Action<Person>> expression2 = person => person.SetName(firstname: "James", lastname: "Taylor");

var result = expression1.IsEquivalentTo(expression2);
```

result shows `False`.

## Example 4

This code simply won't compile, as the two expressions need to have the same delegate type.

```Csharp
Expression<Func<Person, string>> expression1 = p => p.Name;
Expression<Func<Person, bool>> expression2 = person => person.IsDisactivated;

```

# Download

You can download the package from the Nuget repository [here](https://www.nuget.org/packages/ExpressionTreeComparer/).

# Future features

- Constructors and methods with argument type parameters.
- Cashing.