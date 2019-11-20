// Copyright © BBT Software AG. All rights reserved.

namespace BBT.StrategyPattern.Tests
{
    using BBT.StrategyPattern.Tests.Data;
    using BBT.StrategyPattern.Tests.ExampleStrategyImpl;
    using BBT.StrategyPattern.Tests.WithIoc;
    using FluentAssertions;
    using Ninject;
    using Xunit;

    public class WithIocTest
    {
        [Fact]
        public void WorksWithIocNinject()
        {
            // Prepare data
            var calc1 = new CalculationInput() { Number1 = 5, Number2 = 3 };
            var op1 = new Operator() { Operation = OperatorEnum.Addition };
            var op2 = new Operator() { Operation = OperatorEnum.Subtraktion };


            // IoC registrations
            IKernel kernel = new StandardKernel();

            kernel.Bind(typeof(IStrategyLocator<>)).To(typeof(NinjectStrategyLocator<>));

            kernel.Bind<IOperatorStrategy>().To<AdditionStrategy>();
            kernel.Bind<IOperatorStrategy>().To<SubstractionStrategy>();

            kernel.Bind<IGenericStrategyProvider<IOperatorStrategy, Operator>>().To<GenericStrategyProvider<IOperatorStrategy, Operator>>();

            // Use strategy
            IOperatorStrategy strategy;

            strategy = kernel.Get<IGenericStrategyProvider<IOperatorStrategy, Operator>>().GetStrategy(op1);
            strategy.DoCalculate(calc1).Should().Be(8);

            strategy = kernel.Get<IGenericStrategyProvider<IOperatorStrategy, Operator>>().GetStrategy(op2);
            strategy.DoCalculate(calc1).Should().Be(2);
        }
    }
}
