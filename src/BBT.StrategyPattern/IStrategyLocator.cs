// Copyright © BBT Software AG. All rights reserved.

using System.Collections.Generic;

namespace BBT.StrategyPattern
{
    /// <summary>
    /// Defines how strategies are located.
    /// </summary>
    /// <typeparam name="TStrategy">Strategy type to locate.</typeparam>
    public interface IStrategyLocator<TStrategy>
    {
        /// <summary>
        /// Returns all strategies of <typeparamref name="TStrategy"/>.
        /// </summary>
        /// <returns>See above.</returns>
        IEnumerable<TStrategy> GetAllStrategies();
    }
}
