using OmegaSudoku.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.SudokuGame.Algorithms.ConstraintPropagation;

internal static class SudokuStrategies
{

    public static void UseStrategies(int row, int col, Board gameBoard)
    {
        if (gameBoard[row, col] != 0)
        {
            MarkPossibilities(row, col, gameBoard[row, col], gameBoard);

        }
        else
        {
            if (((gameBoard[row * gameBoard.BoardSize + col].PossibleValue + 2) & 1) == 1)
                throw new UnsolvableBoardException();
            int hidden = HiddenCell(row, col, gameBoard);
            if (hidden != 0)

            {
                gameBoard[row, col] = hidden;
                MarkPossibilities(row, col, hidden, gameBoard);
            }


        }
    }

    // Marks every possible value in each cell.
    public static void MarkPossibilities(int row, int col, int num, Board gameBoard)
    {
        gameBoard.UpdateRow(row, num);  // updates entire row
        gameBoard.UpdateCol(col, num);
        gameBoard.UpdateBox(row, col, num);


    }

    // The function gets a specific index for a cell,
    // and returns the number for which the current cell is an hidden cell for
    public static int HiddenCell(int row, int col, Board gameBoard)
    {
        int hiddenValue = HiddenRow(row, col, gameBoard);
        if (hiddenValue != 0)
            return hiddenValue;
        hiddenValue = HiddenCol(row, col, gameBoard);
        if (hiddenValue != 0)
            return hiddenValue;
        hiddenValue = HiddenBox(row, col, gameBoard);
        return hiddenValue;


    }

    public static int HiddenRow(int row, int col, Board gameBoard)
    {
        // check for hidden cell in row
        var currRow = gameBoard.GetRow(row);
        int rowBitmask = 0;

        for (int i = 0; i <= gameBoard.BoardSize; i++) // create the initial bitmask- all 1's
        {
            rowBitmask |= 1 << i;
        }
        foreach (var r in currRow) // change the bitmask according to possibilites in the row
        {
            if (r.Value == '0' && r.Col != col)
                rowBitmask &= r.PossibleValue;


        }
        for (int i = 1; i <= gameBoard.BoardSize; i++)
        {
            if ((gameBoard[row * gameBoard.BoardSize + col].PossibleValue & 1 << i) == 0
                && (rowBitmask & 1 << i) != 0)
                return i;

        }
        return 0;
    }
    public static int HiddenCol(int row, int col, Board gameBoard)
    {
        // check for hidden cell in column
        var currCol = gameBoard.GetCol(col);
        int colBitmask = 0;

        // create the initial bitmask- all 1's
        for (int i = 0; i <= gameBoard.BoardSize; i++)
        {
            colBitmask |= 1 << i;
        }
        foreach (var c in currCol) // change the bitmask according to possibilites in the column
        {
            if (c.Value == '0' && c.Row != row)
                colBitmask &= c.PossibleValue;


        }
        for (int i = 1; i <= gameBoard.BoardSize; i++)
        {
            if ((gameBoard[row * gameBoard.BoardSize + col].PossibleValue & 1 << i) == 0
                && (colBitmask & 1 << i) != 0)
                return i;

        }
        return 0;
    }

    public static int HiddenBox(int row, int col, Board gameBoard)
    {
        // check for hidden cell in box
        var currBox = gameBoard.GetBox(row, col);
        int boxBitmask = 0;

        for (int i = 0; i <= gameBoard.BoardSize; i++) // create the initial bitmask- all 1's
        {
            boxBitmask |= 1 << i;
        }
        foreach (var b in currBox) // change the bitmask according to possibilites in the box
        {
            if (b.Value == '0' && !(b.Col == col && b.Row == row))
                boxBitmask &= b.PossibleValue;


        }
        for (int i = 1; i <= gameBoard.BoardSize; i++)
        {
            if ((gameBoard[row * gameBoard.BoardSize + col].PossibleValue & 1 << i) == 0
                && (boxBitmask & 1 << i) != 0)
                return i;

        }
        return 0;
    }
}
