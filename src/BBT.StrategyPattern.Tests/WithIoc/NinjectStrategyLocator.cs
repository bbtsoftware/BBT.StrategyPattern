// Copyright © BBT Software AG. All rights reserved.

namespace BBT.StrategyPattern.Tests.WithIoc
{
    using System;
    using System.Collections.Generic;
    using Ninject;

    public class NinjectStrategyLocator<T> : IStrategyLocator<T>
    {
        public NinjectStrategyLocator(IKernel kernel)
        {
            this.Kernel = kernel ?? throw new ArgumentNullException(nameof(kernel));
        }

        public IKernel Kernel { get; }

        public IEnumerable<T> GetAllStrategies()
        {
            return Kernel.GetAll<T>();
        }
    }
}
