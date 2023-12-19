using System;

namespace LemonUI.Menus
{
    /// <summary>
    /// Represents a badge that can be applied to a <see cref="NativeItem"/>.
    /// </summary>
    public class BadgeSet
    {
        #region Fields

        private string normalDict = string.Empty;
        private string normalTexture = string.Empty;
        private string hoveredDict = string.Empty;
        private string hoveredTexture = string.Empty;

        #endregion

        #region Properties

        /// <summary>
        /// The texture dictionary where the normal texture is located.
        /// </summary>
        public string NormalDictionary
        {
            get => normalDict;
            set => normalDict = value ?? throw new ArgumentNullException(nameof(value));
        }
        /// <summary>
        /// The texture to use when the item is not hovered.
        /// </summary>
        public string NormalTexture
        {
            get => normalTexture;
            set => normalTexture = value ?? throw new ArgumentNullException(nameof(value));
        }
        /// <summary>
        /// The texture dictionary where the normal texture is located.
        /// </summary>
        public string HoveredDictionary
        {
            get => hoveredDict;
            set => hoveredDict = value ?? throw new ArgumentNullException(nameof(value));
        }
        /// <summary>
        /// The texture to use when the item is hovered.
        /// </summary>
        public string HoveredTexture
        {
            get => hoveredTexture;
            set => hoveredTexture = value ?? throw new ArgumentNullException(nameof(value));
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new empty <see cref="BadgeSet"/>.
        /// </summary>
        public BadgeSet()
        {
        }
        /// <summary>
        /// Creates a new <see cref="BadgeSet"/> where both textures are in the same dictionary.
        /// </summary>
        /// <param name="dict">The dictionary where the textures are located.</param>
        /// <param name="normal">The normal texture name.</param>
        /// <param name="hovered">The hovered texture name.</param>
        public BadgeSet(string dict, string normal, string hovered)
        {
            normalDict = dict ?? throw new ArgumentNullException(nameof(dict));
            normalTexture = normal ?? throw new ArgumentNullException(nameof(normal));
            hoveredDict = dict;
            hoveredTexture = hovered ?? throw new ArgumentNullException(nameof(hovered));
        }
        /// <summary>
        /// Creates a new <see cref="BadgeSet"/> where both textures are in different dictionaries.
        /// </summary>
        /// <param name="normalDict">The dictionary where the normal texture is located.</param>
        /// <param name="normalTexture">The normal texture name.</param>
        /// <param name="hoveredDict">The dictionary where the hovered texture is located.</param>
        /// <param name="hoveredTexture">The hovered texture name.</param>
        public BadgeSet(string normalDict, string normalTexture, string hoveredDict, string hoveredTexture)
        {
            this.normalDict = normalDict ?? throw new ArgumentNullException(nameof(normalDict));
            this.normalTexture = normalTexture ?? throw new ArgumentNullException(nameof(normalTexture));
            this.hoveredDict = hoveredDict ?? throw new ArgumentNullException(nameof(hoveredDict));
            this.hoveredTexture = hoveredTexture ?? throw new ArgumentNullException(nameof(hoveredTexture));
        }

        #endregion
    }
}
