using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    static class Parser
    {
        class ParserImpl
        {
            public List<Token> Tokens { get; set; }
            public int Pos { get; set; }
            public Token Cur { get => Tokens[Pos]; }

            public ParserImpl(List<Token> tokens)
            {
                Tokens = tokens;
                Pos = 1; // skip 'Start' token
            }

            public Result<Node> ParseExpression()
            {
                Node? node;

                // extra scope, so symbol is not declared outside
                {
                    if (Cur is Token.Symbol symbol && symbol.Ty == Token.Symbol.Type.Minus)
                    {
                        Token neg = Cur;
                        Pos++;
                        Result<Node> factor = ParseFactor();
                        if (factor.IsErr()) return factor;

                        node = new Node.UnaryOp(Node.UnaryOp.Type.Neg, factor.Val!, neg.Start, factor.Val!.End);
                    }
                    else
                    {
                        Result<Node> factor = ParseFactor();
                        if (factor.IsErr()) return factor;

                        node = factor.Val!;
                    }
                }

                while (Cur is Token.Symbol symbol)
                {
                    if (symbol.Ty == Token.Symbol.Type.Plus || symbol.Ty == Token.Symbol.Type.Minus)
                    {
                        Pos++;
                        Result<Node> rhs = ParseFactor();
                        if (rhs.IsErr()) return rhs;

                        node = new Node.BinaryOp(node, symbol.Ty switch
                        {
                            Token.Symbol.Type.Plus => Node.BinaryOp.Type.Add,
                            Token.Symbol.Type.Minus => Node.BinaryOp.Type.Sub,
                        }, rhs.Val!);
                    }
                    else break;
                }

                return Result<Node>.NewOk(node!);
            }

            public Result<Node> ParseFactor()
            {
                Result<Node> res = ParseUnit();
                if (res.IsErr()) return res;

                Node? node = res.Val!;

                while (Cur is Token.Symbol symbol)
                {
                    if (symbol.Ty == Token.Symbol.Type.Plus)
                    {
                        Pos++;
                        Result<Node> rhs = ParseFactor();
                        if (rhs.IsErr()) return rhs;

                        node = new Node.BinaryOp(node, symbol.Ty switch
                        {
                            Token.Symbol.Type.Star => Node.BinaryOp.Type.Mul,
                            Token.Symbol.Type.Slash => Node.BinaryOp.Type.Div,
                        }, rhs.Val!);
                    }
                    else break;
                }

                return Result<Node>.NewOk(node!);
            }

            public Result<Node> ParseUnit()
            {
                if (Cur is Token.Symbol symbol)
                {
                    if (symbol.Ty == Token.Symbol.Type.LPar)
                    {
                        Pos++;
                        Result<Node> res = ParseExpression();
                        if (res.IsErr()) return res;

                        if (Cur is Token.Symbol symbol2 && symbol2.Ty == Token.Symbol.Type.RPar)
                        {
                            Pos++;
                            return res;
                        }
                        else
                        {
                            // Todo: improve error message
                            return Result<Node>.NewErr("Unexpected token, expected ')'");
                        }
                    }
                    else
                    {
                        // Todo: improve error message
                        return Result<Node>.NewErr("Unexpected token");
                    }
                }
                else if (Cur is Token.Value value)
                {
                    var res = Result<Node>.NewOk(new Node.Value(value));
                    Pos++;
                    return res;
                }
                else
                {
                    throw new ArgumentException("Unhandle type of token");
                }
            }
        }

        static public Result<Node> Parse(List<Token> tokens)
        {
            ParserImpl impl = new(tokens);
            var res = impl.ParseExpression();
            if (res.IsErr()) return res;

            if (impl.Cur is Token.Symbol symbol && symbol.Ty == Token.Symbol.Type.End)
            {
                return res;
            }
            // Todo: give better error message;
            return Result<Node>.NewErr($"Unexpected token");
        }
    }
}
