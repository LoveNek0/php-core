using PHP.Core.Lang.AST;
using PHP.Core.Lang.Exceptions;
using PHP.Core.Lang.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.AST.Constructions;
using PHP.Core.Lang.AST.Constructions.Function;
using PHP.Core.Lang.AST.Data;
using PHP.Core.Lang.AST.Operators;
using PHP.Core.Lang.AST.Operators.Access;
using PHP.Core.Lang.AST.Operators.Assignment;
using PHP.Core.Lang.AST.Operators.Binary;
using PHP.Core.Lang.AST.Operators.Binary.Bitwise;
using PHP.Core.Lang.AST.Operators.Binary.Comparsion;
using PHP.Core.Lang.AST.Operators.Binary.Logical;
using PHP.Core.Lang.AST.Operators.Operations;
using PHP.Core.Lang.AST.Operators.Ternary;
using PHP.Core.Lang.AST.Operators.Unary;
using PHP.Core.Lang.AST.Operators.Unary.TypeCast;
using PHP.Core.Lang.AST.Structures;
using PHP.Core.Lang.AST.Structures.Function;
using PHP.Core.Lang.AST.Structures.Loops;
using ASTInstanceOfOperator = PHP.Core.Lang.AST.Operators.Binary.ASTInstanceOfOperator;

namespace PHP.Core.Lang
{
    public class ASTBuilder
    {
        private static TokenType[] _ignore =
        {
            TokenType.Comment,
            TokenType.DocComment,
            TokenType.Whitespace,
            TokenType.OpenTagWithEcho,
            TokenType.OpenTag,
            TokenType.CloseTag,
        };

        private readonly TokenItem[] _tokens;
        private int _index;

        public ASTBuilder(TokenItem[] tokens)
        {
            this._tokens = (from TokenItem item in tokens where !_ignore.Contains(item.Type) select item).ToArray();
        }

        private TokenItem GetToken(int offset = 0)
        {
            if (_index + offset < 0 || _index + offset >= _tokens.Length)
                throw new Exception("Trying to get token item out of bounds");
            return _tokens[_index + offset];
        }

        private TokenItem GetToken(int offset = 0, params TokenType[] expected)
        {
            TokenItem token = GetToken(offset);
            if (expected.Length == 0 || expected.Contains(token.Type))
                return token;
            throw new UnexpectedTokenException(token, expected);
        }

        private TokenItem GetToken(params TokenType[] expected) => GetToken(0, expected);

        private TokenItem NextToken(params TokenType[] expected)
        {
            TokenItem token = GetToken(expected);
            _index++;
            return token;
        }

        private bool IsMatch(int offset, params TokenType[] expected)
        {
            TokenItem token = GetToken(offset);
            return expected.Length == 0 || expected.Contains(token.Type);
        }

        private bool IsMatch(params TokenType[] expected) => IsMatch(0, expected);

        public ASTRoot Build()
        {
            ASTRoot root = new ASTRoot();
            while (!IsMatch(TokenType.EndOfFile))
            {
                if (IsMatch(TokenType.Semicolon))
                {
                    NextToken();
                    continue;
                }

                root._children.Add(ParseGlobalLine());
            }

            return root;
        }

        private ASTNode ParseGlobalLine()
        {
            if (IsMatch(TokenType.Function))
                return ParseConstruction();
            return ParseLine();
        }
        private ASTNode ParseLine()
        {
            while (IsMatch(TokenType.Semicolon))
                NextToken();
            
            if (IsMatch(
                    TokenType.Variable, 
                    TokenType.ConstString, 
                    TokenType.Increment,
                    TokenType.Decrement,
                    TokenType.Echo,
                    TokenType.Print,
                    TokenType.Return,
                    TokenType.Unset))
            {
                ASTNode node = ParseCommands();
                NextToken(TokenType.Semicolon);
                return node;
            }

            throw new UnexpectedTokenException(GetToken());
        }

        //  Constructions

        private ASTNode ParseConstruction()
        {
            return ParseFunction();
        }
        private ASTNode ParseFunction()
        {
            TokenItem token = NextToken(TokenType.Function);
            TokenItem name = NextToken(TokenType.ConstString);
            List<ASTFunctionArgument> arguments = new List<ASTFunctionArgument>();
            NextToken(TokenType.BraceOpen);
            while (!IsMatch(TokenType.BraceClose))
            {
                arguments.Add(ParseFunctionArgument());
                if (!IsMatch(TokenType.Comma))
                    break;
                NextToken(TokenType.Comma);
            }
            NextToken(TokenType.BraceClose);
            TokenItem returnType = null;
            if (IsMatch(TokenType.Colon))
            {
                NextToken(TokenType.Colon);
                returnType = NextToken(
                    TokenType.TypeMixed,
                    TokenType.TypeCallable,
                    TokenType.TypeBool,
                    TokenType.TypeInt,
                    TokenType.TypeFloat,
                    TokenType.TypeString,
                    TokenType.TypeArray,
                    TokenType.TypeObject,
                    TokenType.ConstString);
            }
            List<ASTNode> body = new List<ASTNode>();
            NextToken(TokenType.CurlyBraceOpen);
            while (!IsMatch(TokenType.CurlyBraceClose))
                body.Add(ParseLine());
            NextToken(TokenType.CurlyBraceClose);
            return new ASTFunction(token, name, arguments.ToArray(), returnType, body.ToArray());
        }
        
        //  Command operators
        private ASTNode ParseCommands()
        {
            return ParseEchoOperator();
        }
        private ASTNode ParseEchoOperator()
        {
            if (IsMatch(TokenType.Echo))
                return new ASTEchoOperator(NextToken(), ParseExpression());
            return ParseReturnOperator();
        }
        private ASTNode ParseReturnOperator()
        {
            if (IsMatch(TokenType.Return))
                return new ASTReturnOperator(NextToken(), ParseExpression());
            return ParseUnsetOperator();
        }
        private ASTNode ParseUnsetOperator()
        {
            if (IsMatch(TokenType.Unset))
            {
                TokenItem token = NextToken();
                List<ASTNode> variables = new List<ASTNode>();
                NextToken(TokenType.BraceOpen);
                while (!IsMatch(TokenType.BraceClose))
                {
                    ASTNode node = ParseExpression();
                    if (!(node is ASTVariableData) &&
                        !(node is ASTObjectAccessOperator) &&
                        !(node is ASTNullsafeObjectAccessOperator) &&
                        !(node is ASTStaticAccessOperator) &&
                        !(node is ASTArrayAccessOperator))
                        throw new SyntaxException($"The unset operator can only be applied to variables", node.Token);
                    variables.Add(node);
                    if(!IsMatch(TokenType.Comma))
                        break;
                    NextToken(TokenType.Comma);
                }
                NextToken(TokenType.BraceClose);
                return new ASTUnsetOperator(token, variables.ToArray());
            }
            return ParseExpression();
        }

        //  Expressions
        private ASTNode ParseExpression()
        {
            return ParsePrintOperator();
        }
        private ASTNode ParsePrintOperator()
        {
            if (IsMatch(TokenType.Print))
                return new ASTPrintOperator(NextToken(), ParseExpression());
            return ParseAssignmentOperator();
        }
        
        private ASTNode ParseAssignmentOperator()
        {
            ASTNode left = ParseLambdaFunction();
            if (IsMatch(
                    TokenType.Assignment,
                    TokenType.AssignmentAdd,
                    TokenType.AssignmentSub,
                    TokenType.AssignmentMul,
                    TokenType.AssignmentDiv,
                    TokenType.AssignmentMod,
                    TokenType.AssignmentPow,
                    TokenType.AssignmentConcat,
                    TokenType.AssignmentCoalesce,
                    TokenType.AssignmentBitAnd,
                    TokenType.AssignmentBitNot,
                    TokenType.AssignmentBitXor,
                    TokenType.AssignmentBitShiftLeft,
                    TokenType.AssignmentBitShiftRight))
            {
                if(!(left is ASTVariableData) &&
                   !(left is ASTObjectAccessOperator) &&
                   !(left is ASTNullsafeObjectAccessOperator) &&
                   !(left is ASTStaticAccessOperator) &&
                   !(left is ASTArrayAccessOperator))
                    throw new SyntaxException($"Unexpected token \"{left.Token.Type}\" before assignment operator", left.Token);
                TokenItem token = NextToken();
                switch (token.Type)
                {
                    case TokenType.Assignment:
                        return new ASTAssignmentOperator(token, left, ParseExpression());
                    case TokenType.AssignmentAdd:
                        return new ASTAssignmentAddOperator(token, left, ParseExpression());
                    case TokenType.AssignmentSub:
                        return new ASTAssignmentSubOperator(token, left, ParseExpression());
                    case TokenType.AssignmentMul:
                        return new ASTAssignmentMulOperator(token, left, ParseExpression());
                    case TokenType.AssignmentDiv:
                        return new ASTAssignmentDivOperator(token, left, ParseExpression());
                    case TokenType.AssignmentMod:
                        return new ASTAssignmentModOperator(token, left, ParseExpression());
                    case TokenType.AssignmentPow:
                        return new ASTAssignmentPowOperator(token, left, ParseExpression());
                    case TokenType.AssignmentConcat:
                        return new ASTAssignmentConcatOperator(token, left, ParseExpression());
                    case TokenType.AssignmentCoalesce:
                        return new ASTAssignmentCoalesceOperator(token, left, ParseExpression());
                    case TokenType.AssignmentBitAnd:
                        return new ASTAssignmentBitAndOperator(token, left, ParseExpression());
                    case TokenType.AssignmentBitNot:
                        return new ASTAssignmentBitNotOperator(token, left, ParseExpression());
                    case TokenType.AssignmentBitXor:
                        return new ASTAssignmentBitXorOperator(token, left, ParseExpression());
                    case TokenType.AssignmentBitShiftLeft:
                        return new ASTAssignmentBitShiftLeftOperator(token, left, ParseExpression());
                    case TokenType.AssignmentBitShiftRight:
                        return new ASTAssignmentBitShiftRightOperator(token, left, ParseExpression());
                }
            }
            return left;
        }
        
        private ASTFunctionArgument ParseFunctionArgument()
        {
            TokenItem type = IsMatch(TokenType.TypeBool,
                TokenType.TypeInt,
                TokenType.TypeFloat,
                TokenType.TypeString,
                TokenType.TypeArray,
                TokenType.TypeObject,
                TokenType.TypeMixed,
                TokenType.TypeCallable) ? NextToken() : null;
            bool isPointer = false;
            if (IsMatch(TokenType.BitAnd))
            {
                NextToken();
                isPointer = true;
            }
            bool isMultiParams = false;
            if (IsMatch(TokenType.Ellipsis))
            {
                NextToken();
                isMultiParams = true;
            }
            TokenItem name = NextToken(TokenType.Variable);
            ASTNode byDefault = null;
            if (IsMatch(TokenType.Assignment))
            {
                NextToken();
                byDefault = ParseExpression();
                if(!(byDefault is ASTBooleanData) && 
                   !(byDefault is ASTConstStringData) &&
                   !(byDefault is ASTFloatData) &&
                   !(byDefault is ASTIntegerData) &&
                   !(byDefault is ASTNullData) &&
                   !(byDefault is ASTStringData) &&
                   !(byDefault is ASTVariableData) &&
                   !(byDefault is ASTArrayData))
                    throw new SyntaxException($"Unexpected token \"{byDefault.Token.Type}\" giving as default value", byDefault.Token); 
            }
            return new ASTFunctionArgument(type, isPointer, isMultiParams, name, byDefault);
        }
        private ASTNode ParseLambdaFunction()
        {
            if (IsMatch(TokenType.Function))
            {
                TokenItem token = NextToken();
                NextToken(TokenType.BraceOpen);
                List<ASTFunctionArgument> arguments = new List<ASTFunctionArgument>();
                while(!IsMatch(TokenType.BraceClose)){
                    arguments.Add(ParseFunctionArgument());
                    if (!IsMatch(TokenType.Comma))
                        break;
                    NextToken(TokenType.Comma);
                }
                NextToken(TokenType.BraceClose);
                List<ASTLambdaFunctionUseArgument> use = new List<ASTLambdaFunctionUseArgument>();
                if (IsMatch(TokenType.Use))
                {
                    NextToken();
                    NextToken(TokenType.BraceOpen);
                    while(!IsMatch(TokenType.BraceClose))
                    {
                        TokenItem pointer = IsMatch(TokenType.BitAnd) ? NextToken() : null;
                        TokenItem name = NextToken(TokenType.Variable);
                        use.Add(new ASTLambdaFunctionUseArgument(pointer, name));
                        if (!IsMatch(TokenType.Comma))
                            break;
                        NextToken(TokenType.Comma);
                    }
                    NextToken(TokenType.BraceClose);
                }
                NextToken(TokenType.CurlyBraceOpen);
                List<ASTNode> body = new List<ASTNode>();
                while (!IsMatch(TokenType.CurlyBraceClose))
                    body.Add(ParseLine());
                NextToken(TokenType.CurlyBraceClose);
                return new ASTLambdaFunction(token, arguments.ToArray(), use.ToArray(), body.ToArray());
            }
            return ParseArrowFunction();
        }
        private ASTNode ParseArrowFunction()
        {
            if (IsMatch(TokenType.Fn))
            {
                TokenItem token = NextToken(TokenType.Fn);
                bool resultAsPointer = false;
                if (IsMatch(TokenType.BitAnd))
                {
                    resultAsPointer = true;
                    NextToken();
                }
                List<ASTFunctionArgument> arguments = new List<ASTFunctionArgument>();
                NextToken(TokenType.BraceOpen);
                while (!IsMatch(TokenType.BraceClose))
                {
                    arguments.Add(ParseFunctionArgument());
                    if(!IsMatch(TokenType.Comma))
                        break;
                    NextToken(TokenType.Comma);
                }
                NextToken(TokenType.BraceClose);
                NextToken(TokenType.DoubleArrow);
                ASTNode body = ParseExpression();
                return new ASTArrowFunction(token, resultAsPointer, arguments.ToArray(), body);
            }
            return ParseTernaryOperator();
        }

        private ASTNode ParseTernaryOperator()
        {
            ASTNode condition = ParseLogicalOperator();
            if (IsMatch(TokenType.Query))
            {
                TokenItem firstToken = NextToken(TokenType.Query);
                ASTNode left = ParseExpression();
                NextToken(TokenType.Colon);
                ASTNode right = ParseExpression();
                return new ASTConditionalOperator(firstToken, condition, left, right);
            }
            return condition;
        }
        private ASTNode ParseLogicalOperator()
        {
            ASTNode left = ParseBitwiseOperator();
            if (IsMatch(TokenType.LogicalAnd))
                return new ASTLogicalAndOperator(NextToken(), left, ParseLogicalOperator());
            if (IsMatch(TokenType.LogicalOr))
                return new ASTLogicalOrOperator(NextToken(), left, ParseLogicalOperator());
            if (IsMatch(TokenType.LogicalXor))
                return new ASTLogicalXorOperator(NextToken(), left, ParseLogicalOperator());
            return left;
        }
        private ASTNode ParseBitwiseOperator()
        {
            ASTNode left = ParseComparisonEqualsOperator();
            if (IsMatch(TokenType.BitAnd))
                return new ASTBitAndOperator(NextToken(), left, ParseBitwiseOperator());
            if (IsMatch(TokenType.BitNot))
                return new ASTBitNotOperator(NextToken(), left, ParseBitwiseOperator());
            if (IsMatch(TokenType.BitXor))
                return new ASTBitXorOperator(NextToken(), left, ParseBitwiseOperator());
            if (IsMatch(TokenType.BitShiftLeft))
                return new ASTBitShiftLeftOperator(NextToken(), left, ParseBitwiseOperator());
            if (IsMatch(TokenType.BitShiftRight))
                return new ASTBitShiftRightOperator(NextToken(), left, ParseBitwiseOperator());
            return left;
        }
        private ASTNode ParseComparisonEqualsOperator()
        {
            ASTNode left = ParseComparisonSizeOperator();
            if (IsMatch(TokenType.IsEqual))
                return new ASTIsEqualOperator(NextToken(), left, ParseComparisonSizeOperator());
            if (IsMatch(TokenType.IsNotEqual))
                return new ASTIsNotEqualOperator(NextToken(), left, ParseComparisonSizeOperator());
            if (IsMatch(TokenType.IsSpaceship))
                return new ASTIsSpaceshipOperator(NextToken(), left, ParseComparisonSizeOperator());
            return left;
        }
        private ASTNode ParseComparisonSizeOperator()
        {
            ASTNode left = ParseNullCoalesceOperator();
            if (IsMatch(TokenType.IsGreater))
                return new ASTIsGreaterOperator(NextToken(), left, ParseNullCoalesceOperator());
            if (IsMatch(TokenType.IsSmaller))
                return new ASTIsSmallerOperator(NextToken(), left, ParseNullCoalesceOperator());
            if (IsMatch(TokenType.IsGreaterOrEqual))
                return new ASTIsGreaterOrEqualOperator(NextToken(), left, ParseNullCoalesceOperator());
            if (IsMatch(TokenType.IsSmallerOrEqual))
                return new ASTIsSmallerOrEqualOperator(NextToken(), left, ParseNullCoalesceOperator());
            return left;
        }
        private ASTNode ParseNullCoalesceOperator()
        {
            ASTNode left = ParseAddition();
            if (IsMatch(TokenType.Coalesce))
                left = new ASTNullCoalesceOperator(NextToken(), left, ParseExpression());
            return left;
        }
        private ASTNode ParseAddition()
        {
            ASTNode left = ParseMultiplication();
            if (IsMatch(TokenType.Add))
                left = new ASTAddOperator(NextToken(), left, ParseAddition());
            if (IsMatch(TokenType.Sub))
                left = new ASTSubOperator(NextToken(), left, ParseAddition());
            return left;
        }
        private ASTNode ParseMultiplication()
        {
            ASTNode left = ParseMod();
            if (IsMatch(TokenType.Mul))
                left = new ASTMulOperator(NextToken(), left, ParseMultiplication());
            if (IsMatch(TokenType.Div))
                left = new ASTDivOperator(NextToken(), left, ParseMultiplication());
            return left;
        }
        private ASTNode ParseMod()
        {
            ASTNode left = ParsePow();
            if (IsMatch(TokenType.Mod))
                left = new ASTModOperator(NextToken(), left, ParseMod());
            return left;
        }
        private ASTNode ParsePow()
        {
            ASTNode left = ParseConcat();
            if (IsMatch(TokenType.Pow))
                left = new ASTPowOperator(NextToken(), left, ParsePow());
            return left;
        }
        private ASTNode ParseConcat()
        {
            ASTNode left = ParseInstanceOfOperator();
            if (IsMatch(TokenType.Concat))
                left = new ASTConcatOperator(NextToken(), left, ParseConcat());
            return left;
        }
        private ASTNode ParseInstanceOfOperator()
        {
            ASTNode left = ParseTypeCastOperators();
            if (IsMatch(TokenType.InstanceOf))
            {
                if(!(left is ASTArrayCastOperator) &&
                   !(left is ASTBooleanCastOperator) &&
                   !(left is ASTCustomTypeCastOperator) &&
                   !(left is ASTFloatCastOperator) &&
                   !(left is ASTIntegerCastOperator) &&
                   !(left is ASTObjectCastOperator) &&
                   !(left is ASTStringCastOperator) &&
                   !(left is ASTVariableData) &&
                   !(left is ASTConstStringData) &&
                   !(left is ASTObjectAccessOperator) &&
                   !(left is ASTNullsafeObjectAccessOperator) &&
                   !(left is ASTStaticAccessOperator) &&
                   !(left is ASTNewOperator) &&
                   !(left is ASTCloneOperator) &&
                   !(left is ASTFunctionCallOperator) &&
                   !(left is ASTArrayAccessOperator)) 
                    throw new SyntaxException($"Unexpected token \"{left.Token.Type}\" before instance", left.Token);
                TokenItem token = NextToken();
                ASTNode right = ParseCloneOperator();
                return new ASTInstanceOfOperator(token, left, right);
            }
            return left;
        }
        private ASTNode ParseTypeCastOperators()
        {
            if (IsMatch(TokenType.BraceOpen) &&
                IsMatch(1,
                    TokenType.TypeBool,
                    TokenType.TypeInt,
                    TokenType.TypeFloat,
                    TokenType.TypeString,
                    TokenType.TypeArray,
                    TokenType.TypeObject,
                    TokenType.Unset) &&
                IsMatch(2, TokenType.BraceClose))
            {
                NextToken(TokenType.BraceOpen);
                TokenItem token = NextToken();
                NextToken(TokenType.BraceClose);
                if (token.Type == TokenType.TypeBool)
                    return new ASTBooleanCastOperator(token, ParseTypeCastOperators());
                if (token.Type == TokenType.TypeInt)
                    return new ASTIntegerCastOperator(token, ParseTypeCastOperators());
                if (token.Type == TokenType.TypeFloat)
                    return new ASTFloatCastOperator(token, ParseTypeCastOperators());
                if (token.Type == TokenType.TypeString)
                    return new ASTStringCastOperator(token, ParseTypeCastOperators());
                if (token.Type == TokenType.TypeArray)
                    return new ASTArrayCastOperator(token, ParseTypeCastOperators());
                if (token.Type == TokenType.TypeObject)
                    return new ASTObjectCastOperator(token, ParseTypeCastOperators());
                if (token.Type == TokenType.Unset)
                    return new ASTUnsetCastOperator(token, ParseTypeCastOperators());
                if (token.Type == TokenType.ConstString)
                    return new ASTCustomTypeCastOperator(token, ParseTypeCastOperators());
            }
            return ParsePreIncDecOperators();
        }
        private ASTNode ParsePreIncDecOperators()
        {
            if (IsMatch(TokenType.Increment, TokenType.Decrement))
            {
                TokenItem token = NextToken();
                ASTNode right = ParsePostIncDecOperators();
                if(!(right is ASTVariableData) &&
                   !(right is ASTObjectAccessOperator) &&
                   !(right is ASTNullsafeObjectAccessOperator) &&
                   !(right is ASTStaticAccessOperator))
                    throw new SyntaxException("Unexpected token after increment/decrement operator", NextToken());
                if (token.Type == TokenType.Increment)
                    return new ASTPreIncOperator(token, right);
                if (token.Type == TokenType.Decrement)
                    return new ASTPreDecOperator(token, right);
            }
            return ParsePostIncDecOperators();
        }
        private ASTNode ParsePostIncDecOperators()
        {
            ASTNode left = ParseCloneOperator();
            if (IsMatch(TokenType.Increment, TokenType.Decrement))
            {
                if(!(left is ASTVariableData) &&
                   !(left is ASTObjectAccessOperator) &&
                   !(left is ASTNullsafeObjectAccessOperator) &&
                   !(left is ASTStaticAccessOperator))
                    throw new SyntaxException("Unexpected token before increment/decrement operator", NextToken());
                TokenItem token = NextToken();
                if (token.Type == TokenType.Increment)
                    return new ASTPostIncOperator(token, left);
                if (token.Type == TokenType.Decrement)
                    return new ASTPostDecOperator(token, left);
            }
            return left;
        }
        private ASTNode ParseCloneOperator()
        {
            if (IsMatch(TokenType.Clone))
            {
                TokenItem token = NextToken();
                ASTNode right = ParseNewOperator();
                if(!(right is ASTVariableData) &&
                   !(right is ASTConstStringData) &&
                   !(right is ASTObjectAccessOperator) &&
                   !(right is ASTNullsafeObjectAccessOperator) &&
                   !(right is ASTStaticAccessOperator) &&
                   !(right is ASTNewOperator))
                    throw new SyntaxException("Unexpected token after \"clone\" operator", NextToken());
                return new ASTCloneOperator(token, right);
            }

            return ParseNewOperator();
        }
        private ASTNode ParseNewOperator()
        {
            if (IsMatch(TokenType.Clone))
            {
                TokenItem token = NextToken();
                ASTNode right = ParsePrefixUnaryOperators();
                if(!(right is ASTVariableData) &&
                   !(right is ASTConstStringData) &&
                   !(right is ASTObjectAccessOperator) &&
                   !(right is ASTNullsafeObjectAccessOperator) &&
                   !(right is ASTStaticAccessOperator))
                    throw new SyntaxException("Unexpected token after \"new\" operator", NextToken());
                return new ASTCloneOperator(token, right);
            }

            return ParsePrefixUnaryOperators();
        }
        private ASTNode ParsePrefixUnaryOperators()
        {
            if (IsMatch(TokenType.Add))
                return new ASTPositiveOperator(NextToken(), ParsePrefixUnaryOperators());
            if (IsMatch(TokenType.Sub))
                return new ASTNegativeOperator(NextToken(), ParsePrefixUnaryOperators());
            if (IsMatch(TokenType.Not))
                return new ASTNotOperator(NextToken(), ParsePrefixUnaryOperators());
            if (IsMatch(TokenType.Negation))
                return new ASTNegationOperator(NextToken(), ParsePrefixUnaryOperators());
            return ParseArrayAccessOperator();
        }
        private ASTNode ParseArrayAccessOperator()
        {
            ASTNode link = ParseFunctionCallOperator();
            if (IsMatch(TokenType.SquareBraceOpen))
            {
                if (!(link is ASTVariableData) &&
                    !(link is ASTObjectAccessOperator) &&
                    !(link is ASTNullsafeObjectAccessOperator) &&
                    !(link is ASTStaticAccessOperator) &&
                    !(link is ASTConstStringData) &&
                    !(link is ASTArrayAccessOperator))
                    throw new SyntaxException($"Unexpected token {link.Token.Type} before array access operator", link.Token);
                TokenItem token = NextToken(TokenType.SquareBraceOpen);
                ASTNode index = IsMatch(TokenType.SquareBraceClose) ? null : ParseExpression();
                NextToken(TokenType.SquareBraceClose);
                return new ASTArrayAccessOperator(token, link, index);
            }
            return link;
        }
        private ASTNode ParseFunctionCallOperator()
        {
            ASTNode left = ParsePointerOperator();
            if (IsMatch(TokenType.BraceOpen))
            {
                if (!(left is ASTArrayAccessOperator) &&
                    !(left is ASTConstStringData) &&
                    !(left is ASTVariableData) &&
                    !(left is ASTObjectAccessOperator) &&
                    !(left is ASTNullsafeObjectAccessOperator) &&
                    !(left is ASTStaticAccessOperator))
                    throw new SyntaxException($"Unexpected token {left.Token.Type} before function call operator", left.Token);
                TokenItem token = NextToken(TokenType.BraceOpen);
                List<ASTNode> arguments = new List<ASTNode>();
                while (!IsMatch(TokenType.BraceClose))
                {
                    arguments.Add(ParseExpression());
                    if (!IsMatch(TokenType.Comma))
                        break;
                    NextToken(TokenType.Comma);
                }
                NextToken(TokenType.BraceClose);
                return new ASTFunctionCallOperator(token, left, arguments.ToArray());
            }
            return left;
        }
        private ASTNode ParsePointerOperator()
        {
            if (IsMatch(TokenType.BitAnd))
            {
                TokenItem token = NextToken(TokenType.BitAnd);
                ASTNode right = ParseObjectAccessOperator();
                if(!(right is ASTVariableData) &&
                   !(right is ASTObjectAccessOperator) &&
                   !(right is ASTNullsafeObjectAccessOperator) &&
                   !(right is ASTStaticAccessOperator))
                    throw new SyntaxException($"Unexpected token \"{right.Token.Type}\" after pointer operator", right.Token);
                return new ASTPointerOperator(token, right);
            }
            return ParseObjectAccessOperator();
        }
        private ASTNode ParseObjectAccessOperator()
        {
            ASTNode left = ParseStaticAccessOperator();
            if (IsMatch(TokenType.ObjectOperator, TokenType.NullsafeObjectOperator))
            {
                if(!(left is ASTVariableData) &&
                   !(left is ASTObjectAccessOperator) &&
                   !(left is ASTNullsafeObjectAccessOperator) &&
                   !(left is ASTStaticAccessOperator))
                    throw new SyntaxException($"Unexpected token {left.Token.Type} before object access operator", left.Token);
                TokenItem token = NextToken(TokenType.ObjectOperator, TokenType.NullsafeObjectOperator);
                ASTNode right = ParseObjectAccessOperator();
                if(!(right is ASTConstStringData) &&
                   !(right is ASTVariableData))
                    throw new SyntaxException($"Unexpected token {right.Token} after object access operator", right.Token);
                if (token.Type == TokenType.ObjectOperator)
                    return new ASTObjectAccessOperator(token, left, right);
                if (token.Type == TokenType.NullsafeObjectOperator)
                    return new ASTNullsafeObjectAccessOperator(token, left, right);
            }
            return left;
        }
        private ASTNode ParseStaticAccessOperator()
        {
            ASTNode left = ParseArrayDefinition();
            if (IsMatch(TokenType.DoubleColon))
            {
                if (!(left is ASTConstStringData) &&
                    !(left is ASTVariableData) &&
                    !(left is ASTObjectAccessOperator) &&
                    !(left is ASTNullsafeObjectAccessOperator) &&
                    !(left is ASTStaticAccessOperator))
                    throw new SyntaxException($"Unexpected token {left.Token.Type} before static call", left.Token);
                TokenItem token = NextToken(TokenType.DoubleColon);
                ASTNode right = ParseObjectAccessOperator();
                if (!(right is ASTConstStringData) &&
                    !(right is ASTVariableData))
                    throw new SyntaxException($"Unexpected token {right.Token.Type} after static call", right.Token);
                return new ASTStaticAccessOperator(token, left, right);
            }
            return left;
        }
        private ASTNode ParseArrayDefinition()
        {
            if (IsMatch(TokenType.TypeArray, TokenType.SquareBraceOpen))
            {
                TokenItem token = NextToken();
                if (IsMatch(TokenType.TypeArray))
                    NextToken(TokenType.BraceOpen);
                List<KeyValuePair<ASTNode, ASTNode>> values = new List<KeyValuePair<ASTNode, ASTNode>>();
                while (!IsMatch(token.Type == TokenType.TypeArray ? TokenType.BraceClose : TokenType.SquareBraceClose))
                {
                    ASTNode key = null;
                    ASTNode value = ParseExpression();
                    if (IsMatch(TokenType.DoubleArrow))
                    {
                        NextToken();
                        key = value;
                        key = ParseExpression();
                    }
                    values.Add(new KeyValuePair<ASTNode, ASTNode>(key, value));
                    if(!IsMatch(TokenType.Comma))
                        break;
                    NextToken(TokenType.Comma);
                }
                NextToken(token.Type == TokenType.TypeArray ? TokenType.BraceClose : TokenType.SquareBraceClose);
                return new ASTArrayData(token, values.ToArray());
            }

            return ParsePrimary();
        }
        private ASTNode ParsePrimary()
        {
            if (IsMatch(TokenType.BraceOpen))
            {
                NextToken();
                ASTNode expression = ParseExpression();
                NextToken(TokenType.BraceClose);
                return expression;
            }
            
            TokenItem token = NextToken(
                TokenType.Integer,
                TokenType.Float, 
                TokenType.String,
                TokenType.Variable,
                TokenType.ConstString,
                TokenType.Null,
                TokenType.True,
                TokenType.False);
            switch (token.Type)
            {
                case TokenType.Null:
                    return new ASTNullData(token);
                case TokenType.True:
                case TokenType.False:
                    return new ASTBooleanData(token);
                case TokenType.Integer:
                    return new ASTIntegerData(token);
                case TokenType.Float:
                    return new ASTFloatData(token);
                case TokenType.String:
                    return new ASTStringData(token);
                case TokenType.Variable:
                    return new ASTVariableData(token);
                case TokenType.ConstString:
                    return new ASTConstStringData(token);
            }
            throw new SyntaxException($"$Unexpected token {token.Type}", token);
        }
    }
}