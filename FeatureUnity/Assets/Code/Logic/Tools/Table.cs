using System;
using System.Runtime.InteropServices;
using Code.Logic.Adapter;
using UnityEngine;

namespace Code.Logic.Tools
{
    public enum UnionValueType
    {
        Null,
        Bool,
        Int,
        Float,
        Double,
        String,
        Object,
        Table,
        ULong,
        UInt,
    }

    public struct TableUnionValue
    {
        #region Generic

        internal static class ValueCastImpl
        {
            private static class ValueCast<T>
            {
                internal static Func<TableUnionValue, T> toFunc;
                internal static Func<T, TableUnionValue> fromFunc;

                internal static TableUnionValue CastFrom(T value)
                {
                    return fromFunc.Invoke(value);
                }

                internal static T CastTo(TableUnionValue value)
                {
                    return toFunc.Invoke(value);
                }
            }

            static ValueCastImpl()
            {
                ValueCast<int>.fromFunc = value => { return new TableUnionValue(UnionValueType.Int, value); };
                ValueCast<int>.toFunc = value => { return value.CheckedInt(); };

                ValueCast<string>.fromFunc = value => { return new TableUnionValue(UnionValueType.String, value); };
                ValueCast<string>.toFunc = value => { return value.CheckedString(); };

                ValueCast<double>.fromFunc = value => { return new TableUnionValue(UnionValueType.Double, value); };
                ValueCast<double>.toFunc = value => { return value.CheckedDouble(); };

                ValueCast<float>.fromFunc = value => { return new TableUnionValue(UnionValueType.Float, value); };
                ValueCast<float>.toFunc = value => { return value.CheckedFloat(); };

                ValueCast<bool>.fromFunc = value => { return new TableUnionValue(UnionValueType.Bool, value); };
                ValueCast<bool>.toFunc = value => { return value.CheckedBool(); };

                ValueCast<ulong>.fromFunc = value => { return new TableUnionValue(UnionValueType.ULong, value); };
                ValueCast<ulong>.toFunc = value => { return value.CheckedULong(); };

                ValueCast<uint>.fromFunc = value => { return new TableUnionValue(UnionValueType.UInt, value); };
                ValueCast<uint>.toFunc = value => { return value.CheckedUInt(); };
            }

            internal static TableUnionValue CastFrom<T>(T value)
            {
                return ValueCast<T>.CastFrom(value);
            }

            internal static T CastTo<T>(TableUnionValue value)
            {
                return ValueCast<T>.CastTo(value);
            }
        }

        //需要通过反射进行类型识别，所以存在一定的运行时开销
        public static TableUnionValue CastFrom<T>(T value)
        {
            var type = typeof(T);
            //if (type.IsEnum)
            // {
            //     return ValueCastImpl.CastFrom(System.Runtime.CompilerServices.Unsafe.As<T, int>(ref value));
            // }

            if (!type.IsPrimitive && !type.IsEnum && type.IsValueType) //判断是结构体
            {
                SuperDebug.LogWarningFormat("It is not recommended to convert the struct to TableUnionValue");
                return new TableUnionValue(value);
            }

            if (type.IsClass)
            {
                return CastFromObject(value);
            }

            return ValueCastImpl.CastFrom(value);
        }

        //需要通过反射进行类型识别，所以存在一定的运行时开销
        public static T CastTo<T>(TableUnionValue value)
        {
            var type = typeof(T);
            if (type.IsPrimitive || type == typeof(string))
            {
                return ValueCastImpl.CastTo<T>(value);
            }

            // if (type.IsEnum)
            // {
            //     var intValue = value.CheckedInt();
            //     return System.Runtime.CompilerServices.Unsafe.As<int, T>(ref intValue);
            // }

            if (type.IsClass)
            {
                return (T)value._unionValue.objectValue;
                ;
            }

            if (!type.IsPrimitive && !type.IsEnum && type.IsValueType) //判断是结构体
            {
                SuperDebug.LogWarningFormat("It is not recommended to convert the TableUnionValue to struct");
                return (T)value._unionValue.objectValue;
            }

            return ValueCastImpl.CastTo<T>(value);
        }

        #endregion

        #region Property

        public static readonly TableUnionValue NullTableUnionValue = new TableUnionValue(UnionValueType.Null, 0);

        [StructLayout(LayoutKind.Explicit)]
        internal struct UnionValue
        {
            [FieldOffset(0)] public bool boolValue;
            [FieldOffset(0)] public float floatValue;
            [FieldOffset(0)] public int intValue;
            [FieldOffset(0)] public ulong ulongValue;
            [FieldOffset(0)] public double doubleValue;
            [FieldOffset(8)] public string stringValue;
            [FieldOffset(8)] public object objectValue;
            [FieldOffset(8)] public Table tableValue;

            public static implicit operator UnionValue(int value)
            {
                UnionValue unionValue = new UnionValue
                {
                    intValue = value
                };
                return unionValue;
            }

            public static implicit operator UnionValue(uint value)
            {
                UnionValue unionValue = new UnionValue
                {
                    intValue = (int)value
                };
                return unionValue;
            }

            public static implicit operator UnionValue(ulong value)
            {
                UnionValue unionValue = new UnionValue
                {
                    ulongValue = value
                };
                return unionValue;
            }

            public static implicit operator UnionValue(float value)
            {
                UnionValue unionValue = new UnionValue
                {
                    floatValue = value
                };
                return unionValue;
            }

            public static implicit operator UnionValue(double value)
            {
                UnionValue unionValue = new UnionValue
                {
                    doubleValue = value
                };
                return unionValue;
            }

            public static implicit operator UnionValue(string value)
            {
                UnionValue unionValue = new UnionValue
                {
                    stringValue = value
                };
                return unionValue;
            }

            public static implicit operator UnionValue(bool value)
            {
                UnionValue unionValue = new UnionValue
                {
                    boolValue = value
                };
                return unionValue;
            }

            public static UnionValue CastFromObject(object obj)
            {
                UnionValue value = new UnionValue
                {
                    objectValue = obj
                };
                return value;
            }
        }

        private const string NullString = "null";
        private UnionValue _unionValue;
        private UnionValueType _unionValueType;

        public UnionValueType UnionValueType
        {
            get { return _unionValueType; }
        }

        public bool IsNull
        {
            get { return _unionValueType == UnionValueType.Null; }
        }

        private TableUnionValue(UnionValueType type, UnionValue value)
        {
            _unionValue = value;
            _unionValueType = type;
        }

        #endregion

        #region Ctor

        public TableUnionValue(object value)
        {
            _unionValue = new UnionValue();
            _unionValueType = UnionValueType.Null;

            if (value == null)
            {
                return;
            }

            if (value is Table table)
            {
                _unionValue.tableValue = table;
                _unionValueType = UnionValueType.Table;
                return;
            }
            else
            {
                _unionValue.objectValue = value;
                _unionValueType = UnionValueType.Object;
                return;
            }
        }


        public static implicit operator TableUnionValue(int value)
        {
            return new TableUnionValue(UnionValueType.Int, value);
        }

        public static implicit operator TableUnionValue(bool value)
        {
            return new TableUnionValue(UnionValueType.Bool, value);
        }

        public static implicit operator TableUnionValue(double value)
        {
            return new TableUnionValue(UnionValueType.Double, value);
        }

        public static implicit operator TableUnionValue(float value)
        {
            return new TableUnionValue(UnionValueType.Float, value);
        }

        public static implicit operator TableUnionValue(Table value)
        {
            return new TableUnionValue(UnionValueType.Object, UnionValue.CastFromObject(value));
        }

        public static implicit operator TableUnionValue(string value)
        {
            if (value == null)
            {
                return new TableUnionValue();
            }

            return new TableUnionValue(UnionValueType.String, value);
        }

        public static TableUnionValue CastFromObject(object value)
        {
            return new TableUnionValue(value);
        }

        #endregion

        #region Interface

        //TODO::DoSomething
        public TableUnionValue this[TableUnionValue key]
        {
            get
            {
                if (_unionValueType == UnionValueType.Table)
                {
                    //return _unionValue.tableValue[key];
                }

                SuperDebug.LogError("table union value support indexer when type is table");
                return TableUnionValue.NullTableUnionValue;
            }
            set
            {
                if (_unionValueType == UnionValueType.Table)
                {
                    //_unionValue.tableValue[key] = value;
                }

                SuperDebug.LogError("table union value support indexer when type is table");
            }
        }

        public override string ToString()
        {
            switch (_unionValueType)
            {
                case UnionValueType.Null:
                {
                    return NullString;
                }
                case UnionValueType.Bool:
                {
                    return _unionValue.boolValue.ToString();
                }
                case UnionValueType.Int:
                {
                    return _unionValue.intValue.ToString();
                }
                case UnionValueType.String:
                {
                    return _unionValue.stringValue;
                }
                case UnionValueType.Float:
                {
                    return _unionValue.floatValue.ToString();
                }
                case UnionValueType.Double:
                {
                    return _unionValue.doubleValue.ToString();
                }
                case UnionValueType.Object:
                {
                    if (_unionValue.objectValue != null)
                    {
                        return _unionValue.objectValue.ToString();
                    }
                    else
                    {
                        return NullString;
                    }
                }
                default:
                {
                    return NullString;
                }
            }
        }

        public override int GetHashCode()
        {
            switch (_unionValueType)
            {
                case UnionValueType.Null:
                {
                    return 0;
                }
                case UnionValueType.Bool:
                {
                    return _unionValue.boolValue.GetHashCode();
                }
                case UnionValueType.Int:
                {
                    return _unionValue.intValue.GetHashCode();
                }
                case UnionValueType.String:
                {
                    return _unionValue.stringValue.GetHashCode();
                }
                case UnionValueType.Float:
                {
                    return _unionValue.floatValue.GetHashCode();
                }
                case UnionValueType.Double:
                {
                    return _unionValue.doubleValue.GetHashCode();
                }
                case UnionValueType.Object:
                {
                    if (_unionValue.objectValue != null)
                    {
                        return _unionValue.objectValue.GetHashCode();
                    }
                    else
                    {
                        return 0;
                    }
                }
                default:
                {
                    return 0;
                }
            }
        }

        public bool UnionEuqals(in TableUnionValue value)
        {
            if (_unionValueType != value._unionValueType)
            {
                return false;
            }

            if (!GetHashCode().Equals(value.GetHashCode()))
            {
                return false;
            }

            switch (_unionValueType)
            {
                case UnionValueType.Bool:
                {
                    return _unionValue.boolValue == value._unionValue.boolValue;
                }
                case UnionValueType.Int:
                {
                    return _unionValue.intValue == value._unionValue.intValue;
                }
                case UnionValueType.Float:
                {
                    return Mathf.Approximately(_unionValue.floatValue, value._unionValue.floatValue);
                }
                case UnionValueType.Double:
                {
                    return Math.Abs(_unionValue.doubleValue - value._unionValue.doubleValue) < 0.0001f;
                }
                case UnionValueType.String:
                {
                    return _unionValue.stringValue == value._unionValue.stringValue;
                }
                case UnionValueType.Object:
                {
                    return _unionValue.objectValue == value._unionValue.objectValue;
                }
                default:
                {
                    return false;
                }
            }
        }

        public bool UnionEuqals(object value)
        {
            if (value == null)
            {
                return _unionValueType == UnionValueType.Null;
            }

            return value == _unionValue.objectValue;
        }

        public bool UnionEuqals(int value)
        {
            if (_unionValueType != UnionValueType.Int)
            {
                return false;
            }

            return value == _unionValue.intValue;
        }


        public bool UnionEuqals(bool value)
        {
            if (_unionValueType != UnionValueType.Bool)
            {
                return false;
            }

            return value == _unionValue.boolValue;
        }

        public bool UnionEuqals(string value)
        {
            if (_unionValueType != UnionValueType.String)
            {
                return false;
            }

            return value == _unionValue.stringValue;
        }

        public bool UnionEuqals(float value)
        {
            if (_unionValueType != UnionValueType.Float)
            {
                return false;
            }

            return Mathf.Approximately(value, _unionValue.floatValue);
        }

        public bool UnionEuqals(double value)
        {
            if (_unionValueType != UnionValueType.Double)
            {
                return false;
            }

            return Math.Abs(value - _unionValue.doubleValue) < 0.0001f;
        }

        #endregion

        #region Utils

        [System.Diagnostics.Contracts.Pure]
        public int CheckedInt()
        {
            if (_unionValueType != UnionValueType.Int)
            {
                SuperDebug.Log($"Value type mismatching , value type is {_unionValueType} but target is {UnionValueType.Int}");
                return 0;
            }

            return _unionValue.intValue;
        }

        [System.Diagnostics.Contracts.Pure]
        public ulong CheckedULong()
        {
            if (_unionValueType != UnionValueType.ULong)
            {
                SuperDebug.Log($"Value type mismatching , value type is {_unionValueType} but target is {UnionValueType.Int}");
                return 0;
            }

            return _unionValue.ulongValue;
        }

        [System.Diagnostics.Contracts.Pure]
        public uint CheckedUInt()
        {
            if (_unionValueType != UnionValueType.UInt)
            {
                SuperDebug.Log($"Value type mismatching , value type is {_unionValueType} but target is {UnionValueType.Int}");
                return 0;
            }

            return (uint)_unionValue.intValue;
        }

        [System.Diagnostics.Contracts.Pure]
        public bool CheckedBool()
        {
            if (_unionValueType != UnionValueType.Bool)
            {
                SuperDebug.Log($"Value type mismatching , value type is {_unionValueType} but target is {UnionValueType.Bool}");
                return false;
            }

            return _unionValue.boolValue;
        }

        [System.Diagnostics.Contracts.Pure]
        public float CheckedFloat()
        {
            if (_unionValueType != UnionValueType.Float)
            {
                SuperDebug.Log($"Value type mismatching , value type is {_unionValueType} but target is {UnionValueType.Float}");
                return 0.0f;
            }

            return _unionValue.floatValue;
        }

        [System.Diagnostics.Contracts.Pure]
        public double CheckedDouble()
        {
            if (_unionValueType != UnionValueType.Double)
            {
                SuperDebug.Log($"Value type mismatching , value type is {_unionValueType} but target is {UnionValueType.Double}");
                return 0.0;
            }

            return _unionValue.doubleValue;
        }


        [System.Diagnostics.Contracts.Pure]
        public string CheckedString()
        {
            if (_unionValueType != UnionValueType.String)
            {
                SuperDebug.Log($"Value type mismatching , value type is {_unionValueType} but target is {UnionValueType.String}");
                return string.Empty;
            }

            return _unionValue.stringValue;
        }

        public T CheckedObject<T>() where T : class
        {
            if (_unionValueType == UnionValueType.Null)
            {
                return null;
            }

            if (_unionValueType != UnionValueType.Object)
            {
                SuperDebug.Log($"Value type mismatching , value type is {_unionValueType} but target is {UnionValueType.Object}");
                return null;
            }

            return _unionValue.objectValue as T;
        }

        /// <summary>
        /// 进行安全检查，但允许返回空值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T CastObject<T>() where T : class
        {
            if (_unionValueType != UnionValueType.Object)
            {
                return null;
            }

            return _unionValue.objectValue as T;
        }

        #endregion
    }

    public class Table
    {
    }
}