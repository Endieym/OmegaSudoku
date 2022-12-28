using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku;

internal static class Constants
{

    public const string Seperator = "#~----------------------------------~#";
    public const string WelcomeMessage = "Welcome to Omega Sudoko Solver!" +
        "\nThis program is designed to solve any difficulty of a Sudoko game" +
        "\nEnter a sudoku position to be solved in the form of a string, like the following example:\n" +
        "\n800000070006010053040600000000080400003000700020005038000000800004050061900002000";
    public const string InputMessage = "Enter a string:";

    public const int BoardScale = 9;
    public static readonly int BoxSize = (int)Math.Sqrt(BoardScale);

}
