using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.Utils;

internal static class Constants
{

    public const string Seperator = "#~----------------------------------~#";
    public const string WelcomeMessage = "Welcome to Omega Sudoko Solver!" +
        "\nThis program is designed to solve any difficulty of a Sudoko game" +
        "\nPlease choose one of the following inputs, by entering the character" +
        "\n C. Console Input" +
        "\n F. File Input";
        
    public const string ConsoleInput = "\nEnter a sudoku position to be solved in the form of a string, like the following example:\n" +
        "\n800000070006010053040600000000080400003000700020005038000000800004050061900002000";
    public const string InputMessage = "Enter a string:\n";
    public const string FileMessage = "Enter the file name:\n";
    public const string RELATIVE_PATH = "..\\..\\";
    public const string DefaultWrite = RELATIVE_PATH + "SudokuSolution.txt";
    public enum inputType {FILE, CONSOLE};
    public static inputType currentInput;
    public const int MaxScale = 25;
    public const int NUM_CONSTRAINTS = 4;


}
