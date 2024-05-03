using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Constructions.Loops
{

    public class ASTWhile : ASTNode
    {
        public readonly ASTNode Condition;
        public readonly ASTNode Body;

        internal ASTWhile(TokenItem token, ASTNode condition, ASTNode body) : base(token)
        { 
            Condition = condition;
            Body = body;
        }

        public override string ToString() => $"[while({Condition})\n{Body}]";
    }
}
