---
Order: 20
Title: Use with IoC
Description: Example how to use BBT.StrategyPattern with IoC.
---

```csharp
public void WorksWithIocNinject()
{
    var calc1 = new CalculationInput() { Number1 = 5, Number2 = 3 };
    var op1 = new Operator() { Operation = OperatorEnum.Addition };
    var op2 = new Operator() { Operation = OperatorEnum.Subtraktion };

    IKernel kernel = new StandardKernel();

    kernel
        .Bind<IStrategyLocator<IOperatorStrategy>>()
        .ToMethod((con) => new NinjectStrategyLocator<IOperatorStrategy>(kernel));

    kernel
        .Bind<IOperatorStrategy>()
        .To<AdditionStrategy>();
    kernel
        .Bind<IOperatorStrategy>()
        .To<SubtraktionStrategy>();
    kernel
        .Bind<IGenericStrategyProvider<IOperatorStrategy, Operator>>()
        .To<GenericStrategyProvider<IOperatorStrategy, Operator>>();

    IOperatorStrategy strategy;

    strategy = kernel.Get<IGenericStrategyProvider<IOperatorStrategy, Operator>>().GetStrategy(op1);
    strategy.DoCalculate(calc1).Should().Be(8);

    strategy = kernel.Get<IGenericStrategyProvider<IOperatorStrategy, Operator>>().GetStrategy(op2);
    strategy.DoCalculate(calc1).Should().Be(2);
}
```
