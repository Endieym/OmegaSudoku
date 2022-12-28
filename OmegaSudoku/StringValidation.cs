using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku;

internal static class StringValidation
{
    public static bool Validate(string Input)
    {
        if(string.IsNullOrWhiteSpace(Input))
            throw new EmptyStringException();
        if (!CheckType(Input, "digit"))
            return false;
        if (!CheckLength(Input, Constants.BoardScale))
            throw new IllegalSizeException(Input.Length); 
        return true;
    }
    public static bool CheckType(string Input, string Type) 
    {
        int pos = 0;
        foreach (char c in Input)
        {
            if (!IsType(c, "digit"))
                throw new IllegalCharacterException(Input, pos);
            pos++;
        }
        return true;
    
    }

    public static bool CheckLength(string Input, int scale)
    {
        return Input.Length == scale * scale;
    }
    
    public static bool IsType(char c, string type)
    {
        if (type == "digit")
        {
            if (c < '0' || c > '9')
                return false;
            return true;
        }
        else
            return false;
    }
    
}
