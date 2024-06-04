namespace LemonUI.TinyTween
{
    /// <summary>
    /// Represents a tween for floating-point values.
    /// </summary>
    public class FloatTween : Tween<float>
    {
        private static readonly LerpFunc<float> LerpFunc = LerpFloat;

        private static float LerpFloat(float start, float end, float progress)
        {
            return start + ((end - start) * progress);
        }

        /// <summary>
        /// Initializes a new instance of the FloatTween class.
        /// </summary>
        public FloatTween()
        : base(LerpFunc)
        {
        }
    }
}
