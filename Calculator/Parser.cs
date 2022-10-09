using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Node? lhs;
                if (Cur is Token.Symbol symbol && symbol.Ty == Token.Symbol.Type.Minus)
                {
                    Token neg = Cur;
                    Pos++;
                    Result<Node> factor = ParseFactor();
                    if (factor.IsErr()) return factor;

                    lhs = new Node.UnaryOp(Node.UnaryOp.Type.Neg, factor.Val!, neg.Start, factor.Val!.End);
                }
                else
                {
                    Result<Node> factor = ParseFactor();
                    if (factor.IsErr()) return factor;

                    lhs = factor.Val!;
                }

                throw new NotImplementedException();
            }

            public Result<Node> ParseFactor()
            {
                throw new NotImplementedException();
            }
        }

        static public Result<Node> Parse(List<Token> tokens)
        {
            ParserImpl impl = new(tokens);
            var res = impl.ParseExpression();
            if (res.IsErr()) return res;

            if (impl.Cur is Token.Symbol symbol && symbol.Ty == Token.Symbol.Type.End)
            {
                // Todo: give better error message;
                return Result<Node>.NewErr($"Unexpected token");
            }

            return res;
        }
    }
}
