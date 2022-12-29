using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku;

internal static class SudokuSolver
{
    public static void SolveSuduko(string ValidatedInput, int BoardSize)
    {
        Board board = new Board(ValidatedInput, BoardSize);
        
        board.PrintBoard();
        InitialBoardValidation(board);
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
