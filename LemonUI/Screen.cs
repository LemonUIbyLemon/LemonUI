#if FIVEMV2
using CitizenFX.FiveM;
using CitizenFX.FiveM.Native;
using CitizenFX.FiveM.GUI;
#elif FIVEM
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
#elif RAGEMP
using RAGE.Game;
#elif RPH
using Rage;
using Rage.Native;
using Control = Rage.GameControl;
#elif SHVDN3 || SHVDNC
using GTA;
using GTA.Native;
using GTA.UI;
#elif ALTV
using AltV.Net.Client;
using LemonUI.Elements;
#endif
using System;
using System.Drawing;
using LemonUI.Tools;

namespace LemonUI
{
    /// <summary>
    /// Contains a set of tools to work with the screen information.
    /// </summary>
    [Obsolete("Use the LemonUI.Tools and LemonUI.Math namespaces.")]
    public static class Screen
    {
        #region Properties

        /// <summary>
        /// The Aspect Ratio of the screen resolution.
        /// </summary>
        public static float AspectRatio => GameScreen.AspectRatio;
#if ALTV
        /// <summary>
        /// Gets the actual Screen resolution the game is being rendered at.
        /// </summary>
        public static Size Resolution => GameScreen.AbsoluteResolution.ToSize();
#endif
        /// <summary>
        /// The location of the cursor on screen between 0 and 1.
        /// </summary>
        public static PointF CursorPositionRelative => GameScreen.Cursor.ToRelative();

        #endregion

        #region Functions

        /// <summary>
        /// Converts a relative resolution into one scaled to 1080p.
        /// </summary>
        /// <param name="relativeX">The relative value of X.</param>
        /// <param name="relativeY">The relative value of Y.</param>
        /// <param name="absoluteX">The value of X scaled to 1080p.</param>
        /// <param name="absoluteY">The value of Y scaled to 1080p.</param>
        public static void ToAbsolute(float relativeX, float relativeY, out float absoluteX, out float absoluteY)
        {
            absoluteX = relativeX.ToXScaled();
            absoluteY = relativeY.ToYScaled();
        }
        /// <summary>
        /// Converts a 1080p-based resolution into relative values.
        /// </summary>
        /// <param name="absoluteX">The 1080p based X coord.</param>
        /// <param name="absoluteY">The 1080p based Y coord.</param>
        /// <param name="relativeX">The value of X converted to relative.</param>
        /// <param name="relativeY">The value of Y converted to relative.</param>
        public static void ToRelative(float absoluteX, float absoluteY, out float relativeX, out float relativeY)
        {
            relativeX = absoluteX.ToXRelative();
            relativeY = absoluteY.ToYRelative();
        }
        /// <summary>
        /// Checks if the cursor is inside of the specified area.
        /// </summary>
        /// <remarks>
        /// This function takes values scaled to 1080p and is aware of the alignment set via SET_SCRIPT_GFX_ALIGN.
        /// </remarks>
        /// <param name="pos">The start of the area.</param>
        /// <param name="size">The size of the area to check.</param>
        /// <returns><see langword="true"/> if the cursor is in the specified bounds, <see langword="false"/> otherwise.</returns>
        public static bool IsCursorInArea(PointF pos, SizeF size) => IsCursorInArea(pos.X, pos.Y, size.Width, size.Height);
        /// <summary>
        /// Checks if the cursor is inside of the specified area.
        /// </summary>
        /// <remarks>
        /// This function takes values scaled to 1080p and is aware of the alignment set via SET_SCRIPT_GFX_ALIGN.
        /// </remarks>
        /// <param name="x">The start X position.</param>
        /// <param name="y">The start Y position.</param>
        /// <param name="width">The height of the search area from X.</param>
        /// <param name="height">The height of the search area from Y.</param>
        /// <returns><see langword="true"/> if the cursor is in the specified bounds, <see langword="false"/> otherwise.</returns>
        public static bool IsCursorInArea(float x, float y, float width, float height)
        {
            // intentionally kept this way to avoid breaking backwards compatibility
            PointF realPos = GetRealPosition(x, y).ToRelative();
            return GameScreen.IsCursorInArea(realPos.X, realPos.Y, width, height);
        }
        /// <summary>
        /// Converts the specified position into one that is aware of <see cref="SetElementAlignment(GFXAlignment, GFXAlignment)"/>.
        /// </summary>
        /// <param name="og">The original 1080p based position.</param>
        /// <returns>A new 1080p based position that is aware of the the Alignment.</returns>
        public static PointF GetRealPosition(PointF og) => GetRealPosition(og.X, og.Y);
        /// <summary>
        /// Converts the specified position into one that is aware of <see cref="SetElementAlignment(GFXAlignment, GFXAlignment)"/>.
        /// </summary>
        /// <param name="x">The 1080p based X position.</param>
        /// <param name="y">The 1080p based Y position.</param>
        /// <returns>A new 1080p based position that is aware of the the Alignment.</returns>
        public static PointF GetRealPosition(float x, float y) => SafeZone.GetSafePosition(x, y);
        /// <summary>
        /// Shows the cursor during the current game frame.
        /// </summary>
        public static void ShowCursorThisFrame() => GameScreen.ShowCursorThisFrame();
        /// <summary>
        /// Sets the alignment of game elements like <see cref="Elements.ScaledRectangle"/>, <see cref="Elements.ScaledText"/> and <see cref="Elements.ScaledTexture"/>.
        /// </summary>
        /// <param name="horizontal">The Horizontal alignment of the items.</param>
        /// <param name="vertical">The vertical alignment of the items.</param>
        public static void SetElementAlignment(Alignment horizontal, GFXAlignment vertical) => SafeZone.SetAlignment(horizontal, vertical);
        /// <summary>
        /// Sets the alignment of game elements like <see cref="Elements.ScaledRectangle"/>, <see cref="Elements.ScaledText"/> and <see cref="Elements.ScaledTexture"/>.
        /// </summary>
        /// <param name="horizontal">The Horizontal alignment of the items.</param>
        /// <param name="vertical">The vertical alignment of the items.</param>
        public static void SetElementAlignment(GFXAlignment horizontal, GFXAlignment vertical) => SafeZone.SetAlignment(horizontal, vertical);
        /// <summary>
        /// Resets the alignment of the game elements.
        /// </summary>
        public static void ResetElementAlignment() => SafeZone.ResetAlignment();

        #endregion
    }
}
