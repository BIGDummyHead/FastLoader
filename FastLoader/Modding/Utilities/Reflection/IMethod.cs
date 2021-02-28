using System;
using System.Reflection;

namespace FastandLow.Modding.Utilities
{
    /// <summary>
    /// Interface for a method
    /// </summary>
    public interface IMethod
    {
        /// <summary>
        /// does the method have params
        /// </summary>
        bool HasParameters { get; }
        /// <summary>
        /// does the method have a return type?
        /// </summary>
        bool HasReturnType { get; }

        /// <summary>
        /// an instance of the type
        /// </summary>
        object Instance { set; }
        /// <summary>
        /// Method Base
        /// </summary>
        MethodBase MethodBase { set; }
        /// <summary>
        /// Name of the Method
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Params
        /// </summary>
        Type[] Parameters { get; set; }
        /// <summary>
        /// Return Type
        /// </summary>
        object ReturnType { get; set; }

        /// <summary>
        /// Invoke A Method
        /// </summary>
        /// <param name="parameters">Values to invoke the method with</param>
        void CallMethod(params object[] parameters);

        /// <summary>
        /// Check if the return type matches with <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        bool IsReturnType<T>(T type) where T : Type;

        /// <summary>
        /// Set the info for the method, helps setup your type
        /// </summary>
        /// <param name="info"></param>
        /// <param name="instance"></param>
        void SetInfo(MethodInfo info, object instance);
    }
}
