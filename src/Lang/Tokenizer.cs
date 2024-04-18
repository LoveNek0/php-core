using PHP.Core.Lang.Exceptions;
using PHP.Core.Lang.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PHP.Core.Lang
{
    public class Tokenizer
    {
       public static TokenType[] _queue = new[]
        {
            TokenType.DocComment,                 // /** */
            TokenType.Comment,                    // // # /* */
            TokenType.OpenTagWithEcho,         //  <?= <%=
            TokenType.OpenTag,                 //  <? <?php <%
            TokenType.CloseTag,                 //  ?> %>
            TokenType.BoolCast,                  //  (bool)(boolean)
            TokenType.IntCast,                  //  (int)(integer)
            TokenType.FloatCast,                 //  (float)(real)(double)
            TokenType.ArrayCast,                  //  (array)
            TokenType.ObjectCast,                 //  (object)
            TokenType.StringCast,                 //  (string)
            TokenType.UnsetCast,                 //  (unset)
            TokenType.InstanceOf,                //  instanceof
            TokenType.New,                       //  new
            TokenType.Clone,                    //  clone
            TokenType.Comma,                      //  ,
            TokenType.Semicolon,                  //  ;
            TokenType.DoubleColon,                  //  ::
            TokenType.Colon,                        //  :
            TokenType.BraceOpen,                  //  (
            TokenType.BraceClose,                 //  )
            TokenType.CurlyBraceOpen,             //  {  
            TokenType.CurlyBraceClose,            //  }
            TokenType.SquareBraceOpen,            //  [
            TokenType.SquareBraceClose,           //  ]
            TokenType.Increment,                  //  ++
            TokenType.AssignmentAdd,              //  +=
            TokenType.Add,                        //  +
            TokenType.AssignmentSub,              //  -=
            TokenType.Decrement,                  //  --
            TokenType.ObjectOperator,               //  ->
            TokenType.Sub,                        //  -
            TokenType.AssignmentMul,              //  *=
            TokenType.AssignmentPow,              //  **=
            TokenType.Pow,                        //  **
            TokenType.Mul,                        //  *
            TokenType.AssignmentDiv,              //  /=
            TokenType.Div,                        //  /
            TokenType.AssignmentMod,              //  %=
            TokenType.Mod,                        //  %
            TokenType.AssignmentConcat,           //  .=
            TokenType.Concat,                     //  .
            TokenType.IsIdentical,                //  ===
            TokenType.IsEqual,                    //  ==
            TokenType.Assignment,                 //  =
            TokenType.AssignmentBitAnd,           //  &=
            TokenType.LogicalAnd,                 //  && and
            TokenType.BitAnd,                     //  &
            TokenType.AssignmentBitXor,           //  ^=
            TokenType.BitXor,                     //  ^
            TokenType.AssignmentBitNot,           //  |=
            TokenType.LogicalOr,                  //  || or
            TokenType.BitNot,                     //  |
            TokenType.Negation,                   //  ~
            TokenType.AssignmentBitShiftLeft,     //  <<=
            TokenType.BitShiftLeft,               //  <<
            TokenType.IsSpaceship,                //  <=>
            TokenType.IsSmallerOrEqual,           //  <=
            TokenType.IsNotIdentical,             //  !==
            TokenType.IsNotEqual,                 //  != <>
            TokenType.Not,                        //  !
            TokenType.IsSmaller,                  // <
            TokenType.AssignmentBitShiftRight,    //  >>=
            TokenType.BitShiftRight,              //  >>
            TokenType.IsGreaterOrEqual,           //  >=
            TokenType.IsGreater,                  // >
            TokenType.LogicalXor,                 //  xor
            TokenType.AssignmentCoalesce,         //  ??=
            TokenType.Coalesce,                   //  ??
            TokenType.NullsafeObjectOperator,       //  ?->
            TokenType.Query,                      //  ?
            TokenType.Float,                      //  1.5
            TokenType.Integer,                    //  123 012 0x1ac
            TokenType.String,                     //  "" ''
            TokenType.Variable,                 //  $var_1
            TokenType.ConstString,                  //   Hello_World
            TokenType.Whitespace,                 //  \t \r \n
        };
        
        public readonly string Code;

        private int _position;
        private int _line;
        private int _column;
        
        public Tokenizer(string code)
        {
            this.Code = code;
            Reset();
        }

        public void Reset()
        {
            _position = 0;
            _line = 0;
            _column = 0;
        }

        public bool HasNext() => _position <= Code.Length;

        public TokenItem NextToken()
        {
            if (_position == Code.Length)
            {
                _position++;
                return new TokenItem(TokenType.EndOfFile, "", _line, _column, _position - 1);
            }

            string part = Code.Substring(_position);
            foreach (TokenType type in _queue)
            {
                Match match = Regex.Match(part, $"^{type.GetPattern()}");
                if (match.Success)
                {
                    TokenItem item = new TokenItem(type, match.Value, _position, _line, _column);
                    foreach (char symbol in match.Value)
                        if (symbol == '\n')
                        {
                            _line++;
                            _column = 0;
                        }
                        else
                            _column++;
                    _position += match.Length;
                    return item;
                }
            }
            throw new LexicalException($"Unexpected symbol \"{Code[_position]}\"", _line, _column);
        }

        public TokenItem[] GetTokens()
        {
            List<TokenItem> tokens = new List<TokenItem>();
            while (HasNext())
                tokens.Add(NextToken());
            return tokens.ToArray();
        }
    }
}
