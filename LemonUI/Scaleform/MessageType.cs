namespace LemonUI.Scaleform
{
    /// <summary>
    /// The type for a big message.
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// A centered message with customizable text an d background colors.
        /// Internally called SHOW_SHARD_CENTERED_MP_MESSAGE.
        /// </summary>
        Customizable = 0,
        /// <summary>
        /// Used when you rank up on GTA Online.
        /// Internally called SHOW_SHARD_CREW_RANKUP_MP_MESSAGE.
        /// </summary>
        RankUp = 1,
        /// <summary>
        /// The Mission Passed screen on PS3 and Xbox 360.
        /// Internally called SHOW_MISSION_PASSED_MESSAGE.
        /// </summary>
        MissionPassedOldGen = 2,
        /// <summary>
        /// The Message Type shown on the Wasted screen.
        /// Internally called SHOW_SHARD_WASTED_MP_MESSAGE.
        /// </summary>
        Wasted = 3,
        /// <summary>
        /// Used on the GTA Online Freemode event announcements.
        /// Internally called SHOW_PLANE_MESSAGE.
        /// </summary>
        Plane = 4,
        /// <summary>
        /// Development leftover from when GTA Online was Cops and Crooks.
        /// Internally called SHOW_BIG_MP_MESSAGE.
        /// </summary>
        CopsAndCrooks = 5,
        /// <summary>
        /// Message shown when the player purchases a weapon.
        /// Internally called SHOW_WEAPON_PURCHASED.
        /// </summary>
        Weapon = 6,
        /// <summary>
        /// Unknown where this one is used.
        /// Internally called SHOW_CENTERED_MP_MESSAGE_LARGE.
        /// </summary>
        CenteredLarge = 7,
    }
}
