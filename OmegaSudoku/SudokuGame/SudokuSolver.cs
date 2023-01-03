using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmegaSudoku.Exceptions;

namespace OmegaSudoku.SudokuGame;

internal static class SudokuSolver
{
    public static void SolveSuduko(string ValidatedInput, int BoardSize)
    {
        Board board = new(ValidatedInput, BoardSize);

        board.PrintBoard();
        InitialBoardValidation(board);
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        Solver solve = new Solver(board);
        solve.BacktrackSolve();
        stopwatch.Stop();
        board.PrintBoard();    
        Console.WriteLine("Elapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);

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
