using PHP.Core.Lang.AST;
using PHP.Core.Lang.Exceptions;
using PHP.Core.Lang.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
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
        private static readonly TokenType[] ignoreTokenTypes =
        {
            TokenType.T_OPEN_TAG,
            TokenType.T_CLOSE_TAG,
            TokenType.T_WHITESPACE,
            TokenType.T_COMMENT
        };

        private TokenItem[] tokens;
        private int index;

        public static ASTRoot Build(string code)
        {
            Tokenizer tokenizer = new Tokenizer(code);
            ASTBuilder astBuilder = new ASTBuilder(tokenizer.Tokenize());
            return astBuilder.Build();
        }

        public static ASTRoot BuildFromFile(string path) => Build(File.ReadAllText(path));
        
        public ASTBuilder(TokenItem[] tokens)
        {
            this.tokens = (from TokenItem item in tokens where !ignoreTokenTypes.Contains(item.Type) select item)
                .ToArray();
        }

        private TokenItem GetToken(int offset = 0)
        {
            if (index + offset < 0 || index + offset >= tokens.Length)
                throw new Exception("Trying to get token item out of bounds");
            return tokens[index + offset];
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
            index++;
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
            while (!IsMatch(TokenType.T_EOF))
            {
                if (IsMatch(TokenType.T_SEMICOLON))
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
            if (IsMatch(TokenType.T_SEMICOLON))
            {
                NextToken(TokenType.T_SEMICOLON);
                return ParseLine();
            }
            if (IsMatch(TokenType.T_IF))
                return ParseIf();
            
            //  Loops
            if (IsMatch(TokenType.T_WHILE))
                return ParseWhile();
            if (IsMatch(TokenType.T_DO))
                return ParseDoWhile();
            if (IsMatch(TokenType.T_FOR))
                return ParseFor();
            if (IsMatch(TokenType.T_FOREACH))
                return ParseForeach();
            
            if (IsMatch(TokenType.T_FUNCTION))
                return ParseFunction();
            if (IsMatch(TokenType.T_VARIABLE, TokenType.T_STATIC_STRING))
            {
                ASTNode node = ParseExpression();
                NextToken(TokenType.T_SEMICOLON);
                return node;
            }

            throw new UnexpectedTokenException(GetToken());
        }

        private ASTNode ParseIf()
        {
            ASTIf node = new ASTIf(NextToken(TokenType.T_IF));
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
            if (IsMatch(TokenType.T_ELSE))
            {
                NextToken(TokenType.T_ELSE);
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
            ASTWhile node = new ASTWhile(NextToken(TokenType.T_WHILE));
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
            ASTDoWhile node = new ASTDoWhile(NextToken(TokenType.T_DO));
            if (IsMatch(TokenType.T_CURLY_BRACE_OPEN))
            {
                NextToken(TokenType.T_CURLY_BRACE_OPEN);
                while (!IsMatch(TokenType.T_CURLY_BRACE_CLOSE))
                    node._lines.Add(ParseLine());
                NextToken(TokenType.T_CURLY_BRACE_CLOSE);
            }
            else
                node._lines.Add(ParseLine());
            NextToken(TokenType.T_WHILE);
            NextToken(TokenType.T_BRACE_OPEN);
            node._condition = ParseExpression();
            NextToken(TokenType.T_BRACE_CLOSE);
            NextToken(TokenType.T_SEMICOLON);
            return node;
        }
        private ASTNode ParseFor()
        {
            ASTFor node = new ASTFor(NextToken(TokenType.T_FOR));
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
            ASTForeach node = new ASTForeach(NextToken(TokenType.T_FOREACH));
            NextToken(TokenType.T_BRACE_OPEN);
            node._container = ParseExpression();
            NextToken(TokenType.T_AS);
            node._key = ParseObjectOperator();
            NextToken(TokenType.T_DOUBLE_ARROW);
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
            ASTFunction node = new ASTFunction(NextToken(TokenType.T_FUNCTION));
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
            if (IsMatch(TokenType.T_COLON))
            {
                NextToken(TokenType.T_COLON);
                node._returnType = NextToken(TokenType.T_STATIC_STRING);
            }
            NextToken(TokenType.T_CURLY_BRACE_OPEN);
            while (!IsMatch(TokenType.T_CURLY_BRACE_CLOSE))
                node._lines.Add(ParseLine());
            NextToken(TokenType.T_CURLY_BRACE_CLOSE);
            return node;
        }
    
        //  Expression
        private ASTNode ParseExpression()
        {
            return ParseAssignment();
        }
        private ASTNode ParseAssignment()
        {
            ASTNode left = ParseLogical();

            if (IsMatch(
                    TokenType.T_ASSIGNMENT,
                    TokenType.T_ADD_ASSIGNMENT,
                    TokenType.T_SUB_ASSIGNMENT,
                    TokenType.T_MUL_ASSIGNMENT,
                    TokenType.T_DIV_ASSIGNMENT,
                    TokenType.T_POW_ASSIGNMENT,
                    TokenType.T_MOD_ASSIGNMENT,
                    TokenType.T_CONCAT_ASSIGNMENT))
            {
                TokenItem token = NextToken();

                if (left.Token.Type == TokenType.T_VARIABLE ||
                    left.Token.Type == TokenType.T_OBJECT_OPERATOR ||
                    left.Token.Type == TokenType.T_DOUBLE_COLON)
                {
                    ASTNode right = ParseAssignment();
                    left = new ASTBinary(token, left, right);
                }
                else
                    throw new SyntaxException("Unexpected token before assignment operator", token);
            }
            else if (IsMatch(
                         TokenType.T_IS_EQUAL,
                         TokenType.T_IS_NOT_EQUAL,
                         TokenType.T_IS_SMALLER,
                         TokenType.T_IS_SMALLER_OR_EQUAL,
                         TokenType.T_IS_GREATER,
                         TokenType.T_IS_GREATER_OR_EQUAL))
            {
                TokenItem token = NextToken();
                ASTNode right = ParseComparison();
                left = new ASTBinary(token, left, right);
            }
            else if (IsMatch(
                         TokenType.T_BIT_AND,
                         TokenType.T_LOGICAL_OR,
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
        private ASTNode ParseAddition()
        {
            ASTNode left = ParseMultiplication();
            while (IsMatch(TokenType.T_ADD, TokenType.T_SUB))
            {
                TokenItem token = NextToken();
                ASTNode right = ParseMultiplication();
                left = new ASTBinary(token, left, right);
            }
            return left;
        }
        private ASTNode ParseMultiplication()
        {
            ASTNode left = ParsePow();
            while (IsMatch(TokenType.T_MUL, TokenType.T_DIV, TokenType.T_MOD))
            {
                TokenItem token = NextToken();
                ASTNode right = ParsePow();
                left = new ASTBinary(token, left, right);
            }
            return left;
        }
        private ASTNode ParsePow()
        {
            ASTNode left = ParseObjectOperator();
            while (IsMatch(TokenType.T_POW))
            {
                TokenItem token = NextToken();
                ASTNode right = ParseObjectOperator();
                left = new ASTBinary(token, left, right);
            }
            return left;
        }
        private ASTNode ParseObjectOperator()
        {
            if (IsMatch(TokenType.T_VARIABLE))
            {
                ASTNode left = new ASTData(NextToken(TokenType.T_VARIABLE));
                while (IsMatch(TokenType.T_OBJECT_OPERATOR, TokenType.T_NULLSAFE_OBJECT_OPERATOR))
                {
                    TokenItem token = NextToken();
                    ASTNode right;
                    if (IsMatch(TokenType.T_CURLY_BRACE_OPEN))
                    {
                        NextToken(TokenType.T_CURLY_BRACE_OPEN);
                        right = ParseAddition();
                        NextToken(TokenType.T_CURLY_BRACE_CLOSE);
                    }
                    else
                        right = new ASTData(NextToken(TokenType.T_STATIC_STRING));
                    left = new ASTBinary(token, left, right);
                }
                return left;
            }

            return ParseUnary();
        }
        private ASTNode ParseUnary()
        {
            if (IsMatch(TokenType.T_ADD, TokenType.T_SUB))
            {
                TokenItem token = NextToken();
                ASTNode expression = ParseUnary();
                return new ASTUnary(token, expression, ASTUnary.OperatorSide.Left);
            }
            return ParsePrimary();
        }
        private ASTNode ParsePrimary()
        {
            if (IsMatch(TokenType.T_BRACE_OPEN))
            {
                NextToken();
                ASTNode expression = ParseExpression();
                NextToken(TokenType.T_BRACE_CLOSE);
                return expression;
            }
            return new ASTData(NextToken(
                TokenType.T_LNUMBER,
                TokenType.T_DNUMBER, 
                TokenType.T_STRING, 
                TokenType.T_VARIABLE,
                TokenType.T_STATIC_STRING));
        }
    }
}