using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Constructions.Function
{
    public class ASTLambdaFunctionUseArgument
    {
        public readonly TokenItem Pointer;
        public readonly TokenItem Name;

        public ASTLambdaFunctionUseArgument(TokenItem pointer, TokenItem name)
        {
            Pointer = pointer;
            Name = name;
        }

        public override string ToString() => $"{(Pointer != null ? Pointer.Data : "")}{Name.Data}";
    }
}