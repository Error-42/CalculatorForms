using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    static class Lexer
    {
        public static Result<List<Token>> Lex(string input)
        {
            int pos = 0;
            List<Token> tokens = new();

            while (pos < input.Length)
            {
                if (input[pos] == '+')
                {
                    tokens.Add(new Token.Symbol(pos, Token.Symbol.Type.Plus));
                    pos++;
                }
                else if (input[pos] == '-')
                { 
                    tokens.Add(new Token.Symbol(pos, Token.Symbol.Type.Minus));
                    pos++;
                }
                else if (input[pos] == '*')
                {
                    tokens.Add(new Token.Symbol(pos, Token.Symbol.Type.Star));
                    pos++;
                }
                else if (input[pos] == '/')
                {
                    tokens.Add(new Token.Symbol(pos, Token.Symbol.Type.Slash));
                    pos++;
                }
                else if (input[pos] == '(')
                {
                    tokens.Add(new Token.Symbol(pos, Token.Symbol.Type.LPar));
                    pos++;
                }
                else if (input[pos] == ')')
                {
                    tokens.Add(new Token.Symbol(pos, Token.Symbol.Type.RPar));
                    pos++;
                }
                else if (input[pos] >= '0' && input[pos] <= '9')
                {
                    int start = pos;
                    double val = 0;

                    while (pos < input.Length && input[pos] >= '0' && input[pos] <= '9')
                    {
                        val *= 10;
                        val += input[pos] - '0';
                        pos++;
                    }

                    if (input[pos] == '.')
                    {
                        double mul = 1;
                        pos++;

                        while (pos < input.Length && input[pos] >= '0' && input[pos] <= '9')
                        {
                            mul /= 10;
                            val += (input[pos] - '0') * mul;
                            pos++;
                        }
                    }

                    tokens.Add(new Token.Value(start, pos, val));
                }
                else
                {
                    return Result<List<Token>>.NewErr($"Unexpected character '{input[pos]}'");
                }
            }

            return Result<List<Token>>.NewOk(tokens);
        }
    }
}
