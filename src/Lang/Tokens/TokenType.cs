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
        [TokenTypePattern(@"((?<!\\)(?:\\\\)*'((?:\\'|[^'])*)(?<!\\)(?:\\\\)*')|((?<!\\)(?:\\\\)*""((?:\\""|[^""])*)(?<!\\)(?:\\\\)*"")")]
        String,                     //  "" ''
        [TokenTypePattern(@"")]
        VariablePlaceholder,
        [TokenTypePattern(@"[a-zA-Z_][a-zA-Z0-9_]*")]
        ConstString,               //  HELLO_WORLD
        [TokenTypePattern(@"\b[Nn][Uu][Ll][Ll]\b")]
        Null,                       //  null
        [TokenTypePattern(@"\b[Tt][Rr][Uu][Ee]\b")]
        True,                       //  true
        [TokenTypePattern(@"\b[Ff][Aa][Ll][Ss][Ee]\b")]
        False,                      //  false
        [TokenTypePattern(@"([$]+([a-zA-Z_][a-zA-Z0-9_]*))")]
        Variable,                 //  $var_1
        

        //  Function
        [TokenTypePattern(@"\b[Ff][Uu][Nn][Cc][Tt][Ii][Oo][Nn]\b")]
        Function,                 //  function
        [TokenTypePattern(@"\b[Ss][Tt][Aa][Tt][Ii][Cc]\b")]
        Static,                   //  static
        [TokenTypePattern(@"\b[Rr][Ee][Tt][Uu][Rr][Nn]\b")]
        Return,                   //  return
        [TokenTypePattern(@"\b[Yy][Ii][Ee][Ll][Dd]\b")]
        Yield,                    //  yield
        [TokenTypePattern(@"\b[Yy][Ii][Ee][Ll][Dd][_][Ff][Rr][Oo][Mm]\b")]
        YieldFrom,               //  yield_from
        [TokenTypePattern(@"[.][.][.]")]
        Ellipsis,                 //  ...
        [TokenTypePattern(@"\b[Ff][Nn]\b")]
        Fn,                       //  fn

        //  Types
        [TokenTypePattern(@"\b(([Bb][Oo][Oo][Ll])|([Bb][Oo][Oo][Ll][Ee][Aa][Nn]))\b")]
        TypeBool,                   //  bool
        [TokenTypePattern(@"\b[Ii][Nn][Tt]\b")]
        TypeInt,                    //  int
        [TokenTypePattern(@"\b(([Ff][Ll][Oo][Aa][Tt])|([Dd][Oo][Uu][Bb][Ll][Ee])|([Rr][Ee][Aa][Ll]))\b")]
        TypeFloat,                 //  float
        [TokenTypePattern(@"\b[Ss][Tt][Rr][Ii][Nn][Gg]\b")]
        TypeString,                //  string
        [TokenTypePattern(@"\b[Oo][Bb][Jj][Ee][Cc][Tt]\b")]
        TypeObject,               //  object
        [TokenTypePattern(@"\b[Aa][Rr][Rr][Aa][Yy]\b")]
        TypeArray,                //  array
        [TokenTypePattern(@"\b[Mm][Ii][Xx][Ee][Dd]\b")]
        TypeMixed,                //  mixed
        [TokenTypePattern(@"\b[Cc][Aa][Ll][Ll][Aa][Bb][Ll][Ee]\b")]
        TypeCallable,                 //  callable
        
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

        [TokenTypePattern(@"[=][>]")]
        DoubleArrow,             //  =>

        //  Cycles
        [TokenTypePattern(@"\b[Dd][Oo]\b")]
        Do,                       //  do
        [TokenTypePattern(@"\b[Ww][Hh][Ii][Ll][Ee]\b")]
        While,                    //  while
        [TokenTypePattern(@"\b[Ff][Oo][Rr]\b")]
        For,                      //  for
        [TokenTypePattern(@"\b[Ff][Oo][Rr][Ee][Aa][Cc][Hh]\b")]
        Foreach,                  //  foreach
        [TokenTypePattern(@"\b[Aa][Ss]\b")]
        As,                       //  as
        [TokenTypePattern(@"\b[Cc][Oo][Nn][Tt][Ii][Nn][Uu][Ee]\b")]
        Continue,                 //  continue
        [TokenTypePattern(@"\b[Bb][Rr][Ee][Aa][Kk]\b")]
        Break,                    //  break

        //  Switch
        [TokenTypePattern(@"\b[Ss][Ww][Ii][Tt][Cc][Hh]\b")]
        Switch,                   //  switch
        [TokenTypePattern(@"\b[Cc][Aa][Ss][Ee]\b")]
        Case,                     //  case
        [TokenTypePattern(@"\b[Dd][Ee][Ff][Aa][Uu][Ll][Tt]\b")]
        Default,                  //  default

        //  if ... else
        [TokenTypePattern(@"\b[Ii][Ff]\b")]
        If,                       //  if
        [TokenTypePattern(@"\b[Ee][Ll][Ss][Ee]\b")]
        Else,                     //  else

        //  try .. catch
        Throw,                    //  throw
        Try,                      //  try
        Catch,                    //  catch
        Finally,                  //  finally

        //  Language Constructions
        Const,                    //  const
        [TokenTypePattern(@"\b[Ee][Cc][Hh][Oo]\b")]
        Echo,                     //  echo
        [TokenTypePattern(@"\b[Pp][Rr][Ii][Nn][Tt]\b")]
        Print,                    //  print
        [TokenTypePattern(@"\b[Uu][Nn][Ss][Ee][Tt]\b")]
        Unset,                    //  unset
        [TokenTypePattern(@"\b[Ee][Xx][Ii][Tt]\b")]
        Exit,                     //  exit
        
        
        Match,                    //  match
        Global,                   //  global
        Declare,                  //  declare

        //  Namespaces
        Namespace,                //  namespace
        NamespaceName,           //  a\b\c
        UseStatic,               //  use static]
        [TokenTypePattern(@"\b[Uu][Ss][Ee]\b")]
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
    }
}
