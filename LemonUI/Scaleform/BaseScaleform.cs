#if FIVEMV2
using CitizenFX.FiveM;
using CitizenFX.FiveM.Native;
#elif FIVEM
using CitizenFX.Core;
using CitizenFX.Core.Native;
using System.Threading.Tasks;
#elif RAGEMP
using RAGE.Game;
#elif ALTV
using AltV.Net.Client;
#elif RPH
using Rage.Native;
#elif SHVDN3 || SHVDNC
using GTA;
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
        #region Fields

#if FIVEM || SHVDN3 || FIVEMV2
        /// <summary>
        /// The ID of the scaleform.
        /// </summary>
        [Obsolete("Please use the Handle or Name properties and call the methods manually.", true)]
#endif
#if FIVEMV2
        protected CitizenFX.FiveM.Scaleform scaleform = new CitizenFX.FiveM.Scaleform(string.Empty);
#elif FIVEM
        protected CitizenFX.Core.Scaleform scaleform = new CitizenFX.Core.Scaleform(string.Empty);
#elif SHVDN3
        protected GTA.Scaleform scaleform = new GTA.Scaleform(string.Empty);
#endif

        #endregion

        #region Properties

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
        public virtual bool Visible { get; set; }
        /// <summary>
        /// If the Scaleform is loaded or not.
        /// </summary>
        public bool IsLoaded
        {
            get
            {
#if FIVEMV2
                return Natives.HasScaleformMovieLoaded(Handle);
#elif FIVEM
                return API.HasScaleformMovieLoaded(Handle);
#elif ALTV
                return Alt.Natives.HasScaleformMovieLoaded(Handle);
#elif RAGEMP
                return Invoker.Invoke<bool>(Natives.HasScaleformMovieLoaded, Handle);
#elif RPH
                return NativeFunction.CallByHash<bool>(0x85F01B8D5B90570E, Handle);
#elif SHVDN3 || SHVDNC
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
            Name = sc ?? throw new ArgumentNullException(nameof(sc));

#if FIVEMV2
            Handle = Natives.RequestScaleformMovie(Name);
#elif FIVEM
            Handle = API.RequestScaleformMovie(Name);
#elif ALTV
            Handle = Alt.Natives.RequestScaleformMovie(Name);
#elif RAGEMP
            Handle = Invoker.Invoke<int>(Natives.RequestScaleformMovie, Name);
#elif RPH
            Handle = NativeFunction.CallByHash<int>(0x11FE353CF9733E6F, Name);
#elif SHVDN3 || SHVDNC
            Handle = Function.Call<int>(Hash.REQUEST_SCALEFORM_MOVIE, Name);
#endif
        }

        #endregion

        #region Tools

        private void CallFunctionBase(string function, params object[] parameters)
        {
            if (function == null)
            {
                throw new ArgumentNullException(nameof(function), "The function name is null.");
            }

            if (string.IsNullOrWhiteSpace(function))
            {
                throw new ArgumentOutOfRangeException(nameof(function), "The function name is empty or white space.");
            }
#if FIVEMV2
            Natives.BeginScaleformMovieMethod(Handle, function);
#elif FIVEM
            API.BeginScaleformMovieMethod(Handle, function);
#elif ALTV
            Alt.Natives.BeginScaleformMovieMethod(Handle, function);
#elif RAGEMP
            Invoker.Invoke(0xF6E48914C7A8694E, Handle, function);
#elif RPH
            NativeFunction.CallByHash<int>(0xF6E48914C7A8694E, Handle, function);
#elif SHVDN3 || SHVDNC
            Function.Call((Hash)0xF6E48914C7A8694E, Handle, function);
#endif

            foreach (object obj in parameters)
            {
                if (obj == null)
                {
                    throw new ArgumentNullException(nameof(parameters), "Unexpected null function argument in parameters.");
                }
                else if (obj is int objInt)
                {
#if FIVEMV2
                    Natives.ScaleformMovieMethodAddParamInt(objInt);
#elif FIVEM
                    API.ScaleformMovieMethodAddParamInt(objInt);
#elif ALTV
                    Alt.Natives.ScaleformMovieMethodAddParamInt(objInt);
#elif RAGEMP
                    Invoker.Invoke(0xC3D0841A0CC546A6, objInt);
#elif RPH
                    NativeFunction.CallByHash<int>(0xC3D0841A0CC546A6, objInt);
#elif SHVDN3 || SHVDNC
                    Function.Call((Hash)0xC3D0841A0CC546A6, objInt);
#endif
                }
                else if (obj is string objString)
                {
#if FIVEMV2
                    Natives.BeginTextCommandScaleformString("STRING");
                    Natives.AddTextComponentSubstringPlayerName(objString);
                    Natives.EndTextCommandScaleformString();
#elif FIVEM
                    API.BeginTextCommandScaleformString("STRING");
                    API.AddTextComponentSubstringPlayerName(objString);
                    API.EndTextCommandScaleformString();
#elif ALTV
                    Alt.Natives.BeginTextCommandScaleformString("STRING");
                    Alt.Natives.AddTextComponentSubstringPlayerName(objString);
                    Alt.Natives.EndTextCommandScaleformString();
#elif RAGEMP
                    Invoker.Invoke(Natives.BeginTextCommandScaleformString, "STRING");
                    Invoker.Invoke(Natives.AddTextComponentSubstringPlayerName, objString);
                    Invoker.Invoke(Natives.EndTextCommandScaleformString);
#elif RPH

                    NativeFunction.CallByHash<int>(0x80338406F3475E55, "STRING");
                    NativeFunction.CallByHash<int>(0x6C188BE134E074AA, objString);
                    NativeFunction.CallByHash<int>(0x362E2D3FE93A9959);
#elif SHVDN3 || SHVDNC
                    Function.Call((Hash)0x80338406F3475E55, "STRING");
                    Function.Call((Hash)0x6C188BE134E074AA, objString);
                    Function.Call((Hash)0x362E2D3FE93A9959);
#endif
                }
                else if (obj is float objFloat)
                {
#if FIVEMV2
                    Natives.ScaleformMovieMethodAddParamFloat(objFloat);
#elif FIVEM
                    API.ScaleformMovieMethodAddParamFloat(objFloat);
#elif ALTV
                    Alt.Natives.ScaleformMovieMethodAddParamFloat(objFloat);
#elif RAGEMP
                    Invoker.Invoke(0xD69736AAE04DB51A, objFloat);
#elif RPH
                    NativeFunction.CallByHash<int>(0xD69736AAE04DB51A, objFloat);
#elif SHVDN3 || SHVDNC
                    Function.Call((Hash)0xD69736AAE04DB51A, objFloat);
#endif
                }
                else if (obj is double objDouble)
                {
#if FIVEMV2
                    Natives.ScaleformMovieMethodAddParamFloat((float)objDouble);
#elif FIVEM
                    API.ScaleformMovieMethodAddParamFloat((float)objDouble);
#elif ALTV
                    Alt.Natives.ScaleformMovieMethodAddParamFloat((float)objDouble);
#elif RAGEMP
                    Invoker.Invoke(0xD69736AAE04DB51A, (float)objDouble);
#elif RPH
                    NativeFunction.CallByHash<int>(0xD69736AAE04DB51A, (float)objDouble);
#elif SHVDN3 || SHVDNC
                    Function.Call((Hash)0xD69736AAE04DB51A, (float)objDouble);
#endif
                }
                else if (obj is bool objBool)
                {
#if FIVEMV2
                    Natives.ScaleformMovieMethodAddParamBool(objBool);
#elif FIVEM
                    API.ScaleformMovieMethodAddParamBool(objBool);
#elif ALTV
                    Alt.Natives.ScaleformMovieMethodAddParamBool(objBool);
#elif RAGEMP
                    Invoker.Invoke(0xC58424BA936EB458, objBool);
#elif RPH
                    NativeFunction.CallByHash<int>(0xC58424BA936EB458, objBool);
#elif SHVDN3 || SHVDNC
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

        #region Functions

        /// <summary>
        /// Checks if the specified Scaleform Return Value is ready to be fetched.
        /// </summary>
        /// <param name="id">The Identifier of the Value.</param>
        /// <returns><see langword="true"/> if the value is ready, <see langword="false"/> otherwise.</returns>
        public bool IsValueReady(int id)
        {
#if FIVEMV2
            return Natives.IsScaleformMovieMethodReturnValueReady(id);
#elif FIVEM
            return API.IsScaleformMovieMethodReturnValueReady(id);
#elif ALTV
            return Alt.Natives.IsScaleformMovieMethodReturnValueReady(id);
#elif RAGEMP
            return Invoker.Invoke<bool>(Natives._0x768FF8961BA904D6, id);
#elif RPH
            return NativeFunction.CallByHash<bool>(0x768FF8961BA904D6, id);
#elif SHVDN3 || SHVDNC
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
#if FIVEMV2
                return (T)(object)Natives.GetScaleformMovieMethodReturnValueString(id).ToString();
#elif FIVEM
                return (T)(object)API.GetScaleformMovieMethodReturnValueString(id);
#elif ALTV
                return (T)(object)Alt.Natives.GetScaleformMovieMethodReturnValueString(id);
#elif RAGEMP
                return Invoker.Invoke<T>(0xE1E258829A885245, id);
#elif RPH
                return (T)NativeFunction.CallByHash(0xE1E258829A885245, typeof(string), id);
#elif SHVDN3 || SHVDNC
                return (T)(object)Function.Call<string>(Hash.GET_SCALEFORM_MOVIE_METHOD_RETURN_VALUE_STRING, id);
#endif
            }
            else if (typeof(T) == typeof(int))
            {
#if FIVEMV2
                return (T)(object)Natives.GetScaleformMovieMethodReturnValueInt(id);
#elif FIVEM
                return (T)(object)API.GetScaleformMovieMethodReturnValueInt(id);
#elif ALTV
                return (T)(object)Alt.Natives.GetScaleformMovieMethodReturnValueInt(id);
#elif RAGEMP
                return Invoker.Invoke<T>(0x2DE7EFA66B906036, id);
#elif RPH
                return (T)(object)NativeFunction.CallByHash<int>(0x2DE7EFA66B906036, id);
#elif SHVDN3 || SHVDNC
                return (T)(object)Function.Call<int>(Hash.GET_SCALEFORM_MOVIE_METHOD_RETURN_VALUE_INT, id);
#endif
            }
            else if (typeof(T) == typeof(bool))
            {
#if FIVEMV2
                return (T)(object)Natives.GetScaleformMovieMethodReturnValueBool(id);
#elif FIVEM
                return (T)(object)API.GetScaleformMovieMethodReturnValueBool(id);
#elif ALTV
                return (T)(object)Alt.Natives.GetScaleformMovieMethodReturnValueBool(id);
#elif RAGEMP
                return Invoker.Invoke<T>(0xD80A80346A45D761, id);
#elif RPH
                return (T)(object)NativeFunction.CallByHash<bool>(0xD80A80346A45D761, id);
#elif SHVDN3 || SHVDNC
                return (T)(object)Function.Call<bool>(Hash.GET_SCALEFORM_MOVIE_METHOD_RETURN_VALUE_BOOL, id);
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
#if FIVEMV2
            Natives.EndScaleformMovieMethod();
#elif FIVEM
            API.EndScaleformMovieMethod();
#elif ALTV
            Alt.Natives.EndScaleformMovieMethod();
#elif RAGEMP
            Invoker.Invoke(0xC6796A8FFA375E53);
#elif RPH
            NativeFunction.CallByHash<int>(0xC6796A8FFA375E53);
#elif SHVDN3 || SHVDNC
            Function.Call((Hash)0xC6796A8FFA375E53);
#endif
        }
#if FIVEM || SHVDN3 || SHVDNC || FIVEMV2
        /// <summary>
        /// Calls a scaleform function and gets it's return value as soon as is available. 
        /// </summary>
        /// <param name="function">The function to call.</param>
        /// <param name="parameters">The parameters to call the function with.</param>
        /// <typeparam name="T">The type of return parameter.</typeparam>
        /// <returns>The value returned by the function.</returns>
        #if FIVEM
        public async Task<T> CallFunction<T>(string function, params object[] parameters)
        #else
        public T CallFunction<T>(string function, params object[] parameters)
        #endif
        {
            int id = CallFunctionReturn(function, parameters);

            while (!IsValueReady(id))
            {
                #if FIVEM
                await BaseScript.Delay(0);
                #elif SHVDN3 || SHVDNC
                Script.Yield();
                #endif
            }

            return GetValue<T>(id);
        }
#endif
        /// <summary>
        /// Calls a Scaleform function with a return value.
        /// </summary>
        /// <param name="function">The name of the function to call.</param>
        /// <param name="parameters">The parameters to pass.</param>
        public int CallFunctionReturn(string function, params object[] parameters)
        {
            CallFunctionBase(function, parameters);
#if FIVEMV2
            return Natives.EndScaleformMovieMethodReturnValue();
#elif FIVEM
            return API.EndScaleformMovieMethodReturnValue();
#elif ALTV
            return Alt.Natives.EndScaleformMovieMethodReturnValue();
#elif RAGEMP
            return Invoker.Invoke<int>(0xC50AA39A577AF886);
#elif RPH
            return NativeFunction.CallByHash<int>(0xC50AA39A577AF886);
#elif SHVDN3 || SHVDNC
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
#if FIVEMV2
            Natives.DrawScaleformMovieFullscreen(Handle, 255, 255, 255, 255, 0);
#elif FIVEM
            API.DrawScaleformMovieFullscreen(Handle, 255, 255, 255, 255, 0);
#elif ALTV
            Alt.Natives.DrawScaleformMovieFullscreen(Handle, 255, 255, 255, 255, 0);
#elif RAGEMP
            Invoker.Invoke(Natives.DrawScaleformMovieFullscreen, Handle, 255, 255, 255, 255, 0);
#elif RPH
            NativeFunction.CallByHash<int>(0x0DF606929C105BE1, Handle, 255, 255, 255, 255, 0);
#elif SHVDN3 || SHVDNC
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
#if FIVEMV2
            Natives.SetScaleformMovieAsNoLongerNeeded(ref id);
#elif FIVEM
            API.SetScaleformMovieAsNoLongerNeeded(ref id);
#elif ALTV
            Alt.Natives.SetScaleformMovieAsNoLongerNeeded(ref id);
#elif RAGEMP
            IntReference idPtr = new IntReference(id);
            Invoker.Invoke(Natives.SetScaleformMovieAsNoLongerNeeded, idPtr);
#elif RPH
            using (NativePointer idPtr = new NativePointer(4))
            {
                idPtr.SetValue(id);
                NativeFunction.CallByHash<int>(0x6DD8F5AA635EB4B2, idPtr);
            }
#elif SHVDN3 || SHVDNC
            unsafe
            {
                Function.Call(Hash.SET_SCALEFORM_MOVIE_AS_NO_LONGER_NEEDED, &id);
            }
#endif
        }

        #endregion
    }
}
