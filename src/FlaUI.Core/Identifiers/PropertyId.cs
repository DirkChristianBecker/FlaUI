using System;
using FlaUI.Core.Conditions;

namespace FlaUI.Core.Identifiers
{
    /// <summary>
    /// A wrapper around the property ids
    /// </summary>
    public class PropertyId : ConvertibleIdentifierBase
    {
        /// <summary>
        /// Fixed PropertyId which is used for patterns that are not supported by the framework.
        /// </summary>
        public static readonly PropertyId NotSupportedByFramework = new PropertyId(-1, "Not supported");

        /// <summary>
        /// C-Tor
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public PropertyId(int id, string name)
            : base(id, name)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="convertMethod"></param>
        /// <returns></returns>
        public PropertyId SetConverter(Func<AutomationBase, object, object> convertMethod)
        {
            return SetConverter<PropertyId>(convertMethod);
        }

        /// <summary>
        /// Returns a condition for this property with the given value.
        /// </summary>
        public PropertyCondition GetCondition(object value)
        {
            return new PropertyCondition(this, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="automationType"></param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static PropertyId Register(AutomationType automationType, int id, string name)
        {
            return RegisterProperty(automationType, id, name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="automationType"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static PropertyId Find(AutomationType automationType, int id)
        {
            return FindProperty(automationType, id);
        }
    }
}
