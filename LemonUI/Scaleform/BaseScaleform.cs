#if FIVEM
using CitizenFX.Core.Native;
#elif RAGEMP
using RAGE.Game;
#elif RPH
using Rage.Native;
#elif SHVDN3
using GTA.Native;
#endif
using System;

namespace LemonUI.Scaleform
{
    /// <summary>
    /// Represents a generic Scaleform object.
    /// </summary>
    public abstract class BaseScaleform : IScaleform
    {
        #region Private Fields

#if FIVEM || SHVDN3
        /// <summary>
        /// The ID of the scaleform.
        /// </summary>
        [Obsolete("Please use the Handle or Name properties and call the methods manually.", true)]
#endif
#if FIVEM
        protected CitizenFX.Core.Scaleform scaleform = new CitizenFX.Core.Scaleform("");
#elif SHVDN3
        protected GTA.Scaleform scaleform = new GTA.Scaleform("");
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
#elif RAGEMP
                return Invoker.Invoke<bool>(Natives.HasScaleformMovieLoaded, Handle);
#elif RPH
                return NativeFunction.CallByHash<bool>(0x85F01B8D5B90570E, Handle);
#elif SHVDN3
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

#if FIVEM
            Handle = API.RequestScaleformMovie(Name);
#elif RAGEMP
            Handle = Invoker.Invoke<int>(Natives.RequestScaleformMovie, Name);
#elif RPH
            Handle = NativeFunction.CallByHash<int>(0x11FE353CF9733E6F, Name);
#elif SHVDN3
            Handle = Function.Call<int>(Hash.REQUEST_SCALEFORM_MOVIE, Name);
#endif
        }

        #endregion

        #region Private Functions

        private void CallFunctionBase(string function, params object[] parameters)
        {
#if FIVEM
            API.BeginScaleformMovieMethod(Handle, function);
#elif RAGEMP
            Invoker.Invoke(0xF6E48914C7A8694E, Handle, function);
#elif RPH
            NativeFunction.CallByHash<int>(0xF6E48914C7A8694E, Handle, function);
#elif SHVDN3
            Function.Call((Hash)0xF6E48914C7A8694E, Handle, function);
#endif

            foreach (object obj in parameters)
            {
                if (obj is int objInt)
                {
#if FIVEM
                    API.ScaleformMovieMethodAddParamInt(objInt);
#elif RAGEMP
                    Invoker.Invoke(0xC3D0841A0CC546A6, objInt);
#elif RPH
                    NativeFunction.CallByHash<int>(0xC3D0841A0CC546A6, objInt);
#elif SHVDN3
                    Function.Call((Hash)0xC3D0841A0CC546A6, objInt);
#endif
                }
                else if (obj is string objString)
                {
#if FIVEM
                    API.BeginTextCommandScaleformString("STRING");
                    API.AddTextComponentSubstringPlayerName(objString);
                    API.EndTextCommandScaleformString();
#elif RAGEMP
                    Invoker.Invoke(Natives.BeginTextCommandScaleformString, "STRING");
                    Invoker.Invoke(Natives.AddTextComponentSubstringPlayerName, objString);
                    Invoker.Invoke(Natives.EndTextCommandScaleformString);
#elif RPH

                    NativeFunction.CallByHash<int>(0x80338406F3475E55, "STRING");
                    NativeFunction.CallByHash<int>(0x6C188BE134E074AA, objString);
                    NativeFunction.CallByHash<int>(0x362E2D3FE93A9959);
#elif SHVDN3
                    Function.Call((Hash)0x80338406F3475E55, "STRING");
                    Function.Call((Hash)0x6C188BE134E074AA, objString);
                    Function.Call((Hash)0x362E2D3FE93A9959);
#endif
                }
                else if (obj is float objFloat)
                {
#if FIVEM
                    API.ScaleformMovieMethodAddParamFloat(objFloat);
#elif RAGEMP
                    Invoker.Invoke(0xD69736AAE04DB51A, objFloat);
#elif RPH
                    NativeFunction.CallByHash<int>(0xD69736AAE04DB51A, objFloat);
#elif SHVDN3
                    Function.Call((Hash)0xD69736AAE04DB51A, objFloat);
#endif
                }
                else if (obj is double objDouble)
                {
#if FIVEM
                    API.ScaleformMovieMethodAddParamFloat((float)objDouble);
#elif RAGEMP
                    Invoker.Invoke(0xD69736AAE04DB51A, (float)objDouble);
#elif RPH
                    NativeFunction.CallByHash<int>(0xD69736AAE04DB51A, (float)objDouble);
#elif SHVDN3
                    Function.Call((Hash)0xD69736AAE04DB51A, (float)objDouble);
#endif
                }
                else if (obj is bool objBool)
                {
#if FIVEM
                    API.ScaleformMovieMethodAddParamBool(objBool);
#elif RAGEMP
                    Invoker.Invoke(0xC58424BA936EB458, objBool);
#elif RPH
                    NativeFunction.CallByHash<int>(0xC58424BA936EB458, objBool);
#elif SHVDN3
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
#elif RAGEMP
            return Invoker.Invoke<bool>(Natives._0x768FF8961BA904D6, id);
#elif RPH
            return NativeFunction.CallByHash<bool>(0x768FF8961BA904D6, id);
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
#elif RAGEMP
                return Invoker.Invoke<T>(0xE1E258829A885245, id);
#elif RPH
                return (T)NativeFunction.CallByHash(0xE1E258829A885245, typeof(string), id);
#elif SHVDN3
                return (T)(object)Function.Call<string>(Hash.GET_SCALEFORM_MOVIE_METHOD_RETURN_VALUE_STRING, id);
#endif
            }
            else if (typeof(T) == typeof(int))
            {
#if FIVEM
                return (T)(object)API.GetScaleformMovieMethodReturnValueInt(id);
#elif RAGEMP
                return Invoker.Invoke<T>(0x2DE7EFA66B906036, id);
#elif RPH
                return (T)(object)NativeFunction.CallByHash<int>(0x2DE7EFA66B906036, id);
#elif SHVDN3
                return (T)(object)Function.Call<int>(Hash.GET_SCALEFORM_MOVIE_METHOD_RETURN_VALUE_INT, id);
#endif
            }
            else if (typeof(T) == typeof(bool))
            {
#if FIVEM
                return (T)(object)API.GetScaleformMovieMethodReturnValueBool(id);
#elif RAGEMP
                return Invoker.Invoke<T>(0xD80A80346A45D761, id);
#elif RPH
                return (T)(object)NativeFunction.CallByHash<bool>(0xD80A80346A45D761, id);
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
#elif RAGEMP
            Invoker.Invoke(0xC6796A8FFA375E53);
#elif RPH
            NativeFunction.CallByHash<int>(0xC6796A8FFA375E53);
#elif SHVDN3
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
#elif RAGEMP
            return Invoker.Invoke<int>(0xC50AA39A577AF886);
#elif RPH
            return NativeFunction.CallByHash<int>(0xC50AA39A577AF886);
#elif SHVDN3
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
        public virtual void DrawFullScreen()
        {
            if (!Visible)
            {
                return;
            }
            Update();
#if FIVEM
            API.DrawScaleformMovieFullscreen(Handle, 255, 255, 255, 255, 0);
#elif RAGEMP
            Invoker.Invoke(Natives.DrawScaleformMovieFullscreen, 255, 255, 255, 255, 0);
#elif RPH
            NativeFunction.CallByHash<int>(0x0DF606929C105BE1, Handle, 255, 255, 255, 255, 0);
#elif SHVDN3
            Function.Call(Hash.DRAW_SCALEFORM_MOVIE_FULLSCREEN, Handle, 255, 255, 255, 255, 0);
#endif
        }
        /// <summary>
        /// Draws the scaleform full screen.
        /// </summary>
        public virtual void Draw() => DrawFullScreen();
        /// <summary>
        /// Draws the scaleform full screen.
        /// </summary>
        public virtual void Process() => DrawFullScreen();
        /// <summary>
        /// Marks the scaleform as no longer needed.
        /// </summary>
        public void Dispose()
        {
            int id = Handle;
#if FIVEM
            API.SetScaleformMovieAsNoLongerNeeded(ref id);
#elif RAGEMP
            Invoker.Invoke(Natives.SetScaleformMovieAsNoLongerNeeded, id);
#elif RPH
            NativeFunction.CallByHash<int>(0x1D132D614DD86811, new NativeArgument(id));
#elif SHVDN3
            Function.Call(Hash.SET_SCALEFORM_MOVIE_AS_NO_LONGER_NEEDED, new OutputArgument(id));
#endif
        }

        #endregion
    }
}
