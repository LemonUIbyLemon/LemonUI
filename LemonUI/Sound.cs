#if FIVEM
using CitizenFX.Core;
using CitizenFX.Core.Native;
#elif RPH
using Rage;
using Rage.Native;
#elif SHVDN2
using GTA;
using GTA.Native;
#elif SHVDN3
using GTA;
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
        /// Plays the sound for the local <see cref="Player"/>.
        /// </summary>
        public void PlayFrontend()
        {
#if FIVEM
            API.PlaySoundFrontend(-1, File, Set, false);
            int id = API.GetSoundId();
            API.ReleaseSoundId(id);
#elif RPH
            NativeFunction.CallByHash<int>(0x67C540AA08E4A6F5, -1, File, Set, false);
            int id = NativeFunction.CallByHash<int>(0x430386FE9BF80B45);
            NativeFunction.CallByHash<int>(0x353FC880830B88FA, id);
#elif (SHVDN2 || SHVDN3)
            Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, File, Set, false);
            int id = Function.Call<int>(Hash.GET_SOUND_ID);
            Function.Call(Hash.RELEASE_SOUND_ID, id);
#endif
        }
    }
}
