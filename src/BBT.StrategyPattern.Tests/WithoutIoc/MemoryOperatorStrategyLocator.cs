// Copyright © BBT Software AG. All rights reserved.

using System.Collections.Generic;
using BBT.StrategyPattern.Tests.ExampleStrategyImpl;

namespace BBT.StrategyPattern.Tests.WithoutIoc
{
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
