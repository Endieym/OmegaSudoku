using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.SudokuGame;

internal class Solver
{

    private readonly Board gameBoard;

    public Solver(Board gameBoard)
    {
        this.gameBoard = gameBoard;
        MarkingOptimization(); // first optimizations -> marks possible values
    }

    public void MarkingOptimization() // Marks every possible value in each cell.
    {
        for (int row = 0; row < gameBoard.BoardSize; row++)
        {
            for (int col = 0; col < gameBoard.BoardSize; col++)
            {
                if (gameBoard[row,col] != 0)
                {
                    gameBoard.UpdateRow(row, gameBoard[row, col]);  // updats entire row
                    gameBoard.UpdateCol(col, gameBoard[row, col]);
                    gameBoard.UpdateBox(row, col, gameBoard[row, col]);

                }

            }
        }
    }
    
    public bool BacktrackSolve()
    {
        for (int row = 0; row < gameBoard.BoardSize; row++)
        {
            for (int col = 0; col < gameBoard.BoardSize; col++)
            {
                if (gameBoard[row, col] == 0)
                {
                    for (int i = 1; i <= gameBoard.BoardSize; i++)
                    {
                       
                        if (IsValidBitwise(row, col, i))
                        {
                            gameBoard[row, col] = i;

                            if (BacktrackSolve())
                                return true;

                            gameBoard[row, col] = 0;
                        }


                    }
                    return false;
                }
                
                
            }
        }
        return true;
    }

    public bool IsValidBitwise(int row, int col, int num)
    {
        if ((gameBoard[row*gameBoard.BoardSize+col].PossibleValue & (1 << num)) > 0)
        {
            return false;
        }
        
        
        // check row using bitmask
        int rowMask = 0;
        for (int i = 0; i < gameBoard.BoardSize; i++)
        {
            rowMask |= 1 << gameBoard[row, i];
        }
        if ((rowMask & (1 << num)) > 0)
        {
            return false;
        }

        // check col using bitmask
        int colMask = 0;
        for (int i = 0; i < gameBoard.BoardSize; i++)
        {
            colMask |= 1 << gameBoard[i, col];
        }
        if ((colMask & (1 << num)) > 0)
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
        if ((gridMask & (1 << num)) > 0)
        {
            return false;
        }

        // number is valid
        return true;
    }
    public bool IsValid(int row, int col, char c)
    {
        var rowE = gameBoard.GetRow(row);
        var colE = gameBoard.GetCol(col);
        var boxE = gameBoard.GetBox(row, col);
        for(int i = 0; i< gameBoard.BoardSize; i++)
        {
            if (rowE.ElementAt(i).Value == c)
                return false;
            if (colE.ElementAt(i).Value == c)
                return false;
            if (boxE.ElementAt(i).Value == c)
                return false;
        }
        return true;
    }
}
