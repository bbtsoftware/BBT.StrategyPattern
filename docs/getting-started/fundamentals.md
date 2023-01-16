# Fundamentals of strategy pattern

Apply code structure, make code readable and fulfill open/close and single responsibility principles using strategy pattern.

## Case study: The Switch Case Block

A lot of documentation and tutorials exist explaining the implementation of the strategy pattern.
For an overview see [Strategy pattern (wikipedia)], at least the chapter of [Strategy and open/closed principle] is recommended to read.
The approach of this fundamentals article is to introduce and explain the concepts and reasons of strategy pattern based on a case study: The refactoring of a switch case block.
Many code bases contain distinction of cases implemented by switch case blocks. Those implementations have numerous issues.

### Violation of open/close principle

See also [Open/close principle (wikipedia)]. An appropriate class design supports the enhancement of functionality (open for extension) without changing the class itself (closed for modification).
Switch case blocks do not fulfill this principle.

```csharp
public void Foo(FooEnum fooEnum)
{
    switch (fooEnum)
    {
        case FooEnum.Foo1:
            Bar1();
            break;
        case FooEnum.Foo2:
            Bar2();
            break;
        default:
            throw new NotImplementedException();
    }
}
```

If the FooEnum is extended by further enum values, the switch case block must be modified as well.

### Violation of single responsibility principle

See also [Single responsibility principle (wikipedia)]. The switch case block from the example above does two things.

1. A distinction of cases based on the enum values
2. The execution of the enum specific methods

As a consequence two reasons exist to modify this method.

1. new enum value are added
2. the enum specific method calls change

Therefore the single responsibility principle is violated and consequently the reusability is limited.
Neither the distinction of cases nor the code within the case blocks could be used separately.

### Maintainability

Most likely the above mentioned distinction of cases happens in multiple code areas.
Each time the FooEnum is extended all those areas must be modified and extended as well.
As a consequence effort increases. Without appropriate test coverage code stability decreases.
This are the main reasons why distinction of cases should be centralized and separated from executing code.

### Readability and code structure

To fully understand the switch case block from the example above the whole block must be analyzed. Each case section can be freely implemented.
The case section has no limitations and can access all members and variables within the method scope. The code has a lack of structure.

## Improvement: Use strategy

As a first step the functionality of the case sections should be separated into classes. Those classes should be structured by implementing a common interface.

```csharp
public interface IFooStrategy
{
    void Bar();
}
```

This interface is implemented by each class:

```csharp
public class Foo1Strategy : IFooStrategy
{
    public void Bar()
    {
        ...
    }
}
```

Access to those strategies is provided by a StrategyProvider:

```csharp
public class FooStrategyProvider
{
    public IFooStrategy GetStrategy(FooEnum fooEnum)
    {
        switch (fooEnum)
        {
            case FooEnum.Foo1:
                return new Foo1Strategy();
            case FooEnum.Foo2:
                return new Foo2Strategy();
                ...
            default:
                throw new NotImplementedException();
        }
    }
}
```

Usage of the strategy within the original method:

```csharp
public class Foo
{
    private readonly FooStrategyProvider FooStrategyProvider = new FooStrategyProvider();

    public void Foo(FooEnum fooEnum)
    {
        var barStrategy = this.FooStrategyProvider.GetStrategy(fooEnum);
        barStrategy.Bar();
    }
}
```

### Improvements with strategy usage

##### Single responsibility principle

With the introduction of FooStrategyFactory the distinction of cases is moved into its own class and separated from execution logic in case blocks.
This leads to higher chance of reuse. If other areas of code also need a distinction based on FooEnum, the FooStrategyFactory can be reused.

#### Maintainability

Because the switch case block of FooEnum is centralized by FooStrategyFactory, maintainability and code stability is improved.
Enhancements on FooEnum affects only this single existing class (and the implementation of a new FooStrategy).

#### Readability and code structure

After the refactoring the original method became explicit, structured and focused on the Bar method call.

### Issues

#### Violation of open/closed principle

The strategy factory still consist of the switch case block. If the FooEnum is enhanced the GetStrategy method must still be modified as well.

#### Violation of single responsibility principle

Besides the strategy selection the implementation of the FooStrategyFactory has another responsibility: The instantiation of the strategy instance.
This aspect should be separated and should have its own realization. There are third party libraries available for object instantiation.
Commonly used are IoC-containers. With those containers the behaviour of object lifecycle (e.g. transient, singleton) can be declared and therefore delegated to those libraries.

## Improvement: Use generic strategy and provider

BBT.StrategyPattern defines an interface for declaration of strategies based on a generic type parameter.

```csharp
public interface IGenericStrategy<T>
{
    bool IsResponsible(T criterion);
}
```

By implementing IsResponsible each strategy itself defines for which criterion it is responsible. The concrete implementation of FooStrategy changes to:

```csharp
public class Foo1Strategy : IFooStrategy
{
    public bool IsResponsible(FooEnum fooEnum)
    {
        return fooEnum == FooEnum.Foo1;
    }

    public void Bar()
    {
        ...
    }
}
```

BBT.StrategyPattern provides a generic strategy provider which can be used to resolve the specific strategy based on the strategy's IsResponsible predicate.
BBT.StrategyPattern's generic strategy provider consults the IStrategyLocator interface to get all concrete strategies implementing the concerned strategy interface.
The IStrategyLocator interface must be implemented, most commonly by using an IoC-container infrastructure.

Using BBT.StrategyPattern the example above can be refined to:

```csharp
public class Foo
{
    private readonly FooStrategyProvider FooStrategyProvider;

    // Constructor injection of generic strategy provider, common for IoC / dependency injection framework.
    public Foo(IGenericStrategyProvider<IFooStrategy, FooEnum> fooStrategyProvider)
    {
        this.FooStrategyProvider = fooStrategyProvider;
    }

    public void Foo(FooEnum fooEnum)
    {
        var barStrategy = this.FooStrategyProvider.GetStrategy(fooEnum);
        barStrategy.Bar();
    }
}
```

How the strategies are obtained lies in responsibility of IStrategyLocator and IoC framework.
Using ninject it could look like:

```csharp
// IoC registrations
IKernel kernel = new StandardKernel();

// Generic type unbound registration of strategy locator.
// When resolved a locator object of concrete generic type is instantiated.
kernel.Bind(typeof(IStrategyLocator<>)).To(typeof(NinjectStrategyLocator<>));

// Bindings of the foo strategy implementations.
kernel.Bind<IFooStrategy>().To<Foo1Strategy>();
kernel.Bind<IFooStrategy>().To<Foo2Strategy>();
```

### Improvements with generic strategy and generic strategy provider usage

#### Open/closed principle

When FooEnum is enhanced, with the generic strategy provider, additional strategies can be added without modifying existing classes.
As soon as the new strategies are registered in the IoC container, the provider is extended.

#### Single responsibility principle

The generic provider has one single responsibility: Providing the requested strategy. How this strategy is instantiated is separated and handled be the strategy locator (i.e. IoC container).

[Strategy pattern (Wikipedia)]: https://en.wikipedia.org/wiki/Strategy_pattern
[Strategy and open/closed principle]: https://en.wikipedia.org/wiki/Strategy_pattern#Strategy_and_open.2Fclosed_principle
[Open/close principle (Wikipedia)]: https://en.wikipedia.org/wiki/Open%E2%80%93closed_principle
[Single responsibility principle (wikipedia)]: https://en.wikipedia.org/wiki/Single_responsibility_principle
