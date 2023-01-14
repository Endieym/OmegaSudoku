using System.Diagnostics;
using OmegaSudoku.Exceptions;
using OmegaSudoku.UI;

namespace OmegaSudoku.SudokuGame;

internal static class SudokuSolver
{
    public static bool SolveSuduko(string ValidatedInput, int BoardSize)
    {
        Board board = new(ValidatedInput, BoardSize);

        board.PrintBoard();
        if (!InitialBoardValidation(board))
        {
            return false;
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
            return false;
        }
        finally
        {
            stopwatch.Stop();
            board.PrintBoard();
            printTime(stopwatch);
        }


        return true;


    }
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
