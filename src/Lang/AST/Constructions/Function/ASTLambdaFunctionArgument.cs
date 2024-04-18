using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Constructions.Function
{
    public class ASTLambdaFunctionArgument
    {
        public readonly TokenItem Type;
        public readonly TokenItem Pointer;
        public readonly TokenItem MultiParams;
        public readonly TokenItem Name;
        
        internal ASTLambdaFunctionArgument(TokenItem type, TokenItem pointer, TokenItem multiParams, TokenItem name)
        {
            Type = type;
            Pointer = pointer;
            MultiParams = multiParams;
            Name = name;
        }

        public override string ToString() => $"{(Type != null ? Type.Data + " " : "")}{(Pointer != null ? Pointer.Data : "")}{(MultiParams != null ? MultiParams.Data : "")}{Name.Data}";
    }
}