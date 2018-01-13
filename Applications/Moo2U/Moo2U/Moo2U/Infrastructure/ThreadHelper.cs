namespace Moo2U.Infrastructure {
    using System;

    /// <summary>
    /// Class ThreadHelper.
    /// </summary>
    public static class ThreadHelper {

        /// <summary>
        /// Called from any thread, returns <c>true</c> when the current thread is the main thread; othewise, <c>false</c>.
        /// </summary>
        /// <value>The is on main thread.</value>
        public static Boolean IsOnMainThread => Environment.CurrentManagedThreadId == MainThreadId;

        /// <summary>
        /// Gets the main thread identifier.
        /// </summary>
        /// <value>The main thread identifier.</value>
        public static Int32 MainThreadId { get; private set; }

        /// <summary>
        /// Initializes the specified main thread identifier.
        /// </summary>
        /// <param name="mainThreadId">The main thread identifier.</param>
        public static void Initialize(Int32 mainThreadId) {
            MainThreadId = mainThreadId;
        }

    }
}
