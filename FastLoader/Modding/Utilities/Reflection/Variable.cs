using System.Reflection;

namespace FastandLow.Modding.Utilities
{
    public class Variable : IVariable
    {
        public object Instance { private get; set; }
        public FieldInfo ThisFieldInfo { private get; set; }
        public PropertyInfo ThisPropertyInfo { private get; set; }

        public string Name { get; set; }
        public bool IsProperty { get; set; }
        public bool IsField { get; set; }
        public object Value { get; set; }

        public T GetValue<T>()
        {
            return (T)Value;
        }

        public void SetValue<T>(T val)
        {
            if (IsProperty)
            {
                this.ThisPropertyInfo.SetValue(this.Instance, val, null);
            }
            else if (IsField)
            {
                this.ThisFieldInfo.SetValue(this.Instance, val);
            }
        }

        public void SetInfo(FieldInfo info, PropertyInfo pInfo, bool isProp, object instance)
        {
            Instance = instance;
            this.ThisFieldInfo = info;
            this.ThisPropertyInfo = pInfo;
            this.IsProperty = isProp;
            this.IsField = !IsProperty;
        }
    }
}
