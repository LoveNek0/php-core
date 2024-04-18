using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PHP.Core.Lang.Tokens
{
    public enum TokenType
    {
        [TokenTypePattern]
        EndOfFile,
        
        [TokenTypePattern(@"((\/\*\*)[\w\W\s\S]*(\*\/))")]
        DocComment,
        [TokenTypePattern(@"((\/\/(.|\w)*)|((\/\*)[\w\W\s\S]*?(\*\/)))")]
        Comment,                    //  // /**/
        [TokenTypePattern(@"\s+")]
        Whitespace,
        
        [TokenTypePattern(@"[<](([?]([pP][hH][pP])?)|[%])")]
        OpenTag,                    //  <?php <%
        [TokenTypePattern(@"<[?%]=")]
        OpenTagWithEcho,            //  <?= <%=
        [TokenTypePattern(@"[?%][>]")]
        CloseTag,                   //  ?> %>
        
        [TokenTypePattern(@"[,]")]
        Comma,                      //  ,
        [TokenTypePattern(@"[;]")]
        Semicolon,                  //  ;

        [TokenTypePattern(@"[(]")]
        BraceOpen,                  //  (
        [TokenTypePattern(@"[)]")]
        BraceClose,                 //  )
        [TokenTypePattern(@"[{]")]
        CurlyBraceOpen,             //  {
        [TokenTypePattern(@"[}]")]
        CurlyBraceClose,            //  }
        [TokenTypePattern(@"[\[]")]
        SquareBraceOpen,            //  [
        [TokenTypePattern(@"[\]]")]
        SquareBraceClose,           //  ]
        
        [TokenTypePattern(@"[!]")]
        Not,                         //  !
        [TokenTypePattern(@"[+]")]
        Add,                        //  +
        [TokenTypePattern(@"[-]")]
        Sub,                        //  -
        [TokenTypePattern(@"[*]")]
        Mul,                        //  *
        [TokenTypePattern(@"[\/]")]
        Div,                        //  /
        [TokenTypePattern(@"[%]")]
        Mod,                        //  %
        [TokenTypePattern(@"[*][*]")]
        Pow,                        //  **
        [TokenTypePattern(@"[.]")]
        Concat,                     //  .

        [TokenTypePattern(@"[=]")]
        Assignment,                 //  =
        [TokenTypePattern(@"[+][=]")]
        AssignmentAdd,              //  +=
        [TokenTypePattern(@"[-][=]")]
        AssignmentSub,              //  -=
        [TokenTypePattern(@"[*][=]")]
        AssignmentMul,              //  *=
        [TokenTypePattern(@"[\/][=]")]
        AssignmentDiv,              //  /=
        [TokenTypePattern(@"[%][=]")]
        AssignmentMod,              //  %=
        [TokenTypePattern(@"[*][*][=]")]
        AssignmentPow,              //  **=
        [TokenTypePattern(@"[.][=]")]
        AssignmentConcat,           //  .=
        
        [TokenTypePattern(@"[&]")]
        BitAnd,                     //  &
        [TokenTypePattern(@"[\^]")]
        BitXor,                     //  ^
        [TokenTypePattern(@"[|]")]
        BitNot,                     //  |
        [TokenTypePattern(@"[<][<]")]
        BitShiftLeft,               //  <<
        [TokenTypePattern(@"[>][>]")]
        BitShiftRight,              //  >>
        [TokenTypePattern(@"[~]")]
        Negation,                  //  ~

        [TokenTypePattern(@"[&][=]")]
        AssignmentBitAnd,           //  &=
        [TokenTypePattern(@"[\^][=]")]
        AssignmentBitXor,           //  ^=
        [TokenTypePattern(@"[|][=]")]
        AssignmentBitNot,           //  |=
        [TokenTypePattern(@"[<][<][=]")]
        AssignmentBitShiftLeft,     //  <<=
        [TokenTypePattern(@"[>][>][=]")]
        AssignmentBitShiftRight,    //  >>=

        [TokenTypePattern(@"[?][?]")]
        Coalesce,                   //  ??
        [TokenTypePattern(@"[?][?][=]")]
        AssignmentCoalesce,         //  ??=

        [TokenTypePattern(@"([&][&])|(\b[Aa][Nn][Dd]\b)")]
        LogicalAnd,                 //  && and
        [TokenTypePattern(@"([|][|])|(\b[Oo][Rr]\b)")]
        LogicalOr,                  //  || or
        [TokenTypePattern(@"\b[Xx][Oo][Rr]\b")]
        LogicalXor,                 //  xor

        [TokenTypePattern(@"[=][=]")]
        IsEqual,                    //  ==
        [TokenTypePattern(@"[=][=][=]")]
        IsIdentical,                //  ===
        [TokenTypePattern(@"([!][=])|([<][>])")]
        IsNotEqual,                 //  != <>
        [TokenTypePattern(@"[!][=][=]")]
        IsNotIdentical,             //  !==
        [TokenTypePattern(@"[>]")]
        IsGreater,                  // >
        [TokenTypePattern(@"[<]")]
        IsSmaller,                  // <
        [TokenTypePattern(@"[>][=]")]
        IsGreaterOrEqual,           //  >=
        [TokenTypePattern(@"[<][=]")]
        IsSmallerOrEqual,           //  <=
        [TokenTypePattern(@"[<][=][>]")]
        IsSpaceship,                //  <=>

        [TokenTypePattern(@"[-][-]")]
        Decrement,                  //  --
        [TokenTypePattern(@"[+][+]")]
        Increment,                  //  ++

        [TokenTypePattern(@"[0-9]+")]
        Integer,                    //  123 012 0x1ac
        [TokenTypePattern(@"([0-9]+)[.]([0-9]+)")]
        Float,                      //  1.5
        [TokenTypePattern(@"('(?:(([^\\'])|(\\.)))*')")]
        String,                     //  "" ''
        [TokenTypePattern(@"[a-zA-Z_][a-zA-Z0-9_]*")]
        ConstString,               //  HELLO_WORLD

        [TokenTypePattern(@"([$]+([a-zA-Z_][a-zA-Z0-9_]*))")]
        Variable,                 //  $var_1

        //  Function
        [TokenTypePattern(@"\b[Ff][Uu][Nn][Cc][Tt][Ii][Oo][Nn]\b")]
        Function,                 //  function
        [TokenTypePattern(@"\b[Ss][Tt][Aa][Tt][Ii][Cc]\b")]
        Static,                   //  static
        [TokenTypePattern(@"[Rr][Ee][Tt][Uu][Rr][Nn]")]
        Return,                   //  return
        [TokenTypePattern(@"[Yy][Ii][Ee][Ll][Dd]")]
        Yield,                    //  yield
        [TokenTypePattern(@"[Yy][Ii][Ee][Ll][Dd][_][Ff][Rr][Oo][Mm]")]
        YieldFrom,               //  yield_from
        [TokenTypePattern(@"[Cc][Aa][Ll][Ll][Aa][Bb][Ll][Ee]")]
        Callable,                 //  callable
        [TokenTypePattern(@"[.][.][.]")]
        Ellipsis,                 //  ...
        [TokenTypePattern(@"[Ff][Nn]")]
        Fn,                       //  fn

        //  Class
        [TokenTypePattern(@"[Aa][Bb][Ss][Tt][Rr][Aa][Cc][Tt]")]
        Abstract,                 //  abstract
        [TokenTypePattern(@"[Ii][Nn][Tt][Ee][Rr][Ff][Aa][Cc][Ee]")]
        Interface,                //  interface
        [TokenTypePattern(@"[Tt][Rr][Aa][Ii][Tt]")]
        Trait,                    //  trait
        [TokenTypePattern(@"[Cc][Ll][Aa][Ss][Ss]")]
        Class,                    //  class
        [TokenTypePattern(@"[Pp][Uu][Bb][Ll][Ii][Cc]")]
        Public,                   //  public
        [TokenTypePattern(@"[Pp][Rr][Ii][Vv][Aa][Tt][Ee]")]
        Private,                  //  private
        [TokenTypePattern(@"[Pp][Rr][Oo][Tt][Ee][Cc][Tt][Ee][Dd]")]
        Protected,                //  protected
        [TokenTypePattern(@"[\:][\:]")]
        DoubleColon,             //  ::
        [TokenTypePattern(@"[-][>]")]
        ObjectOperator,          //  ->
        [TokenTypePattern(@"[?][-][>]")]
        NullsafeObjectOperator, //  ?->
        [TokenTypePattern(@"\b[Cc][Ll][Oo][Nn][Ee]\b")]
        Clone,                    //  clone
        [TokenTypePattern(@"[&]")]
        Extends,                  //  extends
        [TokenTypePattern(@"[&]")]
        Implements,               //  implements
        [TokenTypePattern(@"[&]")]
        Final,                    //  final
        [TokenTypePattern(@"\b[Ii][Nn][Ss][Tt][Aa][Nn][Cc][Ee][Oo][Ff]\b")]
        InstanceOf,               //  instanceof
        [TokenTypePattern(@"[&]")]
        InsteadOf,                //  insteadof
        [TokenTypePattern(@"\b[Nn][Ee][Ww]\b")]
        New,                      //  new
        [TokenTypePattern(@"[?]")]
        Query,                    //  ?
        [TokenTypePattern(@"[&]")]
        Attribute,                                //  attribytes

        //  Array
        Array,                    //  array()
        DoubleArrow,             //  =>

        //  Casting
        [TokenTypePattern(@"[(]\s*(([Ii][Nn][Tt])|([Ii][Nn][Tt][Ee][Gg][Ee][Rr]))\s*[)]")]
        IntCast,                 //  (int) (integer)
        [TokenTypePattern(@"[(]\s*(([Rr][Ee][Aa][Ll])|([Dd][Oo][Uu][Bb][Ll][Ee])|([Ff][Ll][Oo][Aa][Tt]))\s*[)]")]
        FloatCast,              //  (real) (double) (float)
        [TokenTypePattern(@"[(]\s*([Ss][Tt][Rr][Ii][Nn][Gg])\s*[)]")]
        StringCast,              //  (string)
        [TokenTypePattern(@"[(]\s*(([Bb][Oo][Oo][Ll])|([Bb][Oo][Oo][Ll][Ee][Aa][Nn]))\s*[)]")]
        BoolCast,                //  (bool) (boolean)
        [TokenTypePattern(@"[(]\s*([Aa][Rr][Rr][Aa][Yy])\s*[)]")]
        ArrayCast,               //  (array)
        [TokenTypePattern(@"[(]\s*([Oo][Bb][Jj][Ee][Cc][Tt])\s*[)]")]
        ObjectCast,              //  (object)
        [TokenTypePattern(@"[(]\s*([Uu][Nn][Ss][Ee][Tt])\s*[)]")]
        UnsetCast,               //  (unset)

        //  Cycles
        Do,                       //  do
        While,                    //  while
        EndWhile,                 //  endwhile
        For,                      //  for
        EndFor,                   //  endfor
        Foreach,                  //  foreach
        EndForeach,               //  endforeach
        As,                       //  as
        Continue,                 //  continue

        //  Switch
        Switch,                   //  switch
        EndSwitch,                //  endswitch
        Case,                     //  case
        Break,                    //  break
        Default,                  //  default

        //  if ... else
        If,                       //  if
        Else,                     //  else
        ElseIf,                   //  elseif
        EndIf,                    //  endif

        //  try .. catch
        Throw,                    //  throw
        Try,                      //  try
        Catch,                    //  catch
        Finally,                  //  finally

        //  Magic Constants
        /*
        T_FILE,                     //  __FILE__
        T_FUNC_C,                   //  __FUNCTION__
        T_CLASS_C,                  //  __CLASS__
        T_DIR,                      //  __DIR__
        T_TRAIT_C,                  //  __TRAIT__
        T_LINE,                     //  __LINE__
        T_METHOD_C,                 //  __METHOD__
        T_NS_C,                     //  __NAMESPACE__
        */

        //  Language Constructions
        Const,                    //  const
        [TokenTypePattern(@"\b[Ee][Cc][Hh][Oo]\b")]
        Echo,                     //  echo
        [TokenTypePattern(@"\b[Pp][Rr][Ii][Nn][Tt]\b")]
        Print,                    //  print
        
        Match,                    //  match
        Global,                   //  global
        Declare,                  //  declare
        EndDeclare,               //  enddeclare

        //  Namespaces
        Namespace,                //  namespace
        NamespaceName,           //  a\b\c
        Use,                      //  use
        NamespaceCallName,      //  \a\b\c

        //  Goto points
        [TokenTypePattern(@"\b[Gg][Oo][Tt][Oo]\b")]
        Goto,                     //  goto
        [TokenTypePattern(@"[\:]")]
        Colon,                    //  :

        //  Including Operators
        Include,                  //  include
        IncludeOnce,             //  include_once
        Require,                  //  require
        RequireOnce,             //  require_once

        //  Basic Functions
        //  T_UNSET,                    //  unset()
        //  T_EMPTY,                    //  empty()
        //  T_EVAL,                     //  eval()
        //  T_EXIT,                     //  exit() die()
        //  T_HALT_COMPILER,            //  __halt_compiler()
        //  T_ISSET,                    //  isset()
        //  T_LIST,                     //  list()
        //  T_PRINT,                    //  print()

        //  Incorrect symbols
        //  T_BAD_CHARACTER, \n (0x0a) и \r (0x0d)
    }
}
