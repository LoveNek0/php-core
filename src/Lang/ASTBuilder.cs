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
using PHP.Core.Lang.AST.Operators;
using PHP.Core.Lang.AST.Structures;
using PHP.Core.Lang.AST.Structures.Function;
using PHP.Core.Lang.AST.Structures.Loops;

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

        public static ASTRoot Build(string code)
        {
            Tokenizer tokenizer = new Tokenizer(code);
            ASTBuilder astBuilder = new ASTBuilder(tokenizer.GetTokens());
            return astBuilder.Build();
        }

        public static ASTRoot BuildFromFile(string path) => Build(File.ReadAllText(path));

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

                root._children.Add(ParseLine());
            }

            return root;
        }

        private ASTNode ParseLine()
        {
            while (IsMatch(TokenType.Semicolon))
                NextToken();

            /*
            if (IsMatch(TokenType.If))
                return ParseIf();

            //  Loops
            if (IsMatch(TokenType.While))
                return ParseWhile();
            if (IsMatch(TokenType.Do))
                return ParseDoWhile();
            if (IsMatch(TokenType.For))
                return ParseFor();
            if (IsMatch(TokenType.Foreach))
                return ParseForeach();

            if (IsMatch(TokenType.Function))
                return ParseFunction();

            */
            if (IsMatch(TokenType.Variable))
            {
                ASTNode node = ParseExpression();
                NextToken(TokenType.Semicolon);
                return node;
            }

            throw new UnexpectedTokenException(GetToken());
        }

        /*
        private ASTNode ParseIf()
        {
            ASTIf node = new ASTIf(NextToken(TokenType.If));
            NextToken(TokenType.T_BRACE_OPEN);
            node._condition = ParseExpression();
            NextToken(TokenType.T_BRACE_CLOSE);
            if (IsMatch(TokenType.T_CURLY_BRACE_OPEN))
            {
                NextToken(TokenType.T_CURLY_BRACE_OPEN);
                while (!IsMatch(TokenType.T_CURLY_BRACE_CLOSE))
                    node._ifBlock.Add(ParseLine());
                NextToken(TokenType.T_CURLY_BRACE_CLOSE);
            }
            else
                node._ifBlock.Add(ParseLine());
            if (IsMatch(TokenType.Else))
            {
                NextToken(TokenType.Else);
                if (IsMatch(TokenType.T_CURLY_BRACE_OPEN))
                {
                    NextToken(TokenType.T_CURLY_BRACE_OPEN);
                    while (!IsMatch(TokenType.T_CURLY_BRACE_CLOSE))
                        node._elseBlock.Add(ParseLine());
                    NextToken(TokenType.T_CURLY_BRACE_CLOSE);
                }
                else
                    node._elseBlock.Add(ParseLine());
            }
            return node;
        }

        //  Loops
        private ASTNode ParseWhile()
        {
            ASTWhile node = new ASTWhile(NextToken(TokenType.While));
            NextToken(TokenType.T_BRACE_OPEN);
            node._condition = ParseExpression();
            NextToken(TokenType.T_BRACE_CLOSE);
            if (IsMatch(TokenType.T_CURLY_BRACE_OPEN))
            {
                NextToken(TokenType.T_CURLY_BRACE_OPEN);
                while (!IsMatch(TokenType.T_CURLY_BRACE_CLOSE))
                    node._lines.Add(ParseLine());
                NextToken(TokenType.T_CURLY_BRACE_CLOSE);
            }
            else
                node._lines.Add(ParseLine());

            return node;
        }
        private ASTNode ParseDoWhile()
        {
            ASTDoWhile node = new ASTDoWhile(NextToken(TokenType.Do));
            if (IsMatch(TokenType.T_CURLY_BRACE_OPEN))
            {
                NextToken(TokenType.T_CURLY_BRACE_OPEN);
                while (!IsMatch(TokenType.T_CURLY_BRACE_CLOSE))
                    node._lines.Add(ParseLine());
                NextToken(TokenType.T_CURLY_BRACE_CLOSE);
            }
            else
                node._lines.Add(ParseLine());
            NextToken(TokenType.While);
            NextToken(TokenType.T_BRACE_OPEN);
            node._condition = ParseExpression();
            NextToken(TokenType.T_BRACE_CLOSE);
            NextToken(TokenType.T_SEMICOLON);
            return node;
        }
        private ASTNode ParseFor()
        {
            ASTFor node = new ASTFor(NextToken(TokenType.For));
            NextToken(TokenType.T_BRACE_OPEN);
            if(!IsMatch(TokenType.T_SEMICOLON))
                node._initialAction = ParseExpression();
            NextToken(TokenType.T_SEMICOLON);
            if(!IsMatch(TokenType.T_SEMICOLON))
                node._condition = ParseExpression();
            NextToken(TokenType.T_SEMICOLON);
            if(!IsMatch(TokenType.T_BRACE_CLOSE))
                node._postAction = ParseExpression();
            NextToken(TokenType.T_BRACE_CLOSE);
            if (IsMatch(TokenType.T_CURLY_BRACE_OPEN))
            {
                NextToken(TokenType.T_CURLY_BRACE_OPEN);
                while (!IsMatch(TokenType.T_CURLY_BRACE_CLOSE))
                    node._lines.Add(ParseLine());
                NextToken(TokenType.T_CURLY_BRACE_CLOSE);
            }
            else
                node._lines.Add(ParseLine());
            return node;
        }
        private ASTNode ParseForeach()
        {
            ASTForeach node = new ASTForeach(NextToken(TokenType.Foreach));
            NextToken(TokenType.T_BRACE_OPEN);
            node._container = ParseExpression();
            NextToken(TokenType.As);
            node._key = ParseObjectOperator();
            NextToken(TokenType.DoubleArrow);
            node._value = ParseObjectOperator();
            NextToken(TokenType.T_BRACE_CLOSE);
            if (IsMatch(TokenType.T_CURLY_BRACE_OPEN))
            {
                NextToken(TokenType.T_CURLY_BRACE_OPEN);
                while (!IsMatch(TokenType.T_CURLY_BRACE_CLOSE))
                    node._lines.Add(ParseLine());
                NextToken(TokenType.T_CURLY_BRACE_CLOSE);
            }
            else
                node._lines.Add(ParseLine());
            return node;
        }

        private ASTNode ParseFunction()
        {
            ASTFunction node = new ASTFunction(NextToken(TokenType.Function));
            node._name = NextToken(TokenType.T_STATIC_STRING);
            NextToken(TokenType.T_BRACE_OPEN);
            while (true)
            {
                if (IsMatch(TokenType.T_BRACE_CLOSE))
                    break;
                ASTFunctionArgument argument = new ASTFunctionArgument();
                if (IsMatch(TokenType.T_STATIC_STRING))
                    argument._type = NextToken(TokenType.T_STATIC_STRING);
                if (IsMatch(TokenType.T_BIT_AND))
                {
                    NextToken(TokenType.T_BIT_AND);
                    argument._isPointer = true;
                }
                argument._variable = NextToken(TokenType.T_VARIABLE);
                if (IsMatch(TokenType.T_ASSIGNMENT))
                {
                    NextToken(TokenType.T_ASSIGNMENT);
                    argument._default = ParseAddition();
                }
                node._arguments.Add(argument);
                if (IsMatch(TokenType.T_COMMA))
                {
                    NextToken(TokenType.T_COMMA);
                    continue;
                }
                break;
            }
            NextToken(TokenType.T_BRACE_CLOSE);
            if (IsMatch(TokenType.Colon))
            {
                NextToken(TokenType.Colon);
                node._returnType = NextToken(TokenType.T_STATIC_STRING);
            }
            NextToken(TokenType.T_CURLY_BRACE_OPEN);
            while (!IsMatch(TokenType.T_CURLY_BRACE_CLOSE))
                node._lines.Add(ParseLine());
            NextToken(TokenType.T_CURLY_BRACE_CLOSE);
            return node;
        }
        */

        //  Expression
        private ASTNode ParseExpression()
        {
            return ParseAssignment();
        }

        private ASTNode ParseAssignment()
        {
            ASTNode left = ParseNullCoalesceOperator();

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
                TokenItem token = NextToken();

                if (left.Token.Type == TokenType.Variable)
                {
                    ASTNode right = ParseAssignment();
                    left = new ASTBinary(token, left, right);
                }
                else
                    throw new SyntaxException("Unexpected token before assignment operator", token);
            }

            return left;
        }

        /*
        private ASTNode ParseLogical()
        {
            ASTNode left = ParseBitwise();

            while (IsMatch(TokenType.T_LOGICAL_AND, TokenType.T_LOGICAL_OR, TokenType.T_LOGICAL_XOR))
            {
                TokenItem token = NextToken();
                ASTNode right = ParseBitwise();
                left = new ASTBinary(token, left, right);
            }

            return left;
        }
        private ASTNode ParseBitwise()
        {
            ASTNode left = ParseComparison();

            if (IsMatch(
                    TokenType.T_BIT_AND,
                    TokenType.T_BIT_XOR,
                    TokenType.T_BIT_NOT,
                    TokenType.T_BIT_SHIFT_LEFT,
                    TokenType.T_BIT_SHIFT_RIGHT))
            {
                TokenItem token = NextToken();
                ASTNode right = ParseBitwise();
                left = new ASTBinary(token, left, right);
            }

            return left;
        }

        private ASTNode ParseComparison()
        {
            ASTNode left = ParseAddition();
            if (IsMatch(
                    TokenType.T_IS_EQUAL,
                    TokenType.T_IS_NOT_EQUAL,
                    TokenType.T_IS_SMALLER,
                    TokenType.T_IS_SMALLER_OR_EQUAL,
                    TokenType.T_IS_GREATER,
                    TokenType.T_IS_GREATER_OR_EQUAL
                ))
            {
                TokenItem token = NextToken();
                ASTNode right = ParseComparison();
                left = new ASTBinary(token, left, right);
            }

            return left;
        }
        */
        
        private ASTNode ParseNullCoalesceOperator()
        {
            ASTNode left = ParseAddition();
            if (IsMatch(TokenType.Coalesce))
                left = new ASTBinary(NextToken(), left, ParseExpression());
            return left;
        }
        
        private ASTNode ParseAddition()
        {
            ASTNode left = ParseMultiplication();
            if (IsMatch(TokenType.Add, TokenType.Sub))
                left = new ASTBinary(NextToken(), left, ParseAddition());
            return left;
        }

        private ASTNode ParseMultiplication()
        {
            ASTNode left = ParseMod();
            if (IsMatch(TokenType.Mul, TokenType.Div))
                left = new ASTBinary(NextToken(), left, ParseMultiplication());
            return left;
        }

        private ASTNode ParseMod()
        {
            ASTNode left = ParsePow();
            if (IsMatch(TokenType.Mod))
                left = new ASTBinary(NextToken(), left, ParseMod());
            return left;
        }

        private ASTNode ParsePow()
        {
            ASTNode left = ParseConcat();
            if (IsMatch(TokenType.Pow))
                left = new ASTBinary(NextToken(), left, ParsePow());
            return left;
        }

        private ASTNode ParseConcat()
        {
            ASTNode left = ParseObjectAccessOperator();
            if (IsMatch(TokenType.Concat))
                left = new ASTBinary(NextToken(), left, ParseConcat());
            return left;
        }
        
        private ASTNode ParseObjectAccessOperator()
        {
            ASTNode left = ParseSign();
            if (IsMatch(TokenType.BraceOpen))
                left = ParseFunctionCall(left);
            if(left.Token.Type == TokenType.Variable || left.Token.Type == TokenType.ConstString)
                while (IsMatch(TokenType.ObjectOperator, TokenType.NullsafeObjectOperator, TokenType.DoubleColon))
                {
                    TokenItem token = NextToken();
                    ASTNode right;
                    if (IsMatch(TokenType.CurlyBraceOpen))
                    {
                        NextToken(TokenType.CurlyBraceOpen);
                        right = ParseExpression();
                        NextToken(TokenType.CurlyBraceClose);
                    }
                    else
                    {
                        GetToken(TokenType.ConstString, TokenType.Variable);
                        right = ParseSign();
                    }

                    left = new ASTBinary(token, left, right);
                    if (IsMatch(TokenType.BraceOpen))
                        left = ParseFunctionCall(left);
                }

            return left;
        }
        
        private ASTNode ParseFunctionCall(ASTNode node)
        {
            if (node.Token.Type != TokenType.Integer && node.Token.Type != TokenType.Float && IsMatch(TokenType.BraceOpen))
            {
                TokenItem token = NextToken(TokenType.BraceOpen);

                List<ASTNode> @params = new List<ASTNode>();
                while (!IsMatch(TokenType.BraceClose))
                {
                    @params.Add(ParseExpression());
                    if (!IsMatch(TokenType.BraceClose))
                        NextToken(TokenType.Comma);
                }
                NextToken(TokenType.BraceClose);
                node = new ASTFunctionCall(token, node, @params.ToArray());
            }
            return node;
        }
        
        private ASTNode ParseSign()
        {
            if (IsMatch(TokenType.Add, TokenType.Sub))
                return new ASTUnary(NextToken(), ParseSign(), ASTUnary.OperatorSide.Left);
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

            return new ASTData(NextToken(
                TokenType.Integer,
                TokenType.Float, 
                TokenType.String,
                TokenType.Variable,
                TokenType.ConstString));
        }
    }
}