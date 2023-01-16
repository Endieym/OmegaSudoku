using OmegaSudoku.SudokuGame.Algorithms.Backtracking;
using OmegaSudoku.SudokuGame.Algorithms.ConstraintPropagation;
using OmegaSudoku.SudokuGame.Algorithms.DLX;
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
    public void ChangePossibilites(int row, int col,
        IEnumerable<int> rowPossibilites,
        IEnumerable<int> colPossibilites,
        IEnumerable<int> boxPossibilites)
    {
        var currRow = gameBoard.GetRow(row);
        int i = 0;
        foreach(var r in currRow)
        {
            r.PossibleValue = rowPossibilites.ElementAt(i++);
        }
        var currCol = gameBoard.GetCol(col);
        i = 0;
        foreach (var c in currCol)
        {
            c.PossibleValue = colPossibilites.ElementAt(i++);
        }
        var currBox = gameBoard.GetBox(row, col);
        i = 0;
        foreach (var b in currBox)
        {
            b.PossibleValue = boxPossibilites.ElementAt(i++);
        }

    }
    public void ApplyConstraints()
    {
        for (int row = 0; row < gameBoard.BoardSize; row++)
        {
            for (int col = 0; col < gameBoard.BoardSize; col++)
            {
                SudokuStrategies.UseStrategies(row, col, gameBoard);

            }
        }
        
    }
    public void Solve()
    {
        ConstraintSolve();
        if (EvaluateSolution() == 1)
            SudokuDLX.Solve(gameBoard);
        else
            BacktrackSolve();
    }
    
    public int EvaluateSolution()
    {
        if (gameBoard.BoardSize > 16)
            return 1;
        else
            return 0;
       
    }

    // A function to solve the puzzle using constraint propagation
    public void ConstraintSolve()
    {
        ConstraintPropagation.ConstraintSolve(gameBoard);
    }



    public void BacktrackSolve()
    {
        Backtracking.BacktrackSolve(gameBoard);
    }    

    public bool IsValidBitwise(int row, int col, int num)
    {
       
        
        
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
