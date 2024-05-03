using System;
using System.Linq;
using Microsoft.VisualBasic;
using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Constructions.Function
{
    public class ASTLambdaFunction : ASTNode
    {
        public readonly bool ResultAsPointer;
        public readonly ASTFunctionArgument[] Arguments;
        public readonly ASTLambdaFunctionUseArgument[] Use;
        public readonly TokenItem ReturnType;
        public readonly ASTNode[] Body;
        
        internal ASTLambdaFunction(TokenItem token, bool resultAsPointer, ASTFunctionArgument[] arguments, TokenItem returnType, ASTLambdaFunctionUseArgument[] use, ASTNode[] body) : base(token)
        {
            ResultAsPointer = resultAsPointer;
            Arguments = arguments;
            Use = use;
            ReturnType = returnType;
            Body = body;
        }

        public override string ToString() => 
            $"function{(ResultAsPointer?"&":"")}({string.Join(", ", Arguments.Select(a => a.ToString()))}){(Use.Length > 0 ? $"use ({string.Join(", ", Use.Select(a => a.ToString()))})" : "")}{(ReturnType != null ? $": {ReturnType.Data}" : "")}{{\n{string.Join("\n", Body.Select(a => a.ToString()))}\n}}";
    }
}