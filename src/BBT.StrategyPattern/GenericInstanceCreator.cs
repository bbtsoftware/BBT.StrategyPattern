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
        /// <summary>
        /// See <see cref="IInstanceCreator{TInterface, TClass}.Create"/>.
        /// </summary>
        /// <returns>A new instance of <typeparamref name="TClass"/>.</returns>
        public TInterface Create()
        {
            return new TClass();
        }
    }
}
