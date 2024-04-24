using System;
using System.Linq;
using Microsoft.VisualBasic;
using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Constructions.Function
{
    public class ASTLambdaFunction : ASTNode
    {
        public readonly ASTFunctionArgument[] Arguments;
        public readonly ASTLambdaFunctionUseArgument[] Use;
        public readonly ASTNode[] Body;
        
        internal ASTLambdaFunction(TokenItem token, ASTFunctionArgument[] arguments, ASTLambdaFunctionUseArgument[] use, ASTNode[] body) : base(token)
        {
            Arguments = arguments;
            Use = use;
            Body = body;
        }

        public override string ToString() => 
            $"function({string.Join(", ", Arguments.Select(a => a.ToString()))}){(Use.Length > 0 ? $"use ({string.Join(", ", Use.Select(a => a.ToString()))})" : "")}{{\n{string.Join("\n", Body.Select(a => a.ToString()))}\n}}";
    }
}