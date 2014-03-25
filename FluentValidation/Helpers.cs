using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace FluentValidation
{
    static class Helpers
    {
        internal static string FormatError(string format, params object[] arguments)
        {
            return String.Format(CultureInfo.CurrentCulture, format, arguments);
        }

#if !NET35
        /// <summary>
        /// Trims away a given surrounding type, returning just the generic type argument,
        /// if the given type is in fact a generic type with just one type argument and
        /// the generic type matches a given wrapper type.  Otherwise, it returns the original type.
        /// </summary>
        /// <param name="type">The type to trim, or return unmodified.</param>
        /// <param name="wrapper">The SomeType&lt;&gt; generic type definition to trim away from <paramref name="type"/> if it is present.</param>
        /// <returns><paramref name="type"/>, if it is not a generic type instance of <paramref name="wrapper"/>; otherwise the type argument.</returns>
        internal static Type TrimGenericWrapper(Type type, Type wrapper)
        {
            Type[] typeArgs;
            if (type.IsGenericType && type.GetGenericTypeDefinition() == wrapper && (typeArgs = type.GetGenericArguments()).Length == 1)
            {
                return typeArgs[0];
            }
            else
            {
                return type;
            }
        }
#endif

#if NET35
        internal static bool IsEmptyOrWhiteSpace(this string value)
        {
            if (value.Length == 0) return true;

            var length = value.Length;

            for( int i = 0; i < length; i++)
            {
                if (!Char.IsWhiteSpace(value[i])) return false;
            }

            return true;
        }
#endif


        internal static bool IsEnumEmpty(this IEnumerable enumerable)
        {
            var collection = enumerable as ICollection;

            if (collection != null)
            {
                return collection.Count == 0;
            }
            else
            {
                IEnumerator enumerator = enumerable.GetEnumerator();

                using (enumerator as IDisposable)
                {
                    return !enumerator.MoveNext();
                }
            }
        }
    }
}
