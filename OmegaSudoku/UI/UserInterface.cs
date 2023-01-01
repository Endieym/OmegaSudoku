using OmegaSudoku.Exceptions;
using OmegaSudoku.SudokuGame;
using OmegaSudoku.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.UI;

internal static class UserInterface
{
    public static void InitiateUI() // Initiates the User Interface, calls for input
    {
        WelcomeMessage();
        GetInput();


    }

    public static void WelcomeMessage() // Prints a welcome message using Constants class
    {

        Console.WriteLine(Constants.Seperator);
        Console.WriteLine(Constants.WelcomeMessage);
        Console.WriteLine(Constants.Seperator);
    }

    public static void GetInput() // Asks for a string from the user, and sends the string to validation, then to solver
    {
        Console.WriteLine(Constants.InputMessage);

        string input = Console.ReadLine();


        try
        {
            StringValidation.Validate(input);
            Console.WriteLine("Success!");

            SudokuSolver.SolveSuduko(input, (int)Math.Sqrt(input.Length));

        }
        catch (InputException ie)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ie.Message);
            Console.ResetColor();
            GetInput();

        }

    }

}
