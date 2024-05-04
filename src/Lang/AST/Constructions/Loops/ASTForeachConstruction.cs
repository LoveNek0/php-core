using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Constructions.Loops
{

    public class ASTForeachConstruction : ASTNode
    {
        public readonly ASTNode Collection;
        public readonly TokenItem Key;
        public readonly TokenItem Value;
        public readonly ASTNode Body;

        internal ASTForeachConstruction(TokenItem token, ASTNode collection, TokenItem key, TokenItem value, ASTNode body) : base(token)
        {
            Collection = collection;
            Key = key;
            Value = value;
            Body = body;
        }

        public override string ToString() =>
            $"[foreach({Collection} as {(Key == null ? "" : Key.Data + " => ")}{Value.Data})\n{Body}]";
    }
}
