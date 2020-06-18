using LemonUI.Elements;
using LemonUI.Menus;
using System;
using System.Drawing;

namespace LemonUI.Items
{
    /// <summary>
    /// Basic Rockstar-like item.
    /// </summary>
    public class NativeItem : IItem
    {
        #region Private Fields

        /// <summary>
        /// If this item can be used or not.
        /// </summary>
        private bool enabled = true;

        #endregion

        #region Public Properties

        /// <summary>
        /// If this item can be used or not.
        /// </summary>
        public bool Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                // Save the value
                enabled = value;
                // And set the correct color for enabled and disabled
                TitleObj.Color = value ? NativeMenu.colorWhite : NativeMenu.colorDisabled;
            }
        }
        /// <summary>
        /// The title of the item.
        /// </summary>
        public string Title
        {
            get => TitleObj.Text;
            set => TitleObj.Text = value;
        }
        /// <summary>
        /// The description of the item.
        /// </summary>
        public string Description
        {
            get => DescriptionObj.Text;
            set => DescriptionObj.Text = value;
        }
        /// <summary>
        /// The title object.
        /// </summary>
        protected internal ScaledText TitleObj { get; }
        /// <summary>
        /// The description object.
        /// </summary>
        protected internal ScaledText DescriptionObj { get; }

        #endregion

        #region Events

        /// <summary>
        /// Event triggered when the item is selected.
        /// </summary>
        public event EventHandler Selected;

        #endregion

        #region Constructors

        public NativeItem(string title) : this(title, "")
        {
        }

        public NativeItem(string title, string description)
        {
            TitleObj = new ScaledText(PointF.Empty, title, 0.345f)
            {
                Color = NativeMenu.colorWhiteSmoke
            };
            DescriptionObj = new ScaledText(PointF.Empty, description);
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// Draws the item.
        /// </summary>
        public void Draw()
        {
            TitleObj?.Draw();
        }

        #endregion
    }
}
