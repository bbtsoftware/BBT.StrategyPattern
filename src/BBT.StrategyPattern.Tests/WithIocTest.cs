// Copyright © BBT Software AG. All rights reserved.

using BBT.StrategyPattern.Tests.Data;
using BBT.StrategyPattern.Tests.ExampleStrategyImpl;
using BBT.StrategyPattern.Tests.WithIoc;
using Ninject;
using Shouldly;
using System;
using Xunit;

namespace BBT.StrategyPattern.Tests
{
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
            strategy.DoCalculate(calc1).ShouldBe(8);

            strategy = kernel.Get<IGenericStrategyProvider<IOperatorStrategy, Operator>>().GetStrategy(op2);
            strategy.DoCalculate(calc1).ShouldBe(2);
        }

        [Fact]
        public void MissingStrategies_ThrowsExpectedException()
        {
            // Prepare data
            var calc1 = new CalculationInput() { Number1 = 5, Number2 = 3 };
            var op1 = new Operator() { Operation = OperatorEnum.Addition };
            var op2 = new Operator() { Operation = OperatorEnum.Subtraktion };

            // IoC registrations (note: do not register any IOperatorStrategy strategy)
            IKernel kernel = new StandardKernel();

            kernel.Bind(typeof(IStrategyLocator<>)).To(typeof(NinjectStrategyLocator<>));
            kernel.Bind<IGenericStrategyProvider<IOperatorStrategy, Operator>>().To<GenericStrategyProvider<IOperatorStrategy, Operator>>();

            // Act
            var act = () => kernel.Get<IGenericStrategyProvider<IOperatorStrategy, Operator>>().GetStrategy(op2);

            // Assert
            var ex = Should.Throw<InvalidOperationException>(act);
            ex.Message.ShouldBe("No strategies of type IOperatorStrategy are available from the locator.");
        }

        [Fact]
        public void MissingStrategyForCriteria_ThrowsExpectedException()
        {
            // Prepare data
            var calc1 = new CalculationInput() { Number1 = 5, Number2 = 3 };
            var op1 = new Operator() { Operation = OperatorEnum.Addition };
            var op2 = new Operator() { Operation = OperatorEnum.Subtraktion };

            // IoC registrations (note: do not register SubstractionStrategy)
            IKernel kernel = new StandardKernel();

            kernel.Bind(typeof(IStrategyLocator<>)).To(typeof(NinjectStrategyLocator<>));

            kernel.Bind<IOperatorStrategy>().To<AdditionStrategy>();

            kernel.Bind<IGenericStrategyProvider<IOperatorStrategy, Operator>>().To<GenericStrategyProvider<IOperatorStrategy, Operator>>();

            // Act
            var act = () => kernel.Get<IGenericStrategyProvider<IOperatorStrategy, Operator>>().GetStrategy(op2);

            // Assert
            var ex = Should.Throw<InvalidOperationException>(act);
            ex.Message.ShouldBe("No strategy of type IOperatorStrategy available from the locator being responsible for criterion of type Operator.");
        }
    }
}