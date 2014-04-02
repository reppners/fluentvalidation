﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FluentValidation {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Strings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Strings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("FluentValidation.Strings", typeof(Strings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{0}&apos; value must be convertible to type &apos;{1}&apos;..
        /// </summary>
        internal static string Argument_ConvertStringFail {
            get {
                return ResourceManager.GetString("Argument_ConvertStringFail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{0}&apos; must have one or more elements..
        /// </summary>
        internal static string Argument_EmptyEnumerable {
            get {
                return ResourceManager.GetString("Argument_EmptyEnumerable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;String&apos; cannot be empty..
        /// </summary>
        internal static string Argument_EmptyString {
            get {
                return ResourceManager.GetString("Argument_EmptyString", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{0}&apos; was expected to not be empty..
        /// </summary>
        internal static string Argument_EmptyValue {
            get {
                return ResourceManager.GetString("Argument_EmptyValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{0}&apos; was expected to be empty..
        /// </summary>
        internal static string Argument_NotEmptyValue {
            get {
                return ResourceManager.GetString("Argument_NotEmptyValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{0}&apos; was expected to be null..
        /// </summary>
        internal static string Argument_NotNullValue {
            get {
                return ResourceManager.GetString("Argument_NotNullValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;String&apos; cannot contain only white space..
        /// </summary>
        internal static string Argument_WhiteSpaceString {
            get {
                return ResourceManager.GetString("Argument_WhiteSpaceString", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Multiple Exceptions Occurred..
        /// </summary>
        internal static string DefaultAggregateExceptionMessage {
            get {
                return ResourceManager.GetString("DefaultAggregateExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An internal error occurred. Please contact support..
        /// </summary>
        internal static string InternalExceptionMessage {
            get {
                return ResourceManager.GetString("InternalExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot find an instance of the &apos;{0}&apos; service..
        /// </summary>
        internal static string ServiceMissing {
            get {
                return ResourceManager.GetString("ServiceMissing", resourceCulture);
            }
        }
    }
}
