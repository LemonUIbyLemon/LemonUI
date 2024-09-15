#if FIVEMV2
using CitizenFX.FiveM;
using CitizenFX.FiveM.GUI;
using CitizenFX.FiveM.Native;
#elif FIVEM
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
#elif ALTV
using AltV.Net.Client;
#elif RAGEMP
using RAGE.Game;
#elif RPH
using Rage;
using Rage.Native;
using Control = Rage.GameControl;
#elif SHVDN3 || SHVDNC
using GTA;
using GTA.UI;
#endif
using LemonUI.Elements;
using System;
using System.Drawing;
using LemonUI.Tools;

namespace LemonUI.Menus
{
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

                if (style == GridStyle.Row)
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
            set => labelTop.Text = value ?? throw new ArgumentNullException(nameof(value));
        }
        /// <summary>
        /// The text label shown on the bottom.
        /// </summary>
        public string LabelBottom
        {
            get => labelBottom.Text;
            set => labelBottom.Text = value ?? throw new ArgumentNullException(nameof(value));
        }
        /// <summary>
        /// The text label shown on the left.
        /// </summary>
        public string LabelLeft
        {
            get => labelLeft.Text;
            set => labelLeft.Text = value ?? throw new ArgumentNullException(nameof(value));
        }
        /// <summary>
        /// The text label shown on the right.
        /// </summary>
        public string LabelRight
        {
            get => labelRight.Text;
            set => labelRight.Text = value ?? throw new ArgumentNullException(nameof(value));
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

        #region Constructors

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

            if (!Controls.IsUsingController)
            {
                if (GameScreen.IsCursorInArea(grid.Position, grid.Size) && Controls.IsPressed(Control.CursorAccept))
                {
                    PointF cursor = GameScreen.Cursor;
                    PointF pos = innerPosition;

                    PointF start = new PointF(cursor.X - pos.X, cursor.Y - pos.Y);
                    SizeF size = innerSize;

                    x = (start.X / size.Width).ToXRelative();
                    y = (start.Y / size.Height).ToYRelative();
                }
                else
                {
                    return;
                }
            }
            else
            {
#if ALTV
                Controls.DisableThisFrame(Control.LookUd);
                Controls.DisableThisFrame(Control.LookLr);
#else
                Controls.DisableThisFrame(Control.LookUpDown);
                Controls.DisableThisFrame(Control.LookLeftRight);
#endif
                Controls.EnableThisFrame(Control.ScriptRightAxisX);
                Controls.EnableThisFrame(Control.ScriptRightAxisY);

#if FIVEMV2
                float rX = Natives.GetControlNormal(0, (int)Control.ScriptRightAxisX);
                float rY = Natives.GetControlNormal(0, (int)Control.ScriptRightAxisY);
                float frameTime = Natives.GetFrameTime();
#elif FIVEM
                float rX = Game.GetControlNormal(0, Control.ScriptRightAxisX);
                float rY = Game.GetControlNormal(0, Control.ScriptRightAxisY);
                float frameTime = Game.LastFrameTime;
#elif ALTV
                float rX = Alt.Natives.GetControlNormal(0, (int)Control.ScriptRightAxisX);
                float rY = Alt.Natives.GetControlNormal(0, (int)Control.ScriptRightAxisY);
                float frameTime = Alt.Natives.GetFrameTime();
#elif RAGEMP
                float rX = Invoker.Invoke<float>(0xEC3C9B8D5327B563, 0, (int)Control.ScriptRightAxisX);
                float rY = Invoker.Invoke<float>(0xEC3C9B8D5327B563, 0, (int)Control.ScriptRightAxisY);
                float frameTime = Invoker.Invoke<float>(Natives.GetFrameTime);
#elif RPH
                float rX = NativeFunction.CallByHash<float>(0xEC3C9B8D5327B563, 0, (int)Control.ScriptRightAxisX);
                float rY = NativeFunction.CallByHash<float>(0xEC3C9B8D5327B563, 0, (int)Control.ScriptRightAxisY);
                float frameTime = Game.FrameTime;
#elif SHVDN3 || SHVDNC
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
