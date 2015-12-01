using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsMvvm.ExtensionMethods
{

    /// <summary>
    /// Klasse mit Erweiterungsmethoden
    /// </summary>
    public static class StringExtensions
    {


        /// <summary>
        /// Adds quotes to a string.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>The quoted string</returns>
        public static string AddQuotes(this string str)
        {
            return '"' + str + '"';
        }


        /// <summary>
        /// Gets the description from value.
        /// </summary>
        /// <param name="value">The enum value</param>
        /// <returns></returns>
        public static string GetDescriptionFromValue(this Enum value)
        {
            string output = null;
            Type type = value.GetType();

            //Look for our 'StringValueAttribute' 
            //in the field's custom attributes
            FieldInfo fi = type.GetField(value.ToString());
            var attrs =
               fi.GetCustomAttributes(typeof(DescriptionAttribute),
                                       false) as DescriptionAttribute[];
            if (attrs.Length > 0)
            {
                output = attrs[0].Description;
            }

            return output;
        }

        /// <summary>
        /// Gets the value from description.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <exception cref="System.ArgumentException">Not found.;description</exception>
        public static T GetValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException("Not found.", "description");
            // or return default(T);
        }

        /// <summary>
        /// COnverts from one Enum type To another.
        /// </summary>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <param name="inp">The inp.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Input not Enum</exception>
        public static T2 ToEnum<T2>(this Enum inp)
        {
            if (!(inp is Enum))
            {
                throw new ArgumentException("Input not Enum");
            }
            string val = inp.ToString();
            Type x = inp.GetType();
            T2 erg = (T2)Enum.Parse(typeof(T2), val);
            return erg;
        }

    }
}
