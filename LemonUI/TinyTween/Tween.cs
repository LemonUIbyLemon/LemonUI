using System;

namespace LemonUI.TinyTween
{
    /// <summary>
    /// Represents a generic tween with different stop behaviors.
    /// </summary>
    /// <typeparam name="T">The type of value being tweened.</typeparam>
    public class Tween<T> : ITween<T>, ITween
        where T : struct
    {
        #region Fields
        /// <summary>
        /// The lerp function used for the tween.
        /// </summary>
        private readonly LerpFunc<T> lerpFunc;
        /// <summary>
        /// The current time of the tween.
        /// </summary>
        private float currentTime;
        /// <summary>
        /// The duration of the tween.
        /// </summary>
        private float duration;
        /// <summary>
        /// The scale function used for the tween.
        /// </summary>
        private ScaleFunc scaleFunc;
        /// <summary>
        /// The state of the tween.
        /// </summary>
        private TweenState state;
        /// <summary>
        /// The starting value of the tween.
        /// </summary>
        private T start;
        /// <summary>
        /// The ending value of the tween.
        /// </summary>
        private T end;
        /// <summary>
        /// The current value of the tween.
        /// </summary>
        private T value;
        /// <summary>
        /// Gets the current time of the tween.
        /// </summary>
        public float CurrentTime => currentTime;
        /// <summary>
        /// Gets the duration of the tween.
        /// </summary>
        public float Duration => duration;
        /// <summary>
        /// Gets the state of the tween.
        /// </summary>
        public TweenState State => state;
        /// <summary>
        /// Gets the starting value of the tween.
        /// </summary>
        public T StartValue => start;
        /// <summary>
        /// Gets the ending value of the tween.
        /// </summary>
        public T EndValue => end;
        /// <summary>
        /// Gets the current value of the tween.
        /// </summary>
        public T CurrentValue => value;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the Tween class with the specified lerp function.
        /// </summary>
        /// <param name="lerpFunc">The lerp function used for the tween.</param>
        public Tween(LerpFunc<T> lerpFunc)
        {
            this.lerpFunc = lerpFunc;
            state = TweenState.Stopped;
        }
        #endregion

        #region Functions
        /// <summary>
        /// Starts the tween with the specified start and end values, duration, and scale function.
        /// </summary>
        /// <param name="start">The starting value of the tween.</param>
        /// <param name="end">The ending value of the tween.</param>
        /// <param name="duration">The duration of the tween.</param>
        /// <param name="scaleFunc">The scale function to be used for the tween.</param>
        /// <exception cref="ArgumentException">Thrown when the duration is less than or equal to 0.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the scaleFunc is null.</exception>
        public void Start(T start, T end, float duration, ScaleFunc scaleFunc)
        {
            if (duration <= 0f)
            {
                throw new ArgumentException("duration must be greater than 0");
            }
            if (scaleFunc == null)
            {
                throw new ArgumentNullException("scaleFunc");
            }
            currentTime = 0f;
            this.duration = duration;
            this.scaleFunc = scaleFunc;
            state = TweenState.Running;
            this.start = start;
            this.end = end;
            UpdateValue();
        }

        /// <summary>
        /// Pauses the tween if it is currently running.
        /// </summary>
        public void Pause()
        {
            if (state == TweenState.Running)
            {
                state = TweenState.Paused;
            }
        }

        /// <summary>
        /// Resumes the tween if it is currently paused.
        /// </summary>
        public void Resume()
        {
            if (state == TweenState.Paused)
            {
                state = TweenState.Running;
            }
        }

        /// <summary>
        /// Stops the tween with the specified stop behavior.
        /// </summary>
        /// <param name="stopBehavior">The stop behavior to apply.</param>
        public void Stop(StopBehavior stopBehavior)
        {
            state = TweenState.Stopped;
            if (stopBehavior == StopBehavior.ForceComplete)
            {
                currentTime = duration;
                UpdateValue();
            }
        }

        /// <summary>
        /// Updates the tween with the specified elapsed time.
        /// </summary>
        /// <param name="elapsedTime">The elapsed time since the last update.</param>
        public void Update(float elapsedTime)
        {
            if (state == TweenState.Running)
            {
                currentTime += elapsedTime;
                if (currentTime >= duration)
                {
                    currentTime = duration;
                    state = TweenState.Stopped;
                }
                UpdateValue();
            }
        }

        private void UpdateValue()
        {
            value = lerpFunc(start, end, scaleFunc(currentTime / duration));
        }
        #endregion
    }
}