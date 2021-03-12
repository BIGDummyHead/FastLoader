using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FastandLow.Modding.Utilities
{
    /// <summary>
    /// A method
    /// </summary>
    public class Method 
    {
        /// <summary>
        /// The name of the method
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The return type
        /// </summary>
        public object ReturnType { get; set; }
        /// <summary>
        /// Param Types
        /// </summary>
        public Type[] Parameters { get; set; }
        /// <summary>
        /// Does this have return types?
        /// </summary>
        public bool HasReturnType { get { return this.ReturnType != null; } }
        /// <summary>
        /// Does this have parameters
        /// </summary>
        public bool HasParameters { get { return this.Parameters != null && this.Parameters.Length != 0; } }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="match"></param>
        /// <returns></returns>
        public bool IsReturnType<T>(T match) where T : Type
        {
            bool res;
            if (this.ReturnType == null && match != null)
                res = false;
            else
            {
                Type typeMatch = match.GetType();
                res = (typeMatch == match);
            }

            return res;
        }
        /// <summary>
        /// The methodbase
        /// </summary>
        public MethodBase MethodBase { private get; set; }
        /// <summary>
        /// an Instance
        /// </summary>
        public object Instance { private get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="instance"></param>
        public void SetInfo(MethodInfo info, object instance)
        {
            this.Instance = instance;
            this.Name = info.Name;
            this.ReturnType = info.ReturnType;
            this.MethodBase = info;
            ParameterInfo[] infoP = info.GetParameters();
            Type[] param;

            if (infoP == null)
                param = null;
            else
            {
                IEnumerable<Type> enumerable = from p in infoP select p.ParameterType;
                param = enumerable?.ToArray<Type>();
            }

            this.Parameters = param;
        }

        /// <summary>
        /// Invoke the method
        /// </summary>
        /// <param name="parameters"></param>
        public object CallMethod(params object[] parameters)
        {
            return MethodBase.Invoke(this.Instance, parameters);
        }
    }
}
