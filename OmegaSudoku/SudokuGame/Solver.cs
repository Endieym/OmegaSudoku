using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.SudokuGame;

internal class Solver
{

    private Board gameBoard;

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
                if (gameBoard[row, col].Value == '0')
                {
                    for (int i = 1; i <= gameBoard.BoardSize; i++)
                    {
                        if(IsValid(row,col, (char)('0' + i))){
                            gameBoard[row, col].Value = (char)('0' + i);

                            if (BacktrackSolve())
                                return true;
                            else
                                gameBoard[row, col].Value = '0';
                        }
                    }
                    return false;
                }
                
            }
        }
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
