# Core principles

Goal of this infrastructure is to support the providing of specific strategies achieved by using generics.
This infrastructure introduces a generic strategy interface managed by the generic strategy provider.
The library introduces an `IGenericStrategy<T>` which needs to be implemented for every case a strategy with a given criterion is supporting.
The instances of the `IGenericStrategy<T>` are managed by the `IGenericStrategyProvider<out TStrategy, in TCriterion>` which resolves the correct implementation for the given criterion.
The generic parameter acts as selection criterion and declares the responsibility of the specific strategy.

## Advantages

* The generic strategy interface leads to more structured code.
* Based on their generic type parameter, strategies are categorized and make their responsibility explicit. As a consequence this leads to higher chance of reuse and better readable code.
* The implementation is driven by SOLID principles like open/closed, single responsibility and dependency inversion. For further explanations see [Fundamentals of strategy pattern].

## Implementation details

* Introduces a generic strategy interface
* The generic parameter represents the criterion for the selection of the specific strategy
* Works with any IoC framework
* For most benefit make generic type unbound IoC registration for `GenericStrategyProvider<,>`

[Fundamentals of strategy pattern]: ./fundamentals
