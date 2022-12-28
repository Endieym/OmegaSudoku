using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku;

[Serializable]
internal class InputException : Exception
{
    public InputException() { }

    public InputException(string message) : base("Input Error: \n" 
        + message) { }
    public InputException(string message, Exception inner) : base("Input Error: \n" 
        + message, inner) { }

    public InputException(string input, int pos) : base("Input Error!\n"+
        String.Concat(Enumerable.Repeat(' ', pos)) +'↓'+
        '\n' +input) { }

}

[Serializable]

internal class EmptyStringException : InputException
{
    public EmptyStringException() : base("String is Empty!") { }

}

internal class IllegalSizeException : InputException
{
    public IllegalSizeException(int length) : base("String is not at the correct length!\n"+
        "Input length: "+ length +
        "\n"+"Expected length: "+(Constants.BoardScale* Constants.BoardScale)) { }

}

internal class IllegalCharacterException : InputException
{
    public IllegalCharacterException(string input ,int pos) : base(input, pos) { }

}

