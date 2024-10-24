﻿using PHP.Core.Lang.Exceptions;
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
            TokenType.InstanceOf,                //  instanceof
            TokenType.New,                       //  new
            TokenType.Clone,                    //  clone
            TokenType.Echo,                     //  echo
            TokenType.Print,                     //  print
            TokenType.TypeMixed,                 //  mixed
            TokenType.TypeBool,                  //  bool
            TokenType.TypeCallable,                //  callable
            TokenType.TypeInt,                       //  int
            TokenType.TypeFloat,                     //  float
            TokenType.TypeString,                    //  string
            TokenType.TypeArray,                     //  array
            TokenType.TypeObject,                    //  object
            TokenType.Unset,                     //  unset
            TokenType.Null,                      //  null
            TokenType.True,                      //  true
            TokenType.False,                       //  false
            TokenType.Use,                       //  use
            TokenType.Fn,                        //  fn
            TokenType.Function,                    //  function
            TokenType.Return,                    //  return
            TokenType.Static,                    //  static
            TokenType.Yield,                     //  yield
            TokenType.YieldFrom,                 //  yield_from
            TokenType.While,                     //  while
            TokenType.Do,                        //  do
            TokenType.For,                       //  for
            TokenType.Foreach,                   //  foreach
            TokenType.If,                        //  if
            TokenType.Else,                      //  else
            TokenType.As,                        //  as
            TokenType.Break,                     //  break
            TokenType.Continue,                  //  continue
            TokenType.Switch,                    //  switch
            TokenType.Case,                      //  case
            TokenType.Default,                   //  default
            TokenType.Exit,                      //  exit
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
            TokenType.DoubleArrow,                //  =>
            TokenType.Sub,                        //  -
            TokenType.AssignmentMul,              //  *=
            TokenType.AssignmentPow,              //  **=
            TokenType.Pow,                        //  **
            TokenType.Mul,                        //  *
            TokenType.AssignmentDiv,              //  /=
            TokenType.Div,                        //  /
            TokenType.AssignmentMod,              //  %=
            TokenType.Mod,                        //  %
            TokenType.Ellipsis,                  //  ...
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
            //TokenType.String,                     //  "" ''
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
        
        private void Reset()
        {
            _position = 0;
            _line = 0;
            _column = 0;
        }

        private bool HasNext() => _position <= Code.Length;

        private TokenItem NextToken()
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
            {
                /*TokenItem item = NextToken();
                if (item.Type == TokenType.String)
                {
                    
                }
                tokens.Add();*/
                tokens.Add(NextToken());
            }
            return tokens.ToArray();
        }
    }
}
