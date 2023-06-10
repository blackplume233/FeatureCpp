using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Logic.Tools
{
    public interface IParamContext
    {
        // public float GetParamfloat(string name);
        // public string GetParamString(string name);
        // public int GetParamInt(string name);
        // public bool GetParamBool(string name);
        TableUnionValue GetParam(string name);
    }


    public interface IExpressionVal
    {
        UnionValueType GetRetType();
        TableUnionValue Execute(IParamContext paramContext);
        bool Vaildity();
    }

    public enum ExpressionValType
    {
        Float = UnionValueType.Float,
        String = UnionValueType.String,
        Int = UnionValueType.Int,
        Bool = UnionValueType.Float,
        InVaild = UnionValueType.Null
    }

    [Serializable]
    public class FixedExpressionVal : IExpressionVal
    {
        public TableUnionValue Value { get; }

        public FixedExpressionVal(TableUnionValue value)
        {
            Value = value;
        }

        public UnionValueType GetRetType()
        {
            return Value.UnionValueType;
        }

        public TableUnionValue Execute(IParamContext paramContext)
        {
            return Value;
        }

        public bool Vaildity()
        {
            return true;
        }
    }

    [Serializable]
    public class RefExpressionValue : IExpressionVal
    {
        public UnionValueType Type { get; }
        public string RefName;
        public ExpressionValType ExpressionRetValType;

        public RefExpressionValue(string refName, ExpressionValType type)
        {
            RefName = refName;
            ExpressionRetValType = type;
        }

        public UnionValueType GetRetType()
        {
            return (UnionValueType)ExpressionRetValType;
        }

        public TableUnionValue Execute(IParamContext paramContext)
        {
            return paramContext.GetParam(RefName);
        }

        public bool Vaildity()
        {
            return true;
        }
    }

    [Serializable]
    public abstract class BinaryExpressionVal : IExpressionVal
    {
        public virtual UnionValueType GetRetType()
        {
            return UnionValueType.Null;
        }

        public virtual TableUnionValue Execute(IParamContext paramContext)
        {
            return TableUnionValue.NullTableUnionValue;
        }

        public bool Vaildity()
        {
            return false;
        }

        public IExpressionVal Left;
        public IExpressionVal Right;
    }

    [Serializable]
    public class BracketExpressionVal : IExpressionVal
    {
        public ExpressionValType expressionRetValType;

        public UnionValueType GetRetType()
        {
            return (UnionValueType)expressionRetValType;
        }

        public TableUnionValue Execute(IParamContext paramContext)
        {
            return TableUnionValue.NullTableUnionValue;
        }

        public bool Vaildity()
        {
            if (ExpressionVal == null)
            {
                return false;
            }

            foreach (var expressionVal in ExpressionVal)
            {
                if (!expressionVal.Vaildity())
                {
                    return false;
                }

                if ((ExpressionValType)expressionVal.GetRetType() != expressionRetValType)
                {
                    return false;
                }
            }

            return true;
        }

        [SerializeField] public IExpressionVal[] ExpressionVal = null;
    }

    [Serializable]
    public class CompareExpressionVal : BinaryExpressionVal
    {
        public enum CompareType
        {
            Equal,
            NotEqual,
            Greater,
            Less,
            GreaterEqual,
            LessEqual
        }

        public CompareType compareType;

        public CompareExpressionVal(IExpressionVal left, IExpressionVal right, CompareType type)
        {
            Left = left;
            Right = right;
            compareType = type;
        }

        public override UnionValueType GetRetType()
        {
            return UnionValueType.Bool;
        }

        public override TableUnionValue Execute(IParamContext paramContext)
        {
            var type = Left.GetRetType();
            switch (type)
            {
                case UnionValueType.Float:
                    return CompareFloat(paramContext);
                case UnionValueType.Bool:
                    return CompareBool(paramContext);
                case UnionValueType.String:
                    return CompareString(paramContext);
                case UnionValueType.Int:
                    return CompareInt(paramContext);
                default:
                    return false;
            }
        }

        public bool CompareInt(IParamContext paramContext)
        {
            const float tolerance = 0.00001f;
            var left = Left.Execute(paramContext).CheckedInt();
            var right = Right.Execute(paramContext).CheckedInt();
            switch (compareType)
            {
                case CompareType.Equal:
                    return Math.Abs(left - right) < tolerance;
                case CompareType.NotEqual:
                    return Math.Abs(left - right) > tolerance;
                case CompareType.Greater:
                    return left > right;
                case CompareType.Less:
                    return left < right;
                case CompareType.GreaterEqual:
                    return left >= right;
                case CompareType.LessEqual:
                    return left <= right;
            }

            return false;
        }

        public bool CompareString(IParamContext paramContext)
        {
            var left = Left.Execute(paramContext).CheckedString();
            var right = Right.Execute(paramContext).CheckedString();
            switch (compareType)
            {
                case CompareType.Equal:
                    return left == right;
                case CompareType.NotEqual:
                    return left != right;
            }

            return false;
        }

        public bool CompareBool(IParamContext paramContext)
        {
            var left = Left.Execute(paramContext).CheckedBool();
            var right = Right.Execute(paramContext).CheckedBool();
            switch (compareType)
            {
                case CompareType.Equal:
                    return left == right;
                case CompareType.NotEqual:
                    return left != right;
            }

            return false;
        }

        public bool CompareFloat(IParamContext paramContext)
        {
            const float tolerance = 0.00001f;
            var left = Left.Execute(paramContext).CheckedFloat();
            var right = Right.Execute(paramContext).CheckedFloat();
            switch (compareType)
            {
                case CompareType.Equal:
                    return Math.Abs(left - right) < tolerance;
                case CompareType.NotEqual:
                    return Math.Abs(left - right) > tolerance;
                case CompareType.Greater:
                    return left > right;
                case CompareType.Less:
                    return left < right;
                case CompareType.GreaterEqual:
                    return left >= right;
                case CompareType.LessEqual:
                    return left <= right;
            }

            return false;
        }
    }


    [Serializable]
    public class ConditionExpression : IExpressionVal
    {
        public IExpressionVal Condition;
        public bool isNot;

        public ConditionExpression(IExpressionVal condition, bool isNot = false)
        {
            Condition = condition;
            this.isNot = isNot;
        }

        public ConditionExpression()
        {
            Condition = null;
        }

        public UnionValueType GetRetType()
        {
            return UnionValueType.Bool;
        }

        public TableUnionValue Execute(IParamContext paramContext)
        {
            return Condition.Execute(paramContext).CheckedBool() != isNot;
        }

        public bool Vaildity()
        {
            return Condition != null && Condition.Vaildity() && Condition.GetRetType() == UnionValueType.Bool;
        }
    }

    [Serializable]
    public class OrExpressionVal : BinaryExpressionVal
    {
        public OrExpressionVal(IExpressionVal left, IExpressionVal right)
        {
            Left = left;
            Right = right;
        }

        public override UnionValueType GetRetType()
        {
            return UnionValueType.Bool;
        }

        public override TableUnionValue Execute(IParamContext paramContext)
        {
            var left = Left.Execute(paramContext).CheckedBool();
            var right = Right.Execute(paramContext).CheckedBool();
            return left && right;
        }
    }

    [Serializable]
    public class AndExpressionVal : BinaryExpressionVal
    {
        public AndExpressionVal(IExpressionVal left, IExpressionVal right)
        {
            Left = left;
            Right = right;
        }

        public override UnionValueType GetRetType()
        {
            return UnionValueType.Bool;
        }

        public override TableUnionValue Execute(IParamContext paramContext)
        {
            var left = Left.Execute(paramContext).CheckedBool();
            var right = Right.Execute(paramContext).CheckedBool();
            return left && right;
        }
    }

    [Serializable]
    public class MathExpressionVal : BinaryExpressionVal
    {
        public enum MathOpType
        {
            Add,
            Sub,
            Mul,
            Div,
            Mod
        }

        public MathOpType mathOpType;

        public MathExpressionVal(MathOpType type, IExpressionVal left, IExpressionVal right)
        {
            mathOpType = type;
            Left = left;
            Right = right;
        }

        public override UnionValueType GetRetType()
        {
            return Left.GetRetType();
        }

        public override TableUnionValue Execute(IParamContext paramContext)
        {
            if (Left.GetRetType() == UnionValueType.Float)
            {
                switch (mathOpType)
                {
                    case MathOpType.Add:
                        return Left.Execute(paramContext).CheckedFloat() + Right.Execute(paramContext).CheckedFloat();
                    case MathOpType.Sub:
                        return Left.Execute(paramContext).CheckedFloat() - Right.Execute(paramContext).CheckedFloat();
                    case MathOpType.Div:
                        return Left.Execute(paramContext).CheckedFloat() / Right.Execute(paramContext).CheckedFloat();
                    case MathOpType.Mul:
                        return Left.Execute(paramContext).CheckedFloat() * Right.Execute(paramContext).CheckedFloat();
                    case MathOpType.Mod:
                        return Left.Execute(paramContext).CheckedFloat() % Right.Execute(paramContext).CheckedFloat();
                }
            }
            else if (GetRetType() == UnionValueType.Int)
            {
                switch (mathOpType)
                {
                    case MathOpType.Add:
                        return Left.Execute(paramContext).CheckedInt() + Right.Execute(paramContext).CheckedInt();
                    case MathOpType.Sub:
                        return Left.Execute(paramContext).CheckedInt() - Right.Execute(paramContext).CheckedInt();
                    case MathOpType.Div:
                        return Left.Execute(paramContext).CheckedInt() / Right.Execute(paramContext).CheckedInt();
                    case MathOpType.Mul:
                        return Left.Execute(paramContext).CheckedInt() * Right.Execute(paramContext).CheckedInt();
                    case MathOpType.Mod:
                        return Left.Execute(paramContext).CheckedInt() % Right.Execute(paramContext).CheckedInt();
                }
            }

            return 0;
        }
    }
}