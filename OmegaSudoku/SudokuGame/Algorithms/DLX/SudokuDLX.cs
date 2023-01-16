using OmegaSudoku.Exceptions;
using OmegaSudoku.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OmegaSudoku.SudokuGame.Algorithms.SparseMatrix;

namespace OmegaSudoku.SudokuGame.Algorithms.DLX;

internal class SudokuDLX
{
    /// <summary>
    /// Tries to solve using Dancing Links X Algorithm
    /// </summary>
    /// <param name="puzzle"></param>
    /// <returns>Whether or not a solution was found</returns>
    public static bool Solve(Board puzzle)
    {

        int size = puzzle.BoardSize;
        SparseMatrix DlxMatrix = GetMatrix(size, puzzle);
        DancingLinks dlx = new DancingLinks(DlxMatrix);
        Stack<DancingNode> nodes = dlx.DancingSolve();
        return(ProccessSolution(nodes, puzzle));

    }

    /// <summary>
    /// Creates a cover matrix using the board and its size
    /// </summary>
    /// <param name="puzzle"></param>
    /// <param name="BoardSize"></param>
    /// <returns>the cover matrix (byte[,])</returns>
    public static byte[,] CreateCoverMatrix(Board puzzle, int BoardSize)
    {
        byte[,] byteBoard = new byte[BoardSize * BoardSize * BoardSize,
            BoardSize * BoardSize * Constants.NUM_CONSTRAINTS];
        int currRow = 0;
        int currCellConstraint = 0;
        int currColConstraint; 
        int currRowConstraint = BoardSize * BoardSize * 2; // located after column constraint
        int currBoxConstraint = BoardSize * BoardSize * 3; // located after row constraint

        for (int i = 0; i < BoardSize; i++)
        {
            currColConstraint = BoardSize * BoardSize; // located right after the cell constraint
            for (int j = 0; j < BoardSize; j++)
            {
                int currValue = puzzle[i, j];
                int currBox = puzzle.GetBoxIndex(i, j);
                for(byte num = 1; num <= BoardSize; num++)
                {
                    if(currValue == 0 || currValue == num)
                    {
                        byteBoard[currRow, currCellConstraint] = 1;
                        byteBoard[currRow, currColConstraint] = 1;
                        byteBoard[currRow, currRowConstraint + num-1] = 1;
                        byteBoard[currRow, currBoxConstraint +currBox*BoardSize + num-1] = 1;
                    }
                    currRow++;
                    currColConstraint++;
                }
                currCellConstraint++;
            }
            currRowConstraint += BoardSize;
        }


        return byteBoard;
    }
    /// <summary>
    /// Creates a sparse matrix for the dlx
    /// </summary>
    /// <param name="gameScale"></param>
    /// <param name="puzzle"></param>
    /// <returns>The sparse matrix</returns>
    public static SparseMatrix GetMatrix(int gameScale, Board puzzle)
    {
        SparseMatrix matrix = new SparseMatrix(gameScale * gameScale 
            * Constants.NUM_CONSTRAINTS);

        byte[,] byteBoard = CreateCoverMatrix(puzzle, gameScale);
        matrix.ConvertToDLX(byteBoard);
        
        return matrix;
    }
    
    /// <summary>
    /// Procceses the solution, sets the values in the original board
    /// </summary>
    /// <param name="solution"></param>
    /// <param name="gameBoard"></param>
    /// <returns>Whether or not a solution was found</returns>
    public static bool ProccessSolution(Stack<DancingNode> solution, Board gameBoard)
    {

        if (solution.Count == 0)
            return false;

        while(solution.Count > 0)
        {
            DancingNode node = solution.Pop();

            DancingNode first = node;
            DancingNode firstTemp = node.Right;

            while (firstTemp != first)
            {
                if (firstTemp.Column.Index < first.Column.Index)
                    first = firstTemp;

                firstTemp = firstTemp.Right;
            }
            int firstIndex = first.Column.Index ;
            int secondIndex = first.Right.Column.Index;

            int row = firstIndex / gameBoard.BoardSize;
            int col = firstIndex % gameBoard.BoardSize;
            int num = secondIndex % gameBoard.BoardSize  + 1;

            gameBoard[row, col] = num;
        }
        return true;
    }
}

