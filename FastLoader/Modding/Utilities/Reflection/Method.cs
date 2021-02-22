using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DummyLoader.Utilities
{
    public class Method : IMethod
    {
        public string Name { get; set; }
        public object ReturnType { get; set; }
        public Type[] Parameters { get; set; }
        public bool HasReturnType { get { return this.ReturnType != null; } }

        public bool HasParameters { get { return this.Parameters != null && this.Parameters.Length != 0; } }

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

        public MethodBase MethodBase { private get; set; }
        public object Instance { private get; set; }

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

        public void CallMethod(params object[] parameters)
        {
            MethodBase.Invoke(this.Instance, parameters);
        }
    }
}
