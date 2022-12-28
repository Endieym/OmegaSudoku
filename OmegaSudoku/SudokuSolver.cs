using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku;

internal static class SudokuSolver
{
    public static void SolveSuduko(string ValidatedInput)
    {
        Board board = new Board(ValidatedInput);
        
        board.PrintBoard();
        InitialBoardValidation(board);
    }

    public static bool InitialBoardValidation(Board board)
    {
        return BoardValidation.BoardValidate(board);
    }

    
}
