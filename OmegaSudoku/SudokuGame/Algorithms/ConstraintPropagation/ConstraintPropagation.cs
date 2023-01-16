using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.SudokuGame.Algorithms.ConstraintPropagation;

internal static class ConstraintPropagation
{
    /// <summary>
    /// Applies constraints on the entire board
    /// </summary>
    /// <param name="gameBoard"></param>
    static void ApplyConstraints(Board gameBoard)
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
    public static void ConstraintSolve(Board gameBoard)
    {
        // Keep applying constraints until no further progress can be made
        bool progress;
        do
        {
            ApplyConstraints(gameBoard);
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
                            if ((gameBoard[i * gameBoard.BoardSize + j].PossibleValue & 1 << k) == 0)
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

}
