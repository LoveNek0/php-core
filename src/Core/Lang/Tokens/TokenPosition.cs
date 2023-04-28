using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.Tokens
{
    public class TokenPosition
    {
        public readonly int Index;
        public readonly int Line;
        public readonly int Column;

        internal TokenPosition(int position, int line, int column)
        {
            Index = position;
            Line = line;
            Column = column;
        }

        public override string ToString() => $"Position(Index: {Index}, Line: {Line}, Column: {Column})";
    }
}
