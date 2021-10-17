using System;
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
    /// Represents a generic Scaleform object.
    /// </summary>
    public abstract class BaseScaleform : IScaleform
    {
        #region Private Fields

#if (FIVEM || SHVDN2 || SHVDN3)
        /// <summary>
        /// The ID of the scaleform.
        /// </summary>
        [Obsolete("Please use the Handle or Name properties and call the methods manually.")]
#endif
#if FIVEM
        protected CitizenFX.Core.Scaleform scaleform = null;
#elif (SHVDN2 || SHVDN3)
        protected GTA.Scaleform scaleform = null;
#endif

        #endregion

        #region Public Properties

        /// <summary>
        /// The ID or Handle of the Scaleform.
        /// </summary>
        public int Handle { get; private set; }
        /// <summary>
        /// The Name of the Scaleform.
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// If the Scaleform should be visible or not.
        /// </summary>
        public bool Visible { get; set; }
        /// <summary>
        /// If the Scaleform is loaded or not.
        /// </summary>
        public bool IsLoaded
        {
            get
            {
#if FIVEM
                return API.HasScaleformMovieLoaded(Handle);
#elif RPH
                return NativeFunction.CallByHash<bool>(0x85F01B8D5B90570E, Handle);
#elif (SHVDN2 || SHVDN3)
                return Function.Call<bool>(Hash.HAS_SCALEFORM_MOVIE_LOADED, Handle);
#endif
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new Scaleform class with the specified Scaleform object name.
        /// </summary>
        /// <param name="sc">The Scalform object.</param>
        public BaseScaleform(string sc)
        {
            Name = sc;

            LoadScaleform();
        }

        #endregion

        #region Protected Functions

        /// <summary>
        /// Load a given Scaleform
        /// </summary>
        protected void LoadScaleform()
        {
#if FIVEM
            Handle = API.RequestScaleformMovie(Name);
#elif RPH
            Handle = NativeFunction.CallByHash<int>(0x11FE353CF9733E6F, Name);
#elif (SHVDN2 || SHVDN3)
            Handle = Function.Call<int>(Hash.REQUEST_SCALEFORM_MOVIE, Name);
#endif

#pragma warning disable CS0618 // Type or member is obsolete
#if FIVEM
            scaleform = new CitizenFX.Core.Scaleform(Name);
#elif (SHVDN2 || SHVDN3)
            scaleform = new GTA.Scaleform(Name);
#endif
#pragma warning restore CS0618 // Type or member is obsolete
        }

        #endregion

        #region Private Functions

        private void CallFunctionBase(string function, params object[] parameters)
        {
#if FIVEM
            API.BeginScaleformMovieMethod(Handle, function);
#elif (SHVDN2 || SHVDN3)
            Function.Call((Hash)0xF6E48914C7A8694E, Handle, function);
#endif

            foreach (object obj in parameters)
            {
                if (obj is int objInt)
                {
#if FIVEM
                    API.ScaleformMovieMethodAddParamInt(objInt);
#elif (SHVDN2 || SHVDN3)
                    Function.Call((Hash)0xC3D0841A0CC546A6, objInt);
#endif
                }
                else if (obj is string objString)
                {
#if FIVEM
                    API.BeginTextCommandScaleformString("STRING");
                    API.AddTextComponentSubstringPlayerName(objString);
                    API.EndTextCommandScaleformString();
#elif (SHVDN2 || SHVDN3)
                    Function.Call((Hash)0x80338406F3475E55, "STRING");
                    Function.Call((Hash)0x6C188BE134E074AA, objString);
                    Function.Call((Hash)0x362E2D3FE93A9959);
#endif
                }
                else if (obj is float objFloat)
                {
#if FIVEM
                    API.ScaleformMovieMethodAddParamFloat(objFloat);
#elif (SHVDN2 || SHVDN3)
                    Function.Call((Hash)0xD69736AAE04DB51A, objFloat);
#endif
                }
                else if (obj is double objDouble)
                {
#if FIVEM
                    API.ScaleformMovieMethodAddParamFloat((float)objDouble);
#elif (SHVDN2 || SHVDN3)
                    Function.Call((Hash)0xD69736AAE04DB51A, (float)objDouble);
#endif
                }
                else if (obj is bool objBool)
                {
#if FIVEM
                    API.ScaleformMovieMethodAddParamBool(objBool);
#elif (SHVDN2 || SHVDN3)
                    Function.Call((Hash)0xC58424BA936EB458, objBool);
#endif
                }
                else
                {
                    throw new ArgumentException($"Unexpected argument type {obj.GetType().Name}.", nameof(parameters));
                }
            }
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// Checks if the specified Scaleform Return Value is ready to be fetched.
        /// </summary>
        /// <param name="id">The Identifier of the Value.</param>
        public bool IsValueReady(int id)
        {
#if FIVEM
            return API.IsScaleformMovieMethodReturnValueReady(id);
#elif RPH
            return NativeFunction.CallByHash<bool>(0x768FF8961BA904D6, id);
#elif SHVDN2
            return Function.Call<bool>(Hash._0x768FF8961BA904D6, id);
#elif SHVDN3
            return Function.Call<bool>(Hash.IS_SCALEFORM_MOVIE_METHOD_RETURN_VALUE_READY, id);
#endif
        }
        /// <summary>
        /// Gets a specific value.
        /// </summary>
        /// <typeparam name="T">The type of value.</typeparam>
        /// <param name="id">The Identifier of the value.</param>
        /// <returns>The value returned by the native.</returns>
        public T GetValue<T>(int id)
        {
            if (typeof(T) == typeof(string))
            {
#if FIVEM
                return (T)(object)API.GetScaleformMovieMethodReturnValueString(id);
#elif RPH
                return (T)NativeFunction.CallByHash(0xE1E258829A885245, typeof(string), id);
#elif SHVDN2
                return (T)(object)Function.Call<string>(Hash._0xE1E258829A885245, id);
#elif SHVDN3
                return (T)(object)Function.Call<string>(Hash.GET_SCALEFORM_MOVIE_METHOD_RETURN_VALUE_STRING, id);
#endif
            }
            else if (typeof(T) == typeof(int))
            {
#if FIVEM
                return (T)(object)API.GetScaleformMovieMethodReturnValueInt(id);
#elif RPH
                return (T)(object)NativeFunction.CallByHash<int>(0x2DE7EFA66B906036, id);
#elif SHVDN2
                return (T)(object)Function.Call<int>(Hash._0x2DE7EFA66B906036, id);
#elif SHVDN3
                return (T)(object)Function.Call<int>(Hash.GET_SCALEFORM_MOVIE_METHOD_RETURN_VALUE_INT, id);
#endif
            }
            else if (typeof(T) == typeof(bool))
            {
#if FIVEM
                return (T)(object)API.GetScaleformMovieMethodReturnValueBool(id);
#elif RPH
                return (T)(object)NativeFunction.CallByHash<bool>(0xD80A80346A45D761, id);
#elif SHVDN2
                return (T)(object)Function.Call<bool>((Hash)0xD80A80346A45D761, id);
#elif SHVDN3
                return (T)(object)Function.Call<bool>(Hash._GET_SCALEFORM_MOVIE_METHOD_RETURN_VALUE_BOOL, id);
#endif
            }
            else
            {
                throw new InvalidOperationException($"Expected string, int or bool, got {typeof(T).Name}.");
            }
        }
        /// <summary>
        /// Calls a Scaleform function.
        /// </summary>
        /// <param name="function">The name of the function to call.</param>
        /// <param name="parameters">The parameters to pass.</param>
        public void CallFunction(string function, params object[] parameters)
        {
            CallFunctionBase(function, parameters);
#if FIVEM
            API.EndScaleformMovieMethod();
#elif RPH
            NativeFunction.CallByHash<int>(0xC6796A8FFA375E53);
#elif (SHVDN2 || SHVDN3)
            Function.Call((Hash)0xC6796A8FFA375E53);
#endif
        }
        /// <summary>
        /// Calls a Scaleform function with a return value.
        /// </summary>
        /// <param name="function">The name of the function to call.</param>
        /// <param name="parameters">The parameters to pass.</param>
        public int CallFunctionReturn(string function, params object[] parameters)
        {
            CallFunctionBase(function, parameters);
#if FIVEM
            return API.EndScaleformMovieMethodReturnValue();
#elif RPH
            return NativeFunction.CallByHash<int>(0xC50AA39A577AF886);
#elif (SHVDN2 || SHVDN3)
            return Function.Call<int>((Hash)0xC50AA39A577AF886);
#endif
        }
        /// <summary>
        /// Updates the parameters of the Scaleform.
        /// </summary>
        public abstract void Update();
        /// <summary>
        /// Draws the scaleform full screen.
        /// </summary>
        public void DrawFullScreen()
        {
            if (!Visible)
            {
                return;
            }
            Update();
#if FIVEM
            API.DrawScaleformMovieFullscreen(Handle, 255, 255, 255, 255, 0);
#elif RPH
            NativeFunction.CallByHash<int>(0x0DF606929C105BE1, Handle, 255, 255, 255, 255, 0);
#elif (SHVDN2 || SHVDN3)
            Function.Call(Hash.DRAW_SCALEFORM_MOVIE_FULLSCREEN, Handle, 255, 255, 255, 255, 0);
#endif
        }
        /// <summary>
        /// Draws the scaleform full screen.
        /// </summary>
        public void Draw() => DrawFullScreen();
        /// <summary>
        /// Draws the scaleform full screen.
        /// </summary>
        public void Process() => DrawFullScreen();
        /// <summary>
        /// Marks the scaleform as no longer needed.
        /// </summary>
        public void Dispose()
        {
            int id = Handle;
#if FIVEM
            API.SetScaleformMovieAsNoLongerNeeded(ref id);
#elif RPH
            NativeFunction.CallByHash<int>(0x1D132D614DD86811, new NativeArgument(id));
#elif (SHVDN2 || SHVDN3)
            Function.Call(Hash.SET_SCALEFORM_MOVIE_AS_NO_LONGER_NEEDED, new OutputArgument(id));
#endif
        }

        #endregion
    }
}
