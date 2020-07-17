#if FIVEM
using CitizenFX.Core.Native;
#elif SHVDN2
using GTA.Native;
#elif SHVDN3
using GTA.Native;
#endif

namespace LemonUI
{
    /// <summary>
    /// Contains information for a Game Sound that is played at specific times.
    /// </summary>
    public class Sound
    {
        /// <summary>
        /// The Set where the sound is located.
        /// </summary>
        public string Set { get; set; }
        /// <summary>
        /// The name of the sound file.
        /// </summary>
        public string File { get; set; }

        /// <summary>
        /// Creates a new <see cref="Sound"/> class with the specified Sound Set and File.
        /// </summary>
        /// <param name="set">The Set where the sound is located.</param>
        /// <param name="file">The name of the sound file.</param>
        public Sound(string set, string file)
        {
            Set = set;
            File = file;
        }

        /// <summary>
        /// Plays the sound for the local user.
        /// </summary>
        public void PlayFrontend()
        {
#if FIVEM
            API.PlaySoundFrontend(-1, File, Set, false);
            int id = API.GetSoundId();
            API.ReleaseSoundId(id);
#else
            Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, File, Set, false);
            int id = Function.Call<int>(Hash.GET_SOUND_ID);
            Function.Call(Hash.RELEASE_SOUND_ID, id);
#endif
        }
    }
}
