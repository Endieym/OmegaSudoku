using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku;

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

        if (StringValidation.Validate(input))
        {
            SudokuSolver.SolveSuduko(input);
        }
        else
        {
            Console.WriteLine("Incorrect input!");
            GetInput();
        }

    }

}
