// Copyright © BBT Software AG. All rights reserved.

using BBT.StrategyPattern.Tests.Data;
using Shouldly;

namespace BBT.StrategyPattern.Tests.ExampleStrategyImpl
{
    public class SubstractionStrategy : IOperatorStrategy
    {
        public double DoCalculate(CalculationInput calc)
        {
            return calc.Number1 - calc.Number2;
        }

        public bool IsResponsible(Operator criterion)
        {
            criterion.ShouldNotBeNull();

            return criterion.Operation == OperatorEnum.Subtraktion;
        }
    }
}
