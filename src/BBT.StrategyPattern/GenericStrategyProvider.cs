﻿// Copyright © BBT Software AG. All rights reserved.

namespace BBT.StrategyPattern
{
    using System;
    using System.Linq;

    /// <summary>
    /// Generic implementation of <see cref="IGenericStrategyProvider{TStrategy,TCriterion}"/>.
    /// </summary>
    /// <typeparam name="TStrategy">See link above.</typeparam>
    /// <typeparam name="TCriterion">See link above.</typeparam>
    public class GenericStrategyProvider<TStrategy, TCriterion>
        : IGenericStrategyProvider<TStrategy, TCriterion>
        where TStrategy : IGenericStrategy<TCriterion>
    {
        private readonly IStrategyLocator<TStrategy> strategyLocator;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericStrategyProvider{TStrategy, TCriterion}"/> class.
        /// </summary>
        /// <param name="strategyLocator">The <see cref="IStrategyLocator{TStrategy}"/> which can locate the strategies of which the responsible on is provided.</param>
        public GenericStrategyProvider(IStrategyLocator<TStrategy> strategyLocator)
        {
            this.strategyLocator = strategyLocator ?? throw new ArgumentNullException(nameof(strategyLocator));
        }

        /// <inheritdoc/>
        public TStrategy GetStrategy(TCriterion criterion)
        {
            var strategies = this.strategyLocator.GetAllStrategies();

            // If no strategies for TStrategy can be found.
            if (!strategies.Any())
            {
                throw new InvalidOperationException($"No strategies of type {typeof(TStrategy).Name} are available from the locator.");
            }

            var strategy = strategies.SingleOrDefault(x => x.IsResponsible(criterion));

            // If no strategy responsible for TCriterion can be found.
            if (strategy == null)
            {
                throw new InvalidOperationException($"No strategy of type {typeof(TStrategy).Name} available from the locator being responsible for criterion of type {typeof(TCriterion).Name}.");
            }

            return strategy;
        }
    }
}