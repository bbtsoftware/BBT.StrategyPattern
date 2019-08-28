// Copyright © BBT Software AG. All rights reserved.

namespace BBT.StrategyPattern
{
    /// <summary>
    /// Generic strategy factory for criteria of type <typeparamref name="TCriterion"/>
    /// and strategies of type <typeparamref name="TStrategy"/>.
    /// </summary>
    /// <typeparam name="TStrategy">The type of strategy.</typeparam>
    /// <typeparam name="TCriterion">The type of criterium.</typeparam>
    public interface IGenericStrategyProvider<out TStrategy, in TCriterion>
        where TStrategy : IGenericStrategy<TCriterion>
    {
        /// <summary>
        /// Gets the strategy corresponding to <paramref name="criterion"/>.
        /// </summary>
        /// <param name="criterion">Criterion for which the strategy must be responsible.</param>
        /// <returns>A strategy matching <paramref name="criterion"/>. If no or more than one strategies are found an exception is thrown.</returns>
        TStrategy GetStrategy(TCriterion criterion);
    }
}
