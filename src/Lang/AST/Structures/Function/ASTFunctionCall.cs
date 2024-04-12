using System;
using System.Linq;
using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Structures.Function
{
    public class ASTFunctionCall : ASTNode
    {
        public readonly ASTNode Name;
        public readonly ASTNode[] Params;

        internal ASTFunctionCall(TokenItem token, ASTNode name, ASTNode[] @params) : base(token)
        {
            Name = name;
            Params = @params;
        }

        public override string ToString() => $"[{Name}({String.Join(", ", Params.Select(item => item.ToString()))})]";
    }
}