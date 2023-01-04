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
                        if(IsValidBitwise(row,col, i)){
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
        // check row using bitmask
        int rowMask = 0;
        for (int i = 0; i < 9; i++)
        {
            rowMask |= 1 << gameBoard[row, i];
        }
        if ((rowMask & (1 << num)) > 0)
        {
            return false;
        }

        // check col using bitmask
        int colMask = 0;
        for (int i = 0; i < 9; i++)
        {
            colMask |= 1 << gameBoard[i, col];
        }
        if ((colMask & (1 << num)) > 0)
        {
            return false;
        }

        // check 3x3 grid using bitmask
        int gridMask = 0;
        int startRow = row - row % 3;
        int startCol = col - col % 3;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                gridMask |= 1 << gameBoard[startRow + i, startCol + j];
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
