// Copyright © BBT Software AG. All rights reserved.

using BBT.StrategyPattern.Tests.Data;

namespace BBT.StrategyPattern.Tests.ExampleStrategyImpl
{
    public interface IOperatorStrategy : IGenericStrategy<Operator>
    {
        double DoCalculate(CalculationInput calc);
    }
}
