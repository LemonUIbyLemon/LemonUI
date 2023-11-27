namespace LemonUI.TinyTween
{
    /// <summary>
    /// Represents an interface for a tween with different stop behaviors.
    /// </summary>
    public interface ITween
    {
        /// <summary>
        /// Gets the current state of the tween.
        /// </summary>
        TweenState State { get; }
        /// <summary>
        /// Pauses the tween.
        /// </summary>
        void Pause();
        /// <summary>
        /// Resumes the paused tween.
        /// </summary>
        void Resume();
        /// <summary>
        /// Stops the tween with the specified stop behavior.
        /// </summary>
        /// <param name="stopBehavior">The stop behavior of the tween.</param>
        void Stop(StopBehavior stopBehavior);
        /// <summary>
        /// Updates the tween with the elapsed time.
        /// </summary>
        /// <param name="elapsedTime">The elapsed time since the last update.</param>
        void Update(float elapsedTime);
    }

    /// <summary>
    /// Represents an interface for a typed tween with different stop behaviors.
    /// </summary>
    /// <typeparam name="T">The type of value being tweened.</typeparam>
    public interface ITween<T> : ITween
        where T : struct
    {
        /// <summary>
        /// Gets the current value of the tween.
        /// </summary>
        T CurrentValue { get; }
        /// <summary>
        /// Starts the tween with the specified start and end values, duration, and scale function.
        /// </summary>
        /// <param name="start">The starting value of the tween.</param>
        /// <param name="end">The ending value of the tween.</param>
        /// <param name="duration">The duration of the tween.</param>
        /// <param name="scaleFunc">The scale function to be used for the tween.</param>
        void Start(T start, T end, float duration, ScaleFunc scaleFunc);
    }
}
