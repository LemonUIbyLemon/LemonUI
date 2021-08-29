using System.Drawing;

namespace LemonUI.Menus
{
    /// <summary>
    /// Stores the different colors required to make the colors of a <see cref="NativeItem"/> dynamic.
    /// </summary>
    public class ColorSet
    {
        #region Fields

        private static readonly Color colorWhite = Color.FromArgb(255, 255, 255, 255);
        private static readonly Color colorWhiteSmoke = Color.FromArgb(255, 245, 245, 245);
        private static readonly Color colorBlack = Color.FromArgb(255, 0, 0, 0);
        private static readonly Color colorDisabled = Color.FromArgb(255, 163, 159, 148);

        #endregion

        #region Properties

        /// <summary>
        /// The color of the <see cref="NativeItem.Title"/> when the <see cref="NativeItem"/> is not hovered and enabled.
        /// </summary>
        public Color TitleNormal { get; set; } = colorWhiteSmoke;
        /// <summary>
        /// The color of the <see cref="NativeItem.Title"/> when the <see cref="NativeItem"/> is hovered.
        /// </summary>
        public Color TitleHovered { get; set; } = colorBlack;
        /// <summary>
        /// The color of the <see cref="NativeItem.Title"/> when the <see cref="NativeItem"/> is disabled.
        /// </summary>
        public Color TitleDisabled { get; set; } = colorDisabled;
        /// <summary>
        /// The color of the <see cref="NativeItem.AltTitle"/> when the <see cref="NativeItem"/> is not hovered and enabled.
        /// </summary>
        public Color AltTitleNormal { get; set; } = colorWhiteSmoke;
        /// <summary>
        /// The color of the <see cref="NativeItem.AltTitle"/> when the <see cref="NativeItem"/> is hovered.
        /// </summary>
        public Color AltTitleHovered { get; set; } = colorBlack;
        /// <summary>
        /// The color of the <see cref="NativeItem.AltTitle"/> when the <see cref="NativeItem"/> is disabled.
        /// </summary>
        public Color AltTitleDisabled { get; set; } = colorDisabled;
        /// <summary>
        /// The color of the <see cref="NativeSlidableItem"/> arrows when the item is not hovered and enabled.
        /// </summary>
        public Color ArrowsNormal { get; set; } = colorWhiteSmoke;
        /// <summary>
        /// The color of the <see cref="NativeSlidableItem"/> arrows when the item is hovered.
        /// </summary>
        public Color ArrowsHovered { get; set; } = colorBlack;
        /// <summary>
        /// The color of the <see cref="NativeSlidableItem"/> arrows when the item is disabled.
        /// </summary>
        public Color ArrowsDisabled { get; set; } = colorDisabled;
        /// <summary>
        /// The color of the <see cref="NativeItem.LeftBadge"/> when the <see cref="NativeItem"/> is not hovered and enabled.
        /// </summary>
        public Color BadgeLeftNormal { get; set; } = colorWhite;
        /// <summary>
        /// The color of the <see cref="NativeItem.LeftBadge"/> when the <see cref="NativeItem"/> is hovered.
        /// </summary>
        public Color BadgeLeftHovered { get; set; } = colorWhite;
        /// <summary>
        /// The color of the <see cref="NativeItem.LeftBadge"/> when the <see cref="NativeItem"/> is disabled.
        /// </summary>
        public Color BadgeLeftDisabled { get; set; } = colorWhite;
        /// <summary>
        /// The color of the <see cref="NativeItem.RightBadge"/> or <see cref="NativeCheckboxItem"/> checkbox when the <see cref="NativeItem"/> is not hovered and enabled.
        /// </summary>
        public Color BadgeRightNormal { get; set; } = colorWhite;
        /// <summary>
        /// The color of the <see cref="NativeItem.RightBadge"/> or <see cref="NativeCheckboxItem"/> checkbox when the <see cref="NativeItem"/> is hovered.
        /// </summary>
        public Color BadgeRightHovered { get; set; } = colorWhite;
        /// <summary>
        /// The color of the <see cref="NativeItem.RightBadge"/> or <see cref="NativeCheckboxItem"/> checkbox when the <see cref="NativeItem"/> is disabled.
        /// </summary>
        public Color BadgeRightDisabled { get; set; } = colorWhite;

        #endregion
    }
}
