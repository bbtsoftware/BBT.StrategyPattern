// Copyright © BBT Software AG. All rights reserved.

namespace BBT.StrategyPattern
{
    /// <summary>
    /// Generic implementation of <see cref="IInstanceCreator{TInterface, TClass}"/>.
    /// </summary>
    /// <typeparam name="TInterface">Interface type which is returned.</typeparam>
    /// <typeparam name="TClass">Class which is instantiated.</typeparam>
    public class GenericInstanceCreator<TInterface, TClass> : IInstanceCreator<TInterface, TClass>
        where TInterface : class
        where TClass : TInterface, new()
    {
        /// <inheritdoc/>
        public TInterface Create()
        {
            return new TClass();
        }
    }
}
