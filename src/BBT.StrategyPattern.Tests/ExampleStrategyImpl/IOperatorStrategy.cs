// Copyright © BBT Software AG. All rights reserved.

namespace BBT.StrategyPattern.Tests.ExampleStrategyImpl
{
    using BBT.StrategyPattern.Tests.Data;

    public interface IOperatorStrategy : IGenericStrategy<Operator>
    {
        double DoCalculate(CalculationInput calc);
    }
}
