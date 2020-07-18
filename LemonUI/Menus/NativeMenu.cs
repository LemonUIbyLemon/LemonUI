#if FIVEM
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using Font = CitizenFX.Core.UI.Font;
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
    /// <param name="e">A <see cref="SelectedEventArgs"/> with the index informationn.</param>
    public delegate void SelectedEventHandler(object sender, SelectedEventArgs e);

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

        internal SelectedEventArgs(int index, int screen)
        {
            Index = index;
            OnScreen = screen;
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
    public class NativeMenu : IRecalculable, IProcessable
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

        /// <summary>
        /// A Pink-Purple used for debugging.
        /// </summary>
        internal static readonly Color colorDebug = Color.FromArgb(255, 255, 0, 255);
        /// <summary>
        /// The White color.
        /// </summary>
        internal static readonly Color colorWhite = Color.FromArgb(255, 255, 255, 255);
        /// <summary>
        /// The White Smoke color.
        /// </summary>
        internal static readonly Color colorWhiteSmoke = Color.FromArgb(255, 245, 245, 245);
        /// <summary>
        /// The Black color.
        /// </summary>
        internal static readonly Color colorBlack = Color.FromArgb(255, 0, 0, 0);
        /// <summary>
        /// The color for disabled items.
        /// </summary>
        internal static readonly Color colorDisabled = Color.FromArgb(255, 163, 159, 148);

        /// <summary>
        /// The controls required by the menu with both a gamepad and mouse + keyboard.
        /// </summary>
        internal static List<Control> controlsRequired = new List<Control>
        {
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
            Control.Sprint,
            Control.Jump,
            Control.Enter,
            Control.VehicleExit,
            Control.VehicleAccelerate,
            Control.VehicleBrake,
            Control.VehicleMoveLeftRight,
            Control.VehicleFlyYawLeft,
            Control.FlyLeftRight,
            Control.FlyUpDown,
            Control.VehicleFlyYawRight,
            Control.VehicleHandbrake
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
        /// The text of the menu.
        /// </summary>
        private ScaledText bannerText = null;
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
        /// A specific menu to open during the next tick.
        /// </summary>
        internal NativeMenu openNextTick = null;

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
                visible = value;
                if (value)
                {
                    SoundOpened?.PlayFrontend();
                    Shown?.Invoke(this, EventArgs.Empty);
                    TriggerSelectedItem();
                }
                else
                {
                    Close();
                }
            }
        }
        /// <summary>
        /// The title of the menu.
        /// </summary>
        public string Title
        {
            get
            {
                if (bannerText == null)
                {
                    throw new NullReferenceException("The Text parameter of this Menu is null.");
                }
                return bannerText.Text;
            }
            set
            {
                bannerText.Text = value;
            }
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
        /// The text object on top of the banner.
        /// </summary>
        public ScaledText Text
        {
            get => bannerText;
            set => bannerText = value;
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
                // If there are no items, return null
                if (Items.Count == 0)
                {
                    return null;
                }
                // Otherwise, return the correct item from the list
                else
                {
                    return Items[SelectedIndex];
                }
            }
        }
        /// <summary>
        /// The current index of the menu.
        /// </summary>
        public int SelectedIndex
        {
            get
            {
                if (Items == null || Items.Count == 0)
                {
                    return -1;
                }
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
                    throw new InvalidOperationException($"The index is over {Items.Count - 1}");
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

                // If the menu is visible, play the up and down sound
                if (Visible)
                {
                    SoundUpDown?.PlayFrontend();
                }

                // If an item was selected, set the description and trigger it
                if (SelectedItem != null)
                {
                    TriggerSelectedItem();
                }

                // And update the items
                UpdateItemList();
                UpdateItems();
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
        /// The items that this menu contain.
        /// </summary>
        public List<NativeItem> Items { get; } = new List<NativeItem>();
        /// <summary>
        /// Text shown when there are no items in the menu.
        /// </summary>
        public string NoItemsText { get; set; } = "There are no items available";
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
        /// If this menu should be aware of the Safe Zone.
        /// </summary>
        public bool SafeZoneAware { get; set; } = true;
        /// <summary>
        /// If the count of items should be shown on the right of the subtitle.
        /// </summary>
        public CountVisibility ItemCount { get; set; }
        /// <summary>
        /// The instructional buttons shown in the bottom right.
        /// </summary>
        public InstructionalButtons Buttons { get; } = new InstructionalButtons(new InstructionalButton("Select", Control.PhoneSelect), new InstructionalButton("Back", Control.PhoneCancel))
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
            bannerText = new ScaledText(PointF.Empty, title, 1.02f, Font.HouseScript)
            {
                Color = colorWhite,
                Alignment = Alignment.Center
            };
            subtitleText.Text = subtitle.ToUpperInvariant();
            Recalculate();
        }

        #endregion

        #region Tools

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

            // Otherwise, trigger the selected event for this menu
            SelectedEventArgs args = new SelectedEventArgs(index, index - firstItem);
            SelectedItem.OnSelected(this, args);
            SelectedIndexChanged?.Invoke(this, args);
        }
        /// <summary>
        /// Updates the positions of the items.
        /// </summary>
        private void UpdateItems()
        {
            // Store the current values of X and Y
            float currentX = alignment == Alignment.Right ? 1f.ToXAbsolute() - width - offset.X : offset.X;
            float currentY = offset.Y;

            // Add the heights of the banner and subtitle (if there are any)
            if (bannerImage != null)
            {
                currentY += bannerImage.Size.Height;
            }
            if (!string.IsNullOrWhiteSpace(subtitleText.Text))
            {
                countText.Text = $"{SelectedIndex + 1} / {Items.Count}";
                countText.Position = new PointF(currentX + width - countText.Width - 6, currentY + 4.2f);
                currentY += subtitleImage.Size.Height;
            }

            // Set the position and size of the background image
            backgroundImage.literalPosition = new PointF(currentX, currentY);
            backgroundImage.literalSize = new SizeF(width, itemHeight * visibleItems.Count);
            backgroundImage.Recalculate();
            // Set the position of the rectangle that marks the current item
            selectedRect.Position = new PointF(currentX, currentY + ((index - firstItem) * itemHeight));
            // And then do the description background and text
            descriptionText.Text = Items.Count == 0 ? NoItemsText : SelectedItem.Description;
            float description = currentY + ((Items.Count > maxItems ? maxItems : Items.Count) * itemHeight) + heightDiffDescImg;
            descriptionRect.Size = new SizeF(width, descriptionText.LineCount * 35);
            descriptionRect.Position = new PointF(currentX, description);
            descriptionText.Position = new PointF(currentX + posXDescTxt, description + heightDiffDescTxt);

            // Save the size of the items
            SizeF size = new SizeF(width, itemHeight);
            // And start recalculating them
            int i = 0;
            foreach (NativeItem item in visibleItems)
            {
                // Tell the item to recalculate the position
                item.Recalculate(new PointF(currentX, currentY), size, item == SelectedItem);
                // And increase the index of the item and Y position
                i++;
                currentY += itemHeight;
            }
        }
        /// <summary>
        /// Processes the button presses.
        /// </summary>
        private void ProcessControls()
        {
            // Start by disabling all of the controls
            Controls.DisableAll(2);
            // And enable only those required at all times and specific situations
            Controls.EnableThisFrame(controlsRequired);
            if (Controls.IsUsingController)
            {
                Controls.EnableThisFrame(controlsGamepad);
            }
            if (Controls.IsUsingController || !UseMouse)
            {
                Controls.EnableThisFrame(controlsCamera);
            }

            // If the controls are disabled, return
            if (!AcceptsInput)
            {
                return;
            }

            // Check if the controls necessary were pressed
            bool backPressed = Controls.IsJustPressed(Control.PhoneCancel) || Controls.IsJustPressed(Control.FrontendPause);
            bool upPressed = Controls.IsJustPressed(Control.PhoneUp) || Controls.IsJustPressed(Control.CursorScrollUp);
            bool downPressed = Controls.IsJustPressed(Control.PhoneDown) || Controls.IsJustPressed(Control.CursorScrollDown);
            bool selectPressed = Controls.IsJustPressed(Control.FrontendAccept) || Controls.IsJustPressed(Control.PhoneSelect);
            bool leftPressed = Controls.IsJustPressed(Control.PhoneLeft);
            bool rightPressed = Controls.IsJustPressed(Control.PhoneRight);

            // If the player pressed the back button, go back or close the menu
            if (backPressed)
            {
                Back();
                return;
            }

            // If the player pressed up, go to the previous item
            if (upPressed && !downPressed)
            {
                Previous();
                return;
            }
            // If he pressed down, go to the next item
            else if (downPressed && !upPressed)
            {
                Next();
                return;
            }

            // If the player pressed the left or right button, trigger the event and sound
            if (SelectedItem is NativeSlidableItem slidable)
            {
                if (leftPressed)
                {
                    if (SelectedItem.Enabled)
                    {
                        slidable.GoLeft();
                        SoundLeftRight?.PlayFrontend();
                        return;
                    }
                    else
                    {
                        SoundDisabled?.PlayFrontend();
                        return;
                    }
                }
                if (rightPressed)
                {
                    if (SelectedItem.Enabled)
                    {
                        slidable.GoRight();
                        SoundLeftRight?.PlayFrontend();
                        return;
                    }
                    else
                    {
                        SoundDisabled?.PlayFrontend();
                        return;
                    }
                }
            }

            // If the player is using the mouse controls and pressed select
            if (UseMouse && !Controls.IsUsingController)
            {
                // Enable the mouse cursor
#if FIVEM
                API.SetMouseCursorActiveThisFrame();
#elif SHVDN2
                Function.Call(Hash._SHOW_CURSOR_THIS_FRAME);
#elif SHVDN3
                Function.Call(Hash._SET_MOUSE_CURSOR_ACTIVE_THIS_FRAME);
#endif

                // If the select button was not pressed, return
                if (!selectPressed)
                {
                    return;
                }

                // Iterate over the items while keeping the index
                int i = 0;
                foreach (NativeItem item in visibleItems)
                {
                    // If this is a list item and the user pressed the right arrow
                    if (item is NativeSlidableItem slidable1 && Resolution.IsCursorInBounds(slidable1.arrowRight.Position, slidable1.arrowRight.Size))
                    {
                        // If the item is enabled, move to the right
                        if (item.Enabled)
                        {
                            slidable1.GoRight();
                            SoundLeftRight?.PlayFrontend();
                            return;
                        }
                        // Otherwise, play the error sound
                        else
                        {
                            SoundDisabled?.PlayFrontend();
                            return;
                        }
                    }
                    // If this is a list item and the user pressed the left arrow
                    else if (item is NativeSlidableItem slidable2 && Resolution.IsCursorInBounds(slidable2.arrowRight.Position, slidable2.arrowRight.Size))
                    {
                        // If the item is enabled, move to the left
                        if (item.Enabled)
                        {
                            slidable2.GoLeft();
                            SoundLeftRight?.PlayFrontend();
                            return;
                        }
                        // Otherwise, play the error sound
                        else
                        {
                            SoundDisabled?.PlayFrontend();
                            return;
                        }
                    }
                    // If the user selected somewhere in the item area
                    else if (Resolution.IsCursorInBounds(item.title.Position.X - itemOffsetX, item.title.Position.Y - itemOffsetY, Width, itemHeight))
                    {
                        if (item == SelectedItem)
                        {
                            // If is enabled, activate it and play the select sound
                            if (item.Enabled)
                            {
                                item.OnActivated(this);
                                SoundActivated?.PlayFrontend();
                                if (item is NativeCheckboxItem checkboxItem)
                                {
                                    checkboxItem.UpdateTexture(true);
                                }
                                return;
                            }
                            // Otherwise, play the error sound
                            else
                            {
                                SoundDisabled?.PlayFrontend();
                                return;
                            }
                        }
                        // Otherwise, change the index
                        else
                        {
                            SelectedIndex = firstItem + i;
                            SoundUpDown?.PlayFrontend();
                            return;
                        }
                    }

                    // Finally, increase the number
                    i++;
                }
            }

            // If the player selected an item
            if (selectPressed)
            {
                // If there is an item selected and is enabled, trigger it and play the selected sound
                if (SelectedItem != null && SelectedItem.Enabled)
                {
                    SelectedItem.OnActivated(this);
                    SoundActivated?.PlayFrontend();
                    if (SelectedItem is NativeCheckboxItem check)
                    {
                        check.UpdateTexture(true);
                    }
                    return;
                }
                // Otherwise, play the error sound
                else
                {
                    SoundDisabled?.PlayFrontend();
                    return;
                }
            }
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// Adds an item onto the menu.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void Add(NativeItem item)
        {
            // If the item is already on the list, raise an exception
            if (Items.Contains(item))
            {
                throw new InvalidOperationException("The item is already part of the menu.");
            }
            // Also raise an exception if is null
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            // Otherwise, just add it
            Items.Add(item);
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
            // If there is a submenu to open and this menu is closed, show it and return
            if (openNextTick != null && !Visible)
            {
                openNextTick.Visible = true;
                openNextTick = null;
            }

            // If the menu is not visible, return
            if (!visible)
            {
                return;
            }

            // Otherwise, draw the elements
            // Start by setting the alignment for the UI Elements
            // This is to make the UI appear in the correct position in non standard aspect ratios
            // For example: 21:6, 32:9, 11:4, 48:9 (3 16:9 in Surround) and more
            const int L = 76;
            const int T = 84;
            if (SafeZoneAware)
            {
#if FIVEM
                API.SetScriptGfxAlign(L, T);
                API.SetScriptGfxAlignParams(0, 0, 0, 0);
#elif SHVDN2
                Function.Call(Hash._SET_SCREEN_DRAW_POSITION, L, T);
                Function.Call(Hash._0xF5A2C681787E579D, 0, 0, 0, 0);
#elif SHVDN3
                Function.Call(Hash.SET_SCRIPT_GFX_ALIGN, L, T);
                Function.Call(Hash.SET_SCRIPT_GFX_ALIGN_PARAMS, 0, 0, 0, 0);
#endif
            }

            // Start with the banner
            if (bannerImage != null)
            {
                // Draw the background image and text if is present
                bannerImage.Draw();
                if (bannerText != null && !string.IsNullOrWhiteSpace(bannerText.Text))
                {
                    bannerText.Draw();
                }
            }
            // Continue with the subtitle image and text
            subtitleImage.Draw();
            subtitleText?.Draw();
            // The background of the items
            backgroundImage?.Draw();
            // And the item count
            if (ItemCount == CountVisibility.Always || (ItemCount == CountVisibility.Auto && Items.Count > MaxItems))
            {
                countText.Draw();
            }

            // Then go for the visible items with the exception of the one selected
            foreach (NativeItem item in visibleItems)
            {
                if (item != SelectedItem)
                {
                    item.Draw();
                }
            }

            // If this menu has items, draw the rectangle for the selected item
            if (Items.Count > 0)
            {
                selectedRect?.Draw();
            }

            // If there is an item selected, draw it
            if (SelectedItem != null)
            {
                SelectedItem.Draw();
            }

            // If there is some description text, draw the text and background
            if (!string.IsNullOrWhiteSpace(descriptionText.Text))
            {
                descriptionRect.Draw();
                descriptionText.Draw();
            }

            // Time to work on the controls!
            ProcessControls();

            // Reset the alignment if this menu should be aware of the safe zone
            if (SafeZoneAware)
            {
#if FIVEM
                API.ResetScriptGfxAlign();
#elif SHVDN2
                Function.Call(Hash._0xE3A3DB414A373DAB);
#elif SHVDN3
                Function.Call(Hash.RESET_SCRIPT_GFX_ALIGN);
#endif
            }
            // And finish by drawing the instructional buttons
            Buttons.Draw();
        }
        /// <summary>
        /// Calculates the positions and sizes of the elements.
        /// </summary>
        public void Recalculate()
        {
            // Store the current values of X and Y
            float currentX = alignment == Alignment.Right ? 1f.ToXAbsolute() - width - offset.X : offset.X;
            float currentY = offset.Y;

            // If there is a banner and is a valid element
            if (bannerImage != null && bannerImage is BaseElement bannerImageBase)
            {
                // Set the position and size of the banner
                bannerImageBase.literalPosition = new PointF(currentX, currentY);
                bannerImageBase.literalSize = new SizeF(width, bannerImageBase.Size.Height);
                bannerImageBase.Recalculate();
                // If there is a text element, also set the position of it
                if (bannerText != null)
                {
                    bannerText.Position = new PointF(currentX + 209, currentY + 22);
                }
                // Finally, increase the current position of Y based on the banner height
                currentY += bannerImageBase.Size.Height;
            }

            // Time for the subtitle background
            // Set the position and size of it
            subtitleImage.literalPosition = new PointF(currentX, currentY);
            subtitleImage.literalSize = new SizeF(width, subtitleHeight);
            subtitleImage.Recalculate();
            // If there is a text, also set the position of it
            if (subtitleText != null)
            {
                subtitleText.Position = new PointF(currentX + 6, currentY + 4.2f);
            }
            // Finally, increase the size based on the subtitle height
            // currentY += subtitleHeight;

            // Set the size of the selection rectangle
            selectedRect.Size = new SizeF(width, itemHeight);
            // And set the word wrap of the description
            descriptionText.WordWrap = width - posXDescTxt;

            // Then, continue with an item update
            UpdateItems();
        }
        /// <summary>
        /// Returns to the previous menu or closes the existing one.
        /// </summary>
        public void Back()
        {
            // Try to close the menu
            Close();
            // If this menu has been closed and there is a parent menu, open it
            if (!Visible && Parent != null)
            {
                Parent.Visible = true;
            }
        }
        /// <summary>
        /// Closes the menu.
        /// </summary>
        public void Close()
        {
            // Create a new set of event arguments
            CancelEventArgs args = new CancelEventArgs();
            // And trigger the event
            Closing?.Invoke(this, args);

            // If we need to cancel the closure of the menu, return
            if (args.Cancel)
            {
                return;
            }
            // Otherwise, close the menu
            visible = false;
            Closed?.Invoke(this, EventArgs.Empty);
            SoundClose?.PlayFrontend();
        }
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
            if (SelectedIndex == 0)
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
