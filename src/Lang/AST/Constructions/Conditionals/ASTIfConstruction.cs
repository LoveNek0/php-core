using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Constructions.Conditionals
{
    public class ASTIfConstruction : ASTNode
    {
        public readonly ASTNode Condition;
        public readonly ASTNode TrueBlock;
        public readonly ASTNode FalseBlock;
        internal ASTIfConstruction(TokenItem token, ASTNode condition, ASTNode trueBlock, ASTNode falseBlock) : base(token)
        {
            Condition = condition;
            TrueBlock = trueBlock;
            FalseBlock = falseBlock;
        }

        public override string ToString() => $"[if({Condition})\n{TrueBlock}{(FalseBlock != null ? $"\nelse\n{FalseBlock}" : "")}]";
    }
}