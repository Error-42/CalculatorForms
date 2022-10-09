using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    abstract class Token
    {
        public int Start { get; set; }
        public int End { get; set; }

        public Token(int pos)
        {
            Start = pos;
            End = pos + 1;
        }

        public Token(int start, int end)
        {
            Start = start;
            End = end;
        }


        public class Symbol : Token
        {
            public enum Type
            {
                Plus,
                Minus,
                Star,
                Slash,
                LPar,
                RPar,
                Start,
                End,
            }

            public Type Ty { get; }

            public Symbol(int pos, Type type) : base(pos)
            {
                Ty = type;
            }
        }

        public class Value : Token
        {
            public double Val { get; }

            public Value(int start, int end, double value) : base(start, end)
            {
                Val = value;
            }
        }
    }
}
