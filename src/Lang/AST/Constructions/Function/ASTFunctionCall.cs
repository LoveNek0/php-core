using System;
using System.Linq;
using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Constructions.Function
{
    public class ASTFunctionCall : ASTNode
    {
        public readonly ASTNode Name;
        public readonly ASTNode[] Arguments;

        internal ASTFunctionCall(TokenItem token, ASTNode name, ASTNode[] arguments) : base(token)
        {
            Name = name;
            Arguments = arguments;
        }

        public override string ToString() => $"[{Name}({String.Join(", ", Arguments.Select(item => item.ToString()))})]";
    }
}