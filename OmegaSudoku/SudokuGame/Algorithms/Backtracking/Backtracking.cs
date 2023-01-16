using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.SudokuGame.Algorithms.Backtracking;

internal class Backtracking
{
    /// <summary>
    /// Tries to backtrack solve
    /// </summary>
    /// <param name="gameBoard"></param>
    /// <returns>if successful</returns>
    public static bool BacktrackSolve(Board gameBoard)
    {
        int row = -1;
        int col = -1;
        bool isEmpty = true;       
        for (int i = 0; i < gameBoard.BoardSize; i++)
        {
            for (int j = 0; j < gameBoard.BoardSize; j++)
            {
                if (gameBoard[i, j] == 0)
                {
                    row = i;
                    col = j;

                    // we still have some remaining missing values in Sudoku
                    isEmpty = false;
                    break;
                }
            }
            if (!isEmpty)
            {
                break;
            }
        }

        // no empty space left
        if (isEmpty)
        {
            return true;
        }

        int possibilities = gameBoard[row * gameBoard.BoardSize + col].PossibleValue;
        for (int num = 1; num <= gameBoard.BoardSize; num++)
        {
            if ((possibilities & 1 << num) == 0 && IsValidBitwise(row, col, num, gameBoard))
            {
                gameBoard[row, col] = num;

                if (BacktrackSolve(gameBoard))
                {
                    // print(grid, row, col, num)
                    return true;
                }
                else
                {
                    // mark cell as empty (with 0)

                    gameBoard[row, col] = 0;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Checks if valid using bitmasks
    /// </summary>
    /// <param name="row"></param>
    /// <param name="col"></param>
    /// <param name="num"></param>
    /// <param name="gameBoard"></param>
    /// <returns>true or false if valid</returns>
    static bool IsValidBitwise(int row, int col, int num, Board gameBoard)
    {



        // check row using bitmask
        int rowMask = 0;
        for (int i = 0; i < gameBoard.BoardSize; i++)
        {
            rowMask |= 1 << gameBoard[row, i];
        }
        if ((rowMask & 1 << num) > 0)
        {
            return false;
        }

        // check col using bitmask
        int colMask = 0;
        for (int i = 0; i < gameBoard.BoardSize; i++)
        {
            colMask |= 1 << gameBoard[i, col];
        }
        if ((colMask & 1 << num) > 0)
        {
            return false;
        }

        // check box grid using bitmask

        int gridMask = 0;

        var iBox = row / gameBoard.BoxSize;
        var jBox = col / gameBoard.BoxSize;
        for (int i = iBox * gameBoard.BoxSize; i < (iBox + 1) * gameBoard.BoxSize; i++)
        {
            for (int j = jBox * gameBoard.BoxSize; j < (jBox + 1) * gameBoard.BoxSize; j++)
            {
                gridMask |= 1 << gameBoard[i, j];
            }

        }
        if ((gridMask & 1 << num) > 0)
        {
            return false;
        }

        // number is valid
        return true;
    }
}
