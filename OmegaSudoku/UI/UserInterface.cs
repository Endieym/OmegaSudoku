using OmegaSudoku.Exceptions;
using OmegaSudoku.SudokuGame;
using OmegaSudoku.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.UI;

public static class UserInterface
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

        string path = Constants.RELATIVE_PATH;
        string endOfPath = Console.ReadLine();
        if (!endOfPath.EndsWith(".txt"))
        {
            endOfPath += ".txt";
        }
        string input;
        
        if(!File.Exists(endOfPath))
        {

            if (File.Exists(path + endOfPath))
                input = File.ReadAllText(path + endOfPath);
            else
            {
                throw new FileNotFoundException("File does not exist");
            }
        }
        else
        {
            input = File.ReadAllText(endOfPath);

        }

        Console.WriteLine("String input from file:\n");

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
    public static string GetInput(string input) // Asks for a string from the user, and sends the string to validation, then to solver
    {
        string? solution = null;
        try
        {
            StringValidation.Validate(input);
            solution = SudokuSolver.SolveSuduko(input, (int)Math.Sqrt(input.Length));
            if (solution!=null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Solved!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Not solved! try another board");
                Console.ResetColor();
                InitiateUI();
            }

        }
        catch (InputException ie)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ie.Message);
            Console.ResetColor();
            InitiateUI();

        }
        
        return solution;
        

    }

}
