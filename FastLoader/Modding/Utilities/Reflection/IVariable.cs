using System.Reflection;

namespace FastandLow.Modding.Utilities
{
    public interface IVariable
    {
        object Instance { set; }
        bool IsField { get; set; }
        bool IsProperty { get; set; }
        string Name { get; set; }
        FieldInfo ThisFieldInfo { set; }
        PropertyInfo ThisPropertyInfo { set; }
        object Value { get; set; }

        T GetValue<T>();
        void SetInfo(FieldInfo info, PropertyInfo pInfo, bool isProp, object instance);
        void SetValue<T>(T val);
    }
}
