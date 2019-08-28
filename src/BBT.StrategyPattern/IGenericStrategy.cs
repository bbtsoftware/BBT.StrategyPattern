// Copyright © BBT Software AG. All rights reserved.

namespace BBT.StrategyPattern
{
    /// <summary>
    /// The generic strategy for criteria <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of criterium.</typeparam>
    public interface IGenericStrategy<in T>
    {
        /// <summary>
        /// Gets a value indicating whether this strategy is responsible for <paramref name="criterion"/>.
        /// </summary>
        /// <param name="criterion">The criterion (<typeparamref name="T"/>) which is used to determine if this strategy is responsible.</param>
        /// <returns>True if the strategy is deemed responsible.</returns>
        bool IsResponsible(T criterion);
    }
}
