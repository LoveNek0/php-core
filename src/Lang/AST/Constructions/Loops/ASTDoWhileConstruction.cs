using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Constructions.Loops
{
    public class ASTDoWhileConstruction : ASTNode
    {
        public readonly ASTNode Condition;
        public readonly ASTNode Body;

        internal ASTDoWhileConstruction(TokenItem token, ASTNode condition, ASTNode body) : base(token)
        { 
            Condition = condition;
            Body = body;
        }

        public override string ToString() => $"[do\n{Body}\nwhile({Condition})]";
    }
}
