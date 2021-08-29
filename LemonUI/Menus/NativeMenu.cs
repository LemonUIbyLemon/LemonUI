#if FIVEM
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using Font = CitizenFX.Core.UI.Font;
#elif RPH
using Rage;
using Rage.Native;
using System.ComponentModel;
using Control = Rage.GameControl;
using Font = LemonUI.Elements.Font;
#elif SHVDN2
using GTA;
using GTA.Native;
using CancelEventArgs = System.ComponentModel.CancelEventArgs;
using CancelEventHandler = System.ComponentModel.CancelEventHandler;
using Font = GTA.Font;
#elif SHVDN3
using GTA;
using GTA.Native;
using GTA.UI;
using CancelEventArgs = System.ComponentModel.CancelEventArgs;
using CancelEventHandler = System.ComponentModel.CancelEventHandler;
using Font = GTA.UI.Font;
#endif
using LemonUI.Elements;
using LemonUI.Extensions;
using LemonUI.Scaleform;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace LemonUI.Menus
{
    /// <summary>
    /// Represents the method that is called when a new item is selected in the Menu.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="SelectedEventArgs"/> with the index information.</param>
    public delegate void SelectedEventHandler(object sender, SelectedEventArgs e);
    /// <summary>
    /// Represents the method that is called when an item is activated on a menu.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">An <see cref="ItemActivatedArgs"/> with the item information.</param>
    public delegate void ItemActivatedEventHandler(object sender, ItemActivatedArgs e);

    /// <summary>
    /// Represents the selection of an item in the screen.
    /// </summary>
    public class SelectedEventArgs
    {
        /// <summary>
        /// The index of the item in the full list of items.
        /// </summary>
        public int Index { get; }
        /// <summary>
        /// The index of the item in the screen.
        /// </summary>
        public int OnScreen { get; }

        /// <summary>
        /// Creates a new <see cref="SelectedEventArgs"/>.
        /// </summary>
        /// <param name="index">The index of the item in the menu.</param>
        /// <param name="screen">The index of the item based on the number of items shown on screen,</param>
        public SelectedEventArgs(int index, int screen)
        {
            Index = index;
            OnScreen = screen;
        }
    }
    /// <summary>
    /// Represents the arguments of an item activation.
    /// </summary>
    public class ItemActivatedArgs
    {
        /// <summary>
        /// The item that was just activated.
        /// </summary>
        public NativeItem Item { get; }

        internal ItemActivatedArgs(NativeItem item)
        {
            Item = item;
        }
    }

    /// <summary>
    /// The visibility setting for the Item Count of the Menu.
    /// </summary>
    public enum CountVisibility
    {
        /// <summary>
        /// The Item Count is never shown.
        /// </summary>
        Never = -1,
        /// <summary>
        /// The Item Count is shown when is not possible to show all of the items in the screen.
        /// </summary>
        Auto = 0,
        /// <summary>
        /// The Item Count is always shown.
        /// </summary>
        Always = 1
    }

    /// <summary>
    /// Menu that looks like the ones used by Rockstar.
    /// </summary>
    public class NativeMenu : IContainer<NativeItem>
    {
        #region Public Fields

        /// <summary>
        /// The default <see cref="Sound"/> played when the current <see cref="NativeItem"/> is changed or activated.
        /// </summary>
        public static readonly Sound DefaultActivatedSound = new Sound("HUD_FRONTEND_DEFAULT_SOUNDSET", "SELECT");
        /// <summary>
        /// The default <see cref="Sound"/> played when the menu is closed.
        /// </summary>
        public static readonly Sound DefaultCloseSound = new Sound("HUD_FRONTEND_DEFAULT_SOUNDSET", "BACK");
        /// <summary>
        /// The default <see cref="Sound"/> played when the user navigates Up and Down.
        /// </summary>
        public static readonly Sound DefaultUpDownSound = new Sound("HUD_FRONTEND_DEFAULT_SOUNDSET", "NAV_UP_DOWN");
        /// <summary>
        /// The default <see cref="Sound"/> played when the user navigates Left and Right on a <see cref="NativeSlidableItem"/>.
        /// </summary>
        public static readonly Sound DefaultLeftRightSound = new Sound("HUD_FRONTEND_DEFAULT_SOUNDSET", "NAV_LEFT_RIGHT");
        /// <summary>
        /// The default <see cref="Sound"/> played when the user activates a <see cref="NativeItem"/> that is disabled.
        /// </summary>
        public static readonly Sound DefaultDisabledSound = new Sound("HUD_FRONTEND_DEFAULT_SOUNDSET", "ERROR");

        #endregion

        #region Internal Fields

        internal static readonly Color colorWhiteSmoke = Color.FromArgb(255, 245, 245, 245);
        /// <summary>
        /// The search area size for the cursor rotation.
        /// </summary>
        internal static readonly SizeF searchAreaSize = new SizeF(30, 1080);
        /// <summary>
        /// The controls required by the menu with both a gamepad and mouse + keyboard.
        /// </summary>
        internal static List<Control> controlsRequired = new List<Control>
        {
            // Menu Controls
            Control.FrontendAccept,
            Control.FrontendAxisX,
            Control.FrontendAxisY,
            Control.FrontendDown,
            Control.FrontendUp,
            Control.FrontendLeft,
            Control.FrontendRight,
            Control.FrontendCancel,
            Control.FrontendSelect,
            Control.CursorScrollDown,
            Control.CursorScrollUp,
            Control.CursorX,
            Control.CursorY,
            Control.MoveUpDown,
            Control.MoveLeftRight,
            // Camera
            Control.LookBehind,
            Control.VehicleLookBehind,
            // Player
            Control.Sprint,
            Control.Jump,
            Control.Enter,
            Control.SpecialAbility,
            Control.SpecialAbilityPC,
            Control.SpecialAbilitySecondary,
            Control.VehicleSpecialAbilityFranklin,
            // Driving
            Control.VehicleExit,
            Control.VehicleAccelerate,
            Control.VehicleBrake,
            Control.VehicleMoveLeftRight,
            Control.VehicleHandbrake,
            Control.VehicleHorn,
            // Bikes
            Control.VehiclePushbikePedal,
            Control.VehiclePushbikeSprint,
            Control.VehiclePushbikeFrontBrake,
            Control.VehiclePushbikeRearBrake,
            // Flying
            Control.VehicleFlyThrottleUp,
            Control.VehicleFlyThrottleDown,
            Control.VehicleFlyYawLeft,
            Control.VehicleFlyYawRight,
            Control.VehicleFlyRollLeftRight,
            Control.VehicleFlyRollLeftOnly,
            Control.VehicleFlyRollRightOnly,
            Control.VehicleFlyPitchUpDown,
            Control.VehicleFlyPitchUpOnly,
            Control.VehicleFlyPitchDownOnly,
#if RPH
            Control.ScriptedFlyUpDown,
            Control.ScriptedFlyLeftRight,
#else
            Control.FlyUpDown,
            Control.FlyLeftRight,
#endif
            // Rockstar Editor
            Control.SaveReplayClip,
            Control.ReplayStartStopRecording,
            Control.ReplayStartStopRecordingSecondary,
            Control.ReplayRecord,
            Control.ReplaySave,
        };
        /// <summary>
        /// Controls required for the camera to work.
        /// </summary>
        internal static readonly List<Control> controlsCamera = new List<Control>
        {
            Control.LookUpDown,
            Control.LookLeftRight,
        };
        /// <summary>
        /// The controls required in a gamepad.
        /// </summary>
        internal static readonly List<Control> controlsGamepad = new List<Control>
        {
            Control.Aim,
            Control.Attack
        };

        #endregion

        #region Constant fields

        /// <summary>
        /// The height of the menu subtitle background.
        /// </summary>
        internal const float subtitleHeight = 38;
        /// <summary>
        /// The height of one of the items in the screen.
        /// </summary>
        internal const float itemHeight = 37.4f;
        /// <summary>
        /// The height difference between the description and the end of the items.
        /// </summary>
        internal const float heightDiffDescImg = 4;
        /// <summary>
        /// The height difference between the background and text of the description.
        /// </summary>
        internal const float heightDiffDescTxt = 3;
        /// <summary>
        /// The X position of the description text.
        /// </summary>
        internal const float posXDescTxt = 6;
        /// <summary>
        /// The offset to the X value of the item title.
        /// </summary>
        internal const float itemOffsetX = 6;
        /// <summary>
        /// The offset to the Y value of the item title.
        /// </summary>
        internal const float itemOffsetY = 3;

        #endregion

        #region Private Fields

        /// <summary>
        /// A list of GTA V Controls.
        /// </summary>
        private static readonly Control[] controls = (Control[])Enum.GetValues(typeof(Control));
        /// <summary>
        /// If the menu has just been opened and we should check the controls.
        /// </summary>
        private bool justOpenedControlChecks = false;
        /// <summary>
        /// The list of visible items on the screen.
        /// </summary>
        private List<NativeItem> visibleItems = new List<NativeItem>();
        /// <summary>
        /// The subtitle of the menu, without any changes.
        /// </summary>
        private string subtitle = "";
        /// <summary>
        /// If the menu is visible or not.
        /// </summary>
        private bool visible = false;
        /// <summary>
        /// If this menu should be aware of the Safe Zone when doing calculations.
        /// </summary>
        private bool safeZoneAware = true;
        /// <summary>
        /// The index of the selected item in the menu.
        /// </summary>
        private int index = -1;
        /// <summary>
        /// The width of the menu itself.
        /// </summary>
        private float width = 433;
        /// <summary>
        /// The alignment of the menu.
        /// </summary>
        private Alignment alignment = Alignment.Left;
        /// <summary>
        /// The offset of the menu.
        /// </summary>
        private PointF offset = PointF.Empty;
        /// <summary>
        /// The banner of the menu.
        /// </summary>
        private I2Dimensional bannerImage = null;
        /// <summary>
        /// The background of the drawable text.
        /// </summary>
        private readonly ScaledRectangle subtitleImage = new ScaledRectangle(PointF.Empty, SizeF.Empty)
        {
            Color = Color.FromArgb(255, 0, 0, 0)
        };
        /// <summary>
        /// The text of the subtitle.
        /// </summary>
        private readonly ScaledText subtitleText = new ScaledText(PointF.Empty, "", 0.345f, Font.ChaletLondon)
        {
            Color = colorWhiteSmoke
        };
        /// <summary>
        /// The text that shows the current total and index of items.
        /// </summary>
        private readonly ScaledText countText = new ScaledText(PointF.Empty, "", 0.345f, Font.ChaletLondon)
        {
            Color = colorWhiteSmoke
        };
        /// <summary>
        /// The image on the background of the menu.
        /// </summary>
        private readonly ScaledTexture backgroundImage = new ScaledTexture("commonmenu", "gradient_bgd");
        /// <summary>
        /// The rectangle that shows the currently selected item.
        /// </summary>
        private readonly ScaledTexture selectedRect = new ScaledTexture("commonmenu", "gradient_nav");
        /// <summary>
        /// The rectangle with the description text.
        /// </summary>
        private readonly ScaledTexture descriptionRect = new ScaledTexture("commonmenu", "gradient_bgd");
        /// <summary>
        /// The text with the description text.
        /// </summary>
        private readonly ScaledText descriptionText = new ScaledText(PointF.Empty, "", 0.351f);
        /// <summary>
        /// The maximum allowed number of items in the menu at once.
        /// </summary>
        private int maxItems = 10;
        /// <summary>
        /// The first item in the menu.
        /// </summary>
        private int firstItem = 0;
        /// <summary>
        /// The search area on the right side of the screen.
        /// </summary>
        private PointF searchAreaRight = PointF.Empty;
        /// <summary>
        /// The time sice the player has been pressing the Up button.
        /// </summary>
        private long upSince = -1;
        /// <summary>
        /// The time sice the player has been pressing the Down button.
        /// </summary>
        private long downSince = -1;

        #endregion

        #region Public Properties

        /// <summary>
        /// If the menu is visible on the screen.
        /// </summary>
        public bool Visible
        {
            get => visible;
            set
            {
                if (visible == value)
                {
                    return;
                }

                if (value)
                {
                    CancelEventArgs args = new CancelEventArgs();
                    Opening?.Invoke(this, args);
                    if (args.Cancel)
                    {
                        return;
                    }

                    if (ResetCursorWhenOpened)
                    {
                        ResetCursor();
                    }

                    justOpenedControlChecks = true;
                    visible = true;

                    SoundOpened?.PlayFrontend();

                    Shown?.Invoke(this, EventArgs.Empty);
                    TriggerSelectedItem();
                }
                else
                {
                    CancelEventArgs args = new CancelEventArgs();
                    Closing?.Invoke(this, args);
                    if (args.Cancel)
                    {
                        return;
                    }

                    visible = false;
                    Closed?.Invoke(this, EventArgs.Empty);
                    SoundClose?.PlayFrontend();
                }
            }
        }
        /// <summary>
        /// The title of the menu.
        /// </summary>
        public ScaledText Title { get; set; }
        /// <summary>
        /// The font of title menu text.
        /// </summary>
        public Font TitleFont
        {
            get => Title.Font;
            set => Title.Font = value;
        }
        /// <summary>
        /// The font of subtitle text.
        /// </summary>
        public Font SubtitleFont
        {
            get => subtitleText.Font;
            set => subtitleText.Font = value;
        }
        /// <summary>
        /// The font of description text.
        /// </summary>
        public Font DescriptionFont
        {
            get => descriptionText.Font;
            set => descriptionText.Font = value;
        }
        /// <summary>
        /// The font of item count text.
        /// </summary>
        public Font ItemCountFont
        {
            get => countText.Font;
            set => countText.Font = value;
        }
        /// <summary>
        /// The banner shown at the top of the menu.
        /// </summary>
        public I2Dimensional Banner
        {
            get => bannerImage;
            set
            {
                bannerImage = value;
                Recalculate();
            }
        }
        /// <summary>
        /// The offset of the menu position.
        /// </summary>
        public PointF Offset
        {
            get => offset;
            set
            {
                offset = value;
                Recalculate();
            }
        }
        /// <summary>
        /// Returns the currently selected item.
        /// </summary>
        public NativeItem SelectedItem
        {
            get
            {
                // If there are no items or is over the maximum, return null
                int currentIndex = SelectedIndex;
                if (Items.Count == 0 || currentIndex >= Items.Count || currentIndex == -1)
                {
                    return null;
                }
                // Otherwise, return the correct item from the list
                return Items[currentIndex];
            }
            set
            {
                // If the item is not part of the menu, raise an exception
                if (!Items.Contains(value))
                {
                    throw new InvalidOperationException("Item is not part of the Menu.");
                }
                // Otherwise, set the correct index
                SelectedIndex = Items.IndexOf(value);
            }
        }
        /// <summary>
        /// The current index of the menu.
        /// </summary>
        public int SelectedIndex
        {
            get
            {
                // If there are no items or is over the maximum, return -1
                if (Items.Count == 0 || index >= Items.Count)
                {
                    return -1;
                }
                // Otherwise, return the real index
                return index;
            }
            set
            {
                // If the list of items is empty, don't allow the user to set the index
                if (Items == null || Items.Count == 0)
                {
                    throw new InvalidOperationException("There are no items in this menu.");
                }
                // If the value is over or equal than the number of items, raise an exception
                else if (value >= Items.Count)
                {
                    throw new InvalidOperationException($"The index is over {Items.Count - 1}.");
                }
                // If the value is under zero, raise an exception
                else if (value < 0)
                {
                    throw new InvalidOperationException($"The index is under zero.");
                }

                // Calculate the bounds of the menu
                int lower = firstItem;
                int upper = firstItem + maxItems;

                // Time to set the first item based on the total number of items
                // If the item is between the allowed values, do nothing because we are on the correct first item
                if (value >= lower && value < upper - 1)
                {
                }
                // If the upper bound + 1 equals the new index, increase it by one
                else if (upper == value)
                {
                    firstItem += 1;
                }
                // If the first item minus one equals the value, decrease it by one
                else if (lower - 1 == value)
                {
                    firstItem -= 1;
                }
                // Otherwise, set it somewhere
                else
                {
                    // If the value is under the max items, set it to zero
                    if (value < maxItems)
                    {
                        firstItem = 0;
                    }
                    // Otherwise, set it at the bottom
                    else
                    {
                        firstItem = value - maxItems + 1;
                    }
                }

                // Save the index
                index = value;

                // And update the items
                UpdateItemList();
                UpdateItems();

                // If the menu is visible, play the up and down sound
                if (Visible)
                {
                    SoundUpDown?.PlayFrontend();
                }

                // If an item was selected
                if (SelectedItem != null)
                {
                    // And trigger it
                    TriggerSelectedItem();
                }
            }
        }
        /// <summary>
        /// The width of the menu.
        /// </summary>
        public float Width
        {
            get => width;
            set
            {
                width = value;
                Recalculate();
            }
        }
        /// <summary>
        /// The alignment of the menu.
        /// </summary>
        public Alignment Alignment
        {
            get => alignment;
            set
            {
                if (alignment == value)
                {
                    return;
                }

                if (!Enum.IsDefined(typeof(Alignment), value) || value == Alignment.Center)
                {
                    throw new ArgumentException("The Menu can only be aligned to the Left and Right.", nameof(value));
                }

                alignment = value;
                Recalculate();
            }
        }
        /// <summary>
        /// The subtitle of the menu.
        /// </summary>
        public string Subtitle
        {
            get => subtitle;
            set
            {
                subtitle = value;
                subtitleText.Text = value.ToUpperInvariant();
            }
        }
        /// <summary>
        /// The description used when this menu is used as a submenu.
        /// </summary>
        public string Description { get; set; } = "";
        /// <summary>
        /// If the mouse should be used for navigating the menu.
        /// </summary>
        public bool UseMouse { get; set; } = true;
        /// <summary>
        /// If the menu should be closed when the user clicks out of bounds (aka anywhere else other than the items).
        /// </summary>
        public bool CloseOnInvalidClick { get; set; } = true;
        /// <summary>
        /// If the camera should be rotated when the cursor is on the left and right corners of the screen.
        /// </summary>
        public bool RotateCamera { get; set; } = false;
        /// <summary>
        /// The items that this menu contain.
        /// </summary>
        public List<NativeItem> Items { get; } = new List<NativeItem>();
        /// <summary>
        /// Text shown when there are no items in the menu.
        /// </summary>
        public string NoItemsText { get; set; } = "There are no items available";
        /// <summary>
        /// If the cursor should be reset when the menu is opened.
        /// </summary>
        public bool ResetCursorWhenOpened { get; set; } = true;
        /// <summary>
        /// The maximum allowed number of items in the menu at once.
        /// </summary>
        public int MaxItems
        {
            get => maxItems;
            set
            {
                // If the number is under one, raise an exception
                if (value < 1)
                {
                    throw new InvalidOperationException("The maximum numbers on the screen can't be under 1.");
                }
                // Otherwise, save it
                maxItems = value;
            }
        }
        /// <summary>
        /// If this menu should be aware of the Safe Zone when doing calculations.
        /// </summary>
        public bool SafeZoneAware
        {
            get => safeZoneAware;
            set
            {
                safeZoneAware = value;
                Recalculate();
            }
        }
        /// <summary>
        /// If the count of items should be shown on the right of the subtitle.
        /// </summary>
        public CountVisibility ItemCount { get; set; }
        /// <summary>
        /// The instructional buttons shown in the bottom right.
        /// </summary>
        public InstructionalButtons Buttons { get; } = new InstructionalButtons(new InstructionalButton("Select", (Control)176 /*PhoneSelect*/), new InstructionalButton("Back", (Control)177 /*PhoneCancel*/))
        {
            Visible = true
        };
        /// <summary>
        /// The parent menu of this menu.
        /// </summary>
        public NativeMenu Parent { get; set; } = null;
        /// <summary>
        /// If the menu accepts user input for navigation.
        /// </summary>
        public bool AcceptsInput { get; set; } = true;
        /// <summary>
        /// If the conflictive controls should be disabled while the menu is open.
        /// </summary>
        public bool DisableControls { get; set; } = true;
        /// <summary>
        /// The time between item changes when holding left, right, up or down.
        /// </summary>
        /// <remarks>
        /// This property can be set to zero to completely disable it.
        /// </remarks>
        public int HeldTime { get; set; } = 166;
        /// <summary>
        /// The controls that are required for some menu operations.
        /// </summary>
        /// <remarks>
        /// Add controls to this list when you want to detect them as pressed while the menu is open.
        /// </remarks>
        public List<Control> RequiredControls { get; } = new List<Control>();
        /// <summary>
        /// The <see cref="Sound"/> played when the menu is opened.
        /// </summary>
        public Sound SoundOpened { get; set; } = DefaultActivatedSound;
        /// <summary>
        /// The <see cref="Sound"/> played when a <see cref="NativeItem"/> is activated.
        /// </summary>
        public Sound SoundActivated { get; set; } = DefaultActivatedSound;
        /// <summary>
        /// The <see cref="Sound"/> played when the menu is closed.
        /// </summary>
        public Sound SoundClose { get; set; } = DefaultCloseSound;
        /// <summary>
        /// The <see cref="Sound"/> played when the user navigates Up or Down the menu.
        /// </summary>
        public Sound SoundUpDown { get; set; } = DefaultUpDownSound;
        /// <summary>
        /// The <see cref="Sound"/> played when the user navigates Left and Right on a <see cref="NativeSlidableItem"/>.
        /// </summary>
        public Sound SoundLeftRight { get; set; } = DefaultLeftRightSound;
        /// <summary>
        /// The <see cref="Sound"/> played when the user activates a <see cref="NativeItem"/> that is disabled.
        /// </summary>
        public Sound SoundDisabled { get; set; } = DefaultDisabledSound;

        #endregion

        #region Events

        /// <summary>
        /// Event triggered when the menu is being opened.
        /// </summary>
        public event CancelEventHandler Opening;
        /// <summary>
        /// Event triggered when the menu is opened and shown to the user.
        /// </summary>
        public event EventHandler Shown;
        /// <summary>
        /// Event triggered when the menu starts closing.
        /// </summary>
        public event CancelEventHandler Closing;
        /// <summary>
        /// Event triggered when the menu finishes closing.
        /// </summary>
        public event EventHandler Closed;
        /// <summary>
        /// Event triggered when the index has been changed.
        /// </summary>
        public event SelectedEventHandler SelectedIndexChanged;
        /// <summary>
        /// Event triggered when an item in the menu is activated.
        /// </summary>
        public event ItemActivatedEventHandler ItemActivated;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new menu with the specified title.
        /// </summary>
        /// <param name="title">The title of the menu.</param>
        public NativeMenu(string title) : this(title, "", "")
        {
        }

        /// <summary>
        /// Creates a new menu with the specified title and subtitle.
        /// </summary>
        /// <param name="title">The title of the menu.</param>
        /// <param name="subtitle">The subtitle of this menu.</param>
        public NativeMenu(string title, string subtitle) : this(title, subtitle, "")
        {
        }

        /// <summary>
        /// Creates a new menu with the specified title, subtitle and description.
        /// </summary>
        /// <param name="title">The title of the menu.</param>
        /// <param name="subtitle">The subtitle of this menu.</param>
        /// <param name="description">The description used for submenus.</param>
        public NativeMenu(string title, string subtitle, string description) : this(title, subtitle, description, new ScaledTexture(PointF.Empty, new SizeF(0, 108), "commonmenu", "interaction_bgd"))
        {
        }

        /// <summary>
        /// Creates a new menu with the specified title, subtitle, description and banner.
        /// </summary>
        /// <param name="title">The title of the menu.</param>
        /// <param name="subtitle">The subtitle of this menu.</param>
        /// <param name="description">The description used for submenus.</param>
        /// <param name="banner">The drawable to use as the banner.</param>
        public NativeMenu(string title, string subtitle, string description, I2Dimensional banner)
        {
            this.subtitle = subtitle;
            Description = description;
            bannerImage = banner;
            Title = new ScaledText(PointF.Empty, title, 1.02f, Font.HouseScript)
            {
                Alignment = Alignment.Center
            };
            subtitleText.Text = subtitle.ToUpperInvariant();
            Recalculate();
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// Updates the list of visible items on the screen.
        /// </summary>
        private void UpdateItemList()
        {
            // Create a new list for the items
            List<NativeItem> list = new List<NativeItem>();

            // Iterate over the number of items while staying under the maximum
            for (int i = 0; i < MaxItems; i++)
            {
                // Calculate the start of our items
                int start = firstItem + i;

                // If the number of items is over the ones in the list, something went wrong
                // TODO: Decide what to do in this case (exception? silently ignore?)
                if (start >= Items.Count)
                {
                    break;
                }

                // Otherwise, return it as part of the iterator or add it to the list
                list.Add(Items[start]);
            }

            // Finally, replace the list of items
            visibleItems = list;
        }
        /// <summary>
        /// Triggers the Selected event for the current item.
        /// </summary>
        private void TriggerSelectedItem()
        {
            // Get the currently selected item
            NativeItem item = SelectedItem;

            // If is null or the menu is closed, return
            if (item == null || !Visible)
            {
                return;
            }

            // Update the panel
            RecalculatePanel();
            // And trigger the selected event for this menu
            SelectedEventArgs args = new SelectedEventArgs(index, index - firstItem);
            SelectedItem.OnSelected(this, args);
            SelectedIndexChanged?.Invoke(this, args);
        }
        /// <summary>
        /// Recalculates the Position and Size of the Panel of the Selected Item.
        /// </summary>
        private void RecalculatePanel()
        {
            // If the selected item has a panel
            if (SelectedItem?.Panel != null)
            {
                // Save the Y value of the description
                float y = descriptionRect.Position.Y;
                // If it has text, show it after the description instead of taking it's place
                if (!string.IsNullOrWhiteSpace(descriptionText.Text))
                {
                    y += descriptionRect.Size.Height + 10;
                }
                // Finally, set the position of the panel
                SelectedItem.Panel.Recalculate(new PointF(descriptionRect.Position.X, y), Width);
            }
        }
        /// <summary>
        /// Resets the current position of the cursor.
        /// </summary>
        private void ResetCursor()
        {
            const float extraX = 35;
            const float extraY = 325;

            // Get the correct desired position of the cursor as relative
            PointF pos = PointF.Empty;
            if (SafeZoneAware)
            {
                Screen.SetElementAlignment(Alignment, GFXAlignment.Top);
                float x = 0;
                switch (Alignment)
                {
                    case Alignment.Left:
                        x = Offset.X + Width + extraX;
                        break;
                    case Alignment.Right:
                        x = Offset.X - Width - extraX;
                        break;
                }
                pos = Screen.GetRealPosition(x, Offset.Y + extraY).ToRelative();
                Screen.ResetElementAlignment();
            }
            else
            {
                float x = 0;
                switch (Alignment)
                {
                    case Alignment.Left:
                        x = Offset.X + Width + extraX;
                        break;
                    case Alignment.Right:
                        x = 1f.ToXAbsolute() - Offset.X - Width - extraX;
                        break;
                }
                pos = new PointF(x, Offset.Y + extraY).ToRelative();
            }
            // And set the position of the cursor
#if FIVEM
            API.SetCursorLocation(pos.X, pos.Y);
#elif SHVDN2
            Function.Call(Hash._0xFC695459D4D0E219, pos.X, pos.Y);
#elif SHVDN3
            Function.Call(Hash._SET_CURSOR_LOCATION, pos.X, pos.Y);
#endif
        }
        /// <summary>
        /// Updates the positions of the items.
        /// </summary>
        private void UpdateItems()
        {
            // Store the current values of X and Y
            PointF pos;
            if (SafeZoneAware)
            {
                Screen.SetElementAlignment(Alignment, GFXAlignment.Top);
                float x = 0;
                switch (Alignment)
                {
                    case Alignment.Left:
                        x = Offset.X;
                        break;
                    case Alignment.Right:
                        x = Offset.X - Width;
                        break;
                }
                pos = Screen.GetRealPosition(x, Offset.Y);
                Screen.ResetElementAlignment();
            }
            else
            {
                float x = 0;
                switch (Alignment)
                {
                    case Alignment.Left:
                        x = Offset.X;
                        break;
                    case Alignment.Right:
                        x = 1f.ToXAbsolute() - Width - Offset.X;
                        break;
                }
                pos = new PointF(x, Offset.Y);
            }

            // Add the heights of the banner and subtitle (if there are any)
            if (bannerImage != null)
            {
                pos.Y += bannerImage.Size.Height;
            }
            if (!string.IsNullOrWhiteSpace(subtitleText.Text))
            {
                countText.Text = $"{SelectedIndex + 1} / {Items.Count}";
                countText.Position = new PointF(pos.X + width - countText.Width - 6, pos.Y + 4.2f);
                pos.Y += subtitleImage.Size.Height;
            }

            // Set the position and size of the background image
            backgroundImage.literalPosition = new PointF(pos.X, pos.Y);
            backgroundImage.literalSize = new SizeF(width, itemHeight * visibleItems.Count);
            backgroundImage.Recalculate();
            // Set the position of the rectangle that marks the current item
            selectedRect.Position = new PointF(pos.X, pos.Y + ((index - firstItem) * itemHeight));
            // And then do the description background and text
            descriptionText.Text = Items.Count == 0 || SelectedIndex == -1 ? NoItemsText : SelectedItem.Description;
            float description = pos.Y + ((Items.Count > maxItems ? maxItems : Items.Count) * itemHeight) + heightDiffDescImg;
            int lineCount = descriptionText.LineCount;
            descriptionRect.Size = new SizeF(width, (lineCount * (descriptionText.LineHeight + 5)) + (lineCount - 1) + 10);
            descriptionRect.Position = new PointF(pos.X, description);
            descriptionText.Position = new PointF(pos.X + posXDescTxt, description + heightDiffDescTxt);

            // Save the size of the items
            SizeF size = new SizeF(width, itemHeight);
            // And start recalculating them
            int i = 0;
            foreach (NativeItem item in visibleItems)
            {
                // Tell the item to recalculate the position
                item.Recalculate(new PointF(pos.X, pos.Y), size, item == SelectedItem);
                // And increase the index of the item and Y position
                i++;
                pos.Y += itemHeight;
            }

            // Finally, recalculate the panel of the selected item
            RecalculatePanel();
        }
        /// <summary>
        /// Processes the button presses.
        /// </summary>
        private void ProcessControls()
        {
            // If the user wants to disable the controls, do so but only the ones required
            if (DisableControls)
            {
                foreach (Control control in controls)
                {
                    // If the control is required by the menu
                    if (controlsRequired.Contains(control))
                    {
                        continue;
                    }
                    // If the player is using a controller and is required on gamepads
                    if (Controls.IsUsingController && controlsGamepad.Contains(control))
                    {
                        continue;
                    }
                    // If the player is usinng a controller or mouse usage is disabled and is a camera control
                    if ((Controls.IsUsingController || !UseMouse) && controlsCamera.Contains(control))
                    {
                        continue;
                    }
                    // If the control is required by the mod developer
                    if (RequiredControls.Contains(control))
                    {
                        continue;
                    }

                    Controls.DisableThisFrame(control);
                }
            }

            // If the menu is just opened, don't start processing controls until the player has stopped pressing the accept or cancel buttons
            if (justOpenedControlChecks)
            {
                if (Controls.IsPressed((Control)177 /*PhoneCancel*/) || Controls.IsPressed(Control.FrontendPause) ||
                    Controls.IsPressed(Control.FrontendAccept) || Controls.IsPressed((Control)176 /*PhoneSelect*/) ||
                    Controls.IsPressed(Control.CursorAccept))
                {
                    return;
                }
                justOpenedControlChecks = false;
            }

            // If the controls are disabled, the menu has just been opened or the text input field is active, return
#if FIVEM
            bool isKeyboardActive = API.UpdateOnscreenKeyboard() == 0;
#elif RPH
            bool isKeyboardActive = NativeFunction.CallByHash<int>(0x0CF2B696BBF945AE) == 0;
#elif SHVDN2 || SHVDN3
            bool isKeyboardActive = Function.Call<int>(Hash.UPDATE_ONSCREEN_KEYBOARD) == 0;
#endif
            if (!AcceptsInput || isKeyboardActive)
            {
                return;
            }

            // Check if the controls necessary were pressed
            bool backPressed = Controls.IsJustPressed((Control)177 /*PhoneCancel*/) || Controls.IsJustPressed(Control.FrontendPause);
            bool upPressed = Controls.IsJustPressed((Control)172 /*PhoneUp*/) || Controls.IsJustPressed(Control.CursorScrollUp);
            bool downPressed = Controls.IsJustPressed((Control)173 /*PhoneDown*/) || Controls.IsJustPressed(Control.CursorScrollDown);
            bool selectPressed = Controls.IsJustPressed(Control.FrontendAccept) || Controls.IsJustPressed((Control)176 /*PhoneSelect*/);
            bool clickSelected = Controls.IsJustPressed(Control.CursorAccept);
            bool leftPressed = Controls.IsJustPressed((Control)174 /*PhoneLeft*/);
            bool rightPressed = Controls.IsJustPressed((Control)175 /*PhoneRight*/);

            bool upHeld = Controls.IsPressed((Control)172 /*PhoneUp*/) || Controls.IsPressed(Control.CursorScrollUp);
            bool downHeld = Controls.IsPressed((Control)173 /*PhoneDown*/) || Controls.IsPressed(Control.CursorScrollDown);

            // If the player pressed the back button, go back or close the menu
            if (backPressed)
            {
                Back();
                return;
            }

            // If the player pressed up, go to the previous item
            if ((upPressed && !downPressed) || (HeldTime > 0 && upSince != -1 && !upPressed && upHeld && upSince + HeldTime < Game.GameTime))
            {
                upSince = Game.GameTime;
                Previous();
                return;
            }
            // If he pressed down, go to the next item
            if ((downPressed && !upPressed) || (HeldTime > 0 && downSince != -1 && !downPressed && downHeld && downSince + HeldTime < Game.GameTime))
            {
                downSince = Game.GameTime;
                Next();
                return;
            }

            // Get the currently selected item for later use (for the sake of performance)
            NativeItem selectedItem = SelectedItem;

            // If the mouse controls are enabled and the user is not using a controller
            if (UseMouse && !Controls.IsUsingController)
            {
                // Enable the mouse cursor
#if (FIVEM || SHVDN2 || SHVDN3)
                Screen.ShowCursorThisFrame();
#elif RPH
                NativeFunction.CallByHash<int>(0xAAE7CE1D63167423);
#endif

                // If the camera should be rotated when the cursor is on the left and right sections of the screen, do so
                if (RotateCamera)
                {
                    if (Screen.IsCursorInArea(PointF.Empty, searchAreaSize))
                    {
#if (FIVEM || SHVDN2 || SHVDN3)
                        GameplayCamera.RelativeHeading += 5;
#elif RPH
                        Camera.RenderingCamera.Heading += 5;
#endif
                    }
                    else if (Screen.IsCursorInArea(searchAreaRight, searchAreaSize))
                    {
#if (FIVEM || SHVDN2 || SHVDN3)
                        GameplayCamera.RelativeHeading -= 5;
#elif RPH
                        Camera.RenderingCamera.Heading -= 5;
#endif
                    }
                }

                // If the player pressed the click button
                if (clickSelected)
                {
                    // Iterate over the items on the screen
                    foreach (NativeItem item in visibleItems)
                    {
                        // If the item is selected and slidable
                        if (item == selectedItem && item is NativeSlidableItem slidable)
                        {
                            // If the right arrow was pressed, go to the right
                            if (Screen.IsCursorInArea(slidable.RightArrow.Position, slidable.RightArrow.Size))
                            {
                                if (item.Enabled)
                                {
                                    slidable.GoRight();
                                    SoundLeftRight?.PlayFrontend();
                                }
                                else
                                {
                                    SoundDisabled?.PlayFrontend();
                                }
                                return;
                            }
                            // If the user pressed the left arrow, go to the right
                            else if (Screen.IsCursorInArea(slidable.LeftArrow.Position, slidable.LeftArrow.Size))
                            {
                                if (item.Enabled)
                                {
                                    slidable.GoLeft();
                                    SoundLeftRight?.PlayFrontend();
                                }
                                else
                                {
                                    SoundDisabled?.PlayFrontend();
                                }
                                return;
                            }
                        }

                        // If the cursor is inside of the selection rectangle
                        if (Screen.IsCursorInArea(item.title.Position.X - itemOffsetX, item.title.Position.Y - itemOffsetY, Width, itemHeight))
                        {
                            // If the item is selected, activate it
                            if (item == selectedItem)
                            {
                                if (item.Enabled)
                                {
                                    ItemActivated?.Invoke(this, new ItemActivatedArgs(selectedItem));
                                    item.OnActivated(this);
                                    SoundActivated?.PlayFrontend();
                                    if (item is NativeCheckboxItem checkboxItem)
                                    {
                                        checkboxItem.UpdateTexture(true);
                                    }
                                }
                                else
                                {
                                    SoundDisabled?.PlayFrontend();
                                }
                            }
                            // If is is not, set it as the selected item
                            else
                            {
                                SelectedItem = item;
                            }

                            // We found the item that was clicked, stop the function
                            return;
                        }
                    }

                    // If we got here, the user clicked outside of the selected item area
                    // So close the menu if required (same behavior of the interaction menu)
                    if (CloseOnInvalidClick)
                    {
                        if (selectedItem.Panel != null && selectedItem.Panel.Clickable && Screen.IsCursorInArea(selectedItem.Panel.Background.Position, SelectedItem.Panel.Background.Size))
                        {
                            return;
                        }
                        Visible = false;
                    }
                    return;
                }
            }

            // If the player pressed the left or right button, trigger the event and sound
            if (SelectedItem is NativeSlidableItem slidableItem)
            {
                if (leftPressed)
                {
                    if (SelectedItem.Enabled)
                    {
                        slidableItem.GoLeft();
                        SoundLeftRight?.PlayFrontend();
                    }
                    else
                    {
                        SoundDisabled?.PlayFrontend();
                    }
                    return;
                }
                if (rightPressed)
                {
                    if (SelectedItem.Enabled)
                    {
                        slidableItem.GoRight();
                        SoundLeftRight?.PlayFrontend();
                    }
                    else
                    {
                        SoundDisabled?.PlayFrontend();
                    }
                    return;
                }
            }

            // If the player selected an item, activate it
            if (selectPressed)
            {
                if (SelectedItem != null && SelectedItem.Enabled)
                {
                    ItemActivated?.Invoke(this, new ItemActivatedArgs(selectedItem));
                    SelectedItem.OnActivated(this);
                    SoundActivated?.PlayFrontend();
                    if (SelectedItem is NativeCheckboxItem check)
                    {
                        check.UpdateTexture(true);
                    }
                    return;
                }
                else
                {
                    SoundDisabled?.PlayFrontend();
                    return;
                }
            }
        }
        /// <summary>
        /// Draws the UI Elements.
        /// </summary>
        private void Draw()
        {
            NativeItem selected = SelectedItem;

            // Let's start with the basics
            // Draw the banner image and text
            if (bannerImage != null)
            {
                bannerImage.Draw();
                Title?.Draw();
            }
            // And then the subtitle with text and item count
            if (subtitleImage != null)
            {
                subtitleImage.Draw();
                subtitleText?.Draw();
                if (ItemCount == CountVisibility.Always || (ItemCount == CountVisibility.Auto && Items.Count > MaxItems))
                {
                    countText.Draw();
                }
            }
            // If there is some description text, draw the text and background
            if (!string.IsNullOrWhiteSpace(descriptionText.Text))
            {
                descriptionRect.Draw();
                descriptionText.Draw();
            }

            // Time for the items!
            // If there are none, return and do nothing
            if (Items.Count == 0)
            {
                return;
            }

            // Otherwise, start with the background
            backgroundImage?.Draw();
            // Then, draw all of the items with the exception of the one selected
            foreach (NativeItem item in visibleItems)
            {
                if (item != selected)
                {
                    item.Draw();
                }
            }
            // Continue with the white selection rectangle
            selectedRect?.Draw();
            // And finish with the selected item on top (if any)
            if (selected != null)
            {
                selected.Draw();
                if (selected.Panel != null && selected.Panel.Visible)
                {
                    selected.Panel.Process();
                }
            }
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// Adds an item at the end of the menu.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void Add(NativeItem item) => Add(Items.Count, item);
        /// <summary>
        /// Adds an item at the specified position.
        /// </summary>
        /// <param name="position">The position of the item.</param>
        /// <param name="item">The item to add.</param>
        public void Add(int position, NativeItem item)
        {
            // If the item is already on the list, raise an exception
            if (Items.Contains(item))
            {
                throw new InvalidOperationException("The item is already part of the menu.");
            }
            // If the item position is under zero or over the maximum, raise an exception
            if (position < 0 || position > Items.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(position), "The index under Zero or is over the Item Count.");
            }
            // Also raise an exception if is null
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            // Otherwise, just add it
            Items.Insert(position, item);
            // Set the correct index if this is the only item
            if (Items.Count != 0 && SelectedIndex == -1)
            {
                SelectedIndex = 0;
            }
            else
            {
                UpdateItemList();
            }
            // And update the items
            UpdateItems();
        }
        /// <summary>
        /// Adds a specific menu as a submenu with an item.
        /// </summary>
        /// <param name="menu">The menu to add.</param>
        /// <returns>The item that points to the submenu.</returns>
        public NativeSubmenuItem AddSubMenu(NativeMenu menu)
        {
            // If the menu is null, raise an exception
            if (menu == null)
            {
                throw new ArgumentNullException(nameof(menu));
            }

            // Create a new menu item, add it and return it
            NativeSubmenuItem item = new NativeSubmenuItem(menu, this);
            Add(item);
            return item;
        }
        /// <summary>
        /// Adds a specific menu as a submenu with an item and endlabel string.
        /// </summary>
        /// <param name="menu">The menu to add.</param>
        /// <param name="endlabel">The alternative title of the item, shown on the right.</param>
        /// <returns>The item that points to the submenu.</returns>
        public NativeSubmenuItem AddSubMenu(NativeMenu menu, String endlabel)
        {
            NativeSubmenuItem item = AddSubMenu(menu);
            item.AltTitle = endlabel;
            return item;
        }
        /// <summary>
        /// Removes an item from the menu.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        public void Remove(NativeItem item)
        {
            // If the item is not in the menu, ignore it
            if (!Items.Contains(item))
            {
                return;
            }

            // Remove it if there
            // If not, ignore it
            Items.Remove(item);
            // If the index is higher or equal than the max number of items
            // Set the max - 1
            if (SelectedIndex >= Items.Count)
            {
                SelectedIndex = Items.Count - 1;
            }
            else
            {
                UpdateItemList();
            }
            // And update the items
            UpdateItems();
        }
        /// <summary>
        /// Removes the items that match the predicate.
        /// </summary>
        /// <param name="pred">The function to use as a check.</param>
        public void Remove(Func<NativeItem, bool> pred)
        {
            // Create a copy of the list with the existing items and iterate them in order
            List<NativeItem> items = new List<NativeItem>(Items);
            foreach (NativeItem item in items)
            {
                // If the predicate says that this item should be removed, do it
                if (pred(item))
                {
                    Items.Remove(item);
                }
            }
            // Once we finish, make sure that the index is under the limit
            // Set it to the max if it does
            if (SelectedIndex >= Items.Count)
            {
                SelectedIndex = Items.Count - 1;
            }
            else
            {
                UpdateItemList();
            }
            // And update the items
            UpdateItems();
        }
        /// <summary>
        /// Removes all of the items from this menu.
        /// </summary>
        public void Clear()
        {
            // Clear the existing items
            Items.Clear();
            // Set the indexes to zero
            index = 0;
            firstItem = 0;
            // And update the menu information
            UpdateItemList();
            UpdateItems();
        }
        /// <summary>
        /// Checks if an item is part of the menu.
        /// </summary>
        /// <param name="item">The item to check.</param>
        public bool Contains(NativeItem item) => Items.Contains(item);
        /// <summary>
        /// Draws the menu and handles the controls.
        /// </summary>
        public void Process()
        {
            // If the menu is not visible, return
            if (!visible)
            {
                return;
            }

            // Otherwise, draw the elements
            Draw();
            // And then work on the controls
            ProcessControls();
            // And finish by drawing the instructional buttons
            Buttons.Draw();
        }
        /// <summary>
        /// Calculates the positions and sizes of the elements.
        /// </summary>
        public void Recalculate()
        {
            // Store the current values of X and Y
            PointF pos;
            if (SafeZoneAware)
            {
                float x = 0;
                switch (Alignment)
                {
                    case Alignment.Left:
                        x = Offset.X;
                        break;
                    case Alignment.Right:
                        x = Offset.X - Width;
                        break;
                }
                Screen.SetElementAlignment(Alignment, GFXAlignment.Top);
                pos = Screen.GetRealPosition(x, Offset.Y);
                Screen.ResetElementAlignment();
            }
            else
            {
                float x = 0;
                switch (Alignment)
                {
                    case Alignment.Left:
                        x = Offset.X;
                        break;
                    case Alignment.Right:
                        x = 1f.ToXAbsolute() - Width - Offset.X;
                        break;
                }
                pos = new PointF(x, Offset.Y);
            }

            // If there is a banner and is a valid element
            if (bannerImage != null && bannerImage is BaseElement bannerImageBase)
            {
                // Set the position and size of the banner
                bannerImageBase.literalPosition = new PointF(pos.X, pos.Y);
                bannerImageBase.literalSize = new SizeF(width, bannerImageBase.Size.Height);
                bannerImageBase.Recalculate();
                // If there is a text element, also set the position of it
                if (Title != null)
                {
                    Title.Position = new PointF(pos.X + 209, pos.Y + 22);
                }
                // Finally, increase the current position of Y based on the banner height
                pos.Y += bannerImageBase.Size.Height;
            }

            // Time for the subtitle background
            // Set the position and size of it
            subtitleImage.literalPosition = new PointF(pos.X, pos.Y);
            subtitleImage.literalSize = new SizeF(width, subtitleHeight);
            subtitleImage.Recalculate();
            // If there is a text, also set the position of it
            if (subtitleText != null)
            {
                subtitleText.Position = new PointF(pos.X + 6, pos.Y + 4.2f);
            }
            // Finally, increase the size based on the subtitle height
            // currentY += subtitleHeight;

            // Set the size of the selection rectangle
            selectedRect.Size = new SizeF(width, itemHeight);
            // And set the word wrap of the description
            descriptionText.WordWrap = width - posXDescTxt;

            // Set the right size of the rotation
            searchAreaRight = new PointF(1f.ToXAbsolute() - 30, 0);

            // Then, continue with an item update
            UpdateItems();
        }
        /// <summary>
        /// Returns to the previous menu or closes the existing one.
        /// </summary>
        public void Back()
        {
            // Try to close the menu
            Visible = false;
            // If this menu has been closed and there is a parent menu, open it
            if (!Visible && Parent != null)
            {
                Parent.Visible = true;
            }
        }
        /// <summary>
        /// Opens the menu.
        /// </summary>
        [Obsolete("Set Visible to true instead.", true)]
        public void Open() => Visible = true;
        /// <summary>
        /// Closes the menu.
        /// </summary>
        [Obsolete("Set Visible to false instead.", true)]
        public void Close() => Visible = false;
        /// <summary>
        /// Moves to the previous item.
        /// Does nothing if the menu has no items.
        /// </summary>
        public void Previous()
        {
            // If there are no items, return
            if (Items.Count == 0)
            {
                return;
            }

            // If we are on the first item, go back to the last one
            if (SelectedIndex <= 0)
            {
                SelectedIndex = Items.Count - 1;
            }
            // Otherwise, reduce it by one
            else
            {
                SelectedIndex -= 1;
            }
        }
        /// <summary>
        /// Moves to the next item.
        /// Does nothing if the menu has no items.
        /// </summary>
        public void Next()
        {
            // If there are no items, return
            if (Items.Count == 0)
            {
                return;
            }

            // If we are on the last item, go back to the first one
            if (Items.Count - 1 == SelectedIndex)
            {
                SelectedIndex = 0;
            }
            // Otherwise, increase it by one
            else
            {
                SelectedIndex += 1;
            }
        }

        #endregion
    }
}
