// Copyright © BBT Software AG. All rights reserved.

namespace BBT.StrategyPattern.Tests.WithoutIoc
{
    using System.Collections.Generic;
    using BBT.StrategyPattern.Tests.ExampleStrategyImpl;

    public class MemoryOperatorStrategyLocator : IStrategyLocator<IOperatorStrategy>
    {
        public IEnumerable<IOperatorStrategy> GetAllStrategies()
        {
            return new List<IOperatorStrategy>()
            {
                new AdditionStrategy(),
                new SubstractionStrategy(),
            };
        }
    }
}
