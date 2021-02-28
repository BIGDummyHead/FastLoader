using System.Reflection;

namespace FastandLow.Modding.Utilities
{
    /// <summary>
    /// A Field or Property
    /// </summary>
    public interface IVariable
    {
        /// <summary>
        /// Instance of the type
        /// </summary>
        object Instance { set; }
        /// <summary>
        /// Is this a field
        /// </summary>
        bool IsField { get; set; }
        /// <summary>
        /// Is this a property?
        /// </summary>
        bool IsProperty { get; set; }
        /// <summary>
        /// Name of the Field/Property
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// FieldInfo null if not field
        /// </summary>
        FieldInfo ThisFieldInfo { set; }
        /// <summary>
        /// PropertyInfo null if not property
        /// </summary>
        PropertyInfo ThisPropertyInfo { set; }
        /// <summary>
        /// The value
        /// </summary>
        object Value { get; set; }

        /// <summary>
        /// Get the value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetValue<T>();
        /// <summary>
        /// Sets all data
        /// </summary>
        /// <param name="info"></param>
        /// <param name="pInfo"></param>
        /// <param name="isProp"></param>
        /// <param name="instance"></param>
        void SetInfo(FieldInfo info, PropertyInfo pInfo, bool isProp, object instance);
        /// <summary>
        /// Set the value for the field/property
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val"></param>
        void SetValue<T>(T val);
    }
}
