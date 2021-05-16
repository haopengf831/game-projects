using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public abstract class CodeGeneratorBase
{
    public abstract string Generate(string className, Dictionary<string, object> dict);

    protected virtual string GetPropertyName(string key)
    {
        return Regex.Replace(key, "[.]", "_");
    }

    protected virtual string GetTypeName(Type type)
    {
        var typeCode = Type.GetTypeCode(type);
        switch (typeCode)
        {
            case TypeCode.String:
            {
                return "string";
            }
            case TypeCode.Boolean:
            {
                return "bool";
            }
            case TypeCode.SByte:
            {
                return "sbyte";
            }
            case TypeCode.Byte:
            {
                return "byte";
            }
            case TypeCode.Int16:
            {
                return "short";
            }
            case TypeCode.UInt16:
            {
                return "ushort";
            }
            case TypeCode.Int32:
            {
                return "int";
            }
            case TypeCode.UInt32:
            {
                return "uint";
            }
            case TypeCode.Int64:
            {
                return "long";
            }
            case TypeCode.UInt64:
            {
                return "ulong";
            }
            case TypeCode.Char:
            {
                return "char";
            }
            case TypeCode.Single:
            {
                return "float";
            }
            case TypeCode.Double:
            {
                return "double";
            }
            case TypeCode.Decimal:
            {
                return "decimal";
            }
            default:
                if (type.IsArray)
                {
                    if (type == typeof(string[]))
                    {
                        return "string[]";
                    }

                    if (type == typeof(bool[]))
                    {
                        return "bool[]";
                    }

                    if (type == typeof(sbyte[]))
                    {
                        return "sbyte[]";
                    }

                    if (type == typeof(byte[]))
                    {
                        return "byte[]";
                    }

                    if (type == typeof(short[]))
                    {
                        return "short[]";
                    }

                    if (type == typeof(ushort[]))
                    {
                        return "ushort[]";
                    }

                    if (type == typeof(int[]))
                    {
                        return "int[]";
                    }

                    if (type == typeof(uint[]))
                    {
                        return "uint[]";
                    }

                    if (type == typeof(long[]))
                    {
                        return "long[]";
                    }

                    if (type == typeof(ulong[]))
                    {
                        return "ulong[]";
                    }

                    if (type == typeof(char[]))
                    {
                        return "char[]";
                    }

                    if (type == typeof(float[]))
                    {
                        return "float[]";
                    }

                    if (type == typeof(double[]))
                    {
                        return "double[]";
                    }

                    if (type == typeof(decimal[]))
                    {
                        return "decimal[]";
                    }
                }

                return type.Name;
        }
    }
}