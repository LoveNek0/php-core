using System;
using System.Linq;
using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Constructions.Function
{

    public class ASTFunction : ASTNode
    {
        public readonly TokenItem Name;
        public readonly ASTFunctionArgument[] Arguments;
        public readonly TokenItem ReturnType;
        public readonly ASTNode[] Body;
        
        public ASTFunction(TokenItem token, TokenItem name, ASTFunctionArgument[] arguments, TokenItem returnType, ASTNode[] body) : base(token)
        {
            Name = name;
            Arguments = arguments;
            ReturnType = returnType;
            Body = body;
        }

        public override string ToString() =>
            $"function {Name.Data} ({String.Join(", ", Arguments.Select(a => a.ToString()))}){(ReturnType != null ? $": {ReturnType.Data}" : "")}{{\n{String.Join("\n", Body.Select(a => a.ToString()))}\n}}";
    }
}