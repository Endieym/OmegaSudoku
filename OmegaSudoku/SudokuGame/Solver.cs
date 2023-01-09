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

    // A function to solve the puzzle using constraint propagation
    public void ConstraintSolve()
    {
        // Keep applying constraints until no further progress can be made
        bool progress;
        do
        {
            ApplyConstraints();
            progress = false;

            // Look for cells that can only be filled with a single number (singles)
            for (int i = 0; i < gameBoard.BoardSize; i++)
            {
                for (int j = 0; j < gameBoard.BoardSize; j++)
                {
                    if (gameBoard[i, j] == 0)
                    {
                        int count = 0;
                        int value = 0;
                        for (int k = 1; k <= gameBoard.BoardSize; k++)
                        {
                            if ((gameBoard[i *gameBoard.BoardSize + j].PossibleValue & (1 << k)) == 0)
                            {
                                count++;
                                value = k;
                            }
                        }
                        if (count == 1)
                        {
                            gameBoard[i, j] = value;
                            SudokuStrategies.MarkPossibilities(i, j, value, gameBoard);
                            progress = true;
                            break;
                        }
                    }
                }
                if (progress)
                    break;
            }
        } while (progress);
    }



    public bool BacktrackSolve()
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
            if ((possibilities & (1 << num)) == 0 && IsValidBitwise(row, col, num)) 
            {
                gameBoard[row, col] = num;

                if (BacktrackSolve())
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
