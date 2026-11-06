// Copyright © BBT Software AG. All rights reserved.

using BBT.StrategyPattern.Tests.Data;
using BBT.StrategyPattern.Tests.ExampleStrategyImpl;
using BBT.StrategyPattern.Tests.WithoutIoc;
using Shouldly;
using Xunit;

namespace BBT.StrategyPattern.Tests
{
    public class WithoutIocTest
    {
        [Fact]
        public void GenericStrategyWorksInMemory()
        {
            var calc1 = new CalculationInput() { Number1 = 5, Number2 = 3 };
            var op1 = new Operator() { Operation = OperatorEnum.Addition };
            var op2 = new Operator() { Operation = OperatorEnum.Subtraktion };

            var factory = new MemoryOperatorStrategyLocator();
            var strategyProvider = new GenericStrategyProvider<IOperatorStrategy, Operator>(factory);

            IOperatorStrategy strategy;

            strategy = strategyProvider.GetStrategy(op1);
            strategy.DoCalculate(calc1).ShouldBe(8);

            strategy = strategyProvider.GetStrategy(op2);
            strategy.DoCalculate(calc1).ShouldBe(2);
        }
    }
}
