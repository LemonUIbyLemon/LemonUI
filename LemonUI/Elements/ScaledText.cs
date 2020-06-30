#if FIVEM
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using Font = CitizenFX.Core.UI.Font;
#elif SHVDN2
using GTA;
using GTA.Native;
using Font = GTA.Font;
#elif SHVDN3
using GTA.UI;
using GTA.Native;
using Font = GTA.UI.Font;
#endif
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using LemonUI.Extensions;

namespace LemonUI.Elements
{
    /// <summary>
    /// A text string.
    /// </summary>
    public class ScaledText : BaseElement
    {
        #region Consistent Values

        /// <summary>
        /// The size of every chunk of text.
        /// </summary>
        private const int chunkSize = 90;

        #endregion

        #region Private Fields

        /// <summary>
        /// The raw string of text.
        /// </summary>
        private string text = "";
        /// <summary>
        /// The raw string split into equally sized strings.
        /// </summary>
        private List<string> chunks = new List<string>();
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
        /// The number of lines that this text uses.
        /// </summary>
        public int LineCount
        {
            get
            {
                // Start the string measuring
#if FIVEM
                API.BeginTextCommandLineCount("CELL_EMAIL_BCON");
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
#if FIVEM
                return API.GetTextScaleHeight(Scale, (int)Font);
#elif SHVDN2
                return Function.Call<float>(Hash._0xDB88A37483346780, Scale, (int)Font);
#elif SHVDN3
                return Function.Call<float>(Hash._GET_TEXT_SCALE_HEIGHT, Scale, (int)Font);
#endif
            }
        }
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
        public override Alignment Alignment
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
        public ScaledText(PointF pos, string text, float scale, Font font) : base(pos, SizeF.Empty)
        {
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
            API.SetTextFont((int)Font);
            API.SetTextScale(1f, Scale);
            API.SetTextColour(Color.R, Color.G, Color.B, Color.A);
            if (Shadow)
            {
                API.SetTextDropShadow();
            }
            if (Outline)
            {
                API.SetTextOutline();
            }
            switch (Alignment)
            {
                case Alignment.Center:
                    API.SetTextCentre(true);
                    break;
                case Alignment.Right:
                    API.SetTextRightJustify(true);
                    API.SetTextWrap(0f, relativePosition.X);
                    break;
            }
            if (WordWrap > 0)
            {
                API.SetTextWrap(relativePosition.X, realWrap);
            }
            API.SetTextEntry("CELL_EMAIL_BCON");
            foreach (string chunk in chunks)
            {
                API.AddTextComponentString(chunk);
            }
#else
            Function.Call(Hash.SET_TEXT_FONT, (int)Font);
            Function.Call(Hash.SET_TEXT_SCALE, 1f, Scale);
            Function.Call(Hash.SET_TEXT_COLOUR, Color.R, Color.G, Color.B, Color.A);
            if (Shadow)
            {
                Function.Call(Hash.SET_TEXT_DROP_SHADOW);
            }
            if (Outline)
            {
                Function.Call(Hash.SET_TEXT_OUTLINE);
            }
            switch (Alignment)
            {
                case Alignment.Center:
                    Function.Call(Hash.SET_TEXT_CENTRE, true);
                    break;
                case Alignment.Right:
                    Function.Call(Hash.SET_TEXT_RIGHT_JUSTIFY, true);
                    Function.Call(Hash.SET_TEXT_WRAP, 0f, relativePosition.X);
                    break;
            }
            if (WordWrap > 0)
            {
                Function.Call(Hash.SET_TEXT_WRAP, relativePosition.X, realWrap);
            }
            Function.Call((Hash)0x25FBB336DF1804CB, "CELL_EMAIL_BCON"); // _SET_TEXT_ENTRY on v2, BEGIN_TEXT_COMMAND_DISPLAY_TEXT on v3
            foreach (string chunk in chunks)
            {
                Function.Call((Hash)0x6C188BE134E074AA, chunk); // _ADD_TEXT_COMPONENT_STRING on v2, ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME on v3
            }
#endif
        }
        /// <summary>
        /// Slices the string of text into appropiately saved chunks.
        /// </summary>
        private void Slice()
        {
            // Start by getting the total number of bytes in the string
            int byteCount = Encoding.UTF8.GetByteCount(text);
            // And calculate the total number of slices that we need
            int chunkCount = (int)Math.Ceiling((float)byteCount / chunkSize);

            // If we only need one slice, add the entire text
            if (chunkCount == 1)
            {
                chunks.Clear();
                chunks.Add(text);
            }
            // Otherwise
            else
            {
                // Create a new list of chunks
                List<string> newChunks = new List<string>();
                // And get the entire string as UTF-8 bytes
                byte[] bytes = Encoding.UTF8.GetBytes(text);

                // And add the chunks that we need
                for (int i = 0; i < chunkCount; i++)
                {
                    // Calculate where we should start and how many we should grab
                    int start = i * chunkCount;
                    int count = Math.Min(chunkSize, byteCount - start);
                    // And snatch them as a string
                    string chunk = Encoding.UTF8.GetString(bytes, start, count);
                    // And add it to the temp list
                    newChunks.Add(chunk);
                }

                // Once we have finished, replace the old chunks
                chunks = newChunks;
            }
        }
        /// <summary>
        /// Recalculates the size, position and word wrap of this item.
        /// </summary>
        public override void Recalculate()
        {
            // Do the normal Size and Position recalculation
            base.Recalculate();
            // And recalculate the word wrap if necessary
            if (internalWrap <= 0)
            {
                realWrap = 0;
            }
            else
            {
                realWrap = (Position.X + internalWrap) / 1f.ToXAbsolute();
            }
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// Draws the text on the screen.
        /// </summary>
        public override void Draw()
        {
            Add();
#if FIVEM
            API.DrawText(relativePosition.X, relativePosition.Y);
#else
            Function.Call((Hash)0xCD015E5BB0D96A57, relativePosition.X, relativePosition.Y); // _DRAW_TEXT on v2, END_TEXT_COMMAND_DISPLAY_TEXT on v3
#endif
        }

        #endregion
    }
}
