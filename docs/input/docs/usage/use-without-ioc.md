---
Order: 10
Title: Use without IoC
Description: Example how to use BBT.StrategyPattern without IoC.
---

```csharp
public void GenericStrategyWorksInMemory()
{
    var calc1 = new CalculationInput() { Number1 = 5, Number2 = 3 };
    var op1 = new Operator() { Operation = OperatorEnum.Addition };
    var op2 = new Operator() { Operation = OperatorEnum.Subtraktion };

    var factory = new MemoryOperatorStrategyLocator();
    var strategyProvider = new GenericStrategyProvider<IOperatorStrategy, Operator>(factory);

    IOperatorStrategy strategy;

    strategy = strategyProvider.GetStrategy(op1);
    strategy.DoCalculate(calc1).Should().Be(8);

    strategy = strategyProvider.GetStrategy(op2);
    strategy.DoCalculate(calc1).Should().Be(2);
}
```
