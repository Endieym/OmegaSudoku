using System.Diagnostics;
using OmegaSudoku.Exceptions;
using OmegaSudoku.UI;
using OmegaSudoku.Utils;

namespace OmegaSudoku.SudokuGame;

internal static class SudokuSolver
{
    /// <summary>
    /// This function tries to solve the sudoku board given, if the sudoku is unsolvable,
    /// will print a message accordingly
    /// Prints the time it took to solve the sudoku puzzle
    /// And writes it to SudokuSolution file when read a file in input
    /// </summary>
    /// <param name="ValidatedInput"></param>
    /// <param name="BoardSize"></param>
    /// <returns></returns>
    public static string? SolveSuduko(string ValidatedInput, int BoardSize)
    {
        Board board = new(ValidatedInput, BoardSize);
        string solution;
        board.PrintBoard();
        if (!InitialBoardValidation(board))
        {
            return null;
        }
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        Solver solve = new(board);

        try
        {
            solve.Solve(); 
        }
        catch(BoardException be)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(be.Message);
            Console.ResetColor();
            return null;
        }
        finally
        {
            stopwatch.Stop();
            board = solve.GetBoard();
            board.PrintBoard();
            if(Constants.currentInput == Constants.inputType.FILE)
            {
                writeSolutionToFile(board); 
            }
            printTime(stopwatch);
        }

        return board.ToStringLine();

    }
    
   // writes solution to defualt solution file
    public static void writeSolutionToFile(Board board)
    {
        File.WriteAllText(Constants.DefaultWrite, board.ToStringLine());
    }
    //prints the time it took to solve, with different colors for each timespan
    public static void printTime(Stopwatch stopwatch)
    {
        long elapsedTime = stopwatch.ElapsedMilliseconds;
        if (elapsedTime < 10)
            Console.ForegroundColor = ConsoleColor.Magenta;
        else if (elapsedTime < 25)
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
        else if (elapsedTime < 100)
            Console.ForegroundColor = ConsoleColor.DarkBlue;
        else
            Console.ForegroundColor = ConsoleColor.DarkRed;

       
        Console.WriteLine("Elapsed Time is {0} ms", elapsedTime);
        Console.ResetColor();

    }

    // Validates if the board is legal ( no duplicates)
    public static bool InitialBoardValidation(Board board)
    {
        try
        {
            return BoardValidation.BoardValidate(board);
        }
        catch (BoardException be)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(be.Message);
            Console.ResetColor();
            return false;
        }

    }
}
