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
        for (int i = 0; i < gameBoard.BoardSize; i++)
        {
            for (int j = 0; j < gameBoard.BoardSize; j++)
            {
                 
            }
        }
        return true;
    }
}
