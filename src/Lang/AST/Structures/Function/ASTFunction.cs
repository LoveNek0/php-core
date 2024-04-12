using System;
using System.Collections.Generic;
using System.Linq;
using PHP.Core.Lang.AST.Base;
using PHP.Core.Lang.Tokens;

namespace PHP.Core.Lang.AST.Structures.Function
{

    public class ASTFunction : ASTNode
    {
        public readonly ASTNode Name;
        public readonly ASTFunctionArgument[] Arguments;
        public readonly TokenItem ReturnType;
        public readonly ASTNode[] Lines;
        
        internal ASTFunction(TokenItem token) : base(token)
        {
        }

        public override string ToString() =>
            $"function {Name} ({String.Join(", ", Arguments.GetEnumerator())}){{\n{String.Join("\n", Lines.GetEnumerator())}\n}}";
    }
}