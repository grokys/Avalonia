using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Avalonia.Utils
{
    internal static partial class Helper
    {

        class ConverterKey
        {
            public ICustomAttributeProvider Info;
            public Type Type;

            public override int GetHashCode()
            {
                return Type.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                var x = (ConverterKey)obj;
                return Info == x.Info && Type == x.Type;
            }
        }

        internal static CultureInfo DefaultCulture = CultureInfo.GetCultureInfo("en-US");
        static Dictionary<Type, Func<TypeConverter>> classTypeConverters = new Dictionary<Type, Func<TypeConverter>>();
        static Dictionary<ConverterKey, Func<TypeConverter>> cachedConverters = new Dictionary<ConverterKey, Func<TypeConverter>>();

        public static bool AreEqual(object x, object y)
        {
            return AreEqual(null, x, y);
        }

        public static bool AreEqual(Type type, object x, object y)
        {
            if (x == null)
                return y == null;
            if (y == null)
                return false;

            if (type == null)
            {
                if (x.GetType().IsValueType || x.GetType() == typeof(string))
                    return x.Equals(y);
            }
            else if (type.IsValueType)
            {
                return x.Equals(y);
            }

            return x == y;
        }

        public static TypeConverter GetConverterFor(ICustomAttributeProvider info, Type target_type)
        {
            var x = GetConverterCreatorFor(info, target_type);
            return x == null ? null : x();
        }

        public static Func<TypeConverter> GetConverterCreatorFor(ICustomAttributeProvider info, Type target_type)
        {
            Func<TypeConverter> creator;
            var converterKey = new ConverterKey { Info = info, Type = target_type };
            if (cachedConverters.TryGetValue(converterKey, out creator))
                return creator;

            TypeConverter converter = null;
            Attribute[] attrs;
            TypeConverterAttribute at = null;
            Type t = null;

            // first check for a TypeConverter attribute on the property
            if (info != null)
            {
                try
                {
                    attrs = (Attribute[])info.GetCustomAttributes(true);
                    foreach (Attribute attr in attrs)
                    {
                        if (attr is TypeConverterAttribute)
                        {
                            at = (TypeConverterAttribute)attr;
                            break;
                        }
                    }
                }
                catch (Exception)
                {
                }
            }

            if (at == null)
            {
                // we didn't find one on the property.
                // check for one on the Type.
                try
                {
                    attrs = (Attribute[])target_type.GetCustomAttributes(true);
                    foreach (Attribute attr in attrs)
                    {
                        if (attr is TypeConverterAttribute)
                        {
                            at = (TypeConverterAttribute)attr;
                            break;
                        }
                    }
                }
                catch (Exception)
                {
                }
            }

            if (at == null)
            {
                if (target_type == typeof(bool?))
                {
                    t = typeof(NullableBoolConverter);
                }
                else
                {
                    cachedConverters.Add(converterKey, null);
                    return null;
                }
            }
            else
            {
                t = Type.GetType(at.ConverterTypeName);
            }

            if (t == null || !typeof(TypeConverter).IsAssignableFrom(t))
            {
                cachedConverters.Add(converterKey, null);
                return null;
            }

            ConstructorInfo ci = t.GetConstructor(new Type[] { typeof(Type) });

            if (ci != null)
            {
                creator = System.Linq.Expressions.Expression.Lambda<Func<TypeConverter>>(
                    System.Linq.Expressions.Expression.New(ci,
                    System.Linq.Expressions.Expression.Constant(target_type))).Compile();
            }
            else
                creator = System.Linq.Expressions.Expression.Lambda<Func<TypeConverter>>(
                    System.Linq.Expressions.Expression.New(t)).Compile();

            cachedConverters.Add(converterKey, creator);
            return creator;
        }

        // This method checks for a class level TypeConverter and returns it
        public static TypeConverter GetConverterFor(Type type)
        {
            var x = GetConverterCreatorFor(type);
            return x == null ? null : x();
        }

        // This method checks for a class level TypeConverter and returns it
        public static Func<TypeConverter> GetConverterCreatorFor(Type type)
        {
            if (type == null)
                return null;

            Func<TypeConverter> creator;
            if (classTypeConverters.TryGetValue(type, out creator))
                return creator;

            string converterTypeName = null;
            Type converterType = null;
            foreach (Attribute attr in type.GetCustomAttributes(true))
            {
                if (attr is TypeConverterAttribute)
                {
                    converterTypeName = ((TypeConverterAttribute)attr).ConverterTypeName;
                    break;
                }
            }

            if (!string.IsNullOrEmpty(converterTypeName))
                converterType = Type.GetType(converterTypeName);

            if (converterType == null)
            {
                creator = null;
            }
            else
            {
                creator = System.Linq.Expressions.Expression.Lambda<Func<TypeConverter>>(
                    System.Linq.Expressions.Expression.New(converterType)).Compile();
            }
            classTypeConverters.Add(type, creator);
            return creator;
        }

        public static IntPtr StreamToIntPtr(Stream stream)
        {
            byte[] buffer = new byte[1024];
            IntPtr buf = Marshal.AllocHGlobal((int)stream.Length);
            int ofs = 0;
            int nread = 0;

            if (stream.CanSeek && stream.Position != 0)
                stream.Seek(0, SeekOrigin.Begin);

            do
            {
                nread = stream.Read(buffer, 0, 1024);
                Marshal.Copy(buffer, 0, (IntPtr)(((long)buf) + ofs), nread);
                ofs += nread;
            } while (nread != 0);

            return buf;
        }

        static string CanonicalizeFileName(string filename, bool is_xap_mode)
        {
            StringBuilder result = new StringBuilder(filename.Length);
            string extension;
            string append = null;

            if (is_xap_mode)
            {
                extension = Path.GetExtension(filename).ToLower();
                if (extension == ".dll" || extension == ".exe" || extension == ".mdb")
                {
                    // Note: We don't want to change the casing of the dll's
                    // basename since Mono requires the basename to match the
                    // expected case.

                    int path_sep = filename.LastIndexOfAny(new char[] { '\\', '/' });
                    if (path_sep >= 0)
                    {
                        append = filename.Substring(path_sep + 1);
                    }
                    else
                    {
                        append = filename;
                    }
                    filename = filename.Substring(0, filename.Length - append.Length);
                }
            }

            CanonicalizeName(result, filename, filename.Length);

            if (append != null)
                result.Append(append);

            return result.ToString();
        }

        static void CanonicalizeName(StringBuilder sb, string str, int n)
        {
            // Fix path separators and capitalization
            for (int i = 0; i < n; i++)
            {
                if (str[i] != '\\')
                    sb.Append(Char.ToLower(str[i], CultureInfo.InvariantCulture));
                else
                    sb.Append('/');
            }
        }

        public static string CanonicalizeAssemblyPath(string path)
        {
            StringBuilder sb = new StringBuilder(path.Length);
            int i;

            for (i = path.Length; i > 0; i--)
            {
                if (path[i - 1] == '/' || path[i - 1] == '\\')
                    break;
            }

            CanonicalizeName(sb, path, i);

            sb.Append(path, i, path.Length - i);

            return sb.ToString();
        }

        public static string CanonicalizeResourceName(string resource)
        {
            StringBuilder sb = new StringBuilder(resource.Length);

            CanonicalizeName(sb, resource, resource.Length);

            return sb.ToString();
        }
    }
}
