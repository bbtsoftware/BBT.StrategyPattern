// Copyright © BBT Software AG. All rights reserved.

namespace BBT.StrategyPattern
{
    /// <summary>
    /// Helper interface to instantiate instances.
    /// </summary>
    /// <typeparam name="TInterface">The interface type.</typeparam>
    /// <typeparam name="TClass">The class type, must inherit from <typeparamref name="TInterface"/>.</typeparam>
    public interface IInstanceCreator<TInterface, TClass>
        where TInterface : class
        where TClass : TInterface, new()
    {
        /// <summary>
        /// Creates an instance of type <typeparamref name="TClass"/>.
        /// </summary>
        /// <returns>Returns a new instance of <typeparamref name="TClass"/>.</returns>
        TInterface Create();
    }
}
