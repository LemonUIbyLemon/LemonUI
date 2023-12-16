#if ALTV
using AltV.Net.Client;
#elif FIVEM
using CitizenFX.Core.Native;
#elif RAGEMP
using RAGE.Game;
#elif RPH
using Rage.Native;
#elif SHVDN3 || SHVDNC
using GTA.Native;
#endif
using System;
using System.Drawing;

namespace LemonUI.Elements
{
    /// <summary>
    /// A Bink Video file.
    /// </summary>
    public class ScaledBink : BaseElement
    {
        #region Fields

        private string name = string.Empty;
        private int id = 0;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new Bink Video playback.
        /// </summary>
        /// <param name="name">The name of the bik file.</param>
        /// <param name="pos">The position of the video window.</param>
        /// <param name="size">The size of the video window.</param>
        public ScaledBink(string name, PointF pos, SizeF size) : base(pos, size)
        {
            this.name = name ?? throw new ArgumentNullException(nameof(name));

#if ALTV
            id = Alt.Natives.SetBinkMovie(name);
#elif FIVEM
            id = API.SetBinkMovie(name);
#elif RAGEMP
            id = Invoker.Invoke<int>(0xfc36643f7a64338f, name);
#elif RPH
            id = NativeFunction.CallByHash<int>(0xfc36643f7a64338f, name);
#elif SHVDN3 || SHVDNC
            id = Function.Call<int>(Hash.SET_BINK_MOVIE, name);
#endif
        }

        #endregion

        #region Functions

        /// <summary>
        /// Draws the Bink Movie at the specified location.
        /// </summary>
        public override void Draw()
        {
        }

        #endregion
    }
}
