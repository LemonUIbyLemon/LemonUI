#if FIVEM
using CitizenFX.Core.UI;
using Font = CitizenFX.Core.UI.Font;
#elif SHVDN2
using Font = GTA.Font;
#elif SHVDN3
using GTA.UI;
using Font = GTA.UI.Font;
#endif
using LemonUI.Elements;
using LemonUI.Extensions;
using LemonUI.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace LemonUI.Menus
{
    /// <summary>
    /// Menu that looks like the ones used by Rockstar.
    /// </summary>
    public class NativeMenu : INativeMenu, IProcessable
    {
        #region Internal Fields

        /// <summary>
        /// The White color.
        /// </summary>
        internal static readonly Color colorWhite = Color.FromArgb(255, 255, 255);
        /// <summary>
        /// The White Smoke color.
        /// </summary>
        internal static readonly Color colorWhiteSmoke = Color.FromArgb(245, 245, 245);
        /// <summary>
        /// The Black color.
        /// </summary>
        internal static readonly Color colorBlack = Color.FromArgb(0, 0, 0);
        /// <summary>
        /// The color for disabled items.
        /// </summary>
        internal static readonly Color colorDisabled = Color.FromArgb(163, 159, 148);

        /// <summary>
        /// Sound played when the user selects an option.
        /// </summary>
        internal static readonly Sound.Sound soundSelect = new Sound.Sound("HUD_FRONTEND_DEFAULT_SOUNDSET", "SELECT");
        /// <summary>
        /// Sound played whenn the user returns or closes the menu.
        /// </summary>
        internal static readonly Sound.Sound soundBack = new Sound.Sound("HUD_FRONTEND_DEFAULT_SOUNDSET", "BACK");

        #endregion

        #region Private Fields

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
        /// The banner of the menu.
        /// </summary>
        private IScreenDrawable bannerImage = null;
        /// <summary>
        /// The text of the menu.
        /// </summary>
        private ScaledText bannerText = null;
        /// <summary>
        /// The background of the drawable text.
        /// </summary>
        private IScreenDrawable subtitleImage = null;
        /// <summary>
        /// The text of the subtitle.
        /// </summary>
        private ScaledText subtitleText = null;
        /// <summary>
        /// The image on the background of the menu.
        /// </summary>
        private ScaledTexture backgroundImage = null;
        /// <summary>
        /// The rectangle that shows the currently selected item.
        /// </summary>
        private ScaledRectangle selectedRect = null;
        /// <summary>
        /// The maximum allowed number of items in the menu at once.
        /// </summary>
        private int maxItems = 9;
        /// <summary>
        /// The first item in the menu.
        /// </summary>
        private int firstItem = 0;

        #endregion

        #region Private Properties

        /// <summary>
        /// The items that are visible and useable on the screen.
        /// </summary>
        private IEnumerable<NativeItem> VisibleItems
        {
            get
            {
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

                    // Otherwise, return it as part of the iterator
                    yield return (NativeItem)Items[i];
                }
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// If the menu is visible on the screen.
        /// </summary>
        public bool Visible
        {
            get
            {
                return visible;
            }
            set
            {
                visible = value;
                if (visible)
                {
                    soundSelect.PlayFrontend();
                    Shown?.Invoke(this, EventArgs.Empty);
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
        public IScreenDrawable Banner
        {
            get => bannerImage;
            set
            {
                bannerImage = value;
                Recalculate();
                RecalculateTexts();
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
        /// The current index of the menu.
        /// </summary>
        public int Index
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
                if (Items == null || Items.Count == 0)
                {
                    throw new InvalidOperationException("There are no items in this menu.");
                }
                else if (Items.Count >= value)
                {
                    throw new InvalidOperationException($"The index is over {Items.Count - 1}");
                }
                index = value;
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
            get
            {
                return alignment;
            }
            set
            {
                alignment = value;
                UpdateAlignment();
            }
        }
        /// <summary>
        /// The subtitle of the menu.
        /// </summary>
        public string Subtitle
        {
            get => subtitleText.Text;
            set => subtitleText.Text = value;
        }
        /// <summary>
        /// The items that this menu contain.
        /// </summary>
        public List<IItem> Items { get; }
        /// <summary>
        /// Text shown when there are no items in the menu.
        /// </summary>
        public string NoItemsText { get; set; }
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
        /// Event triggered when an item is selected.
        /// </summary>
        public event EventHandler Selected;
        /// <summary>
        /// Event triggered when the selected index has been changed.
        /// </summary>
        public event EventHandler ItemChanged;
        /// <summary>
        /// Event triggered when the index has been changed.
        /// </summary>
        public event EventHandler IndexChanged;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new menu with the default banner texture.
        /// </summary>
        /// <param name="title">The title of the menu.</param>
        /// <param name="subtitle">Subtitle of this menu.</param>
        public NativeMenu(string title, string subtitle) : this(title, new ScaledTexture(PointF.Empty, new SizeF(0, 108), "commonmenu", "interaction_bgd"), subtitle)
        {
        }

        /// <summary>
        /// Creates a new menu with the specified banner texture.
        /// </summary>
        /// <param name="title">The title of the menu.</param>
        /// <param name="banner">The drawable to use as the banner.</param>
        /// <param name="subtitle">Subtitle of this menu.</param>
        public NativeMenu(string title, IScreenDrawable banner, string subtitle)
        {
            Items = new List<IItem>();
            bannerImage = banner;
            bannerText = new ScaledText(PointF.Empty, title, 1.02f, Font.HouseScript)
            {
                Color = colorWhite,
                Alignment = Alignment.Center
            };
            subtitleImage = new ScaledRectangle(PointF.Empty, SizeF.Empty)
            {
                Color = Color.FromArgb(0, 0, 0)
            };
            subtitleText = new ScaledText(PointF.Empty, subtitle, 0.35f, Font.ChaletLondon)
            {
                Color = colorWhiteSmoke
            };
            backgroundImage = new ScaledTexture(PointF.Empty, SizeF.Empty, "commonmenu", "gradient_bgd");
            selectedRect = new ScaledRectangle(PointF.Empty, SizeF.Empty)
            {
                Color = colorWhiteSmoke
            };
            Recalculate();
        }

        #endregion

        #region Tools

        /// <summary>
        /// Updates the positions of the items.
        /// </summary>
        private void UpdateItems()
        {
            // Calculate the Y position based on the existance of a banner and description
            float y = 3;
            if (bannerImage != null)
            {
                y += bannerImage.Size.Height;
            }
            if (subtitleImage != null && !string.IsNullOrWhiteSpace(subtitleText.Text))
            {
                y += subtitleImage.Size.Height;
            }

            // Before we do anything, calculate the X position
            float itemStart = 7.5f.ToXRelative();
            if (alignment == Alignment.Right)
            {
                itemStart = 1 - width.ToXRelative() + itemStart;
            }

            // Iterate over the number of items while counting the number of them
            int i = 0;
            foreach (NativeItem item in VisibleItems)
            {
                // Add the space between items if this is not the first
                y += i == 0 ? 0 : 37.5f;
                // And convert it to a relative value
                item.TitleObj.relativePosition = new PointF(itemStart, y.ToYRelative());

                // If this matches the currently selected item
                if (i + firstItem == index)
                {
                    item.TitleObj.Color = colorBlack;
                }

                // Finally, increase the count by one and move to the next item
                i++;
            }
        }
        /// <summary>
        /// Recalculates the text labels.
        /// </summary>
        public void RecalculateTexts()
        {
            float offset = alignment == Alignment.Right ? 1f - width.ToXRelative() : 0;
            float start = 0;

            if (bannerImage != null)
            {
                start += bannerImage.Size.Height;
            }

            if (bannerText != null)
            {
                bannerText.Position = new PointF(209, 22);
                bannerText.relativePosition.X += offset;
            }
            if (subtitleText != null)
            {
                start += 3;
                subtitleText.Position = new PointF(6, start);
                subtitleText.relativePosition.X += offset;
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
            if (Items.Count != 0 && index == -1)
            {
                index = 0;
            }
            // And recalculate the positions
            Recalculate();
        }
        /// <summary>
        /// Removes an item from the menu.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        public void Remove(NativeItem item)
        {
            // Remove it if there
            // If not, ignore it
            Items.Remove(item);
            // If the index is higher or equal than the max number of items
            // Set the max - 1
            if (index >= Items.Count)
            {
                index = Items.Count - 1;
            }
        }
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

            // Otherwise, draw all other things
            if (bannerImage != null)
            {
                bannerImage.Draw();
                if (bannerText != null && !string.IsNullOrWhiteSpace(bannerText.Text))
                {
                    bannerText.Draw();
                }
            }
            subtitleImage?.Draw();
            subtitleText?.Draw();
            backgroundImage?.Draw();

            foreach (NativeItem item in VisibleItems)
            {
                item.Draw();
            }

            selectedRect?.Draw();
        }
        /// <summary>
        /// Updates the alignment of the items.
        /// This will automatically recalculate the sizes and positions.
        /// </summary>
        public void UpdateAlignment()
        {
            if (bannerImage != null)
            {
                bannerImage.Alignment = alignment;
            }
            if (subtitleImage != null)
            {
                subtitleImage.Alignment = alignment;
            }
            RecalculateTexts();
            UpdateItems();
        }
        /// <summary>
        /// Calculates the positions and sizes of the elements.
        /// </summary>
        public void Recalculate()
        {
            const float itemHeight = 38f;

            // Save the start of the X value
            float start = 0;

            // If there is a banner and is an element
            if (bannerImage != null && bannerImage is BaseElement bannerImageBase)
            {
                // Get the height of the banner
                float bannerHeight = bannerImageBase.Size.Height;
                // Set the position and size
                bannerImageBase.literalPosition = PointF.Empty;
                bannerImageBase.literalSize = new SizeF(width, bannerHeight);
                // Increaser the start location of the next item
                start += bannerHeight;
                bannerImageBase.Recalculate();
            }
            // If there is a subtitle image
            if (subtitleImage != null && subtitleImage is BaseElement subtitleImageBase)
            {
                // Set the position and start
                const float subtitleHeight = 37;
                subtitleImageBase.literalPosition = new PointF(0, start);
                subtitleImageBase.literalSize = new SizeF(width, subtitleHeight);
                // Increase the start location
                start += subtitleHeight;
                subtitleImageBase.Recalculate();
            }
            // If there is a background image
            if (backgroundImage != null)
            {
                // See if we should draw the total number of items or the max allowed
                int count = Items.Count > maxItems ? maxItems : Items.Count;
                // Set the position and size
                backgroundImage.literalPosition = new PointF(0, start);
                backgroundImage.literalSize = new SizeF(width, itemHeight * count);
                backgroundImage.Recalculate();
            }
            // If there is a rectangle for the currently selected item
            if (selectedRect != null)
            {
                // Set the size
                selectedRect.literalPosition = new PointF(0, start);
                selectedRect.literalSize = new SizeF(width, itemHeight);
                selectedRect.Recalculate();
            }

            RecalculateTexts();
            UpdateItems();
        }
        /// <summary>
        /// Closes the menu.
        /// </summary>
        public void Close()
        {
            // Create a new set of event arguments
            CancelEventArgs args = new CancelEventArgs();
            // And trigger the event
            Closed?.Invoke(this, args);

            // If we need to cancel the closure of the menu, return
            if (args.Cancel)
            {
                return;
            }
            // Otherwise, hide the menu
            visible = false;
            soundBack.PlayFrontend();
        }
        /// <summary>
        /// Moves to the previous item.
        /// Does nothing if the menu has no items.
        /// </summary>
        public void Previous()
        {
        }
        /// <summary>
        /// Moves to the next item.
        /// Does nothing if the menu has no items.
        /// </summary>
        public void Next()
        {
        }

        #endregion
    }
}
