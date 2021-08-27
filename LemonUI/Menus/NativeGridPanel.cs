#if FIVEM
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
#elif RPH
using Rage;
using Rage.Native;
using Control = Rage.GameControl;
#elif SHVDN2
using GTA;
using GTA.Native;
#elif SHVDN3
using GTA;
using GTA.Native;
using GTA.UI;
#endif
using LemonUI.Elements;
using LemonUI.Extensions;
using System;
using System.Drawing;

namespace LemonUI.Menus
{
    /// <summary>
    /// The style of the Grid Panel.
    /// </summary>
    public enum GridStyle
    {
        /// <summary>
        /// The full grid with X and Y values.
        /// </summary>
        Full = 0,
        /// <summary>
        /// A single row on the center with the X value only.
        /// </summary>
        Row = 1,
        /// <summary>
        /// A single column on the center with the Y value only.
        /// </summary>
        Column = 2,
    }

    /// <summary>
    /// Represents the method that is called when the value on a grid is changed.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">An <see cref="ItemActivatedArgs"/> with the item information.</param>
    public delegate void GridValueChangedEventHandler(object sender, GridValueChangedArgs e);

    /// <summary>
    /// Represents the Previous and Current X and Y values when changing the position on a grid.
    /// </summary>
    public class GridValueChangedArgs
    {
        /// <summary>
        /// The values present before they were changed.
        /// </summary>
        public PointF Before { get; }
        /// <summary>
        /// The values present after they were changed.
        /// </summary>
        public PointF After { get; }

        internal GridValueChangedArgs(PointF before, PointF after)
        {
            Before = before;
            After = after;
        }
    }

    /// <summary>
    /// Represents a grid where you can select X and Y values.
    /// </summary>
    public class NativeGridPanel : NativePanel
    {
        #region Fields

        private PointF position = PointF.Empty;
        private float width = 0;

        private readonly ScaledText labelTop = new ScaledText(PointF.Empty, "Y+", 0.33f)
        {
            Alignment = Alignment.Center
        };
        private readonly ScaledText labelBottom = new ScaledText(PointF.Empty, "Y-", 0.33f)
        {
            Alignment = Alignment.Center
        };
        private readonly ScaledText labelLeft = new ScaledText(PointF.Empty, "X-", 0.33f)
        {
            Alignment = Alignment.Right
        };
        private readonly ScaledText labelRight = new ScaledText(PointF.Empty, "X+", 0.33f);
        private readonly ScaledTexture grid = new ScaledTexture("pause_menu_pages_char_mom_dad", "nose_grid")
        {
            Color = Color.FromArgb(205, 105, 105, 102)
        };
        private readonly ScaledTexture dot = new ScaledTexture("commonmenu", "common_medal")
        {
            Color = Color.FromArgb(255, 255, 255, 255)
        };
        private PointF innerPosition = PointF.Empty;
        private SizeF innerSize = SizeF.Empty;
        private GridStyle style = GridStyle.Full;
        private float x;
        private float y;

        #endregion

        #region Properties

        /// <inheritdoc/>
        public override bool Clickable => true;
        /// <summary>
        /// The X value between 0 and 1.
        /// </summary>
        public float X
        {
            get
            {
                switch (style)
                {
                    case GridStyle.Full:
                    case GridStyle.Row:
                        return x;
                    default:
                        return 0.5f;
                }
            }
            set
            {
                if (value > 1 || value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                if (style == GridStyle.Column)
                {
                    return;
                }

                PointF before = new PointF(X, Y);
                x = value;
                UpdateDot(before);
            }
        }
        /// <summary>
        /// The X value between 0 and 1.
        /// </summary>
        public float Y
        {
            get
            {
                switch (style)
                {
                    case GridStyle.Full:
                    case GridStyle.Column:
                        return y;
                    default:
                        return 0.5f;
                }
            }
            set
            {
                if (value > 1 || value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                if (style == GridStyle.Full)
                {
                    return;
                }

                PointF before = new PointF(X, Y);
                y = value;
                UpdateDot(before);
            }
        }
        /// <summary>
        /// The text label shown on the top.
        /// </summary>
        public string LabelTop
        {
            get => labelTop.Text;
            set => labelTop.Text = value;
        }
        /// <summary>
        /// The text label shown on the bottom.
        /// </summary>
        public string LabelBottom
        {
            get => labelBottom.Text;
            set => labelBottom.Text = value;
        }
        /// <summary>
        /// The text label shown on the left.
        /// </summary>
        public string LabelLeft
        {
            get => labelLeft.Text;
            set => labelLeft.Text = value;
        }
        /// <summary>
        /// The text label shown on the right.
        /// </summary>
        public string LabelRight
        {
            get => labelRight.Text;
            set => labelRight.Text = value;
        }
        /// <summary>
        /// The style of this grid.
        /// </summary>
        public GridStyle Style
        {
            get => style;
            set
            {
                if (!Enum.IsDefined(typeof(GridStyle), value))
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "The Grid style is not valid! Expected Full, Row or Column.");
                }

                style = value;
                Recalculate();
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Event triggered when X and/or Y values are changed.
        /// </summary>
        public event GridValueChangedEventHandler ValuesChanged;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new <see cref="NativeGridPanel"/>.
        /// </summary>
        public NativeGridPanel() : base()
        {
        }

        #endregion

        #region Functions

        private void Recalculate() => Recalculate(position, width);
        private void UpdateDot(PointF before, bool trigger = true)
        {
            float posX = innerSize.Width * (style == GridStyle.Full || style == GridStyle.Row ? x : 0.5f);
            float posY = innerSize.Height * (style == GridStyle.Full || style == GridStyle.Column ? y : 0.5f);

            dot.Size = new SizeF(45, 45);
            dot.Position = new PointF(innerPosition.X + posX - (dot.Size.Width * 0.5f),
                                      innerPosition.Y + posY - (dot.Size.Height * 0.5f));

            if (trigger)
            {
                ValuesChanged?.Invoke(this, new GridValueChangedArgs(before, new PointF(X, Y)));
            }
        }
        /// <inheritdoc/>
        public override void Recalculate(PointF position, float width)
        {
            this.position = position;
            this.width = width;

            const float height = 270;
            const int offsetX = 20;
            const int offsetY = 20;

            base.Recalculate(position, width);
            Background.Size = new SizeF(width, height);

            switch (style)
            {
                case GridStyle.Full:
                    grid.Position = new PointF(position.X + (width * 0.5f) - 95, position.Y + (height * 0.5f) - 94);
                    grid.Size = new SizeF(192, 192);
                    break;
                case GridStyle.Row:
                    grid.Position = new PointF(position.X + (width * 0.5f) - 95, position.Y + (height * 0.5f) - 15);
                    grid.Size = new SizeF(192, 36);
                    break;
                case GridStyle.Column:
                    grid.Position = new PointF(position.X + (width * 0.5f) - 17, position.Y + (height * 0.5f) - 94);
                    grid.Size = new SizeF(36, 192);
                    break;
            }

            labelTop.Position = new PointF(position.X + (width * 0.5f), position.Y + 10);
            labelBottom.Position = new PointF(position.X + (width * 0.5f), position.Y + height - 34);
            labelLeft.Position = new PointF(position.X + (width * 0.5f) - 102, position.Y + (height * 0.5f) - (labelLeft.LineHeight * 0.5f));
            labelRight.Position = new PointF(position.X + (width * 0.5f) + 102, position.Y + (height * 0.5f) - (labelLeft.LineHeight * 0.5f));

            innerPosition = new PointF(grid.Position.X + offsetX, grid.Position.Y + offsetY);
            innerSize = new SizeF(grid.Size.Width - (offsetX * 2), grid.Size.Height - (offsetY * 2));

            UpdateDot(PointF.Empty, false);
        }
        /// <inheritdoc/>
        public override void Process()
        {
            float previousX = X;
            float previousY = Y;

            Background.Draw();
            switch (style)
            {
                case GridStyle.Full:
                    labelTop.Draw();
                    labelBottom.Draw();
                    labelLeft.Draw();
                    labelRight.Draw();
                    grid.Draw();
                    break;
                case GridStyle.Row:
                    labelLeft.Draw();
                    labelRight.Draw();
                    grid.DrawSpecific(new PointF(0, 0.4f), new PointF(1, 0.6f));
                    break;
                case GridStyle.Column:
                    labelTop.Draw();
                    labelBottom.Draw();
                    grid.DrawSpecific(new PointF(0.4f, 0), new PointF(0.6f, 1));
                    break;
            }
            dot.Draw();

#if FIVEM
            bool usingKeyboard = API.IsInputDisabled(2);
#elif RPH
            bool usingKeyboard = NativeFunction.CallByHash<bool>(0xA571D46727E2B718, 2);
#elif SHVDN2
            bool usingKeyboard = Function.Call<bool>(Hash._0xA571D46727E2B718, 2);
#elif SHVDN3
            bool usingKeyboard = Function.Call<bool>(Hash._IS_INPUT_DISABLED, 2);
#endif
            if (usingKeyboard)
            {
                if (Screen.IsCursorInArea(grid.Position, grid.Size) && Controls.IsPressed(Control.CursorAccept))
                {
                    PointF cursor = Screen.CursorPositionRelative;
                    PointF pos = innerPosition.ToRelative();

                    PointF start = new PointF(cursor.X - pos.X, cursor.Y - pos.Y);
                    SizeF size = innerSize.ToRelative();

                    x = start.X / size.Width;
                    y = start.Y / size.Height;
                }
                else
                {
                    return;
                }
            }
            else
            {
                Controls.DisableThisFrame(Control.LookUpDown);
                Controls.DisableThisFrame(Control.LookLeftRight);
                Controls.EnableThisFrame(Control.ScriptRightAxisX);
                Controls.EnableThisFrame(Control.ScriptRightAxisY);

#if FIVEM || SHVDN2
                float rX = Game.GetControlNormal(0, Control.ScriptRightAxisX);
                float rY = Game.GetControlNormal(0, Control.ScriptRightAxisY);
                float frameTime = Game.LastFrameTime;
#elif RPH
                float rX = NativeFunction.CallByHash<float>(0xEC3C9B8D5327B563, 0, (int)Control.ScriptRightAxisX);
                float rY = NativeFunction.CallByHash<float>(0xEC3C9B8D5327B563, 0, (int)Control.ScriptRightAxisY);
                float frameTime = Game.FrameTime;
#elif SHVDN3
                float rX = Game.GetControlValueNormalized(Control.ScriptRightAxisX);
                float rY = Game.GetControlValueNormalized(Control.ScriptRightAxisY);
                float frameTime = Game.LastFrameTime;
#endif

                x += rX * frameTime;
                y += rY * frameTime;
            }

            // Make sure that the values are not under zero or over one
            if (x < 0)
            {
                x = 0;
            }
            else if (x > 1)
            {
                x = 1;
            }
            if (y < 0)
            {
                y = 0;
            }
            else if (y > 1)
            {
                y = 1;
            }

            if (previousX != x || previousY != y)
            {
                UpdateDot(new PointF(previousX, previousX));
            }
        }

        #endregion
    }
}
