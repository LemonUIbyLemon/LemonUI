namespace LemonUI.Menus
{
    /// <summary>
    /// Represents a badge that can be applied to a <see cref="NativeItem"/>.
    /// </summary>
    public class BadgeSet
    {
        #region Properties

        /// <summary>
        /// The texture dictionary where the normal texture is located.
        /// </summary>
        public string NormalDictionary { get; set; } = "";
        /// <summary>
        /// The texture to use when the item is not hovered.
        /// </summary>
        public string NormalTexture { get; set; } = "";
        /// <summary>
        /// The texture dictionary where the normal texture is located.
        /// </summary>
        public string HoveredDictionary { get; set; } = "";
        /// <summary>
        /// The texture to use when the item is hovered.
        /// </summary>
        public string HoveredTexture { get; set; } = "";

        #endregion

        #region Constructor

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
            NormalDictionary = dict;
            NormalTexture = normal;
            HoveredDictionary = dict;
            HoveredTexture = hovered;
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
            NormalDictionary = normalDict;
            NormalTexture = normalTexture;
            HoveredDictionary = hoveredDict;
            HoveredTexture = hoveredTexture;
        }

        #endregion
    }
}
