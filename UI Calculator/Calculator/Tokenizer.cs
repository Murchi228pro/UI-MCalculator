using System.Numerics;

using System.Collections.Generic;

namespace Calculator
{
    public class Tokenizer
    {
        private string calcText;
        private int currentPos;
        private int length;
        public List<Token> Tokens = new List<Token>();

        public Tokenizer(string calcText)
        {
            this.calcText = calcText;
            this.length = calcText.Length;
            Tokens.Add(new Token("(", TokenType.LPAR));
            tokenize();
            Tokens.Add(new Token(")", TokenType.RPAR));
        }

        private void tokenize()
        {
            while (notEnd())
            {
                if (inNum())
                {
                    tokenizeNumber();
                }
                else if (calcText[currentPos] == ' ')
                {
                    currentPos++;
                }
                else
                {
                    tokenizeOperator();
                }
            }
            Tokens.Add(new Token("", TokenType.EOF));
        }

        private bool inNum(int offset = 0)
        {
            if (notEnd(offset))
            {
                if (char.IsDigit(calcText[currentPos + offset]))
                {
                    return true;
                }
            }
            return false;
        }


        private void tokenizeNumber()
        {
            List<char> numberBuff = new List<char>();
            int offset = 0;
            while (inNum(offset))
            {
                numberBuff.Add(calcText[currentPos + offset]);
                offset++;
            }
            Token token = new Token(string.Join("", numberBuff), TokenType.LITERAL);
            Tokens.Add(token);
            currentPos += offset;
        }

        private void tokenizeOperator()
        {
            Token token;
            switch (calcText[currentPos])
            {
                case '-':
                    token = new Token("-", TokenType.MINUS);
                    break;
                case '+':
                    token = new Token("+", TokenType.PLUS);
                    break;
                case '/':
                    token = new Token("/", TokenType.DIV);
                    break;
                case '*':
                    token = new Token("*", TokenType.MUL);
                    break;
                case '(':
                    token = new Token("(", TokenType.LPAR);
                    break;
                case ')':
                    token = new Token(")", TokenType.RPAR);
                    break;
                default:
                    token = new Token("", TokenType.EOF);
                    break;
            }
            Tokens.Add(token);
            currentPos++;
        }

        private bool notEnd(int offset = 0)
        {
            if (currentPos + offset < length)
            {
                return true;
            }
            return false;
        }


    }
}

