﻿using System.Threading.Tasks;

namespace Forge.OpenAI.Interfaces.Infrastructure
{

    /// <summary>Represents the logger implementation</summary>
    public interface IApiHttpLoggerContext
    {

        /// <summary>Gets the context identifier.</summary>
        /// <value>The context identifier.</value>
        long ContextId { get; }

        /// <summary>Logs the specified data.</summary>
        /// <param name="data">The data.</param>
        /// <returns>
        ///   <br />
        /// </returns>
#if NETCOREAPP3_1_OR_GREATER
        Task Log(object? data);
#else
        Task Log(object data);
#endif

    }

}
