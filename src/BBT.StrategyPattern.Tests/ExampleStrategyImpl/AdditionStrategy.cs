// Copyright © BBT Software AG. All rights reserved.

namespace BBT.StrategyPattern.Tests.ExampleStrategyImpl
{
    using BBT.StrategyPattern.Tests.Data;
    using FluentAssertions;

    public class AdditionStrategy : IOperatorStrategy
    {
        public double DoCalculate(CalculationInput calc)
        {
            return calc.Number1 + calc.Number2;
        }

        public bool IsResponsible(Operator criterion)
        {
            criterion.Should().NotBeNull();

            return criterion.Operation == OperatorEnum.Addition;
        }
    }
}
