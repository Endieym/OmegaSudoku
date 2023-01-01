using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmegaSudoku.Exceptions;

namespace OmegaSudoku.UI;

internal static class StringValidation
{
    public static bool Validate(string Input)
    {
        if (string.IsNullOrWhiteSpace(Input))
            throw new EmptyStringException();
        if (!CheckLength(Input.Length))
            throw new IllegalSizeException(Input.Length);
        if (!CheckType(Input))
            return false;

        return true;
    }
    public static bool CheckType(string Input)
    {
        int pos = 0;
        foreach (char c in Input)
        {
            if (!IsType(c, Input.Length))
                throw new IllegalCharacterException(Input, pos);
            pos++;
        }
        return true;

    }

    public static bool CheckLength(int scale)
    {

        double result = Math.Sqrt(scale);
        if (result > 25 || result < 1)  // Possible range of Board scale is between 1*1 to 25*25
            return false;
        return result % 1 == 0;
    }

    public static bool IsType(char c, int length)
    {

        if (c < '0' || c > '0' + (int)Math.Sqrt(length))
            return false;
        return true;


    }

}
