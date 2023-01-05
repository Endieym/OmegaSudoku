﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.Exceptions;

[Serializable]
internal class InputException : Exception
{
    public InputException() { }

    public InputException(string message) : base("Input Error: \n"
        + message)
    { }
    public InputException(string message, Exception inner) : base("Input Error: \n"
        + message, inner)
    { }

    public InputException(string input, int pos) : base("Input Error!\n" +
        string.Concat(Enumerable.Repeat(' ', pos)) + '↓' +
        '\n' + input)
    { }

}

[Serializable]

internal class EmptyStringException : InputException
{
    public EmptyStringException() : base("String is Empty!") { }

}

internal class IllegalSizeException : InputException
{
    public IllegalSizeException(int length) : base("String is not at the correct length!\n" +
        "Input length: " + length +
        "\n" + "Length of the string should be between 1*1 to 25*25. ")
    { }

}

internal class IllegalCharacterException : InputException
{
    public IllegalCharacterException(string input, int pos) : base(input, pos) { }

}
