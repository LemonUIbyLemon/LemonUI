#if FIVEM
using CitizenFX.Core.Native;
#elif RPH
using Rage.Native;
#elif (SHVDN2 || SHVDN3)
using GTA.Native;
#endif
using System.Drawing;

namespace LemonUI.Elements
{
    /// <summary>
    /// A 2D rectangle.
    /// </summary>
    public class ScaledRectangle : BaseElement
    {
        #region Constructor

        /// <summary>
        /// Creates a new <see cref="ScaledRectangle"/> with the specified Position and Size.
        /// </summary>
        /// <param name="pos">The position of the Rectangle.</param>
        /// <param name="size">The size of the Rectangle.</param>
        public ScaledRectangle(PointF pos, SizeF size) : base(pos, size)
        {
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// Draws the rectangle on the screen.
        /// </summary>
        public override void Draw()
        {
            if (Size == SizeF.Empty)
            {
                return;
            }
#if FIVEM
            API.DrawRect(relativePosition.X, relativePosition.Y, relativeSize.Width, relativeSize.Height, Color.R, Color.G, Color.B, Color.A);
#elif RPH
            NativeFunction.CallByHash<int>(0x3A618A217E5154F0, relativePosition.X, relativePosition.Y, relativeSize.Width, relativeSize.Height, Color.R, Color.G, Color.B, Color.A);
#elif (SHVDN2 || SHVDN3)
            Function.Call(Hash.DRAW_RECT, relativePosition.X, relativePosition.Y, relativeSize.Width, relativeSize.Height, Color.R, Color.G, Color.B, Color.A);
#endif
        }
        /// <summary>
        /// Recalculates the position based on the size.
        /// </summary>
        public override void Recalculate()
        {
            base.Recalculate();
            relativePosition.X += relativeSize.Width * 0.5f;
            relativePosition.Y += relativeSize.Height * 0.5f;
        }

        #endregion
    }
}
