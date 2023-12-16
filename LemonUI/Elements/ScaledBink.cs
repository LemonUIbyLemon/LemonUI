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

        #region Properties

        /// <summary>
        /// The name of the Bink Video file.
        /// </summary>
        public string Name
        {
            get => name;
            set
            {
                name = value ?? throw new ArgumentNullException(nameof(value));

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
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new Bink Video playback.
        /// </summary>
        /// <param name="name">The name of the bik file.</param>
        public ScaledBink(string name) : this(name, PointF.Empty, SizeF.Empty)
        {
        }
        /// <summary>
        /// Creates a new Bink Video playback.
        /// </summary>
        /// <param name="name">The name of the bik file.</param>
        /// <param name="size">The size of the video window.</param>
        public ScaledBink(string name, SizeF size) : this(name, PointF.Empty, size)
        {
        }
        /// <summary>
        /// Creates a new Bink Video playback.
        /// </summary>
        /// <param name="name">The name of the bik file.</param>
        /// <param name="pos">The position of the video window.</param>
        /// <param name="size">The size of the video window.</param>
        public ScaledBink(string name, PointF pos, SizeF size) : base(pos, size)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        #endregion

        #region Functions

        /// <summary>
        /// Draws the Bink Movie at the specified location.
        /// </summary>
        public override void Draw()
        {
#if ALTV
            Alt.Natives.PlayBinkMovie(id);
            Alt.Natives.DrawBinkMovie(id, relativePosition.X, relativePosition.Y, relativeSize.Width, relativeSize.Height, 0.0f, 255, 255, 255, 255);
#elif FIVEM
            API.PlayBinkMovie(id);
            API.DrawBinkMovie(id, relativePosition.X, relativePosition.Y, relativeSize.Width, relativeSize.Height, 0.0f, 255, 255, 255, 255);
#elif RAGEMP
            Invoker.Invoke<int>(0xE178310643033958, id);
            Invoker.Invoke<int>(0x7118E83EEB9F7238, id, relativePosition.X, relativePosition.Y, relativeSize.Width, relativeSize.Height, 0.0f, 255, 255, 255, 255);
#elif RPH
            NativeFunction.CallByHash<int>(0xE178310643033958, id);
            NativeFunction.CallByHash<int>(0x7118E83EEB9F7238, id, relativePosition.X, relativePosition.Y, relativeSize.Width, relativeSize.Height, 0.0f, 255, 255, 255, 255);
#elif SHVDN3 || SHVDNC
            Function.Call<int>(Hash.PLAY_BINK_MOVIE, id);
            Function.Call<int>(Hash.DRAW_BINK_MOVIE, id, relativePosition.X, relativePosition.Y, relativeSize.Width, relativeSize.Height, 0.0f, 255, 255, 255, 255);
#endif
        }
        /// <summary>
        /// Stops the playback of the Bink Video.
        /// </summary>
        /// <remarks>
        /// If <see cref="Draw"/> is called after this function. playback will start again.
        /// </remarks>
        public void Stop()
        {
#if ALTV
            Alt.Natives.StopBinkMovie(id);
#elif FIVEM
            API.StopBinkMovie(id);
#elif RAGEMP
            Invoker.Invoke<int>(0x63606A61DE68898A, id);
#elif RPH
            NativeFunction.CallByHash<int>(0x63606A61DE68898A, id);
#elif SHVDN3 || SHVDNC
            Function.Call<int>(Hash.STOP_BINK_MOVIE, id);
#endif
        }
        /// <inheritdoc/>
        public override void Recalculate()
        {
            base.Recalculate();
            relativePosition.X += relativeSize.Width * 0.5f;
            relativePosition.Y += relativeSize.Height * 0.5f;
        }

        #endregion
    }
}
