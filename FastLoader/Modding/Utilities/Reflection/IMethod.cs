using System;
using System.Reflection;

namespace DummyLoader.Utilities
{
    public interface IMethod
    {
        bool HasParameters { get; }
        bool HasReturnType { get; }
        object Instance { set; }
        MethodBase MethodBase { set; }
        string Name { get; set; }
        Type[] Parameters { get; set; }
        object ReturnType { get; set; }

        void CallMethod(params object[] parameters);
        bool IsReturnType<T>(T type) where T : Type;
        void SetInfo(MethodInfo info, object instance);
    }
}
