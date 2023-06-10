using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.Logic.Adapter;

namespace Code.Logic.Tools
{
    public class ExpressionToken
    {
        public enum PriorityType
        {
            Never,
            Level1,
            Level2,
            Level3,
            Immediate,
        }

        public enum ExpressionTokenType
        {
            FixedValue,
            Identifier,
            Func,
            Variable,
            Operator,
            TwoOperator
        }

        public SubString TokenStr;
        public ExpressionTokenType TokenType;
        public IExpressionVal BoundVal = null;

        //Prepare for future use
        public PriorityType Priority;
        public int RightParamCount;
        public int LeftParamCount;

        public bool IsReady
        {
            get { return BoundVal != null; }
        }

        public ExpressionParserFunctionDefine ParserFunctionDefine;

        private ExpressionToken(SubString tokenStr, ExpressionTokenType tokenType)
        {
            TokenStr = tokenStr;
            TokenType = tokenType;
        }

        public static ExpressionToken CreateToken(SubString tokenStr, ExpressionTokenType tokenType)
        {
            return new ExpressionToken(tokenStr, tokenType);
        }
    }

    public class TokenStack<T>
    {
        protected List<T> _list;
        protected int stackBottom;
        public int Count => _list.Count - stackBottom;

        public void Push(T item)
        {
            _list.Add(item);
        }

        public T Pop()
        {
            if (Count == 0)
            {
                throw new Exception("Stack is empty");
            }

            var ret = _list[_list.Count - 1];
            _list.RemoveAt(_list.Count - 1);
            return ret;
        }

        public T Peek()
        {
            if (Count == 0)
            {
                throw new Exception("Stack is empty");
            }

            return _list[_list.Count - 1];
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException();
                }

                index = stackBottom + index;
                return _list[index];
            }
        }

        public TokenStack(TokenStack<T> parent)
        {
            _list = parent._list;
            stackBottom = _list.Count;
        }

        public TokenStack() : this(0)
        {
        }

        public TokenStack(int capacity)
        {
            _list = new List<T>(capacity);
            stackBottom = 0;
        }
    }

    public class ExpressionParserFunctionDefine
    {
        public string FunctionName;
        public int LeftParamCount;
        public int RightParamCount;
        public ExpressionToken.PriorityType Priority;
        public Func<List<ExpressionToken>, IExpressionVal> Function;

        public virtual void PrepareToken(ExpressionToken token)
        {
            token.TokenType = ExpressionToken.ExpressionTokenType.Func;
            token.Priority = Priority;
            token.LeftParamCount = LeftParamCount;
            token.RightParamCount = RightParamCount;
            token.ParserFunctionDefine = this;
        }

        public virtual void ResolveToken(ExpressionToken token, List<ExpressionToken> tokens)
        {
            token.BoundVal = Function?.Invoke(tokens);
        }
    }

    public class ExpressionParser
    {
        public string IdentifierChars = "abcdefghijklmnopqrstuvwxyz§$" + "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public string NumberChars = "0123456789.";
        public string OperatorChars = "+-*/%&|!";

        //private Stack<ExpressionToken> TokenStacks = new Stack<ExpressionToken>();
        private Dictionary<string, ExpressionParserFunctionDefine> _functionDefineDict = new Dictionary<string, ExpressionParserFunctionDefine>()
        {
        };

        public IExpressionVal Parse(string expression)
        {
            expression = expression.Trim();
            TokenStack<ExpressionToken> tokenStacks = new TokenStack<ExpressionToken>();
            ParseInternal(new SubString(expression, 0, expression.Length), tokenStacks);
            //return tokenStacks[0].;
            for (int i = 0; i < tokenStacks.Count; i++)
            {
                SuperDebug.Log($"{tokenStacks[i].TokenStr.DebugStr} {tokenStacks[i].BoundVal}, {tokenStacks[0].BoundVal.Execute(null)}");
            }

            tokenStacks[0].BoundVal.Execute(null);
            return new BracketExpressionVal();
        }

        void ParseInternal(SubString expression, TokenStack<ExpressionToken> tokenStacks)
        {
            int curIdx = 0;
            while (curIdx < expression.Length)
            {
                //TODO::处理括号的逻辑
                if (expression[curIdx] == '(')
                {
                    int close = FindClosingBracket(expression, curIdx, '(', ')');
                    if (close == -1)
                    {
                        throw new Exception("Expression is invalid, no closing bracket");
                    }

                    var subExpression = expression.Sub(curIdx + 1, close - curIdx - 1);
                    var subTokenStacks = new TokenStack<ExpressionToken>(tokenStacks);
                    ParseInternal(subExpression, subTokenStacks);
                    curIdx = close + 1;
                    continue;
                }

                curIdx += ParseToken(tokenStacks, expression.Sub(curIdx, expression.Length - curIdx));
            }

            for (int i = tokenStacks.Count - 1; i >= 0; i--)
            {
                PrepareResolveToken(tokenStacks[i]);
            }

            //ResloveToken
            Stack<ExpressionToken> tokenCache = new Stack<ExpressionToken>(tokenStacks.Count);
            ExpressionToken.PriorityType priorityType = ExpressionToken.PriorityType.Level3;
            List<ExpressionToken> tokenParams = new List<ExpressionToken>();
            while (priorityType > ExpressionToken.PriorityType.Never)
            {
                while (tokenStacks.Count > 0)
                {
                    tokenCache.Push(tokenStacks.Pop());
                }

                while (tokenCache.Count > 0)
                {
                    var token = tokenCache.Pop();
                    if (token.Priority != priorityType || token.IsReady)
                    {
                        tokenStacks.Push(token);
                        continue;
                    }

                    var leftParamCount = token.LeftParamCount;
                    var rightParamCount = token.RightParamCount;
                    if (tokenCache.Count < rightParamCount || tokenStacks.Count < leftParamCount)
                    {
                        throw new Exception($"Expression is invalid Param Count is not enough {token.TokenStr.ToString()}");
                    }

                    ;
                    //准备ResloveToken的参数
                    tokenParams.Clear();
                    for (int i = 0; i < leftParamCount; i++)
                    {
                        tokenParams.Add(tokenStacks.Pop());
                    }

                    for (int i = 0; i < rightParamCount; i++)
                    {
                        tokenParams.Add(tokenCache.Pop());
                    }

                    ResloveToken(token, tokenParams);
                    tokenStacks.Push(token);
                }

                priorityType--;
            }


            return;
        }

        int ParseToken(TokenStack<ExpressionToken> tokenStacks, SubString expression)
        {
            if (expression.Length == 0)
            {
                return 0;
            }

            var curChar = expression.SafeGet(0);
            var nextChar = expression.SafeGet(1);
            if (expression[0] == '(')
            {
                return 1;
            }
            else if (expression[0] == ')')
            {
                return 1;
            }
            else if (expression[0] == ',')
            {
                return 1;
            }
            else if (expression[0] == ' ')
            {
                return 1;
            }
            else if (OperatorChars.Contains(expression[0]))
            {
                tokenStacks.Push(ExpressionToken.CreateToken(expression.Sub(0, 1), ExpressionToken.ExpressionTokenType.Operator));
                return 1;
            }
            else if (curChar == '=')
            {
                tokenStacks.Push(ExpressionToken.CreateToken(expression.Sub(0, 1), ExpressionToken.ExpressionTokenType.Operator));
                return 1;
            }
            //处理双字符运算符
            else if (curChar == '>' || curChar == '<')
            {
                if (nextChar == '=')
                {
                    tokenStacks.Push(ExpressionToken.CreateToken(expression.Sub(0, 2), ExpressionToken.ExpressionTokenType.TwoOperator));
                    return 2;
                }

                tokenStacks.Push(ExpressionToken.CreateToken(expression.Sub(0, 1), ExpressionToken.ExpressionTokenType.Operator));
                return 1;
            }
            else if (NumberChars.Contains(expression[0]))
            {
                int i = 1;
                for (; i < expression.Length; i++)
                {
                    if (!NumberChars.Contains(expression[i]))
                    {
                        break;
                    }
                }

                tokenStacks.Push(ExpressionToken.CreateToken(expression.Sub(0, i), ExpressionToken.ExpressionTokenType.FixedValue));
                return i;
            }
            else if (IdentifierChars.Contains(expression[0]))
            {
                int i = 1;
                for (; i < expression.Length; i++)
                {
                    if (!IdentifierChars.Contains(expression[i]))
                    {
                        break;
                    }
                }

                tokenStacks.Push(ExpressionToken.CreateToken(expression.Sub(0, i), ExpressionToken.ExpressionTokenType.Identifier));
                return i;
            }
            else
            {
                throw new Exception("Unknow token");
            }

            return 0;
        }

        void PrepareResolveToken(ExpressionToken token)
        {
            if (token.IsReady)
            {
                return;
            }

            if (token.TokenType == ExpressionToken.ExpressionTokenType.FixedValue)
            {
                token.Priority = ExpressionToken.PriorityType.Never;
                token.LeftParamCount = 0;
                token.RightParamCount = 0;

                var valStr = token.TokenStr.ToString();
                if (int.TryParse(valStr, out var intVal))
                {
                    token.BoundVal = new FixedExpressionVal(intVal);
                }
                else if (float.TryParse(valStr, out var floatVal))
                {
                    token.BoundVal = new FixedExpressionVal(floatVal);
                }
            }

            if (token.TokenType == ExpressionToken.ExpressionTokenType.Operator)
            {
                token.LeftParamCount = 1;
                token.RightParamCount = 1;

                var opStr = token.TokenStr[0];
                switch (opStr)
                {
                    case '>':
                        token.Priority = ExpressionToken.PriorityType.Level1;
                        break;
                    case '<':
                        token.Priority = ExpressionToken.PriorityType.Level1;
                        break;
                    case '=':
                        token.Priority = ExpressionToken.PriorityType.Level1;
                        break;
                    case '+':
                        token.Priority = ExpressionToken.PriorityType.Level1;
                        break;
                    case '-':
                        token.Priority = ExpressionToken.PriorityType.Level1;
                        break;
                    case '*':
                        token.Priority = ExpressionToken.PriorityType.Level2;
                        break;
                    case '/':
                        token.Priority = ExpressionToken.PriorityType.Level2;
                        break;
                    case '%':
                        token.Priority = ExpressionToken.PriorityType.Level2;
                        break;
                    case '&':
                        token.Priority = ExpressionToken.PriorityType.Level3;
                        break;
                    case '|':
                        token.Priority = ExpressionToken.PriorityType.Level3;
                        break;
                    case '!':
                        token.LeftParamCount = 0;
                        token.Priority = ExpressionToken.PriorityType.Level3;
                        break;
                }
            }

            if (token.TokenType == ExpressionToken.ExpressionTokenType.TwoOperator)
            {
                token.LeftParamCount = 1;
                token.RightParamCount = 1;
                var opStr = token.TokenStr[0];
                switch (opStr)
                {
                    case '>':
                        token.Priority = ExpressionToken.PriorityType.Level1;
                        break;
                    case '<':
                        token.Priority = ExpressionToken.PriorityType.Level1;
                        break;
                    case '=':
                        token.Priority = ExpressionToken.PriorityType.Level1;
                        break;
                }
            }

            if (token.TokenType == ExpressionToken.ExpressionTokenType.Identifier)
            {
                if (_functionDefineDict.TryGetValue(token.TokenStr.ToString(), out var funcDefine))
                {
                    funcDefine.PrepareToken(token);
                }
                else
                {
                    PrepareVariableToken(token);
                }
            }
        }

        void PrepareVariableToken(ExpressionToken token)
        {
            token.Priority = ExpressionToken.PriorityType.Never;
            token.LeftParamCount = 0;
            token.RightParamCount = 0;
            token.TokenType = ExpressionToken.ExpressionTokenType.Variable;
            token.BoundVal = new RefExpressionValue(token.TokenType.ToString(), ExpressionValType.Int);
        }

        void ResloveToken(ExpressionToken token, List<ExpressionToken> param)
        {
            if (token.TokenType == ExpressionToken.ExpressionTokenType.Operator)
            {
                ResloveOpToken(token, param);
            }
            else if (token.TokenType == ExpressionToken.ExpressionTokenType.TwoOperator)
            {
                ResloveTwoCharOpToken(token, param);
            }
            else
            {
                token.ParserFunctionDefine.ResolveToken(token, param);
            }
        }

        void ResloveTwoCharOpToken(ExpressionToken token, List<ExpressionToken> param)
        {
            var opStr = token.TokenStr[0];
            var leftVal = param[0].BoundVal; //至少一个操作数
            var rightVal = param[1].BoundVal;
            switch (opStr)
            {
                case '>':
                    token.BoundVal = new CompareExpressionVal(leftVal, rightVal, CompareExpressionVal.CompareType.GreaterEqual);
                    return;
                case '<':
                    token.BoundVal = new CompareExpressionVal(leftVal, rightVal, CompareExpressionVal.CompareType.LessEqual);
                    return;
            }
        }

        void ResloveOpToken(ExpressionToken token, List<ExpressionToken> param)
        {
            var opStr = token.TokenStr[0];
            var leftVal = param[0].BoundVal; //至少一个操作数
            switch (opStr)
            {
                case '!':
                    token.BoundVal = new ConditionExpression(leftVal, true);
                    return;
            }


            var rightVal = param[1].BoundVal;
            switch (opStr)
            {
                case '+':
                    token.BoundVal = new MathExpressionVal(MathExpressionVal.MathOpType.Add, leftVal, rightVal);
                    return;
                case '-':
                    token.BoundVal = new MathExpressionVal(MathExpressionVal.MathOpType.Sub, leftVal, rightVal);
                    return;
                case '*':
                    token.BoundVal = new MathExpressionVal(MathExpressionVal.MathOpType.Mul, leftVal, rightVal);
                    return;
                case '/':
                    token.BoundVal = new MathExpressionVal(MathExpressionVal.MathOpType.Div, leftVal, rightVal);
                    return;
                case '%':
                    token.BoundVal = new MathExpressionVal(MathExpressionVal.MathOpType.Mod, leftVal, rightVal);
                    return;

                //& 和 | 的语义有待商榷
                case '&':
                    token.BoundVal = new AndExpressionVal(leftVal, rightVal);
                    return;
                case '|':
                    token.BoundVal = new OrExpressionVal(leftVal, rightVal);
                    return;

                case '>':
                    token.BoundVal = new CompareExpressionVal(leftVal, rightVal, CompareExpressionVal.CompareType.Greater);
                    return;
                case '<':
                    token.BoundVal = new CompareExpressionVal(leftVal, rightVal, CompareExpressionVal.CompareType.Less);
                    return;
                case '=':
                    token.BoundVal = new CompareExpressionVal(leftVal, rightVal, CompareExpressionVal.CompareType.Equal);
                    return;
            }
        }


        #region Bracket

        int FindClosingBracket(SubString aText, int aStart, char aOpen, char aClose)
        {
            int counter = 0;
            for (int i = aStart; i < aText.Length; i++)
            {
                if (aText[i] == aOpen)
                    counter++;
                if (aText[i] == aClose)
                    counter--;
                if (counter == 0)
                    return i;
            }

            return -1;
        }

        #endregion
    }
}