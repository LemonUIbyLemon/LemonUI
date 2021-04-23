#if FIVEM
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using Font = CitizenFX.Core.UI.Font;
#elif RPH
using Rage.Native;
#elif SHVDN2
using GTA;
using GTA.Native;
using Font = GTA.Font;
#elif SHVDN3
using GTA.UI;
using GTA.Native;
using Font = GTA.UI.Font;
#endif
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using LemonUI.Extensions;

namespace LemonUI.Elements
{
    /// <summary>
    /// A text string.
    /// </summary>
    public class ScaledText : IText
    {
        #region Consistent Values

        /// <summary>
        /// The size of every chunk of text.
        /// </summary>
        private const int chunkSize = 90;

        #endregion

        #region Private Fields

        /// <summary>
        /// The absolute 1080p based screen position.
        /// </summary>
        private PointF absolutePosition = PointF.Empty;
        /// <summary>
        /// The relative 0-1 relative position.
        /// </summary>
        private PointF relativePosition = PointF.Empty;
        /// <summary>
        /// The raw string of text.
        /// </summary>
        private string text = "";
        /// <summary>
        /// The raw string split into equally sized strings.
        /// </summary>
        private List<string> chunks = new List<string>();
        /// <summary>
        /// The alignment of the item.
        /// </summary>
        private Alignment alignment = Alignment.Left;
        /// <summary>
        /// The word wrap value passed by the user.
        /// </summary>
        private float internalWrap = 0f;
        /// <summary>
        /// The real word wrap value based on the position of the text.
        /// </summary>
        private float realWrap = 0f;

        #endregion

        #region Public Properties

        /// <summary>
        /// The position of the text.
        /// </summary>
        public PointF Position
        {
            get => absolutePosition;
            set
            {
                absolutePosition = value;
                relativePosition = value.ToRelative();
            }
        }
        /// <summary>
        /// The text to draw.
        /// </summary>
        public string Text
        {
            get => text;
            set
            {
                text = value;
                Slice();
            }
        }
        /// <summary>
        /// The color of the text.
        /// </summary>
        public Color Color { get; set; } = Color.FromArgb(255, 255, 255, 255);
        /// <summary>
        /// The game font to use.
        /// </summary>
        public Font Font { get; set; } = Font.ChaletLondon;
        /// <summary>
        /// The scale of the text.
        /// </summary>
        public float Scale { get; set; } = 1f;
        /// <summary>
        /// If the text should have a drop down shadow.
        /// </summary>
        public bool Shadow { get; set; } = false;
        /// <summary>
        /// If the test should have an outline.
        /// </summary>
        public bool Outline { get; set; } = false;
        /// <summary>
        /// The alignment of the text.
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
        /// The distance from the start position where the text will be wrapped into new lines.
        /// </summary>
        public float WordWrap
        {
            get
            {
                return internalWrap;
            }
            set
            {
                internalWrap = value;
                Recalculate();
            }
        }
        /// <summary>
        /// The width that the text takes from the screen.
        /// </summary>
        public float Width
        {
            get
            {
#if FIVEM
                API.BeginTextCommandWidth("CELL_EMAIL_BCON");
                Add();
                return API.EndTextCommandGetWidth(true) * 1f.ToXAbsolute();
#elif RPH
                NativeFunction.CallByHash<int>(0x54CE8AC98E120CAB, "CELL_EMAIL_BCON");
                Add();
                return NativeFunction.CallByHash<float>(0x85F061DA64ED2F67, true) * 1f.ToXAbsolute();
#elif SHVDN2
                Function.Call(Hash._SET_TEXT_ENTRY_FOR_WIDTH, "CELL_EMAIL_BCON");
                Add();
                return Function.Call<float>(Hash._GET_TEXT_SCREEN_WIDTH, true) * 1f.ToXAbsolute();
#elif SHVDN3
                Function.Call(Hash._BEGIN_TEXT_COMMAND_GET_WIDTH, "CELL_EMAIL_BCON");
                Add();
                return Function.Call<float>(Hash._END_TEXT_COMMAND_GET_WIDTH, true) * 1f.ToXAbsolute();
#endif
            }
        }
        /// <summary>
        /// The number of lines used by this text.
        /// </summary>
        public int LineCount
        {
            get
            {
                // Start the string measuring
#if FIVEM
                API.BeginTextCommandLineCount("CELL_EMAIL_BCON");
#elif RPH
                NativeFunction.CallByHash<int>(0x521FB041D93DD0E4, "CELL_EMAIL_BCON");
#elif SHVDN2
                Function.Call(Hash._SET_TEXT_GXT_ENTRY, "CELL_EMAIL_BCON");
#elif SHVDN3
                Function.Call(Hash._BEGIN_TEXT_COMMAND_LINE_COUNT, "CELL_EMAIL_BCON");
#endif
                // Add the information of this text
                Add();
                // And return the number of lines reported by the game
#if FIVEM
                return API.EndTextCommandGetLineCount(relativePosition.X, relativePosition.Y);
#elif RPH
                return NativeFunction.CallByHash<int>(0x9040DFB09BE75706, relativePosition.X, relativePosition.Y);
#elif SHVDN2
                return Function.Call<int>(Hash._0x9040DFB09BE75706, relativePosition.X, relativePosition.Y);
#elif SHVDN3
                return Function.Call<int>(Hash._END_TEXT_COMMAND_LINE_COUNT, relativePosition.X, relativePosition.Y);
#endif
            }
        }
        /// <summary>
        /// The relative height of each line in the text.
        /// </summary>
        public float LineHeight
        {
            get
            {
                // Height will always be 1080
#if FIVEM
                return 1080 * API.GetTextScaleHeight(Scale, (int)Font);
#elif RPH
                return 1080 * NativeFunction.CallByHash<float>(0xDB88A37483346780, Scale, (int)Font);
#elif SHVDN2
                return 1080 * Function.Call<float>(Hash._0xDB88A37483346780, Scale, (int)Font);
#elif SHVDN3
                return 1080 * Function.Call<float>(Hash._GET_TEXT_SCALE_HEIGHT, Scale, (int)Font);
#endif
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a text with the specified options.
        /// </summary>
        /// <param name="pos">The position where the text should be located.</param>
        /// <param name="text">The text to show.</param>
        public ScaledText(PointF pos, string text) : this(pos, text, 1f, Font.ChaletLondon)
        {
        }

        /// <summary>
        /// Creates a text with the specified options.
        /// </summary>
        /// <param name="pos">The position where the text should be located.</param>
        /// <param name="text">The text to show.</param>
        /// <param name="scale">The scale of the text.</param>
        public ScaledText(PointF pos, string text, float scale) : this(pos, text, scale, Font.ChaletLondon)
        {
        }

        /// <summary>
        /// Creates a text with the specified options
        /// </summary>
        /// <param name="pos">The position where the text should be located.</param>
        /// <param name="text">The text to show.</param>
        /// <param name="scale">The scale of the text.</param>
        /// <param name="font">The font to use.</param>
        public ScaledText(PointF pos, string text, float scale, Font font)
        {
            Position = pos;
            Text = text;
            Scale = scale;
            Font = font;
        }

        #endregion

        #region Tools

        /// <summary>
        /// Adds the text information for measurement.
        /// </summary>
        private void Add()
        {
            if (Scale == 0)
            {
                return;
            }
#if FIVEM
            foreach (string chunk in chunks)
            {
                API.AddTextComponentString(chunk);
            }
            API.SetTextFont((int)Font);
            API.SetTextScale(1f, Scale);
            API.SetTextColour(Color.R, Color.G, Color.B, Color.A);
            API.SetTextJustification((int)Alignment);
            if (Shadow)
            {
                API.SetTextDropShadow();
            }
            if (Outline)
            {
                API.SetTextOutline();
            }
            if (WordWrap > 0)
            {
                switch (Alignment)
                {
                    case Alignment.Center:
                        API.SetTextWrap(relativePosition.X - (realWrap * 0.5f), relativePosition.X + (realWrap * 0.5f));
                        break;
                    case Alignment.Left:
                        API.SetTextWrap(relativePosition.X, relativePosition.X + realWrap);
                        break;
                    case Alignment.Right:
                        API.SetTextWrap(relativePosition.X - realWrap, relativePosition.X);
                        break;
                }
            }
            else if (Alignment == Alignment.Right)
            {
                API.SetTextWrap(0f, relativePosition.X);
            }
#elif RPH
            foreach (string chunk in chunks)
            {
                NativeFunction.CallByHash<int>(0x6C188BE134E074AA, chunk);
            }
            NativeFunction.CallByHash<int>(0x66E0276CC5F6B9DA, (int)Font);
            NativeFunction.CallByHash<int>(0x07C837F9A01C34C9, 1f, Scale);
            NativeFunction.CallByHash<int>(0xBE6B23FFA53FB442, Color.R, Color.G, Color.B, Color.A);
            NativeFunction.CallByHash<int>(0x4E096588B13FFECA, (int)Alignment);
            if (Shadow)
            {
                NativeFunction.CallByHash<int>(0x1CA3E9EAC9D93E5E);
            }
            if (Outline)
            {
                NativeFunction.CallByHash<int>(0x2513DFB0FB8400FE);
            }
            if (WordWrap > 0)
            {
                switch (Alignment)
                {
                    case Alignment.Center:
                        NativeFunction.CallByHash<int>(0x63145D9C883A1A70, relativePosition.X - (realWrap * 0.5f), relativePosition.X + (realWrap * 0.5f));
                        break;
                    case Alignment.Left:
                        NativeFunction.CallByHash<int>(0x63145D9C883A1A70, relativePosition.X, relativePosition.X + realWrap);
                        break;
                    case Alignment.Right:
                        NativeFunction.CallByHash<int>(0x63145D9C883A1A70, relativePosition.X - realWrap, relativePosition.X);
                        break;
                }
            }
            else if (Alignment == Alignment.Right)
            {
                NativeFunction.CallByHash<int>(0x63145D9C883A1A70, 0f, relativePosition.X);
            }
#elif (SHVDN2 || SHVDN3)
            foreach (string chunk in chunks)
            {
                Function.Call((Hash)0x6C188BE134E074AA, chunk); // _ADD_TEXT_COMPONENT_STRING on v2, ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME on v3
            }
            Function.Call(Hash.SET_TEXT_FONT, (int)Font);
            Function.Call(Hash.SET_TEXT_SCALE, 1f, Scale);
            Function.Call(Hash.SET_TEXT_COLOUR, Color.R, Color.G, Color.B, Color.A);
            Function.Call(Hash.SET_TEXT_JUSTIFICATION, (int)Alignment);
            if (Shadow)
            {
                Function.Call(Hash.SET_TEXT_DROP_SHADOW);
            }
            if (Outline)
            {
                Function.Call(Hash.SET_TEXT_OUTLINE);
            }
            if (WordWrap > 0)
            {
                switch (Alignment)
                {
                    case Alignment.Center:
                        Function.Call(Hash.SET_TEXT_WRAP, relativePosition.X - (realWrap * 0.5f), relativePosition.X + (realWrap * 0.5f));
                        break;
                    case Alignment.Left:
                        Function.Call(Hash.SET_TEXT_WRAP, relativePosition.X, relativePosition.X + realWrap);
                        break;
                    case Alignment.Right:
                        Function.Call(Hash.SET_TEXT_WRAP, relativePosition.X - realWrap, relativePosition.X);
                        break;
                }
            }
            else if (Alignment == Alignment.Right)
            {
                Function.Call(Hash.SET_TEXT_WRAP, 0f, relativePosition.X);
            }
#endif
        }
        /// <summary>
        /// Slices the string of text into appropiately saved chunks.
        /// </summary>
        private void Slice()
        {
            // If the entire text is under 90 bytes, save it as is and return
            if (Encoding.UTF8.GetByteCount(text) <= chunkSize)
            {
                chunks.Clear();
                chunks.Add(text);
                return;
            }

            // Create a new list of chunks and a temporary string
            List<string> newChunks = new List<string>();
            string temp = "";

            // Iterate over the characters in the string
            foreach (char character in text)
            {
                // Create a temporary string with the character
                string with = string.Concat(temp, character);
                // If this string is higher than 90 bytes, add the existing string onto the list
                if (Encoding.UTF8.GetByteCount(with) > chunkSize)
                {
                    newChunks.Add(temp);
                    temp = character.ToString();
                    continue;
                }
                // And save the new string generated
                temp = with;
            }

            // If after finishing we still have a piece, save it
            if (temp != "")
            {
                newChunks.Add(temp);
            }

            // Once we have finished, replace the old chunks
            chunks = newChunks;
        }
        /// <summary>
        /// Recalculates the size, position and word wrap of this item.
        /// </summary>
        public void Recalculate()
        {
            // Do the normal Size and Position recalculation
            relativePosition = absolutePosition.ToRelative();
            // And recalculate the word wrap if necessary
            if (internalWrap <= 0)
            {
                realWrap = 0;
            }
            else
            {
                realWrap = internalWrap.ToXRelative();
            }
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// Draws the text on the screen.
        /// </summary>
        public void Draw()
        {
#if FIVEM
            API.SetTextEntry("CELL_EMAIL_BCON");
#elif RPH
            NativeFunction.CallByHash<int>(0x25FBB336DF1804CB, "CELL_EMAIL_BCON");
#elif (SHVDN2 || SHVDN3)
            Function.Call((Hash)0x25FBB336DF1804CB, "CELL_EMAIL_BCON"); // _SET_TEXT_ENTRY on v2, BEGIN_TEXT_COMMAND_DISPLAY_TEXT on v3
#endif

            Add();

#if FIVEM
            API.DrawText(relativePosition.X, relativePosition.Y);
#elif RPH
            NativeFunction.CallByHash<int>(0xCD015E5BB0D96A57, relativePosition.X, relativePosition.Y);
#elif (SHVDN2 || SHVDN3)
            Function.Call((Hash)0xCD015E5BB0D96A57, relativePosition.X, relativePosition.Y); // _DRAW_TEXT on v2, END_TEXT_COMMAND_DISPLAY_TEXT on v3
#endif
        }

        #endregion
    }
}
