using System;

namespace LemonUI.TinyTween
{
    /// <summary>
    /// Provides a collection of scale functions for use in tweens.
    /// </summary>
    public static class ScaleFuncs
    {
        #region Fields
        /// <summary>
        /// Represents a linear scale function.
        /// </summary>
        public static readonly ScaleFunc Linear = LinearImpl;
        /// <summary>
        /// Represents a quadratic ease-in scale function.
        /// </summary>
        public static readonly ScaleFunc QuadraticEaseIn = QuadraticEaseInImpl;
        /// <summary>
        /// Represents a quadratic ease-out scale function.
        /// </summary>
        public static readonly ScaleFunc QuadraticEaseOut = QuadraticEaseOutImpl;
        /// <summary>
        /// Represents a quadratic ease-in-out scale function.
        /// </summary>
        public static readonly ScaleFunc QuadraticEaseInOut = QuadraticEaseInOutImpl;
        /// <summary>
        /// Represents a cubic ease-in scale function.
        /// </summary>
        public static readonly ScaleFunc CubicEaseIn = CubicEaseInImpl;
        /// <summary>
        /// Represents a cubic ease-out scale function.
        /// </summary>
        public static readonly ScaleFunc CubicEaseOut = CubicEaseOutImpl;
        /// <summary>
        /// Represents a cubic ease-in-out scale function.
        /// </summary>
        public static readonly ScaleFunc CubicEaseInOut = CubicEaseInOutImpl;
        /// <summary>
        /// Represents a quartic ease-in scale function.
        /// </summary>
        public static readonly ScaleFunc QuarticEaseIn = QuarticEaseInImpl;
        /// <summary>
        /// Represents a quartic ease-out scale function.
        /// </summary>
        public static readonly ScaleFunc QuarticEaseOut = QuarticEaseOutImpl;
        /// <summary>
        /// Represents a quartic ease-in-out scale function.
        /// </summary>
        public static readonly ScaleFunc QuarticEaseInOut = QuarticEaseInOutImpl;
        /// <summary>
        /// Represents a quintic ease-in scale function.
        /// </summary>
        public static readonly ScaleFunc QuinticEaseIn = QuinticEaseInImpl;
        /// <summary>
        /// Represents a quintic ease-out scale function.
        /// </summary>
        public static readonly ScaleFunc QuinticEaseOut = QuinticEaseOutImpl;
        /// <summary>
        /// Represents a quintic ease-in-out scale function.
        /// </summary>
        public static readonly ScaleFunc QuinticEaseInOut = QuinticEaseInOutImpl;
        /// <summary>
        /// Represents a sine ease-in scale function.
        /// </summary>
        public static readonly ScaleFunc SineEaseIn = SineEaseInImpl;
        /// <summary>
        /// Represents a sine ease-out scale function.
        /// </summary>
        public static readonly ScaleFunc SineEaseOut = SineEaseOutImpl;
        /// <summary>
        /// Represents a sine ease-in-out scale function.
        /// </summary>
        public static readonly ScaleFunc SineEaseInOut = SineEaseInOutImpl;
        #endregion

        #region Functions
        /// <summary>
        /// Returns the progress as is, resulting in a linear scale.
        /// </summary>
        /// <param name="progress">The progress value between 0 and 1.</param>
        /// <returns>The linear scale value.</returns>
        public static float LinearImpl(float progress)
        {
            return progress;
        }

        /// <summary>
        /// Applies quadratic ease-in to the progress.
        /// </summary>
        /// <param name="progress">The progress value between 0 and 1.</param>
        /// <returns>The eased-in scale value.</returns>
        public static float QuadraticEaseInImpl(float progress)
        {
            return EaseInPower(progress, 2);
        }

        /// <summary>
        /// Applies quadratic ease-out to the progress.
        /// </summary>
        /// <param name="progress">The progress value between 0 and 1.</param>
        /// <returns>The eased-out scale value.</returns>
        public static float QuadraticEaseOutImpl(float progress)
        {
            return EaseOutPower(progress, 2);
        }

        /// <summary>
        /// Applies quadratic ease-in-out to the progress.
        /// </summary>
        /// <param name="progress">The progress value between 0 and 1.</param>
        /// <returns>The eased-in-out scale value.</returns>
        public static float QuadraticEaseInOutImpl(float progress)
        {
            return EaseInOutPower(progress, 2);
        }

        /// <summary>
        /// Applies cubic ease-in to the progress.
        /// </summary>
        /// <param name="progress">The progress value between 0 and 1.</param>
        /// <returns>The eased-in scale value.</returns>
        public static float CubicEaseInImpl(float progress)
        {
            return EaseInPower(progress, 3);
        }

        /// <summary>
        /// Applies cubic ease-out to the progress.
        /// </summary>
        /// <param name="progress">The progress value between 0 and 1.</param>
        /// <returns>The eased-out scale value.</returns>
        public static float CubicEaseOutImpl(float progress)
        {
            return EaseOutPower(progress, 3);
        }

        /// <summary>
        /// Applies cubic ease-in-out to the progress.
        /// </summary>
        /// <param name="progress">The progress value between 0 and 1.</param>
        /// <returns>The eased-in-out scale value.</returns>
        public static float CubicEaseInOutImpl(float progress)
        {
            return EaseInOutPower(progress, 3);
        }

        /// <summary>
        /// Applies quartic ease-in to the progress.
        /// </summary>
        /// <param name="progress">The progress value between 0 and 1.</param>
        /// <returns>Returns the progress value modified with quadratic ease-in.</returns>
        public static float QuarticEaseInImpl(float progress)
        {
            return EaseInPower(progress, 4);
        }

        /// <summary>
        /// Applies quartic ease-out to the progress.
        /// </summary>
        /// <param name="progress">The progress value between 0 and 1.</param>
        /// <returns>Returns the progress value modified with quadratic ease-out.</returns>
        public static float QuarticEaseOutImpl(float progress)
        {
            return EaseOutPower(progress, 4);
        }

        /// <summary>
        /// Applies quartic ease-in-out to the progress.
        /// </summary>
        /// <param name="progress">The progress value between 0 and 1.</param>
        /// <returns>Returns the progress value modified with quadratic ease-in-out.</returns>
        public static float QuarticEaseInOutImpl(float progress)
        {
            return EaseInOutPower(progress, 4);
        }

        /// <summary>
        /// Applies quintic ease-in to the progress.
        /// </summary>
        /// <param name="progress">The progress value between 0 and 1.</param>
        /// <returns>Returns the progress value modified with quintic ease-in.</returns>
        public static float QuinticEaseInImpl(float progress)
        {
            return EaseInPower(progress, 5);
        }

        /// <summary>
        /// Applies quintic ease-out to the progress.
        /// </summary>
        /// <param name="progress">The progress value between 0 and 1.</param>
        /// <returns>Returns the progress value modified with quintic ease-out.</returns>
        public static float QuinticEaseOutImpl(float progress)
        {
            return EaseOutPower(progress, 5);
        }

        /// <summary>
        /// Applies quintic ease-in-out to the progress.
        /// </summary>
        /// <param name="progress">The progress value between 0 and 1.</param>
        /// <returns>Returns the progress value modified with quintic ease-in-out.</returns>
        public static float QuinticEaseInOutImpl(float progress)
        {
            return EaseInOutPower(progress, 5);
        }

        /// <summary>
        /// Applies ease-in power to the progress with the specified power.
        /// </summary>
        /// <param name="progress">The progress value between 0 and 1.</param>
        /// <param name="power">The power value for the ease-out function.</param>
        /// <returns>The modified progress value with ease-in power.</returns>
        public static float EaseInPower(float progress, int power)
        {
            return (float)Math.Pow(progress, power);
        }

        /// <summary>
        /// Applies ease-out power to the progress with the specified power.
        /// </summary>
        /// <param name="progress">The progress value between 0 and 1.</param>
        /// <param name="power">The power value for the ease-out function.</param>
        /// <returns>The modified progress value with ease-out power.</returns>
        public static float EaseOutPower(float progress, int power)
        {
            int sign = (power % 2 != 0) ? 1 : -1;
            return (float)(sign * (Math.Pow(progress - 1f, power) + sign));
        }

        /// <summary>
        /// Applies ease-in-out power to the progress with the specified power.
        /// </summary>
        /// <param name="progress">The progress value between 0 and 1.</param>
        /// <param name="power">The power value for the ease-out function.</param>
        /// <returns>The modified progress value with ease-in-out power.</returns>
        public static float EaseInOutPower(float progress, int power)
        {
            progress *= 2f;
            if (progress < 1f)
            {
                return (float)Math.Pow(progress, power) / 2f;
            }
            int sign = (power % 2 != 0) ? 1 : -1;
            return (float)(sign / 2.0 * (Math.Pow(progress - 2f, power) + (sign * 2)));
        }

        /// <summary>
        /// Applies sine ease-in to the progress.
        /// </summary>
        /// <param name="progress">The progress value between 0 and 1.</param>
        /// <returns>The modified progress value with sine ease-in.</returns>
        public static float SineEaseInImpl(float progress)
        {
            return (float)Math.Sin((progress * (float)Math.PI / 2f) - ((float)Math.PI / 2f)) + 1f;
        }

        /// <summary>
        /// Applies sine ease-out to the progress.
        /// </summary>
        /// <param name="progress">The progress value between 0 and 1.</param>
        /// <returns>The modified progress value with sine ease-out.</returns>
        public static float SineEaseOutImpl(float progress)
        {
            return (float)Math.Sin(progress * (float)Math.PI / 2f);
        }

        /// <summary>
        /// Applies sine ease-in-out to the progress.
        /// </summary>
        /// <param name="progress">The progress value between 0 and 1.</param>
        /// <returns>The modified progress value with sine ease-in-out.</returns>
        public static float SineEaseInOutImpl(float progress)
        {
            return (float)(Math.Sin((progress * (float)Math.PI) - ((float)Math.PI / 2f)) + 1.0) / 2f;
        }
        #endregion
    }
}
