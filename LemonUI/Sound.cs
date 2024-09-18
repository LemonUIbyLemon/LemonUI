#if FIVEMV2
using CitizenFX.FiveM;
using CitizenFX.FiveM.Native;
#elif FIVEM
using CitizenFX.Core;
using CitizenFX.Core.Native;
#elif RAGEMP
using RAGE.Game;
#elif RPH
using Rage;
using Rage.Native;
#elif SHVDN3 || SHVDNC
using GTA;
using GTA.Native;
#elif ALTV
using AltV.Net.Client;
using AltV.Net.Client.Elements.Entities;
#endif
using System;

namespace LemonUI
{
    /// <summary>
    /// Contains information for a Game Sound that is played at specific times.
    /// </summary>
    public class Sound
    {
        #region Fields

        private string set = string.Empty;
        private string file = string.Empty;

        #endregion

        #region Properties

        /// <summary>
        /// The ID of the sound, if is being played.
        /// </summary>
        public int Id { get; private set; } = -1;
        /// <summary>
        /// The Set where the sound is located.
        /// </summary>
        public string Set
        {
            get => set;
            set => set = value ?? throw new ArgumentNullException(nameof(value));
        }
        /// <summary>
        /// The name of the sound file.
        /// </summary>
        public string File
        {
            get => file;
            set => file = value ?? throw new ArgumentNullException(nameof(value));
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new <see cref="Sound"/> class with the specified Sound Set and File.
        /// </summary>
        /// <param name="set">The Set where the sound is located.</param>
        /// <param name="file">The name of the sound file.</param>
        public Sound(string set, string file)
        {
            Set = set ?? throw new ArgumentNullException(nameof(set));
            File = file ?? throw new ArgumentNullException(nameof(file));
        }

        #endregion

        #region Functions

        /// <summary>
        /// Plays the sound for the local <see cref="Player"/>.
        /// </summary>
        public void PlayFrontend() => PlayFrontend(true);
        /// <summary>
        /// Plays the sound for the local <see cref="Player"/>.
        /// </summary>
        /// <param name="release">If the sound ID should be automatically released.</param>
        public void PlayFrontend(bool release)
        {
#if FIVEMV2
            Id = Natives.GetSoundId();
            Natives.PlaySoundFrontend(Id, File, Set, true);
#elif FIVEM
            Id = API.GetSoundId();
            API.PlaySoundFrontend(Id, File, Set, true);
#elif RAGEMP
            Id = Invoker.Invoke<int>(Natives.GetSoundId);
            Invoker.Invoke(Natives.PlaySoundFrontend, Id, File, Set, true);
#elif ALTV
            Id = Alt.Natives.GetSoundId();
            Alt.Natives.PlaySoundFrontend(Id, File, Set, true);
#elif RPH
            Id = NativeFunction.CallByHash<int>(0x430386FE9BF80B45);
            NativeFunction.CallByHash<int>(0x67C540AA08E4A6F5, Id, File, Set, true);
#elif SHVDN3 || SHVDNC
            Id = Function.Call<int>(Hash.GET_SOUND_ID);
            Function.Call(Hash.PLAY_SOUND_FRONTEND, Id, File, Set, true);
#endif

            if (release)
            {
                Release();
            }
        }
        /// <summary>
        /// Stops the audio from playing.
        /// </summary>
        public void Stop()
        {
            if (Id == -1)
            {
                return;
            }
#if FIVEMV2
            Natives.StopSound(Id);
#elif FIVEM
            API.StopSound(Id);
#elif RAGEMP
            Invoker.Invoke(Natives.StopSound, Id);
#elif RPH
            NativeFunction.CallByHash<int>(0xA3B0C41BA5CC0BB5, Id);
#elif ALTV
            Alt.Natives.StopSound(Id);
#elif SHVDN3 || SHVDNC
            Function.Call(Hash.STOP_SOUND, Id);
#endif
            Release();
        }
        /// <summary>
        /// Releases the Sound ID.
        /// </summary>
        public void Release()
        {
            if (Id == -1)
            {
                return;
            }

#if FIVEMV2
            Natives.ReleaseSoundId(Id);
#elif FIVEM
            API.ReleaseSoundId(Id);
#elif RAGEMP
            Invoker.Invoke(Natives.ReleaseSoundId, Id);
#elif RPH
            NativeFunction.CallByHash<int>(0x353FC880830B88FA, Id);
#elif ALTV
            Alt.Natives.ReleaseSoundId(Id);
#elif SHVDN3 || SHVDNC
            Function.Call(Hash.RELEASE_SOUND_ID, Id);
#endif
            Id = -1;
        }

        #endregion
    }
}
