namespace LemonUI.TinyTween
{
    /// <summary>
    /// Represents a delegate for a lerp function used in a tween.
    /// </summary>
    /// <typeparam name="T">The type of value being lerped.</typeparam>
    /// <param name="start">The starting value of the lerp.</param>
    /// <param name="end">The ending value of the lerp.</param>
    /// <param name="progress">The progress of the lerp.</param>
    /// <returns>The lerped value based on the progress.</returns>
    public delegate T LerpFunc<T>(T start, T end, float progress);
}
