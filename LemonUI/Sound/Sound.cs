#if FIVEM
using CitizenFX.Core.Native;
#elif SHVDN2
using GTA.Native;
#elif SHVDN3
using GTA.Native;
#endif

namespace LemonUI.Sound
{
    /// <summary>
    /// Class used for playing a sound.
    /// </summary>
    internal class Sound
    {
        /// <summary>
        /// The set where this file is part of.
        /// </summary>
        public string Set { get; }
        /// <summary>
        /// The name of the sound file.
        /// </summary>
        public string File { get; }

        public Sound(string set, string file)
        {
            Set = set;
            File = file;
        }

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
