using PHP.Core.Lang.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.AST.Operators
{
    public class ASTUnary : ASTNode
    {
        public enum OperatorSide
        {
            Left,
            Right
        }
        public readonly ASTNode Operand;
        public readonly OperatorSide Side;
        internal ASTUnary(TokenItem token, ASTNode operand, OperatorSide side) : base(token)
        {
            Operand = operand;
            Side = side;
        }

        public override string ToString() => $"({(Side == OperatorSide.Left ? Token.Data : "")}{Operand.Token.Data}{(Side == OperatorSide.Right ? Token.Data : "")})";
    }
}
