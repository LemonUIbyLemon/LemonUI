using System;
using System.Collections.Generic;
#if FIVEM
using CitizenFX.Core.Native;
#elif RPH
using Rage.Native;
#elif (SHVDN2 || SHVDN3)
using GTA.Native;
#endif

namespace LemonUI.Scaleform
{
    /// <summary>
    /// Celebration screen that is shown at the end of a mission or job.
    /// </summary>
    public class CelebrationPart : BaseScaleform
    {
        private CelebrationLayer _layer;

        private bool _isLoading;
        private bool _loaded;
        private int _durationValue = 0;
        private int _duration = -1;
        private int _start;

        private readonly IList<Action<CelebrationPart>> _items;

        /// <summary>
        /// The duration of each item on the wall.
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// If the Scaleform should be visible or not.
        /// </summary>
        public new bool Visible
        {
            get => base.Visible;
            set
            {
                if (!value)
                {
                    Hide();
                }
                base.Visible = value;
            }
        }

        /// <summary>
        /// The Id of the wall.
        /// </summary>
        public string WallId => "ending";

        /// <summary>
        /// Creates a standard part for the Celebration
        /// </summary>
        /// <param name="layer">The type of layer for this part.</param>
        internal CelebrationPart(CelebrationLayer layer) : this(layer, new List<Action<CelebrationPart>>())
        {
        }

        /// <summary>
        /// Creates a custom CelebrationPart with a given list of items.
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="items"></param>
        internal CelebrationPart(CelebrationLayer layer, IList<Action<CelebrationPart>> items) : base(layer.Scaleform())
        {
            _layer = layer;
            _items = items;
        }

        /// <summary>
        /// Load the Scaleform and start the show the wall when it's done loading.
        /// </summary>
        public override void Update()
        {
            if (_duration == -1)
            {
                if (!_isLoading)
                {
                    LoadScaleform();
                    _isLoading = true;
                }
                else if (!_loaded && Loaded)
                {
                    _loaded = true;
                    Show();
                }
                else if (IsValueReady(_durationValue))
                {
                    _duration = GetValue<int>(_durationValue) + 700;
                }
            }
            else if (GetGameTimer() - _start > _duration)
            {
                Visible = false;
            }
        }

        /// <summary>
        /// Build the wall and make it show it on the Scaleform.
        /// </summary>
        private void Show()
        {
            CallFunction("CREATE_STAT_WALL", WallId, _layer.WallColour(), 3);
            CallFunction("SET_PAUSE_DURATION", Duration);

            foreach (var value in _items)
            {
                value.Invoke(this);
            }

            CallFunction("ADD_BACKGROUND_TO_WALL", WallId, 75, 0);

            _durationValue = CallFunctionReturn("GET_TOTAL_WALL_DURATION");
            CallFunction("SHOW_STAT_WALL", WallId);
            _start = GetGameTimer();
        }

        /// <summary>
        /// Cleanup the wall and reset the scaleform for the next use.
        /// </summary>
        private void Hide()
        {
            CallFunction("CLEANUP", WallId);
            _isLoading = false;
            _loaded = false;
            _durationValue = 0;
            _duration = -1;

            var id = Handle;
#if FIVEM
            API.SetScaleformMovieAsNoLongerNeeded(ref id);
#elif RPH
            NativeFunction.CallByHash<int>(0x1D132D614DD86811, new NativeArgument(id));
#elif (SHVDN2 || SHVDN3)
            Function.Call(Hash.SET_SCALEFORM_MOVIE_AS_NO_LONGER_NEEDED, new OutputArgument(id));
#endif
        }

        /// <summary>
        /// Get the current game timer.
        /// </summary>
        private int GetGameTimer()
        {
#if FIVEM
            return API.GetGameTimer();
#elif RPH
            return NativeFunction.CallByHash<int>(0x9CD27B0045628463);
#elif (SHVDN2 || SHVDN3)
            return Function.Call<int>(Hash.GET_GAME_TIMER);
#endif
        }
    }
}
