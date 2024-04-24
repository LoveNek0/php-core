using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Constructions.Function
{

    public class ASTFunctionArgument
    {
        public readonly TokenItem Type;
        public readonly bool IsIsPointer;
        public readonly bool IsMultiArgument;
        public readonly TokenItem Name;
        public readonly ASTNode ByDefault;

        public ASTFunctionArgument(TokenItem type, bool isPointer, bool isMultiArgument, TokenItem name, ASTNode byDefault)
        {
            Type = type;
            IsIsPointer = isPointer;
            IsMultiArgument = isMultiArgument;
            Name = name;
            ByDefault = byDefault;
        }

        public override string ToString() =>
            $"{(Type != null ? $"{Type.Data} " : "")}{(IsIsPointer ? "&" : "")}{(IsMultiArgument ? "..." : "")}{Name.Data}{(ByDefault != null ? $" = {ByDefault}" : "")}";
    }
}
