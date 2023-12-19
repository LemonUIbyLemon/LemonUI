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
    public class ScaledBink : BaseElement, IDisposable
    {
        #region Fields

        private string name = string.Empty;

        #endregion

        #region Properties

        /// <summary>
        /// The ID of the Bink Video Instance.
        /// </summary>
        public int Id { get; private set; } = -1;
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
                Id = Alt.Natives.SetBinkMovie(name);
#elif FIVEM
                Id = API.SetBinkMovie(name);
#elif RAGEMP
                Id = Invoker.Invoke<int>(0xfc36643f7a64338f, name);
#elif RPH
                Id = NativeFunction.CallByHash<int>(0xfc36643f7a64338f, name);
#elif SHVDN3 || SHVDNC
                Id = Function.Call<int>(Hash.SET_BINK_MOVIE, name);
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

        #region Finalizer

        /// <summary>
        /// Finalizes an instance of the <see cref="ScaledBink"/> class.
        /// </summary>
        ~ScaledBink() => Dispose();

        #endregion

        #region Functions

        /// <summary>
        /// Draws the Bink Movie at the specified location.
        /// </summary>
        public override void Draw()
        {
            if (Id == -1)
            {
                return;
            }

#if ALTV
            Alt.Natives.PlayBinkMovie(Id);
            Alt.Natives.DrawBinkMovie(Id, relativePosition.X, relativePosition.Y, relativeSize.Width, relativeSize.Height, 0.0f, 255, 255, 255, 255);
#elif FIVEM
            API.PlayBinkMovie(Id);
            API.DrawBinkMovie(Id, relativePosition.X, relativePosition.Y, relativeSize.Width, relativeSize.Height, 0.0f, 255, 255, 255, 255);
#elif RAGEMP
            Invoker.Invoke<int>(0xE178310643033958, Id);
            Invoker.Invoke<int>(0x7118E83EEB9F7238, Id, relativePosition.X, relativePosition.Y, relativeSize.Width, relativeSize.Height, 0.0f, 255, 255, 255, 255);
#elif RPH
            NativeFunction.CallByHash<int>(0xE178310643033958, Id);
            NativeFunction.CallByHash<int>(0x7118E83EEB9F7238, Id, relativePosition.X, relativePosition.Y, relativeSize.Width, relativeSize.Height, 0.0f, 255, 255, 255, 255);
#elif SHVDN3 || SHVDNC
            Function.Call<int>(Hash.PLAY_BINK_MOVIE, Id);
            Function.Call<int>(Hash.DRAW_BINK_MOVIE, Id, relativePosition.X, relativePosition.Y, relativeSize.Width, relativeSize.Height, 0.0f, 255, 255, 255, 255);
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
            if (Id == -1)
            {
                return;
            }

#if ALTV
            Alt.Natives.StopBinkMovie(Id);
#elif FIVEM
            API.StopBinkMovie(Id);
#elif RAGEMP
            Invoker.Invoke<int>(0x63606A61DE68898A, Id);
#elif RPH
            NativeFunction.CallByHash<int>(0x63606A61DE68898A, Id);
#elif SHVDN3 || SHVDNC
            Function.Call<int>(Hash.STOP_BINK_MOVIE, Id);
#endif
        }
        /// <summary>
        /// Disposes the Bink Video ID.
        /// </summary>
        public void Dispose()
        {
            if (Id == -1)
            {
                return;
            }

#if ALTV
            Alt.Natives.ReleaseBinkMovie(Id);
#elif FIVEM
            API.ReleaseBinkMovie(Id);
#elif RAGEMP
            Invoker.Invoke<int>(0x04D950EEFA4EED8C, Id);
#elif RPH
            NativeFunction.CallByHash<int>(0x04D950EEFA4EED8C, Id);
#elif SHVDN3 || SHVDNC
            Function.Call<int>(Hash.RELEASE_BINK_MOVIE, Id);
#endif

            Id = -1;
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
