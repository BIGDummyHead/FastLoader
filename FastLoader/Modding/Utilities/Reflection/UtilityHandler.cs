﻿using System;
using System.Reflection;

namespace FastandLow.Modding.Utilities
{
    /// <summary>
    /// Handles Extensions
    /// </summary>
    public static class UtilityHandler
    {
        /// <summary>
        /// Returns a Method
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="methodName"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public static Method GetMethod(this object obj, string methodName, params Type[] types)
        {
            if (string.IsNullOrEmpty(methodName) || obj == null) return null;

            Method methodReturn = new Method();
            MethodInfo methodInfo;

            if (types != null)
                methodInfo = obj.GetType().GetMethod(methodName, AllFlags, null, types, null);
            else
                methodInfo = obj.GetType().GetMethod(methodName, AllFlags);

            if (methodInfo == null)
                throw new Exception("Method Turned Null | Method Does Not Exist | Types Do Not Match");
            else
            {
                methodReturn.SetInfo(methodInfo, obj);
            }

            return methodReturn;
        }

        /// <summary>
        /// Returns a Field/Property
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="objName"></param>
        /// <returns></returns>
        public static Variable GetVariable(this object obj, string objName)
        {
            Variable variable = new Variable();


            if (obj.GetType().GetField(objName, AllFlags) != null)
            {
                var e = GetFieldInfo(obj, objName);
                var values = e.GetValue(obj);

                variable.Value = values;

                variable.Name = e.Name;

                variable.SetInfo(e, null, false, obj);
            }
            else
            {
                var e = GetPropertyInfo(obj, objName);
                var values = e.GetValue(obj, null);

                variable.Value = values;

                variable.Name = e.Name;

                variable.SetInfo(null, e, true, obj);
            }

            return variable;
        }

        #region Core
        private static PropertyInfo GetPropertyInfo(object obj, string propName)
        {
            return obj.GetType().GetProperty(propName, AllFlags);
        }

        private static FieldInfo GetFieldInfo(object obj, string fieldName)
        {
            return obj.GetType().GetField(fieldName, AllFlags);
        }

        /// <summary>
        /// All BindingFlags => (<see cref="BindingFlags"/>)(-1)
        /// </summary>
        public static BindingFlags AllFlags { get { return (BindingFlags)(-1); } }


        #endregion
    }
}
