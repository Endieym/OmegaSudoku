using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OmegaSudoku.SudokuGame.Algorithms.SparseMatrix;

namespace OmegaSudoku.SudokuGame.Algorithms.DLX;

internal class SudokuReduction
{
    public static void Solve(Board puzzle)
    {
        int size = puzzle.BoardSize;
        SparseMatrix matrix = GetMatrix(size, puzzle);
        DancingLinks dlx = new DancingLinks(matrix);

    }
    public static SparseMatrix GetMatrix(int gameScale, Board puzzle)
    {
        SparseMatrix matrix = new SparseMatrix(gameScale * gameScale* gameScale,
            gameScale * gameScale * 4);
        // Iterate through the puzzle and add nodes to the matrix
        for (int i = 0; i < gameScale; i++)
        {
            for (int j = 0; j < gameScale; j++)
            {
                int val = puzzle[i, j];
                if (val != 0)
                {
                    // Add a node for the cell if it has a value
                    int col = (i * gameScale) + j;
                    int row = (i * gameScale) + (j * gameScale) + val - 1;
                    matrix.AddNode(row, col);
                }
                else
                {
                    // Add a node for each possible value in the cell if it's empty
                    for (int k = 0; k < gameScale; k++)
                    {
                        int cellCol = (i * gameScale) + j;
                        int cellRow = (i * gameScale * gameScale) +
                            (j * gameScale) + k;
                        matrix.AddNode(cellRow, cellCol);

                        // Add a node for the row constraint
                        int rowCol = i * gameScale + k;              
                        matrix.AddNode(cellRow, rowCol);

                        // Add a node for the column constraint
                        int colCol = j * gameScale + k;
                        matrix.AddNode(cellRow, colCol);

                        //Add a node for the box constraint
                        int boxCol = (i / gameScale * gameScale + j * gameScale)
                            * gameScale + k;
                        matrix.AddNode(cellRow, boxCol);

                    }
                }
            }
        }
        return matrix;
    }
    void PrintBoard(SparseMatrix matrix, int gameScale)
    {
        int[,] board = new int[gameScale, gameScale];
        for (int i = 0; i < gameScale*gameScale*4; i++)
        {
            Node row = matrix.cols[i].Down;
            while (row != matrix.cols[i])
            {
                int rowVal = row.Row;
                int val = rowVal % gameScale + 1;
                int cellCol = rowVal / (gameScale*gameScale);
                int cellRow = (rowVal - cellCol * (gameScale * gameScale)) / gameScale;
                board[cellRow, cellCol] = val;
                row = row.Down;
            }
        }
        for (int i = 0; i < gameScale; i++)
        {
            for (int j = 0; j < gameScale; j++)
            {
                Console.Write(board[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}

