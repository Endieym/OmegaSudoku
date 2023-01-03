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
        ChooseInput();


    }

    public static void WelcomeMessage() // Prints a welcome message using Constants class
    {

        Console.WriteLine(Constants.Seperator);
        Console.WriteLine(Constants.WelcomeMessage);
        Console.WriteLine(Constants.Seperator);
    }

    // Reads the character and sends to functions getting 
    public static void ChooseInput()  
    {
        char character = (char)Console.Read();
        _ = Console.ReadLine();
        if (character == 'c' || character == 'C')
            GetInput(InputFromConsole());
        else if (character == 'f' || character == 'F')
            GetInput(InputFromFile());
        else
        {
            Console.WriteLine("Wrong char, try again:");
            ChooseInput();
        }
        
    }

    public static string InputFromFile()
    {
        Constants.currentInput = Constants.inputType.FILE;

        Console.WriteLine(Constants.FileMessage);

        string path = "..\\..\\";
        string endOfPath = Console.ReadLine();
        path += endOfPath;
        path += ".txt";

        string input = File.ReadAllText(path);

        Console.WriteLine(input);
        return input;
    }
    
    public static string InputFromConsole()
    {
        Constants.currentInput = Constants.inputType.CONSOLE;
        Console.WriteLine(Constants.InputMessage);

        string input = Console.ReadLine();
        return input;
    }
    public static void GetInput(string input) // Asks for a string from the user, and sends the string to validation, then to solver
    {
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
            WelcomeMessage();

        }

    }

}
