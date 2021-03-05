using System.Reflection;

namespace FastandLow.Modding.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    public class Variable
    {
        /// <summary>
        /// 
        /// </summary>
        public object Instance { private get; set; }
        /// <summary>
        /// 
        /// </summary>
        public FieldInfo ThisFieldInfo { private get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// 
        public PropertyInfo ThisPropertyInfo { private get; set; }
        /// <summary>
        /// 
        /// </summary>

        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsProperty { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsField { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object Value { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetValue<T>()
        {
            return (T)Value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val"></param>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="pInfo"></param>
        /// <param name="isProp"></param>
        /// <param name="instance"></param>
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
