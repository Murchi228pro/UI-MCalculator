namespace Calculator
{
    public enum TokenType
    {
        LITERAL,
        PLUS,
        MINUS,
        MUL,
        DIV,
        LPAR,
        RPAR,
        EOF,
    }
    public class Token
    {
        public TokenType Type;
        public string Value;
        public Token(string value, TokenType tokenType)
        {
            this.Value = value;
            this.Type = tokenType;
        }
    }
}
