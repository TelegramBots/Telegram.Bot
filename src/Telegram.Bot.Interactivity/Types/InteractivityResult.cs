namespace Interactivity.Types
{
    public class InteractivityResult<T>
    {
        /// <summary>
        /// The actual result.
        /// </summary>
        public T Value { get; set; }
        /// <summary>
        /// Indicates whether the process has timed out.
        /// </summary>
        public bool TimedOut { get; set; }
        /// <summary>
        /// Indicates whether or not an error has occured.
        /// </summary>
        public bool Error { get => Value == null && !TimedOut; }
        /// <summary>
        /// Create a new InteractivityResult
        /// </summary>
        /// <param name="value">The value of the result (nullable)</param>
        /// <param name="timedOut">Has the process timed out?</param>
        public InteractivityResult(T value, bool timedOut)
        {
            this.Value = value;
            this.TimedOut = timedOut;
        }

    }
}
